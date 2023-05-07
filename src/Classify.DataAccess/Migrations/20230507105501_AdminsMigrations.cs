using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classify.DataAccess.Migrations
{
    public partial class AdminsMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MidlleName",
                table: "Students",
                newName: "Surname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Students",
                newName: "MidlleName");
        }
    }
}
