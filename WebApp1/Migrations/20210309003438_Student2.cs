using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp1.Migrations
{
    public partial class Student2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_User_UserID",
                table: "Seat");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Seat",
                newName: "StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_UserID",
                table: "Seat",
                newName: "IX_Seat_StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Student_StudentID",
                table: "Seat",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Student_StudentID",
                table: "Seat");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Seat",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_StudentID",
                table: "Seat",
                newName: "IX_Seat_UserID");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isModel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_User_UserID",
                table: "Seat",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
