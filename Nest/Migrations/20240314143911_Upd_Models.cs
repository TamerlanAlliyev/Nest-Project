using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nest.Migrations
{
    public partial class Upd_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWeights_Sizes_SizeId",
                table: "ProductWeights");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWeights_Weights_WeightId",
                table: "ProductWeights");

            migrationBuilder.DropIndex(
                name: "IX_ProductWeights_SizeId",
                table: "ProductWeights");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "ProductWeights");

            migrationBuilder.AlterColumn<int>(
                name: "WeightId",
                table: "ProductWeights",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHover",
                table: "ProductImages",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWeights_Weights_WeightId",
                table: "ProductWeights",
                column: "WeightId",
                principalTable: "Weights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWeights_Weights_WeightId",
                table: "ProductWeights");

            migrationBuilder.DropColumn(
                name: "IsHover",
                table: "ProductImages");

            migrationBuilder.AlterColumn<int>(
                name: "WeightId",
                table: "ProductWeights",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "ProductWeights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductWeights_SizeId",
                table: "ProductWeights",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWeights_Sizes_SizeId",
                table: "ProductWeights",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWeights_Weights_WeightId",
                table: "ProductWeights",
                column: "WeightId",
                principalTable: "Weights",
                principalColumn: "Id");
        }
    }
}
