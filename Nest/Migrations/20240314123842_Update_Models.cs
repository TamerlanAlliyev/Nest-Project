using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nest.Migrations
{
    public partial class Update_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weights_Products_ProductId",
                table: "Weights");

            migrationBuilder.DropIndex(
                name: "IX_Weights_ProductId",
                table: "Weights");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Weights");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Weights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weights_ProductId",
                table: "Weights",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weights_Products_ProductId",
                table: "Weights",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
