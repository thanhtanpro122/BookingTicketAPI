﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookingTicket.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class XeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}