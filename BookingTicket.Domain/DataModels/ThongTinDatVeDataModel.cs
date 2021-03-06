﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTicket.Domain.DataModels
{
    public class ThongTinDatVeDataModel
    {
        public long MaDatCho { get; set; }

        public int SoLuongVe { get; set; }

        public int HinhThucThanhToan { get; set; }

        public string NgayDat { get; set; }

        public string NgayKhoiHanh { get; set; }

        public string HoTenNguoiDat { get; set; }

        public string TuyenXe { get; set; }

        public string ThoiGian { get; set; }

        public string Xe { get; set; }

        public double GiaVe { get; set; }

        public string SDT { get; set; }

        public long? MaDieuHanh { get; set; }

        public long? MaKH { get; set; }
    }
}
