using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingTicket.Domain.ViewModels;
using BookingTicket.Entities.Context;
using BookingTicket.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BookingTicket.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DieuHanhController : Controller
    {
        private readonly BookingTicketContext _context;
        private readonly ITuyenXeLogic tuyenXeLogic;

        public DieuHanhController(ITuyenXeLogic tuyenXeLogic,BookingTicketContext context)
        {
            this.tuyenXeLogic = tuyenXeLogic;
            _context = context;
        }
        public IActionResult Index(long? madieuhanh, long? maxe, DateTime ngaykhoihanh, int? page)
        {           
            ViewBag.ngaykhoihanh = ngaykhoihanh;
            var dieuhanhs = _context.DieuHanhs
                .Include(e => e.Xe)
                .Include(e => e.TuyenXe)
                .Include(e => e.DanhSachChoNgoi)
                .Select(e => new TuyenXe
                {
                    DiaDiemDen = e.TuyenXe.DiaDiemDi,
                    DiaDiemDi = e.TuyenXe.DiaDiemDen,
                    NgayKhoiHanh = e.NgayKhoiHanh,
                    GiaVe = e.TuyenXe.GiaVe,
                    MaTuyenXe = e.TuyenXe.MaTuyenXe,
                    ThoiGIanKetThuc = e.TuyenXe.ThoiGIanKetThuc,
                    ThoiGianKhoiHanh = e.TuyenXe.ThoiGianKhoiHanh,
                    TinhTrang = e.DanhSachChoNgoi.Where(i => i.TinhTrang == 0).Count(),
                    TongGhe=e.DanhSachChoNgoi.Count(),
                    MaDieuHanh = e.MaDieuHanh,
                    MaXe = e.Xe.MaXe,
                    TenXe = e.Xe.BienSoXe
                }).ToList();
            var datevalid = new DateTime(0001, 1, 1);
            if (ngaykhoihanh.Date != datevalid)
            {
                page = 1;
                dieuhanhs = dieuhanhs.Where(x => x.NgayKhoiHanh.Date == ngaykhoihanh.Date).ToList();
            }
            if (madieuhanh != null)
            {
                page = 1;
                dieuhanhs = dieuhanhs.Where(x => x.MaDieuHanh == madieuhanh).ToList();
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

        public IActionResult Create()
        {
            return View();
        }
    }
}