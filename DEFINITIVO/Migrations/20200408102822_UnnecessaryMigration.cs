using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class UnnecessaryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dirección",
                table: "Vendedor");

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Vendedor",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Vendedor");

            migrationBuilder.AddColumn<string>(
                name: "Dirección",
                table: "Vendedor",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");
        }
    }
}
