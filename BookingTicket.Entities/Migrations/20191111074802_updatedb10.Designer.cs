﻿// <auto-generated />
using System;
using BookingTicket.Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookingTicket.Entities.Migrations
{
    [DbContext(typeof(BookingTicketContext))]
    [Migration("20191111074802_updatedb10")]
    partial class updatedb10
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099");

            modelBuilder.Entity("BookingTicket.Entities.Models.ChoNgoi", b =>
                {
                    b.Property<long>("MaChoNgoi")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("MaDieuHanh");

                    b.Property<int>("TinhTrang");

                    b.HasKey("MaChoNgoi");

                    b.HasIndex("MaDieuHanh");

                    b.ToTable("ChoNgoi");
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.DieuHanh", b =>
                {
                    b.Property<long>("MaDieuHanh")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("MaTuyenXe");

                    b.Property<long?>("MaXe");

                    b.Property<DateTime>("NgayKhoiHanh");

                    b.HasKey("MaDieuHanh");

                    b.HasIndex("MaTuyenXe");

                    b.HasIndex("MaXe");

                    b.ToTable("DieuHanh");
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.LoaiChoNgoi", b =>
                {
                    b.Property<long>("MaLoaiChoNgoi")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TenLoaiChoNgoi")
                        .HasColumnType("varchar(150)");

                    b.HasKey("MaLoaiChoNgoi");

                    b.ToTable("LoaiChoNgoi");
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.NguoiDung", b =>
                {
                    b.Property<long>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<string>("DiaChi")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("GioiTinh");

                    b.Property<string>("HoTen")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("MatKhau")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("NgaySinh");

                    b.Property<string>("SDT")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("UserID");

                    b.ToTable("NguoiDung");
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.ThongTinDatCho", b =>
                {
                    b.Property<long>("MadatCho")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CMND")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("HinhThucThanhToan");

                    b.Property<string>("HoTenNguoiDat")
                        .HasColumnType("varchar(150)");

                    b.Property<long?>("MaDieuHanh");

                    b.Property<long?>("MaKH");

                    b.Property<DateTime>("NgayDat");

                    b.Property<string>("SDT")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("SoLuongVe");

                    b.HasKey("MadatCho");

                    b.HasIndex("MaDieuHanh");

                    b.HasIndex("MaKH");

                    b.ToTable("ThongTinDatCho");
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.TuyenXe", b =>
                {
                    b.Property<long>("MaTuyenXe")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<string>("DiaDiemDen")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("DiaDiemDi")
                        .HasColumnType("varchar(150)");

                    b.Property<double>("GiaVe");

                    b.Property<string>("TenTuyenXe")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ThoiGIanKetThuc");

                    b.Property<string>("ThoiGianKhoiHanh");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("MaTuyenXe");

                    b.ToTable("TuyenXe");
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.Ve", b =>
                {
                    b.Property<long>("MaVe")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("GiaVe");

                    b.Property<long?>("MaChoNgoi");

                    b.Property<long>("MaDatCho");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("MaVe");

                    b.HasIndex("MaChoNgoi");

                    b.HasIndex("MaDatCho");

                    b.ToTable("Ve");
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.Xe", b =>
                {
                    b.Property<long>("MaXe")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BienSoXe")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("HangXe")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("SoLuongGhe");

                    b.Property<string>("TenXe")
                        .HasColumnType("varchar(150)");

                    b.Property<long>("loaiChoNgoi");

                    b.HasKey("MaXe");

                    b.HasIndex("loaiChoNgoi");

                    b.ToTable("Xe");
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.ChoNgoi", b =>
                {
                    b.HasOne("BookingTicket.Entities.Models.DieuHanh", "DieuHanh")
                        .WithMany("DanhSachChoNgoi")
                        .HasForeignKey("MaDieuHanh");
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.DieuHanh", b =>
                {
                    b.HasOne("BookingTicket.Entities.Models.TuyenXe", "TuyenXe")
                        .WithMany("DanhSachDieuHanh")
                        .HasForeignKey("MaTuyenXe");

                    b.HasOne("BookingTicket.Entities.Models.Xe", "Xe")
                        .WithMany("DanhSachDieuHanh")
                        .HasForeignKey("MaXe");
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.ThongTinDatCho", b =>
                {
                    b.HasOne("BookingTicket.Entities.Models.DieuHanh", "DieuHanh")
                        .WithMany("DanhSachThongTinDatCho")
                        .HasForeignKey("MaDieuHanh");

                    b.HasOne("BookingTicket.Entities.Models.NguoiDung", "NguoiDatCho")
                        .WithMany("DanhSachDatCho")
                        .HasForeignKey("MaKH");
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.Ve", b =>
                {
                    b.HasOne("BookingTicket.Entities.Models.ChoNgoi", "ChoNgoi")
                        .WithMany("DanhSachChoNgoi")
                        .HasForeignKey("MaChoNgoi");

                    b.HasOne("BookingTicket.Entities.Models.ThongTinDatCho", "ThongtinDatCho")
                        .WithMany("DanhSachVe")
                        .HasForeignKey("MaDatCho")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookingTicket.Entities.Models.Xe", b =>
                {
                    b.HasOne("BookingTicket.Entities.Models.LoaiChoNgoi", "LoaiChoNgoi")
                        .WithMany("DanhSachXe")
                        .HasForeignKey("loaiChoNgoi")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
