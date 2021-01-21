using Microsoft.EntityFrameworkCore.Migrations;

namespace Beachapp.Migrations
{
    public partial class ReadyToGo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Beaches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "Beaches",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
