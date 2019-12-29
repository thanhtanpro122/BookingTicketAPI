using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class AdminsController : Controller
    {
        private readonly BookingTicketContext _context;

        public AdminsController(BookingTicketContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchString, int? page)
        {
            if (HttpContext.Request.Cookies["IsAdmin"] == "0")
            {
                return NotFound();
            }
            if (searchString != null)
            {
                page = 1;
            }
            ViewBag.searchString = searchString;
            var admins = _context.Admins.Where(e=>e.IsSuperAdmin==0).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                admins = admins.Where(s => s.UserName.Contains(searchString)).ToList();
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(admins.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult Create(Admins admin)
        {
            if (_context.Admins.Where(e => e.UserName == admin.UserName).Count() > 0)
            {
                return NotFound();
            }
            admin.MatKhau = GetMD5(admin.MatKhau);
            admin.IsSuperAdmin = 0;
            _context.Add(admin);
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

            var admin = _context.Admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }

            return Json(admin);
        }

        [HttpPost]
        public IActionResult Edit(Admins admin)
        {
            _context.Update(admin);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long? id)
        {
            var admin = _context.Admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }
            _context.Admins.Remove(admin);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public string GetMD5(string matkhau)
        {
            var str = "";
            var buffer = Encoding.UTF8.GetBytes(matkhau);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (var b in buffer)
            {
                str += b.ToString("X2");
            }
            return str;
        }
    }
}