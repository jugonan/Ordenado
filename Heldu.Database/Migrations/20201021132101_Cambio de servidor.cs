using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class Cambiodeservidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "Vendedor",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(24) CHARACTER SET utf8mb4",
                oldMaxLength: 24);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "Vendedor",
                type: "varchar(24) CHARACTER SET utf8mb4",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
