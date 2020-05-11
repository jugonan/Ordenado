using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class UbicacionModificado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Ubicacion");

            migrationBuilder.AddColumn<string>(
                name: "CCAA",
                table: "Ubicacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CP",
                table: "Ubicacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "Ubicacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Letra",
                table: "Ubicacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Ubicacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Ubicacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Poblacion",
                table: "Ubicacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "Ubicacion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCAA",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "CP",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "Calle",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "Letra",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "Poblacion",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "Ubicacion");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Ubicacion",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
