using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingTicket.Entities.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    NgaySinh = table.Column<DateTime>(nullable: false),
                    Avatar = table.Column<string>(type: "varchar(200)", nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinDatCho",
                columns: table => new
                {
                    MadatCho = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    SoLuongVe = table.Column<int>(nullable: false),
                    NgayDat = table.Column<DateTime>(nullable: false),
                    HoTenNguoiDat = table.Column<string>(type: "varchar(150)", nullable: true),
                    SDT = table.Column<string>(type: "varchar(20)", nullable: true),
                    CMND = table.Column<string>(type: "varchar(20)", nullable: true),
                    MaDieuHanh = table.Column<long>(nullable: false),
                    MaKH = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinDatCho", x => x.MadatCho);
                    table.ForeignKey(
                        name: "FK_ThongTinDatCho_NguoiDung_MaKH",
                        column: x => x.MaKH,
                        principalTable: "NguoiDung",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinDatCho_MaKH",
                table: "ThongTinDatCho",
                column: "MaKH");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongTinDatCho");

            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}
