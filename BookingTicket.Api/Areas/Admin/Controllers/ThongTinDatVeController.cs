using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingTicket.Api.Areas.Admin.Filters;
using BookingTicket.Domain.DataModels;
using BookingTicket.Entities.Context;
using BookingTicket.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                thongtindatchos = thongtindatchos.Where(s => s.SDT.Contains(searchString)|| s.HoTenNguoiDat.Contains(searchString)).ToList();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(thongtindatchos.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Edit(long? id)
        {
            var thongtindatve = _context.ThongTinDatChos
                .Include(e => e.DieuHanh).ThenInclude(e => e.Xe)
                .Include(e => e.DieuHanh).ThenInclude(e => e.TuyenXe)
                .Where(e => e.MaDatCho == id)
                .Select(e => new ThongTinDatVeDataModel
                {
                    MaDatCho = e.MaDatCho,
                    MaDieuHanh = e.DieuHanh.MaDieuHanh,
                    SoLuongVe = e.SoLuongVe,
                    HinhThucThanhToan = e.HinhThucThanhToan,
                    NgayDat = e.NgayDat.Date.ToString("dd-MM-yyyy"),
                    NgayKhoiHanh = e.DieuHanh.NgayKhoiHanh.Date.ToString("dd-MM-yyyy"),
                    HoTenNguoiDat = e.HoTenNguoiDat,
                    SDT = e.SDT,
                    MaKH = e.MaKH,
                    TuyenXe = e.DieuHanh.TuyenXe.DiaDiem,
                    ThoiGian = e.DieuHanh.TuyenXe.ThoiGianKhoiHanh.ToString(@"hh\:mm") + "-" + e.DieuHanh.TuyenXe.ThoiGianKetThuc.ToString(@"hh\:mm"),
                    Xe = e.DieuHanh.Xe.BienSoXe,
                    GiaVe = e.DieuHanh.TuyenXe.GiaVe,
                })
                .FirstOrDefault();
            return View(thongtindatve);
        }

        [HttpPost]
        public IActionResult Edit(long? MaDatCho, string HoTenNguoiDat, string SDT)
        {
            var thongtindatve = _context.ThongTinDatChos.Find(MaDatCho);
            if(thongtindatve == null)
            {
                return NotFound();
            }
            thongtindatve.HoTenNguoiDat = HoTenNguoiDat;
            thongtindatve.SDT = SDT;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long? id)
        {
            var thongtindatve = _context.ThongTinDatChos.Find(id);
            if(thongtindatve == null)
            {
                return NotFound();
            }
            _context.ThongTinDatChos.Remove(thongtindatve);

            var ve = _context.Ves.Where(e => e.MaDatCho == id).ToList();
            if(ve == null)
            {
                return NotFound();
            }
            foreach(var item in ve)
            {
                var chongoi = _context.ChoNgois.Find(item.MaChoNgoi);
                if (chongoi==null)
                {
                    return NotFound();
                }
                chongoi.TinhTrang = 0;
                _context.Ves.Remove(item);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetChoNgoiById(long? id)
        {
            var datCho = _context.ThongTinDatChos
                .Include(e => e.DanhSachVe)
                .ThenInclude(e => e.ChoNgoi)
                .FirstOrDefault(e => e.MaDatCho == id);

            var choNgoi = new ArrayList();
            foreach (var ve in datCho.DanhSachVe)
            {
                if(ve.Status != 2)
                {
                    choNgoi.Add(new
                    {
                        MaChoNgoi = ve.ChoNgoi.MaChoNgoi,
                        TinhTrang = ve.ChoNgoi.TinhTrang,
                        MaDieuHanh = ve.ChoNgoi.MaDieuHanh,
                        ViTriChoNgoi = ve.ChoNgoi.ViTriChoNgoi,
                        MaVe = ve.MaVe

                    });
                }
            }
            return Json(choNgoi);
        }
        public IActionResult GetDSChoNgoi(long id, long dieuhanh)
        {
            var listChoNgoi = _context.ChoNgois.Where(e => e.MaDieuHanh == dieuhanh).ToList();

            var stop = listChoNgoi.Count / 10;
            var tableChoNgoi = new List<List<ChoNgoi>>();

            List<ChoNgoi> tableRow = new List<ChoNgoi>(stop);
            for (int i = 0; i < listChoNgoi.Count; i++)
            {
                tableRow.Add(listChoNgoi[i]);
                if ((i + 1) % stop == 0)
                {
                    tableChoNgoi.Add(tableRow);
                    tableRow = new List<ChoNgoi>();
                }
            }
            ViewBag.GheDuocChon = id;
            return PartialView("_DoiChoNgoi", tableChoNgoi);
        }

        public IActionResult ChangeChoNgoi(long idchange, long idhientai, long mave)
        {

            var ve = _context.Ves.Find(mave);
            if (ve == null)
            {
                return NotFound();               
            }
            ve.MaChoNgoi = idchange;

            var chongoihientai = _context.ChoNgois.Find(idhientai);
            if (chongoihientai == null)
            {
                return NotFound();
                
            }
            chongoihientai.TinhTrang = 0;

            var chongoimoi = _context.ChoNgois.Find(idchange);
            if (chongoimoi == null)
            {
                return NotFound();
            }
            chongoimoi.TinhTrang = 1;

            _context.Update(ve);
            _context.Update(chongoihientai);
            _context.Update(chongoimoi);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}