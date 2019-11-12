using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTicket.Domain.DataModels
{
    public class DSVe
    {
        public string DiaDiemDi { get; set; }
        public string DiaDiemDen { get; set; }
        public string ThoiGianKhoiHanh { get; set; }
        public string ThoiGianKetThuc { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayHuy { get; set; }
        public double GiaVe { get; set; }
    }
}
