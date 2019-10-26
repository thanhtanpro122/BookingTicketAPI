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
            var tuyenXes = context.TuyenXes
                .Include(e => e.DanhSachDieuHanh).ThenInclude(e => e.DanhSachChoNgoi)
                .Include(e => e.DanhSachDieuHanh).ThenInclude(e => e.Xe)
                .Where(e => e.DiaDiemDi == diadiemdi && e.DiaDiemDen == diadiemden && e.DanhSachDieuHanh.Any(i => i.NgayKhoiHanh == ngayKhoiHanh))
                .Select(e => new TuyenXe
                {
                    DiaDiemDen = e.DiaDiemDen,
                    DiaDiemDi = e.DiaDiemDi,
                    NgayKhoiHanh = ngayKhoiHanh,
                    GiaVe = e.GiaVe,
                    MaTuyenXe= e.MaTuyenXe,
                    ThoiGIanKetThuc= e.ThoiGIanKetThuc,
                    ThoiGianKhoiHanh= e.ThoiGianKhoiHanh,
                    TinhTrang = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh == ngayKhoiHanh).DanhSachChoNgoi.Where(i => i.TinhTrang == 0).Count(),
                    MaDieuHanh = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh == ngayKhoiHanh).MaDieuHanh,
                    MaXe = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh == ngayKhoiHanh).Xe.MaXe,
                    TenXe = e.DanhSachDieuHanh.First(i => i.NgayKhoiHanh == ngayKhoiHanh).Xe.TenXe
                })
                .ToList();
            return tuyenXes;
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
    }
}
