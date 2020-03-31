using Microsoft.EntityFrameworkCore.Migrations;

namespace MechanicCompany.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_ApplicationUserId",
                table: "Cars");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_ApplicationUserId",
                table: "Cars",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_ApplicationUserId",
                table: "Cars");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_ApplicationUserId",
                table: "Cars",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
