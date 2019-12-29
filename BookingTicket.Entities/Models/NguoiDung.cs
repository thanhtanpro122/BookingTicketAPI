using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTicket.Entities.Models
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        public NguoiDung()
        {
            DanhSachDatCho = new HashSet<ThongTinDatCho>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserID { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string SDT { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string MatKhau { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string HoTen { get; set; }

        public int GioiTinh { get; set; }

        [Column(TypeName = "varchar(1000)")]
        public string DiaChi { get; set; }

        public DateTime? NgaySinh { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Avatar { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public virtual ICollection<ThongTinDatCho> DanhSachDatCho { get; set; }

        public virtual UserCode UserCodes { get; set; }
    }
}