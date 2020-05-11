using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class NuevaMigracionUbicacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Usuario_Ubicacion_UbicacionId",
            //    table: "Usuario");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Vendedor_Ubicacion_UbicacionId",
            //    table: "Vendedor");

            //migrationBuilder.DropIndex(
            //    name: "IX_Vendedor_UbicacionId",
            //    table: "Vendedor");

            //migrationBuilder.DropIndex(
            //    name: "IX_Usuario_UbicacionId",
            //    table: "Usuario");

            //migrationBuilder.DropColumn(
            //    name: "UbicacionId",
            //    table: "Vendedor");

            //migrationBuilder.DropColumn(
            //    name: "UbicacionId",
            //    table: "Usuario");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Ubicacion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VendedorId",
                table: "Ubicacion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_UsuarioId",
                table: "Ubicacion",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_VendedorId",
                table: "Ubicacion",
                column: "VendedorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ubicacion_Usuario_UsuarioId",
                table: "Ubicacion",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ubicacion_Vendedor_VendedorId",
                table: "Ubicacion",
                column: "VendedorId",
                principalTable: "Vendedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ubicacion_Usuario_UsuarioId",
                table: "Ubicacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Ubicacion_Vendedor_VendedorId",
                table: "Ubicacion");

            migrationBuilder.DropIndex(
                name: "IX_Ubicacion_UsuarioId",
                table: "Ubicacion");

            migrationBuilder.DropIndex(
                name: "IX_Ubicacion_VendedorId",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "Ubicacion");

            migrationBuilder.AddColumn<int>(
                name: "UbicacionId",
                table: "Vendedor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UbicacionId",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendedor_UbicacionId",
                table: "Vendedor",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UbicacionId",
                table: "Usuario",
                column: "UbicacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Ubicacion_UbicacionId",
                table: "Usuario",
                column: "UbicacionId",
                principalTable: "Ubicacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendedor_Ubicacion_UbicacionId",
                table: "Vendedor",
                column: "UbicacionId",
                principalTable: "Ubicacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
