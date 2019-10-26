using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTicket.Entities.Models
{
    [Table("TuyenXe")]
    public class TuyenXe
    {
        public TuyenXe()
        {
            DanhSachDieuHanh = new HashSet<DieuHanh>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MaTuyenXe { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string TenTuyenXe { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string DiaDiemDi { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string DiaDiemDen { get; set; }

        public double GiaVe { get; set; }

        public DateTime ThoiGianKhoiHanh { get; set; }
        public DateTime ThoiGIanKetThuc { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public virtual ICollection<DieuHanh> DanhSachDieuHanh { get; set; }
    }
}
