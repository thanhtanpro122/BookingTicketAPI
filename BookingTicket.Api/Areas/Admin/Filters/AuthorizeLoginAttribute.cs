using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTicket.Api.Areas.Admin.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeLoginAttribute : TypeFilterAttribute
    {
        public AuthorizeLoginAttribute() : base(typeof(LoginActionFilter))
        {
        }
    }
}
