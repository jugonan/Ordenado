using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class VisitasAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descuento",
                table: "Producto",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Disponible",
                table: "Producto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Producto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImagenProducto2",
                table: "Producto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenProducto3",
                table: "Producto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrecioFinal",
                table: "Producto",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Visita",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(nullable: false),
                    VendedorId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false),
                    FechaVisita = table.Column<DateTime>(nullable: false),
                    Unidades = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visita_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visita_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visita_Vendedor_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visita_ProductoId",
                table: "Visita",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_UsuarioId",
                table: "Visita",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_VendedorId",
                table: "Visita",
                column: "VendedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visita");

            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Disponible",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "ImagenProducto2",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "ImagenProducto3",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "PrecioFinal",
                table: "Producto");
        }
    }
}
