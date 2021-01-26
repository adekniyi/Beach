using Microsoft.EntityFrameworkCore.Migrations;

namespace Beachapp.Migrations
{
    public partial class addingPosterFieldToBeachModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Beaches",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Beaches");
        }
    }
}
