using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingTicket.Api.Areas.Admin.Filters;
using BookingTicket.Entities.Context;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BookingTicket.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeLogin]
    public class ThongTinDatVeController : Controller
    {
        private readonly BookingTicketContext _context;

        public ThongTinDatVeController(BookingTicketContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchString, int? page)
        {
            var thongtindatchos = _context.ThongTinDatChos.ToList();
            if (searchString != null)
            {
                page = 1;
            }
            ViewBag.searchString = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                thongtindatchos = thongtindatchos.Where(s => s.SDT.Contains(searchString)).ToList();
            }
            
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(thongtindatchos.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Edit(long? id)
        {
            return View();
        }
    }
}