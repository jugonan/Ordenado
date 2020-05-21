using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class ModificacionCondicionesProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Condicion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Condicion_ProductoId",
                table: "Condicion",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Condicion_Producto_ProductoId",
                table: "Condicion",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Condicion_Producto_ProductoId",
                table: "Condicion");

            migrationBuilder.DropIndex(
                name: "IX_Condicion_ProductoId",
                table: "Condicion");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Condicion");
        }
    }
}
