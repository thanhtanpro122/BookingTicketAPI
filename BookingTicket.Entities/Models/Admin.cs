using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTicket.Entities.Models
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AdminID { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string MatKhau { get; set; }

        public int IsSuperAdmin { get; set; }
    }
}
