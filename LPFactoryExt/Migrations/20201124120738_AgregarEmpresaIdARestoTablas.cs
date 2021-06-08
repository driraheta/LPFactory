using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregarEmpresaIdARestoTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Ubicaciones",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Secciones",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "OrdenMateriales",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "OrdenEscandallos",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Ordenes",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Operarios",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Operaciones",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Maquinas",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "IncidenciaTipos",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Incidencias",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Fases",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_EmpresaId",
                table: "Ubicaciones",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_EmpresaId",
                table: "Secciones",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenMateriales_EmpresaId",
                table: "OrdenMateriales",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenEscandallos_EmpresaId",
                table: "OrdenEscandallos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_EmpresaId",
                table: "Ordenes",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Operarios_EmpresaId",
                table: "Operarios",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_EmpresaId",
                table: "Operaciones",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_EmpresaId",
                table: "Maquinas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidenciaTipos_EmpresaId",
                table: "IncidenciaTipos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_EmpresaId",
                table: "Incidencias",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Fases_EmpresaId",
                table: "Fases",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fases_Empresas_EmpresaId",
                table: "Fases",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidencias_Empresas_EmpresaId",
                table: "Incidencias",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_IncidenciaTipos_Empresas_EmpresaId",
                table: "IncidenciaTipos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Maquinas_Empresas_EmpresaId",
                table: "Maquinas",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Operaciones_Empresas_EmpresaId",
                table: "Operaciones",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Operarios_Empresas_EmpresaId",
                table: "Operarios",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Empresas_EmpresaId",
                table: "Ordenes",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenEscandallos_Empresas_EmpresaId",
                table: "OrdenEscandallos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenMateriales_Empresas_EmpresaId",
                table: "OrdenMateriales",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Secciones_Empresas_EmpresaId",
                table: "Secciones",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Ubicaciones_Empresas_EmpresaId",
                table: "Ubicaciones",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fases_Empresas_EmpresaId",
                table: "Fases");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidencias_Empresas_EmpresaId",
                table: "Incidencias");

            migrationBuilder.DropForeignKey(
                name: "FK_IncidenciaTipos_Empresas_EmpresaId",
                table: "IncidenciaTipos");

            migrationBuilder.DropForeignKey(
                name: "FK_Maquinas_Empresas_EmpresaId",
                table: "Maquinas");

            migrationBuilder.DropForeignKey(
                name: "FK_Operaciones_Empresas_EmpresaId",
                table: "Operaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Operarios_Empresas_EmpresaId",
                table: "Operarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Empresas_EmpresaId",
                table: "Ordenes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenEscandallos_Empresas_EmpresaId",
                table: "OrdenEscandallos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenMateriales_Empresas_EmpresaId",
                table: "OrdenMateriales");

            migrationBuilder.DropForeignKey(
                name: "FK_Secciones_Empresas_EmpresaId",
                table: "Secciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Ubicaciones_Empresas_EmpresaId",
                table: "Ubicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Ubicaciones_EmpresaId",
                table: "Ubicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Secciones_EmpresaId",
                table: "Secciones");

            migrationBuilder.DropIndex(
                name: "IX_OrdenMateriales_EmpresaId",
                table: "OrdenMateriales");

            migrationBuilder.DropIndex(
                name: "IX_OrdenEscandallos_EmpresaId",
                table: "OrdenEscandallos");

            migrationBuilder.DropIndex(
                name: "IX_Ordenes_EmpresaId",
                table: "Ordenes");

            migrationBuilder.DropIndex(
                name: "IX_Operarios_EmpresaId",
                table: "Operarios");

            migrationBuilder.DropIndex(
                name: "IX_Operaciones_EmpresaId",
                table: "Operaciones");

            migrationBuilder.DropIndex(
                name: "IX_Maquinas_EmpresaId",
                table: "Maquinas");

            migrationBuilder.DropIndex(
                name: "IX_IncidenciaTipos_EmpresaId",
                table: "IncidenciaTipos");

            migrationBuilder.DropIndex(
                name: "IX_Incidencias_EmpresaId",
                table: "Incidencias");

            migrationBuilder.DropIndex(
                name: "IX_Fases_EmpresaId",
                table: "Fases");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Ubicaciones");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Secciones");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "OrdenMateriales");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "OrdenEscandallos");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Operarios");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Operaciones");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Maquinas");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "IncidenciaTipos");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Incidencias");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Fases");
        }
    }
}
