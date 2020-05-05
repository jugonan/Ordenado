using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class ModificacionTablaVendedores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroRegistro",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "TipoDeEmpresa",
                table: "Vendedor");

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Vendedor",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostal",
                table: "Vendedor",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Dirección",
                table: "Vendedor",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "NumeroTiendas",
                table: "Vendedor",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Paginaweb",
                table: "Vendedor",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Vendedor",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "Dirección",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "NumeroTiendas",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "Paginaweb",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Vendedor");

            migrationBuilder.AddColumn<string>(
                name: "NumeroRegistro",
                table: "Vendedor",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoDeEmpresa",
                table: "Vendedor",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");
        }
    }
}
