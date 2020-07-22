using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class AddOpcionProductoInVisitaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpcionProductoId",
                table: "Visita",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Visita_OpcionProductoId",
                table: "Visita",
                column: "OpcionProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_OpcionProducto_OpcionProductoId",
                table: "Visita",
                column: "OpcionProductoId",
                principalTable: "OpcionProducto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visita_OpcionProducto_OpcionProductoId",
                table: "Visita");

            migrationBuilder.DropIndex(
                name: "IX_Visita_OpcionProductoId",
                table: "Visita");

            migrationBuilder.DropColumn(
                name: "OpcionProductoId",
                table: "Visita");
        }
    }
}
