using Microsoft.EntityFrameworkCore.Migrations;

namespace Assign1_Salesboard_Zephyr.Migrations
{
    public partial class FixedPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Saleitem",
                table: "Saleitem");

            migrationBuilder.RenameTable(
                name: "Saleitem",
                newName: "Cart");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "Saleitem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Saleitem",
                table: "Saleitem",
                column: "Id");
        }
    }
}
