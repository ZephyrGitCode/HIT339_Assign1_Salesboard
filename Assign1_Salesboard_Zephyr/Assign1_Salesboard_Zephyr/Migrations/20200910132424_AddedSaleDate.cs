using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assign1_Salesboard_Zephyr.Migrations
{
    public partial class AddedSaleDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SaleDate",
                table: "Sale",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleDate",
                table: "Sale");
        }
    }
}
