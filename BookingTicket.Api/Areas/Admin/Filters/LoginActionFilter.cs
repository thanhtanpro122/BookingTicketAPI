using BookingTicket.Entities.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTicket.Api.Areas.Admin.Filters
{
    public class LoginActionFilter : IAuthorizationFilter
    {
        private readonly BookingTicketContext _context;

        public LoginActionFilter(BookingTicketContext context)
        {
            _context = context;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var checkusername = context.HttpContext.Request.Cookies["Username"];
            var checkpass = context.HttpContext.Request.Cookies["Password"];
            if (string.IsNullOrEmpty(checkusername) && string.IsNullOrEmpty(checkpass))
            {
                context.Result = new RedirectResult("/Admin/DashBoard/Login");
            }
            else
            {
                var admin_check = _context.Admins.FirstOrDefault(e => e.UserName == checkusername && e.MatKhau == checkpass);
                if (admin_check == null)
                {
                    context.Result = new RedirectResult("/Admin/DashBoard/Login");
                }
            }
        }
    }
}
