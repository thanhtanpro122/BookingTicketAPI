using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTicket.Entities.Models
{
    [Table("DieuHanh")]
    public class DieuHanh
    {
        public DieuHanh()
        {
            DanhSachChoNgoi = new HashSet<ChoNgoi>();
            DanhSachThongTinDatCho = new HashSet<ThongTinDatCho>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MaDieuHanh { get; set; }

        public DateTime NgayKhoiHanh { get; set; }

        [ForeignKey("TuyenXe")]
        public long? MaTuyenXe { get; set; }

        [ForeignKey("Xe")]
        public long? MaXe { get; set; }

        public int Status { get; set; }
        public DateTime? CreatedTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public virtual TuyenXe TuyenXe { get; set; }
        public virtual Xe Xe { get; set; }

        public virtual ICollection<ChoNgoi> DanhSachChoNgoi { get; set; }
        public virtual ICollection<ThongTinDatCho> DanhSachThongTinDatCho { get; set; }


    }
}
