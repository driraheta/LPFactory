using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class RenameTableEstructurasFasesMateriales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstructuraFaseOperaciones_Empresas_EmpresaId",
                table: "EstructuraFaseOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_EstructuraFaseOperaciones_EstructurasFases_EstructuraFaseId",
                table: "EstructuraFaseOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_EstructuraFaseOperaciones_Maquinas_MaquinaId",
                table: "EstructuraFaseOperaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstructuraFaseOperaciones",
                table: "EstructuraFaseOperaciones");

            migrationBuilder.RenameTable(
                name: "EstructuraFaseOperaciones",
                newName: "EstructurasFasesOperaciones");

            migrationBuilder.RenameIndex(
                name: "IX_EstructuraFaseOperaciones_MaquinaId",
                table: "EstructurasFasesOperaciones",
                newName: "IX_EstructurasFasesOperaciones_MaquinaId");

            migrationBuilder.RenameIndex(
                name: "IX_EstructuraFaseOperaciones_EstructuraFaseId",
                table: "EstructurasFasesOperaciones",
                newName: "IX_EstructurasFasesOperaciones_EstructuraFaseId");

            migrationBuilder.RenameIndex(
                name: "IX_EstructuraFaseOperaciones_EmpresaId",
                table: "EstructurasFasesOperaciones",
                newName: "IX_EstructurasFasesOperaciones_EmpresaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstructurasFasesOperaciones",
                table: "EstructurasFasesOperaciones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EstructurasFasesOperaciones_Empresas_EmpresaId",
                table: "EstructurasFasesOperaciones",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstructurasFasesOperaciones_EstructurasFases_EstructuraFaseId",
                table: "EstructurasFasesOperaciones",
                column: "EstructuraFaseId",
                principalTable: "EstructurasFases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstructurasFasesOperaciones_Maquinas_MaquinaId",
                table: "EstructurasFasesOperaciones",
                column: "MaquinaId",
                principalTable: "Maquinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstructurasFasesOperaciones_Empresas_EmpresaId",
                table: "EstructurasFasesOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_EstructurasFasesOperaciones_EstructurasFases_EstructuraFaseId",
                table: "EstructurasFasesOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_EstructurasFasesOperaciones_Maquinas_MaquinaId",
                table: "EstructurasFasesOperaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstructurasFasesOperaciones",
                table: "EstructurasFasesOperaciones");

            migrationBuilder.RenameTable(
                name: "EstructurasFasesOperaciones",
                newName: "EstructuraFaseOperaciones");

            migrationBuilder.RenameIndex(
                name: "IX_EstructurasFasesOperaciones_MaquinaId",
                table: "EstructuraFaseOperaciones",
                newName: "IX_EstructuraFaseOperaciones_MaquinaId");

            migrationBuilder.RenameIndex(
                name: "IX_EstructurasFasesOperaciones_EstructuraFaseId",
                table: "EstructuraFaseOperaciones",
                newName: "IX_EstructuraFaseOperaciones_EstructuraFaseId");

            migrationBuilder.RenameIndex(
                name: "IX_EstructurasFasesOperaciones_EmpresaId",
                table: "EstructuraFaseOperaciones",
                newName: "IX_EstructuraFaseOperaciones_EmpresaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstructuraFaseOperaciones",
                table: "EstructuraFaseOperaciones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EstructuraFaseOperaciones_Empresas_EmpresaId",
                table: "EstructuraFaseOperaciones",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstructuraFaseOperaciones_EstructurasFases_EstructuraFaseId",
                table: "EstructuraFaseOperaciones",
                column: "EstructuraFaseId",
                principalTable: "EstructurasFases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstructuraFaseOperaciones_Maquinas_MaquinaId",
                table: "EstructuraFaseOperaciones",
                column: "MaquinaId",
                principalTable: "Maquinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
