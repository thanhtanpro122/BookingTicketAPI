using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingTicket.Entities.Migrations
{
    public partial class UpdateDb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenXe",
                table: "Xe",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GiaVe",
                table: "TuyenXe",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenXe",
                table: "Xe");

            migrationBuilder.DropColumn(
                name: "GiaVe",
                table: "TuyenXe");
        }
    }
}
