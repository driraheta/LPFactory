using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregaTipoAArticuloNoNulable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_ArticuloTipos_ArticuloTipoId",
                table: "Articulos");

            migrationBuilder.AlterColumn<int>(
                name: "ArticuloTipoId",
                table: "Articulos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_ArticuloTipos_ArticuloTipoId",
                table: "Articulos",
                column: "ArticuloTipoId",
                principalTable: "ArticuloTipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_ArticuloTipos_ArticuloTipoId",
                table: "Articulos");

            migrationBuilder.AlterColumn<int>(
                name: "ArticuloTipoId",
                table: "Articulos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_ArticuloTipos_ArticuloTipoId",
                table: "Articulos",
                column: "ArticuloTipoId",
                principalTable: "ArticuloTipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
