using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingTicket.Entities.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiChoNgoi",
                columns: table => new
                {
                    MaLoaiChoNgoi = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    TenLoaiChoNgoi = table.Column<string>(type: "varchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiChoNgoi", x => x.MaLoaiChoNgoi);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    UserID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    SDT = table.Column<string>(type: "varchar(20)", nullable: true),
                    MatKhau = table.Column<string>(type: "varchar(50)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    HoTen = table.Column<string>(type: "varchar(150)", nullable: true),
                    GioiTinh = table.Column<int>(nullable: false),
                    DiaChi = table.Column<string>(type: "varchar(1000)", nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: true),
                    Avatar = table.Column<string>(type: "varchar(200)", nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "TuyenXe",
                columns: table => new
                {
                    MaTuyenXe = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    TenTuyenXe = table.Column<string>(type: "varchar(150)", nullable: true),
                    DiaDiemDi = table.Column<string>(type: "varchar(150)", nullable: true),
                    DiaDiemDen = table.Column<string>(type: "varchar(150)", nullable: true),
                    GiaVe = table.Column<double>(nullable: false),
                    ThoiGianKhoiHanh = table.Column<DateTime>(nullable: false),
                    ThoiGIanKetThuc = table.Column<DateTime>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TuyenXe", x => x.MaTuyenXe);
                });

            migrationBuilder.CreateTable(
                name: "Xe",
                columns: table => new
                {
                    MaXe = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    TenXe = table.Column<string>(type: "varchar(150)", nullable: true),
                    HangXe = table.Column<string>(type: "varchar(150)", nullable: true),
                    BienSoXe = table.Column<string>(type: "varchar(150)", nullable: true),
                    SoLuongGhe = table.Column<int>(nullable: false),
                    loaiChoNgoi = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xe", x => x.MaXe);
                    table.ForeignKey(
                        name: "FK_Xe_LoaiChoNgoi_loaiChoNgoi",
                        column: x => x.loaiChoNgoi,
                        principalTable: "LoaiChoNgoi",
                        principalColumn: "MaLoaiChoNgoi",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DieuHanh",
                columns: table => new
                {
                    MaDieuHanh = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    NgayKhoiHanh = table.Column<DateTime>(nullable: false),
                    MaTuyenXe = table.Column<long>(nullable: true),
                    MaXe = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieuHanh", x => x.MaDieuHanh);
                    table.ForeignKey(
                        name: "FK_DieuHanh_TuyenXe_MaTuyenXe",
                        column: x => x.MaTuyenXe,
                        principalTable: "TuyenXe",
                        principalColumn: "MaTuyenXe",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DieuHanh_Xe_MaXe",
                        column: x => x.MaXe,
                        principalTable: "Xe",
                        principalColumn: "MaXe",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChoNgoi",
                columns: table => new
                {
                    MaChoNgoi = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    TinhTrang = table.Column<int>(nullable: false),
                    MaDieuHanh = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoNgoi", x => x.MaChoNgoi);
                    table.ForeignKey(
                        name: "FK_ChoNgoi_DieuHanh_MaDieuHanh",
                        column: x => x.MaDieuHanh,
                        principalTable: "DieuHanh",
                        principalColumn: "MaDieuHanh",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinDatCho",
                columns: table => new
                {
                    MadatCho = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    SoLuongVe = table.Column<int>(nullable: false),
                    HinhThucThanhToan = table.Column<int>(nullable: false),
                    NgayDat = table.Column<DateTime>(nullable: false),
                    HoTenNguoiDat = table.Column<string>(type: "varchar(150)", nullable: true),
                    SDT = table.Column<string>(type: "varchar(20)", nullable: true),
                    CMND = table.Column<string>(type: "varchar(20)", nullable: true),
                    MaDieuHanh = table.Column<long>(nullable: true),
                    MaKH = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinDatCho", x => x.MadatCho);
                    table.ForeignKey(
                        name: "FK_ThongTinDatCho_DieuHanh_MaDieuHanh",
                        column: x => x.MaDieuHanh,
                        principalTable: "DieuHanh",
                        principalColumn: "MaDieuHanh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThongTinDatCho_NguoiDung_MaKH",
                        column: x => x.MaKH,
                        principalTable: "NguoiDung",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ve",
                columns: table => new
                {
                    MaVe = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    GiaVe = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    MaDatCho = table.Column<long>(nullable: false),
                    MaChoNgoi = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ve", x => x.MaVe);
                    table.ForeignKey(
                        name: "FK_Ve_ChoNgoi_MaChoNgoi",
                        column: x => x.MaChoNgoi,
                        principalTable: "ChoNgoi",
                        principalColumn: "MaChoNgoi",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ve_ThongTinDatCho_MaDatCho",
                        column: x => x.MaDatCho,
                        principalTable: "ThongTinDatCho",
                        principalColumn: "MadatCho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChoNgoi_MaDieuHanh",
                table: "ChoNgoi",
                column: "MaDieuHanh");

            migrationBuilder.CreateIndex(
                name: "IX_DieuHanh_MaTuyenXe",
                table: "DieuHanh",
                column: "MaTuyenXe");

            migrationBuilder.CreateIndex(
                name: "IX_DieuHanh_MaXe",
                table: "DieuHanh",
                column: "MaXe");

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinDatCho_MaDieuHanh",
                table: "ThongTinDatCho",
                column: "MaDieuHanh");

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinDatCho_MaKH",
                table: "ThongTinDatCho",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_MaChoNgoi",
                table: "Ve",
                column: "MaChoNgoi");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_MaDatCho",
                table: "Ve",
                column: "MaDatCho");

            migrationBuilder.CreateIndex(
                name: "IX_Xe_loaiChoNgoi",
                table: "Xe",
                column: "loaiChoNgoi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ve");

            migrationBuilder.DropTable(
                name: "ChoNgoi");

            migrationBuilder.DropTable(
                name: "ThongTinDatCho");

            migrationBuilder.DropTable(
                name: "DieuHanh");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "TuyenXe");

            migrationBuilder.DropTable(
                name: "Xe");

            migrationBuilder.DropTable(
                name: "LoaiChoNgoi");
        }
    }
}
