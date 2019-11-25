using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingTicket.Entities.Migrations
{
    public partial class update_db_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViTriChoNgoi",
                table: "ChoNgoi",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViTriChoNgoi",
                table: "ChoNgoi");
        }
    }
}
