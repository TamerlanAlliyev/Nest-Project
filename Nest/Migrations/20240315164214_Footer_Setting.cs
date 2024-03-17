using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nest.Migrations
{
    public partial class Footer_Setting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FooterHeads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Head = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterHeads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FooterSubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubList = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    FooterHeadsId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    IPAddress = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterSubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FooterSubs_FooterHeads_FooterHeadsId",
                        column: x => x.FooterHeadsId,
                        principalTable: "FooterHeads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FooterSubs_FooterHeadsId",
                table: "FooterSubs",
                column: "FooterHeadsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FooterSubs");

            migrationBuilder.DropTable(
                name: "FooterHeads");
        }
    }
}
