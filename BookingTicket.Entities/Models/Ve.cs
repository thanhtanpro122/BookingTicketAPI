using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTicket.Entities.Models
{
    [Table("Ve")]
    public class Ve
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MaVe { get; set; }

        public double GiaVe { get; set; }

        public int Status { get; set; } 

        public DateTime UpdateTime { get; set; }

        [ForeignKey("ThongtinDatCho")]
        public long MaDatCho { get; set; }

        [ForeignKey("ChoNgoi")]
        public long? MaChoNgoi { get; set; }

        public virtual ThongTinDatCho ThongtinDatCho { get; set; }

        public virtual ChoNgoi ChoNgoi { get; set; }
    }
}
