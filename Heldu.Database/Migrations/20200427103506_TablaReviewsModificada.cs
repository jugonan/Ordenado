using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class TablaReviewsModificada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "Review",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProductoId",
                table: "Review",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UsuarioId1",
                table: "Review",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Producto_ProductoId",
                table: "Review",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Usuario_UsuarioId1",
                table: "Review",
                column: "UsuarioId1",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Producto_ProductoId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Usuario_UsuarioId1",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_ProductoId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_UsuarioId1",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Review");
        }
    }
}
