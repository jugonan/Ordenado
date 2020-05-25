using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class CodicionesDentroDeProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Condicion");

            migrationBuilder.AddColumn<string>(
                name: "Condiciones",
                table: "Producto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condiciones",
                table: "Producto");

            migrationBuilder.CreateTable(
                name: "Condicion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Entrega = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Horario = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Recogida = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Reserva = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condicion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Condicion_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Condicion_ProductoId",
                table: "Condicion",
                column: "ProductoId");
        }
    }
}
