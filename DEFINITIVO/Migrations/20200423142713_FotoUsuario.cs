using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DEFINITIVO.Migrations
{
    public partial class FotoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoPerfil",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Usuario");

            migrationBuilder.AddColumn<byte[]>(
                name: "FotoUsuario",
                table: "Usuario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoUsuario",
                table: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "FotoPerfil",
                table: "Usuario",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Usuario",
                type: "longblob",
                nullable: true);
        }
    }
}
