using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTicket.Domain.DataModels
{
    public class DieuHanh_Edit
    {
        public long MaDieuHanh { get; set; }
        public long? MaTuyenXe { get; set; }
        public string DiaDiem { get; set; }
        public long? MaXe { get; set; }
        public string TenXe { get; set; }
        public DateTime NgayKhoiHanh { get; set; }
    }
}
