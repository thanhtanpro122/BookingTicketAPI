using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingTicket.Entities.Migrations
{
    public partial class UpdateDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "MaDieuHanh",
                table: "ThongTinDatCho",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "NguoiDung",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "NguoiDung",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "NguoiDung",
                nullable: true,
                oldClrType: typeof(DateTime));

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
                name: "TuyenXe",
                columns: table => new
                {
                    MaTuyenXe = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    TenTuyenXe = table.Column<string>(type: "varchar(150)", nullable: true),
                    DiaDiemDi = table.Column<string>(type: "varchar(150)", nullable: true),
                    DiaDiemDen = table.Column<string>(type: "varchar(150)", nullable: true),
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
                name: "Ve",
                columns: table => new
                {
                    MaVe = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    GiaVe = table.Column<double>(nullable: false),
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
                name: "IX_ThongTinDatCho_MaDieuHanh",
                table: "ThongTinDatCho",
                column: "MaDieuHanh");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ThongTinDatCho_DieuHanh_MaDieuHanh",
                table: "ThongTinDatCho",
                column: "MaDieuHanh",
                principalTable: "DieuHanh",
                principalColumn: "MaDieuHanh",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThongTinDatCho_DieuHanh_MaDieuHanh",
                table: "ThongTinDatCho");

            migrationBuilder.DropTable(
                name: "Ve");

            migrationBuilder.DropTable(
                name: "ChoNgoi");

            migrationBuilder.DropTable(
                name: "DieuHanh");

            migrationBuilder.DropTable(
                name: "TuyenXe");

            migrationBuilder.DropTable(
                name: "Xe");

            migrationBuilder.DropTable(
                name: "LoaiChoNgoi");

            migrationBuilder.DropIndex(
                name: "IX_ThongTinDatCho_MaDieuHanh",
                table: "ThongTinDatCho");

            migrationBuilder.AlterColumn<long>(
                name: "MaDieuHanh",
                table: "ThongTinDatCho",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "NguoiDung",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "NguoiDung",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "NguoiDung",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
