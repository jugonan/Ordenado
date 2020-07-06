using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class TablaVendedorIBANyCIF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CIF",
                table: "Vendedor",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "IBAN",
                table: "Vendedor",
                maxLength: 24,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CIF",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "IBAN",
                table: "Vendedor");
        }
    }
}
