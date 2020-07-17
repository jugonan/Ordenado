using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class TablasProductoYOpcionProductoModificadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Entrega",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Horario",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "InformaciónAdicional",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "PrecioFinal",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Recogida",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Reserva",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Reservas",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "UnidadesStock",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "OpcionProducto");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "UbicacionVendedor",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Calle",
                table: "UbicacionVendedor",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CP",
                table: "UbicacionVendedor",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "UbicacionUsuario",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Calle",
                table: "UbicacionUsuario",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CP",
                table: "UbicacionUsuario",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAltaProducto",
                table: "Producto",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<float>(
                name: "PrecioInicial",
                table: "OpcionProducto",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<float>(
                name: "PrecioFinal",
                table: "OpcionProducto",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddColumn<int>(
                name: "CantidadVendida",
                table: "OpcionProducto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockInicial",
                table: "OpcionProducto",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaAltaProducto",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "CantidadVendida",
                table: "OpcionProducto");

            migrationBuilder.DropColumn(
                name: "StockInicial",
                table: "OpcionProducto");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "UbicacionVendedor",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Calle",
                table: "UbicacionVendedor",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CP",
                table: "UbicacionVendedor",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "UbicacionUsuario",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Calle",
                table: "UbicacionUsuario",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CP",
                table: "UbicacionUsuario",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Descuento",
                table: "Producto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Entrega",
                table: "Producto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Horario",
                table: "Producto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InformaciónAdicional",
                table: "Producto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "Producto",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PrecioFinal",
                table: "Producto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recogida",
                table: "Producto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reserva",
                table: "Producto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reservas",
                table: "Producto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnidadesStock",
                table: "Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioInicial",
                table: "OpcionProducto",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioFinal",
                table: "OpcionProducto",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "OpcionProducto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
