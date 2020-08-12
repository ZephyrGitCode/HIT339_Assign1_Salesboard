using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assign1_Salesboard_Zephyr.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id1 = table.Column<string>(nullable: true),
                    userid = table.Column<int>(nullable: false),
                    Itemname = table.Column<string>(maxLength: 60, nullable: false),
                    Itemdesc = table.Column<string>(maxLength: 255, nullable: true),
                    Category = table.Column<string>(maxLength: 30, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Postdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_AspNetUsers_Id1",
                        column: x => x.Id1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_Id1",
                table: "Item",
                column: "Id1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
