using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestiuneCD.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipSesiune",
                table: "CDs");

            migrationBuilder.AlterColumn<byte>(
                name: "tip",
                table: "CDs",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "nume",
                table: "CDs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<short>(
                name: "dimensiuneMB",
                table: "CDs",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Sesiuni",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCD = table.Column<int>(type: "int", nullable: false),
                    startDateTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    endDateTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    tipSesiune = table.Column<byte>(type: "TINYINT", nullable: false),
                    statusSesiune = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesiuni", x => x.id);
                    table.ForeignKey(
                        name: "FK_Sesiuni_CDs_idCD",
                        column: x => x.idCD,
                        principalTable: "CDs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sesiuni_idCD",
                table: "Sesiuni",
                column: "idCD");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sesiuni");

            migrationBuilder.AlterColumn<int>(
                name: "tip",
                table: "CDs",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT");

            migrationBuilder.AlterColumn<string>(
                name: "nume",
                table: "CDs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "dimensiuneMB",
                table: "CDs",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<int>(
                name: "tipSesiune",
                table: "CDs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
