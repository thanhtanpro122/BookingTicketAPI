using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingTicket.Entities.Context;
using BookingTicket.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BookingTicket.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TuyenXeController : Controller
    {
        private readonly BookingTicketContext _context;

        public TuyenXeController(BookingTicketContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchString, int? page)
        {
            var tuyenxes = _context.TuyenXes.ToList();
            if (searchString != null)
            {
                page = 1;
            }
            ViewBag.searchString = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                tuyenxes = tuyenxes.Where(s => s.TenTuyenXe.Contains(searchString) == true).ToList();
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(tuyenxes.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult Create(TuyenXe tuyenXe)
        {
            tuyenXe.CreatedTime = DateTime.Now;
            return RedirectToAction("Index");
        }
    }
}