using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingTicket.Api.Areas.Admin.Filters;
using BookingTicket.Entities.Context;
using BookingTicket.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BookingTicket.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeLogin]
    public class XeController : Controller
    {
        private readonly BookingTicketContext _context;

        public XeController(BookingTicketContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchString, int? page)
        {
            var loaiChoNgois = _context.LoaiChoNgois.ToList();
            ViewBag.LoaiChoNgoi = new SelectList(loaiChoNgois, "MaLoaiChoNgoi", "TenLoaiChoNgoi");

            if (searchString != null)
            {
                page = 1;
            }
            ViewBag.searchString = searchString;
            var xe = _context.Xes
                .Include(e => e.LoaiChoNgoi)
                .ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                xe = xe.Where(s => s.BienSoXe.Contains(searchString) == true).ToList();
            }
            //return View(await _context.NguoiDungs.ToListAsync());
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(xe.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult Create(Xe xe)
        {
            _context.Add(xe);
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

            var xe = _context.Xes.Find(id);
            if (xe == null)
            {
                return NotFound();
            }

            return Json(xe);
        }

        [HttpPost]
        public IActionResult Edit(Xe xe)
        {
            _context.Update(xe);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long? id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}