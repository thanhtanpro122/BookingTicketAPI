using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingTicket.Domain.DataModels;
using BookingTicket.Domain.ViewModels;
using BookingTicket.Entities.Context;
using BookingTicket.Entities.Models;
using BookingTicket.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BookingTicket.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DieuHanhController : Controller
    {
        private readonly BookingTicketContext _context;
        private readonly ITuyenXeLogic tuyenXeLogic;

        public DieuHanhController(ITuyenXeLogic tuyenXeLogic, BookingTicketContext context)
        {
            this.tuyenXeLogic = tuyenXeLogic;
            _context = context;
        }
        public IActionResult Index(long? matuyenxe, long? maxe, DateTime ngaykhoihanh, int? page)
        {
            var tuyenxe = _context.TuyenXes.ToList();
            ViewBag.TuyenXe = new SelectList(tuyenxe, "MaTuyenXe", "DiaDiem");


            ViewBag.ngaykhoihanh = ngaykhoihanh;
            var dieuhanhs = _context.DieuHanhs
                .Include(e => e.Xe)
                .Include(e => e.TuyenXe)
                .Include(e => e.DanhSachChoNgoi)
                .Select(e => new Domain.ViewModels.TuyenXe
                {
                    DiaDiemDen = e.TuyenXe.DiaDiemDen,
                    DiaDiemDi = e.TuyenXe.DiaDiemDi,
                    NgayKhoiHanh = e.NgayKhoiHanh,
                    GiaVe = e.TuyenXe.GiaVe,
                    MaTuyenXe = e.TuyenXe.MaTuyenXe,
                    ThoiGianKetThuc = e.TuyenXe.ThoiGianKetThuc.ToString("hh:mm"),
                    ThoiGianKhoiHanh = e.TuyenXe.ThoiGianKhoiHanh.ToString("hh:mm"),
                    TinhTrang = e.DanhSachChoNgoi.Where(i => i.TinhTrang == 0).Count(),
                    TongGhe = e.DanhSachChoNgoi.Count(),
                    MaDieuHanh = e.MaDieuHanh,
                    MaXe = e.Xe.MaXe,
                    TenXe = e.Xe.BienSoXe
                })
                .OrderBy(e => e.MaDieuHanh)
                .ToList();
            var datevalid = new DateTime(0001, 1, 1);
            if (ngaykhoihanh.Date != datevalid)
            {
                page = 1;
                dieuhanhs = dieuhanhs.Where(x => x.NgayKhoiHanh.Date == ngaykhoihanh.Date).ToList();
            }
            if (matuyenxe != null)
            {
                page = 1;
                dieuhanhs = dieuhanhs.Where(x => x.MaTuyenXe == matuyenxe).ToList();
            }
            if (maxe != null)
            {
                page = 1;
                dieuhanhs = dieuhanhs.Where(x => x.MaXe == maxe).ToList();
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dieuhanhs.ToPagedList(pageNumber, pageSize));
        }
        public JsonResult GetListTuyenXe(string name)
        {
            var tuyenxe = _context.TuyenXes.Where(x => x.DiaDiem.Contains(name ?? "")).Take(10).ToList();
            return Json(tuyenxe);
        }
        public JsonResult GetListXe()
        {
            try
            {
                var xe = _context.Xes.Take(10).ToList();
                return Json(xe);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IActionResult GetListChoNgoiById(long id)
        {
            var listChoNgoi = _context.ChoNgois.Where(e => e.MaDieuHanh == id).ToList();
            return PartialView("_DsChoNgoi", listChoNgoi);
        }
        public IActionResult UpdateStatusChoNgoi(long id)
        {
            var chongoi = _context.ChoNgois.Find(id);
            if (chongoi.TinhTrang == 1)
            {
                chongoi.TinhTrang = 0;
            }
            else
            {
                chongoi.TinhTrang = 1;
            }
            _context.SaveChanges();
            return Json(chongoi.TinhTrang);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var dieuhanh = _context.DieuHanhs
                .Include(e => e.Xe)
                .Include(e => e.TuyenXe)
                .Where(e => e.MaDieuHanh == id)
                .Select(e => new DieuHanh_Edit
                {
                    MaDieuHanh = e.MaDieuHanh,
                    MaTuyenXe = e.MaTuyenXe,
                    MaXe = e.MaXe,
                    DiaDiem = e.TuyenXe.DiaDiem,
                    NgayKhoiHanh = e.NgayKhoiHanh,
                    TenXe = e.Xe.BienSoXe
                });

            return Json(dieuhanh);
        }

        [HttpPost]
        public IActionResult Edit(DieuHanh dieuHanh)
        {
            dieuHanh.NgayKhoiHanh = dieuHanh.NgayKhoiHanh.Date;
            dieuHanh.UpdateTime = DateTime.Now;
            _context.Update(dieuHanh);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(DieuHanh dieuHanh)
        {
            dieuHanh.NgayKhoiHanh = dieuHanh.NgayKhoiHanh.Date;
            dieuHanh.CreatedTime = DateTime.Now;
            dieuHanh.Status = 0;
            _context.Add(dieuHanh);

            var soluongghe = _context.Xes.Find(dieuHanh.MaXe).SoLuongGhe;

            var listGhe = new List<ChoNgoi>();
            for(var i = 1; i <= soluongghe; i++)
            {
                listGhe.Add(new ChoNgoi
                {
                    DieuHanh = dieuHanh,
                    TinhTrang = 0,
                    ViTriChoNgoi = i
                });
            }

            _context.ChoNgois.AddRange(listGhe);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long? id)
        {
            var deuhanh = _context.DieuHanhs.Find(id);
            _context.DieuHanhs.Remove(deuhanh);

            var listChoNgoi = _context.ChoNgois.Where(e => e.MaDieuHanh == id).ToList();
            _context.ChoNgois.RemoveRange(listChoNgoi);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}