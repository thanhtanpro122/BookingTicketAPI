using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingTicket.Entities.Context;
using BookingTicket.Entities.Models;
using BookingTicket.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookingTicket.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<BookingTicketContext>(options => options.UseMySQL(Configuration.GetConnectionString("BookingTicketConStr")));

            services.AddScoped<INguoiDungLogic, NguoiDungLogic>();
            services.AddScoped<ITuyenXeLogic, TuyenXeLogic>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BookingTicketContext>();
                await context.Database.MigrateAsync();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                  name: "areas_DashBoard",
                  areaName: "Admin",
                  template: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}"
                );
            });


            
        }
    }
}
