using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTicket.Api.Areas.Admin.Filters;
using BookingTicket.Entities.Context;
using BookingTicket.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingTicket.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        private readonly BookingTicketContext _context;

        public DashBoardController(BookingTicketContext context)
        {
            _context = context;
        }

        [AuthorizeLogin]
        public IActionResult Index(DateTime fromdate, DateTime todate)
        {

            return View();
        }


        public IActionResult ThongKe(DateTime fromdate, DateTime todate)
        {
            var ve = _context.Ves
                .Include(e => e.ThongtinDatCho).ThenInclude(e => e.DieuHanh)
                .Where(e => e.Status == 1 && e.ThongtinDatCho.DieuHanh.Status == 1 && e.ThongtinDatCho.DieuHanh.NgayKhoiHanh.Date >= fromdate.Date && e.ThongtinDatCho.DieuHanh.NgayKhoiHanh.Date <= todate.Date)
                .Select(e => new Ve
                {
                    MaVe = e.MaVe,
                    GiaVe = e.GiaVe,
                    Status = e.Status,
                    UpdateTime = e.UpdateTime,
                    MaDatCho = e.MaDatCho,
                    MaChoNgoi = e.MaChoNgoi,
                    ThongtinDatCho = e.ThongtinDatCho,
                    ChoNgoi = e.ChoNgoi
                })
                .GroupBy(e => e.ThongtinDatCho.DieuHanh.NgayKhoiHanh.Date, e => e)
                .Select(e => new
                {
                    Ngay = e.Key.ToString("dd/MM/yyyy"),
                    TongGia = e.Sum(h => h.GiaVe),
                    SoLuongVe = e.Count()
                })
                .ToList(); 

            return Json(ve);
        }

        public IActionResult Login()
        {
            var checkusername = Request.Cookies["Username"];
            var checkpass = Request.Cookies["Password"];
            if (string.IsNullOrEmpty(checkusername) && string.IsNullOrEmpty(checkpass))
            {
                return View();
            }
            var admin_check = _context.Admins.FirstOrDefault(e => e.UserName == checkusername && e.MatKhau == checkpass);
            if (admin_check == null)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Login(BookingTicket.Entities.Models.Admin admin)
        {
            admin.MatKhau = GetMD5Admin(admin.MatKhau);
            var admin_check = _context.Admins.FirstOrDefault(e => e.UserName == admin.UserName && e.MatKhau == admin.MatKhau);
            if (admin_check == null)
            {
                return RedirectToAction("Login");
            }

            var options = new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(15),
                Expires = DateTime.Now.AddDays(15)
            };

            Response.Cookies.Append("Username", admin.UserName, options);
            Response.Cookies.Append("Password", admin.MatKhau, options);
            Response.Cookies.Append("IDAdmin", admin_check.AdminID.ToString(), options);

            return RedirectToAction("Index");
        }

        private string GetMD5Admin(string matkhau)
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

        [AuthorizeLogin]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Username");
            Response.Cookies.Delete("Password");
            Response.Cookies.Delete("IDAdmin");
            return RedirectToAction("Login");
        }

        public ActionResult ChangePassword(string passold, string passnew)
        {
            var id = HttpContext.Request.Cookies["IDAdmin"];
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json(new { status = 401 });
            }
            var idadmin = long.Parse(id);
            if (idadmin == 0)
            {
                return Json(new { status = 401 });
            }
            passold = GetMD5Admin(passold);
            var check = _context.Admins.FirstOrDefault(x => x.AdminID == idadmin && x.MatKhau == passold);
            if(check == null)
            {
                return Json(new { status = 200 });
            }
            check.MatKhau = GetMD5Admin(passnew);
            _context.SaveChanges();
            return Json(new { status = 204 });
        }
    }
}