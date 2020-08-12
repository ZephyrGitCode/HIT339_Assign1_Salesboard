using Microsoft.EntityFrameworkCore.Migrations;

namespace Assign1_Salesboard_Zephyr.Migrations
{
    public partial class Fixedseller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_AspNetUsers_SelleridId",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "SelleridId",
                table: "Item",
                newName: "SellerIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_SelleridId",
                table: "Item",
                newName: "IX_Item_SellerIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_AspNetUsers_SellerIdId",
                table: "Item",
                column: "SellerIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_AspNetUsers_SellerIdId",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "SellerIdId",
                table: "Item",
                newName: "SelleridId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_SellerIdId",
                table: "Item",
                newName: "IX_Item_SelleridId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_AspNetUsers_SelleridId",
                table: "Item",
                column: "SelleridId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
