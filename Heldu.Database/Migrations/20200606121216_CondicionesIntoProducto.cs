using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class CondicionesIntoProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Entrega",
                table: "Producto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Horario",
                table: "Producto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recogida",
                table: "Producto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reserva",
                table: "Producto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Entrega",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Horario",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Recogida",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Reserva",
                table: "Producto");
        }
    }
}
