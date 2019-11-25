using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingTicket.Entities.Migrations
{
    public partial class update_db_14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "DieuHanh",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "DieuHanh",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "DieuHanh");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "DieuHanh");
        }
    }
}
