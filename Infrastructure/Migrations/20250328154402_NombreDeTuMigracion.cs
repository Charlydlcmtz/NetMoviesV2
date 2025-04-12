using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPeliculas.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeTuMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_Categorias_categoriaId",
                table: "Peliculas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "categoriaId",
                table: "Peliculas",
                newName: "CategoriaId");

            migrationBuilder.RenameColumn(
                name: "Clasificacion",
                table: "Peliculas",
                newName: "TipoClasificacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Peliculas_categoriaId",
                table: "Peliculas",
                newName: "IX_Peliculas_CategoriaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Peliculas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Peliculas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TipoClasificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoClasificaciones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_TipoClasificacionId",
                table: "Peliculas",
                column: "TipoClasificacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_Categorias_CategoriaId",
                table: "Peliculas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_TipoClasificaciones_TipoClasificacionId",
                table: "Peliculas",
                column: "TipoClasificacionId",
                principalTable: "TipoClasificaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_Categorias_CategoriaId",
                table: "Peliculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_TipoClasificaciones_TipoClasificacionId",
                table: "Peliculas");

            migrationBuilder.DropTable(
                name: "TipoClasificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Peliculas_TipoClasificacionId",
                table: "Peliculas");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Peliculas",
                newName: "categoriaId");

            migrationBuilder.RenameColumn(
                name: "TipoClasificacionId",
                table: "Peliculas",
                newName: "Clasificacion");

            migrationBuilder.RenameIndex(
                name: "IX_Peliculas_CategoriaId",
                table: "Peliculas",
                newName: "IX_Peliculas_categoriaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Peliculas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Peliculas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_Categorias_categoriaId",
                table: "Peliculas",
                column: "categoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
