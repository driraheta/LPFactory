using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregaTipoAArticulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticuloTipoId",
                table: "Articulos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_ArticuloTipoId",
                table: "Articulos",
                column: "ArticuloTipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_ArticuloTipos_ArticuloTipoId",
                table: "Articulos",
                column: "ArticuloTipoId",
                principalTable: "ArticuloTipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_ArticuloTipos_ArticuloTipoId",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_ArticuloTipoId",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "ArticuloTipoId",
                table: "Articulos");
        }
    }
}
