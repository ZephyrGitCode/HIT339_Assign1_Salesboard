using Microsoft.EntityFrameworkCore.Migrations;

namespace Assign1_Salesboard_Zephyr.Migrations
{
    public partial class FixedsellerAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_AspNetUsers_SellerIdId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_SellerIdId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SellerIdId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_SellerId",
                table: "Item",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_AspNetUsers_SellerId",
                table: "Item",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_AspNetUsers_SellerId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_SellerId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "SellerIdId",
                table: "Item",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Item_SellerIdId",
                table: "Item",
                column: "SellerIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_AspNetUsers_SellerIdId",
                table: "Item",
                column: "SellerIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
