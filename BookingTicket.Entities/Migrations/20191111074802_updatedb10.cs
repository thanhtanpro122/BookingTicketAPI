using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingTicket.Entities.Migrations
{
    public partial class updatedb10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ThoiGianKhoiHanh",
                table: "TuyenXe",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<string>(
                name: "ThoiGIanKetThuc",
                table: "TuyenXe",
                nullable: true,
                oldClrType: typeof(TimeSpan));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ThoiGianKhoiHanh",
                table: "TuyenXe",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ThoiGIanKetThuc",
                table: "TuyenXe",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
