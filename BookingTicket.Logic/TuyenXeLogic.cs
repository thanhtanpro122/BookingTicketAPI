using BookingTicket.Domain.ViewModels;
using BookingTicket.Entities.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingTicket.Logic
{
    public class TuyenXeLogic : ITuyenXeLogic
    {
        private readonly BookingTicketContext context;

        public TuyenXeLogic(BookingTicketContext context)
        {
            this.context = context;
        }
        public List<string> GetDiaDiemDi()
        {
            return context.TuyenXes.Select(e => e.DiaDiemDi).Distinct().ToList();
        }
        public List<string> GetDiaDiemDen()
        {
            return context.TuyenXes.Select(e => e.DiaDiemDen).Distinct().ToList();
        }

        public List<TuyenXe> GetDSTuyenXe(string diadiemdi, string diadiemden, DateTime ngayKhoiHanh)
        {
            var dieuhanhs = context.DieuHanhs
                .Include(e => e.Xe).ThenInclude(e => e.LoaiChoNgoi)
                .Include(e => e.DanhSachChoNgoi)
                .Include(e => e.TuyenXe)
                .Where(e => e.Status == 0 && e.TuyenXe.DiaDiemDi == diadiemdi && e.TuyenXe.DiaDiemDen == diadiemden && e.NgayKhoiHanh.Date == ngayKhoiHanh.Date)
                .Select(e => new TuyenXe
                {
                    DiaDiemDen = e.TuyenXe.DiaDiemDen,
                    DiaDiemDi = e.TuyenXe.DiaDiemDi,
                    NgayKhoiHanh = ngayKhoiHanh.Date,
                    GiaVe = e.TuyenXe.GiaVe,
                    MaTuyenXe = e.TuyenXe.MaTuyenXe,
                    ThoiGianKetThuc = e.TuyenXe.ThoiGianKetThuc.ToString(@"hh\:mm"),
                    ThoiGianKhoiHanh = e.TuyenXe.ThoiGianKhoiHanh.ToString(@"hh\:mm"),
                    TinhTrang = e.DanhSachChoNgoi.Where(i => i.TinhTrang == 0).Count(),
                    TongGhe = e.DanhSachChoNgoi.Count(),
                    MaDieuHanh = e.MaDieuHanh,
                    MaXe = e.Xe.MaXe,
                    TenXe = e.Xe.TenXe
                })
                .ToList();
            //var tuyenXes = context.TuyenXes
            //    .Include(e => e.DanhSachDieuHanh).ThenInclude(e => e.DanhSachChoNgoi)
            //    .Include(e => e.DanhSachDieuHanh).ThenInclude(e => e.Xe).ThenInclude(e=>e.LoaiChoNgoi)
            //    .Where(e => e.DiaDiemDi == diadiemdi && e.DiaDiemDen == diadiemden && e.DanhSachDieuHanh.Any(i => i.NgayKhoiHanh.Date == ngayKhoiHanh.Date))
            //    .Select(e => new TuyenXe
            //    {
            //        DiaDiemDen = e.DiaDiemDen,
            //        DiaDiemDi = e.DiaDiemDi,
            //        NgayKhoiHanh = ngayKhoiHanh.Date,
            //        GiaVe = e.GiaVe,
            //        MaTuyenXe= e.MaTuyenXe,
            //        ThoiGianKetThuc= e.ThoiGianKetThuc.ToString(@"hh\:mm"),
            //        ThoiGianKhoiHanh= e.ThoiGianKhoiHanh.ToString(@"hh\:mm"),
            //        TinhTrang = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh.Date == ngayKhoiHanh.Date).DanhSachChoNgoi.Where(i => i.TinhTrang == 0).Count(),
            //        TongGhe = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh.Date == ngayKhoiHanh.Date).DanhSachChoNgoi.Count(),
            //        MaDieuHanh = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh.Date == ngayKhoiHanh.Date).MaDieuHanh,
            //        MaXe = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh.Date == ngayKhoiHanh.Date).Xe.MaXe,
            //        TenXe = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh.Date == ngayKhoiHanh.Date).Xe.TenXe
            //    })
            //    .ToList();
            return dieuhanhs;
        }

        public List<ChoNgoiItem> GetDSChoNgoi(long maDieuHanh)
        {
            var choNgois = context.ChoNgois.Where(e => e.MaDieuHanh == maDieuHanh)
                .Select(e => new ChoNgoiItem
                {
                    MaChoNgoi = e.MaChoNgoi,
                    TinhTrang = e.TinhTrang,
                    MaDieuHanh = e.MaDieuHanh ?? -1
                }).ToList();
            return choNgois;
        }

        public List<TuyenXe> GetAll()
        {
            var dieuhanhs = context.DieuHanhs
                .Include(e => e.Xe).ThenInclude(e => e.LoaiChoNgoi)
                .Include(e => e.DanhSachChoNgoi)
                .Include(e => e.TuyenXe)
                .Where(e => e.Status == 0 && (e.NgayKhoiHanh.Date == DateTime.Today.AddDays(1) || e.NgayKhoiHanh.Date == DateTime.Today.AddDays(2)))
                .Select(e => new TuyenXe
                {
                    DiaDiemDen = e.TuyenXe.DiaDiemDen,
                    DiaDiemDi = e.TuyenXe.DiaDiemDi,
                    NgayKhoiHanh = e.NgayKhoiHanh.Date,
                    GiaVe = e.TuyenXe.GiaVe,
                    MaTuyenXe = e.TuyenXe.MaTuyenXe,
                    ThoiGianKetThuc = e.TuyenXe.ThoiGianKetThuc.ToString(@"hh\:mm"),
                    ThoiGianKhoiHanh = e.TuyenXe.ThoiGianKhoiHanh.ToString(@"hh\:mm"),
                    TinhTrang = e.DanhSachChoNgoi.Where(i => i.TinhTrang == 0).Count(),
                    TongGhe = e.DanhSachChoNgoi.Count(),
                    MaDieuHanh = e.MaDieuHanh,
                    MaXe = e.Xe.MaXe,
                    TenXe = e.Xe.TenXe
                })
                .ToList();

            //var tuyenXes = context.TuyenXes
            //    .Include(e => e.DanhSachDieuHanh).ThenInclude(e => e.DanhSachChoNgoi)
            //    .Include(e => e.DanhSachDieuHanh).ThenInclude(e => e.Xe)
            //    .Where(e => e.DanhSachDieuHanh.Any(i => i.NgayKhoiHanh.Date == DateTime.Today.AddDays(1) || i.NgayKhoiHanh.Date == DateTime.Today.AddDays(2)))
            //    .Select(e => new TuyenXe
            //    {
            //        DiaDiemDen = e.DiaDiemDen,
            //        DiaDiemDi = e.DiaDiemDi,
            //        NgayKhoiHanh = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh.Date == DateTime.Today.AddDays(1) || i.NgayKhoiHanh.Date == DateTime.Today.AddDays(2)).NgayKhoiHanh.Date,
            //        GiaVe = e.GiaVe,
            //        MaTuyenXe = e.MaTuyenXe,
            //        ThoiGianKetThuc = e.ThoiGianKetThuc.ToString(@"hh\:mm"),
            //        ThoiGianKhoiHanh = e.ThoiGianKhoiHanh.ToString(@"hh\:mm"),
            //        TinhTrang = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh.Date == DateTime.Today.AddDays(1) || i.NgayKhoiHanh.Date == DateTime.Today.AddDays(2)).DanhSachChoNgoi.Where(i => i.TinhTrang == 0).Count(),
            //        MaDieuHanh = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh.Date == DateTime.Today.AddDays(1) || i.NgayKhoiHanh.Date == DateTime.Today.AddDays(2)).MaDieuHanh,
            //        MaXe = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh.Date == DateTime.Today.AddDays(1) || i.NgayKhoiHanh.Date == DateTime.Today.AddDays(2)).Xe.MaXe,
            //        TenXe = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh.Date == DateTime.Today.AddDays(1) || i.NgayKhoiHanh.Date == DateTime.Today.AddDays(2)).Xe.BienSoXe
            //    })
            //    .ToList();
            return dieuhanhs;
        }
    }
}
