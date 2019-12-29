using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTicket.Entities.Models
{
    [Table("UserCode")]
    public class UserCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Required]
        [StringLength(64)]
        public string Code { get; set; }

        public DateTime? ExpiredTime { get; set; }

        public int? Status { get; set; }

        [ForeignKey("NguoiDung")]
        public long UserID { get; set; }

        public virtual NguoiDung NguoiDungs { get; set; }
    }
}
