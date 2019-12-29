using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTicket.Entities.Models
{
    [Table("Xe")]
    public class Xe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MaXe { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string TenXe { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string HangXe { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string BienSoXe { get; set; }

        public int SoLuongGhe { get; set; }

        public int IsDelete { get; set; }

        [ForeignKey("loaiChoNgoi")]
        [JsonProperty("loaiCho")]
        public long loaiChoNgoi { get; set; }

        public virtual LoaiChoNgoi LoaiChoNgoi { get; set; }
        public virtual ICollection<DieuHanh> DanhSachDieuHanh { get; set; }
    }
}
