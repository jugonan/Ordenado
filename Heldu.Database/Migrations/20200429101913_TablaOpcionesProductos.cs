using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class TablaOpcionesProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescripcionEmpresa",
                table: "Vendedor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InformaciónAdicional",
                table: "Producto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LimiteProducto",
                table: "Producto",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpcionProductoId",
                table: "Producto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Reservas",
                table: "Producto",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OpcionProducto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    PrecioInicial = table.Column<decimal>(nullable: false),
                    PrecioFinal = table.Column<decimal>(nullable: false),
                    Descuento = table.Column<string>(nullable: true),
                    ProductoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcionProducto_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpcionProducto_ProductoId",
                table: "OpcionProducto",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpcionProducto");

            migrationBuilder.DropColumn(
                name: "DescripcionEmpresa",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "InformaciónAdicional",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "LimiteProducto",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "OpcionProductoId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Reservas",
                table: "Producto");
        }
    }
}
