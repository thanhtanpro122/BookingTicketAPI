using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingTicket.Entities.Migrations
{
    public partial class updatedb09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ThoiGianKhoiHanh",
                table: "TuyenXe",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ThoiGIanKetThuc",
                table: "TuyenXe",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ThoiGianKhoiHanh",
                table: "TuyenXe",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ThoiGIanKetThuc",
                table: "TuyenXe",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
