using BookingTicket.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTicket.Entities.Context
{
    public class BookingTicketContext : DbContext
    {
        public BookingTicketContext()
        {
        }

        public BookingTicketContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<ThongTinDatCho> ThongTinDatChos { get; set; }
        public DbSet<TuyenXe> TuyenXes { get; set; }
        public DbSet<ChoNgoi> ChoNgois { get; set; }
        public DbSet<DieuHanh> DieuHanhs { get; set; }       
        public DbSet<Ve> Ves { get; set; }
        public DbSet<Xe> Xes { get; set; }
        public DbSet<LoaiChoNgoi> LoaiChoNgois { get; set; }
    }
}
