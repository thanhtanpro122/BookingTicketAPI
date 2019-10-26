using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTicket.Entities.Models
{
    [Table("LoaiChoNgoi")]
    public class LoaiChoNgoi
    {
        public LoaiChoNgoi()
        {
            DanhSachXe = new HashSet<Xe>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MaLoaiChoNgoi { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string TenLoaiChoNgoi { get; set; }

        public virtual ICollection<Xe> DanhSachXe { get; set; }

    }
}
