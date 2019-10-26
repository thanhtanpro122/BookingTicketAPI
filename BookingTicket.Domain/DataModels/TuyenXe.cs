﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTicket.Domain.ViewModels
{
    public class TuyenXe
    {
        public long MaTuyenXe { get; set; }
        public string DiaDiemDi { get; set; }
        public string DiaDiemDen { get; set; }
        public DateTime ThoiGianKhoiHanh { get; set; }
        public DateTime ThoiGIanKetThuc { get; set; }
        public int TinhTrang { get; set; }
        public long MaDieuHanh { get; set; }
        public DateTime NgayKhoiHanh { get; set; }
        public double GiaVe { get; set; }
        public long MaXe { get; set; }
        public string TenXe { get; set; }
        public List<ChoNgoiItem> ChoNgoi { get; set; }
    }
    public class ChoNgoiItem
    {
        public long MaChoNgoi { get; set; }
        public int TinhTrang { get; set; }
        public long MaDieuHanh { get; set; }
    }
}
