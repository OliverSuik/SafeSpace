using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp1.Migrations
{
    public partial class BookingTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "Seat");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingTime",
                table: "Seat",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingTime",
                table: "Seat");

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "Seat",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
