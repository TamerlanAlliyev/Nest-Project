using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nest.Migrations
{
    public partial class Update_Nav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "FooterSubs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "FooterHeads",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "FooterSubs");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "FooterHeads");
        }
    }
}
