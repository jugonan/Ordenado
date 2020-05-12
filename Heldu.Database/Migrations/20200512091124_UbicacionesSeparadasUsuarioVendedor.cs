using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class UbicacionesSeparadasUsuarioVendedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ubicacion");

            migrationBuilder.CreateTable(
                name: "UbicacionUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Pais = table.Column<string>(nullable: true),
                    CCAA = table.Column<string>(nullable: true),
                    Provincia = table.Column<string>(nullable: true),
                    Poblacion = table.Column<string>(nullable: true),
                    CP = table.Column<string>(nullable: true),
                    Calle = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Letra = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UbicacionUsuario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UbicacionVendedor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Pais = table.Column<string>(nullable: true),
                    CCAA = table.Column<string>(nullable: true),
                    Provincia = table.Column<string>(nullable: true),
                    Poblacion = table.Column<string>(nullable: true),
                    CP = table.Column<string>(nullable: true),
                    Calle = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Letra = table.Column<string>(nullable: true),
                    VendedorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionVendedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UbicacionVendedor_Vendedor_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionUsuario_UsuarioId",
                table: "UbicacionUsuario",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionVendedor_VendedorId",
                table: "UbicacionVendedor",
                column: "VendedorId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UbicacionUsuario");

            migrationBuilder.DropTable(
                name: "UbicacionVendedor");

            migrationBuilder.CreateTable(
                name: "Ubicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CCAA = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CP = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Calle = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Letra = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Numero = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Pais = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Poblacion = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Provincia = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ubicacion_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ubicacion_Vendedor_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }
    }
}
