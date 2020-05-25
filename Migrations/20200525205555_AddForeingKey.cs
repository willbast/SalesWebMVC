using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesWebMvc.Migrations
{
    public partial class AddForeingKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seller_Department_DepartamentId",
                table: "Seller");

            migrationBuilder.DropIndex(
                name: "IX_Seller_DepartamentId",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "DepartamentId",
                table: "Seller");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Seller",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seller_DepartmentId",
                table: "Seller",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_Department_DepartmentId",
                table: "Seller",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seller_Department_DepartmentId",
                table: "Seller");

            migrationBuilder.DropIndex(
                name: "IX_Seller_DepartmentId",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Seller");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentId",
                table: "Seller",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seller_DepartamentId",
                table: "Seller",
                column: "DepartamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_Department_DepartamentId",
                table: "Seller",
                column: "DepartamentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
