using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class ImagenesProductosTabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenProducto",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "ImagenProducto2",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "ImagenProducto3",
                table: "Producto");

            migrationBuilder.CreateTable(
                name: "ImagenesProducto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Imagen1 = table.Column<byte[]>(nullable: true),
                    Imagen2 = table.Column<byte[]>(nullable: true),
                    Imagen3 = table.Column<byte[]>(nullable: true),
                    ProductoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenesProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagenesProducto_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagenesProducto_ProductoId",
                table: "ImagenesProducto",
                column: "ProductoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagenesProducto");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImagenProducto",
                table: "Producto",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImagenProducto2",
                table: "Producto",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImagenProducto3",
                table: "Producto",
                type: "longblob",
                nullable: true);
        }
    }
}
