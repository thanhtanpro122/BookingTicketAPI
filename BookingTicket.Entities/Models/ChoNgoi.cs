using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTicket.Entities.Models
{
    [Table("ChoNgoi")]
    public class ChoNgoi
    {
        public ChoNgoi()
        {
            DanhSachChoNgoi = new HashSet<Ve>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MaChoNgoi { get; set; }

        public int TinhTrang { get; set; }

        [ForeignKey("DieuHanh")]
        public long? MaDieuHanh { get; set; }

        public int ViTriChoNgoi { get; set; }

        public virtual ICollection<Ve> DanhSachChoNgoi { get; set; }

        public virtual DieuHanh DieuHanh { get; set; }

    }
}
