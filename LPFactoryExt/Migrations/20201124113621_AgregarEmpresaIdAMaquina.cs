using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregarEmpresaIdAMaquina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Almacenes",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Almacenes_EmpresaId",
                table: "Almacenes",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Almacenes_Empresas_EmpresaId",
                table: "Almacenes",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Almacenes_Empresas_EmpresaId",
                table: "Almacenes");

            migrationBuilder.DropIndex(
                name: "IX_Almacenes_EmpresaId",
                table: "Almacenes");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Almacenes");
        }
    }
}
