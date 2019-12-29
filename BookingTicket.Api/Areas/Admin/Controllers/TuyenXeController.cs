using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingTicket.Api.Areas.Admin.Filters;
using BookingTicket.Entities.Context;
using BookingTicket.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BookingTicket.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeLogin]
    public class TuyenXeController : Controller
    {
        private readonly BookingTicketContext _context;

        public TuyenXeController(BookingTicketContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchString, int? page)
        {
            var tuyenxes = _context.TuyenXes.Where(e=>e.IsDelete==0).ToList();
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
            tuyenXe.IsDelete = 0;
            _context.Add(tuyenXe);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(long id)
        {            
            if (id == 0)
            {
                return NotFound();
            }

            var tuyenxe = _context.TuyenXes.Find(id);
            tuyenxe.ThoiGianKhoiHanh.ToString(@"hh\:mm");
            tuyenxe.ThoiGianKetThuc.ToString(@"hh\:mm");
            if (tuyenxe == null)
            {
                return NotFound();
            }

            return Json(tuyenxe);
        }

        [HttpPost]
        public IActionResult Edit(TuyenXe tuyenXe)
        {
            tuyenXe.UpdateTime = DateTime.Now;

            _context.Update(tuyenXe);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long? id)
        {
            var tuyenxe = _context.TuyenXes.Find(id);
            if (tuyenxe == null)
            {
                return NotFound();
            }
            tuyenxe.IsDelete = 1;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}