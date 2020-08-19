using Microsoft.EntityFrameworkCore.Migrations;

namespace Assign1_Salesboard_Zephyr.Migrations
{
    public partial class CustomInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomInt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomInt",
                table: "AspNetUsers");
        }
    }
}
