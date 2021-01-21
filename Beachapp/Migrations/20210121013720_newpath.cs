using Microsoft.EntityFrameworkCore.Migrations;

namespace Beachapp.Migrations
{
    public partial class newpath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BeachPicture",
                table: "Beaches",
                newName: "PhotoPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Beaches",
                newName: "BeachPicture");
        }
    }
}
