using Microsoft.EntityFrameworkCore.Migrations;

namespace Beachapp.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beaches",
                columns: table => new
                {
                    BeachId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BeachName = table.Column<string>(type: "TEXT", nullable: true),
                    BeachDetails = table.Column<string>(type: "TEXT", nullable: true),
                    BeachPicture = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beaches", x => x.BeachId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beaches");
        }
    }
}
