using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTicket.Entities.Models
{
    [Table("ThongTinDatCho")]
    public class ThongTinDatCho
    {
        public ThongTinDatCho()
        {
            DanhSachVe = new HashSet<Ve>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MaDatCho { get; set; }

        public int SoLuongVe { get; set; }

        public int HinhThucThanhToan { get; set; }

        public DateTime NgayDat { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string HoTenNguoiDat { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string SDT { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string CMND { get; set; }

        [ForeignKey("DieuHanh")]
        public long? MaDieuHanh { get; set; }

        [ForeignKey("NguoiDatCho")]
        public long? MaKH { get; set; }

        public virtual NguoiDung NguoiDatCho { get; set; }

        public virtual ICollection<Ve> DanhSachVe { get; set; }

        public virtual DieuHanh DieuHanh { get; set; }

    }
}
