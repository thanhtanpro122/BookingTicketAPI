using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTicket.Domain.DataModels
{
    public class DatVe
    {
        public string Hoten { get; set; }
        public string sdt { get; set; }
        public int HinhThucThanhToan { get; set; }
        public int? MaKH { get; set; }
        public int maDieuHanh { get; set; }
        public int soluongve { get; set; }
        public DateTime ngayDat { get; set; }
        public double GiaVe { get; set; }
        public int[] maChoNgoi { get; set; }
    }
}
