using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class ImagenesComoByte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ImagenProducto3",
                table: "Producto",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImagenProducto2",
                table: "Producto",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImagenProducto",
                table: "Producto",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagenProducto3",
                table: "Producto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagenProducto2",
                table: "Producto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagenProducto",
                table: "Producto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(byte[]));
        }
    }
}
