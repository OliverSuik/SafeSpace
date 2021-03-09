using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp1.Migrations
{
    public partial class LecturerField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lecturer",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "LecturerID",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_LecturerID",
                table: "Course",
                column: "LecturerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Lecturer_LecturerID",
                table: "Course",
                column: "LecturerID",
                principalTable: "Lecturer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Lecturer_LecturerID",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_LecturerID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "LecturerID",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "Lecturer",
                table: "Course",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
