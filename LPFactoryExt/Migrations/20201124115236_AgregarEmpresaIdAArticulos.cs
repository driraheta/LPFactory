using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregarEmpresaIdAArticulos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Articulos",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "ArticuloFamilias",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_EmpresaId",
                table: "Articulos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloFamilias_EmpresaId",
                table: "ArticuloFamilias",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloFamilias_Empresas_EmpresaId",
                table: "ArticuloFamilias",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Empresas_EmpresaId",
                table: "Articulos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloFamilias_Empresas_EmpresaId",
                table: "ArticuloFamilias");

            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Empresas_EmpresaId",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_EmpresaId",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_ArticuloFamilias_EmpresaId",
                table: "ArticuloFamilias");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "ArticuloFamilias");
        }
    }
}
