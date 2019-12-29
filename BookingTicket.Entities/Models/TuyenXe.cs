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

        public string DiaDiem => DiaDiemDi + "->" + DiaDiemDen;

        public double GiaVe { get; set; }

        public TimeSpan ThoiGianKhoiHanh { get; set; }

        public TimeSpan ThoiGianKetThuc { get; set; }

        public string ThoiGian => ThoiGianKhoiHanh.ToString(@"hh\:mm") + "->" + ThoiGianKetThuc.ToString(@"hh\:mm");

        public DateTime? CreatedTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int IsDelete { get; set; }

        public virtual ICollection<DieuHanh> DanhSachDieuHanh { get; set; }
    }
}
