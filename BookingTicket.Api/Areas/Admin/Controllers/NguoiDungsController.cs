using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingTicket.Entities.Context;
using BookingTicket.Entities.Models;
using X.PagedList;
using BookingTicket.Logic;
using BookingTicket.Api.Areas.Admin.Filters;

namespace BookingTicket.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeLogin]
    public class NguoiDungsController : Controller
    {
        private readonly BookingTicketContext _context;
        private readonly INguoiDungLogic nguoiDungLogic;

        public NguoiDungsController(INguoiDungLogic nguoiDungLogic,BookingTicketContext context)
        {
            this.nguoiDungLogic = nguoiDungLogic;
            _context = context;
        }

        // GET: Admin/NguoiDungs
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.NguoiDungs.ToListAsync());
        //}
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            ViewBag.searchString = searchString;
            var nguoidung = _context.NguoiDungs.OrderByDescending(e => e.UserID).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                nguoidung = nguoidung.Where(s => s.SDT.Contains(searchString) || s.HoTen?.Contains(searchString) == true).ToList();
            }
            //return View(await _context.NguoiDungs.ToListAsync());
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(nguoidung.ToPagedList(pageNumber, pageSize));
        }
        // GET: Admin/NguoiDungs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,SDT,MatKhau,Email,HoTen,GioiTinh,DiaChi,NgaySinh,Avatar,CreatedTime,UpdateTime")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                nguoiDung.MatKhau = nguoiDungLogic.GetMD5(nguoiDung.MatKhau);
                nguoiDung.CreatedTime = DateTime.Now;
                _context.Add(nguoiDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserID,SDT,MatKhau,Email,HoTen,GioiTinh,DiaChi,NgaySinh,Avatar,CreatedTime,UpdateTime")] NguoiDung nguoiDung)
        {
            if (id != nguoiDung.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    nguoiDung.UpdateTime = DateTime.Now;
                    _context.Update(nguoiDung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoiDungExists(nguoiDung.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }

        //// GET: Admin/NguoiDungs/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var nguoiDung = await _context.NguoiDungs
        //        .FirstOrDefaultAsync(m => m.UserID == id);
        //    if (nguoiDung == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(nguoiDung);
        //}

        // GET: Admin/NguoiDungs/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            _context.NguoiDungs.Remove(nguoiDung);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoiDungExists(long id)
        {
            return _context.NguoiDungs.Any(e => e.UserID == id);
        }

    }
}
