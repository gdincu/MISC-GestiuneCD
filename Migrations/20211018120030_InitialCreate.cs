using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestiuneCD.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CDs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dimensiuneMB = table.Column<int>(type: "int", nullable: false),
                    vitezaDeInscriptionare = table.Column<int>(type: "int", nullable: false),
                    tip = table.Column<int>(type: "int", nullable: false),
                    spatiuOcupat = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    nrDeSesiuni = table.Column<int>(type: "int", nullable: false),
                    tipSesiune = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDs", x => x.id);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CDs");
        }
    }
}
