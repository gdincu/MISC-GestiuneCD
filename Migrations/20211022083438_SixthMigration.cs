using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestiuneCD.Migrations
{
    public partial class SixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "vitezaDeInscriptionare",
                table: "CDs",
                newName: "vitezaMaxInscriptionare");

            migrationBuilder.AddColumn<int>(
                name: "VitezaInscriptionare",
                table: "Sesiuni",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VitezaInscriptionare",
                table: "Sesiuni");

            migrationBuilder.RenameColumn(
                name: "vitezaMaxInscriptionare",
                table: "CDs",
                newName: "vitezaDeInscriptionare");
        }
    }
}
