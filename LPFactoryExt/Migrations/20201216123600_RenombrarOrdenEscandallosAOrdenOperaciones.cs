using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class RenombrarOrdenEscandallosAOrdenOperaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenEscandallos_Articulos_ArticuloId",
                table: "OrdenEscandallos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenEscandallos_Empresas_EmpresaId",
                table: "OrdenEscandallos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenEscandallos_Fases_FaseId",
                table: "OrdenEscandallos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenEscandallos_Maquinas_MaquinaId",
                table: "OrdenEscandallos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenEscandallos_Operaciones_OperacionId",
                table: "OrdenEscandallos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenEscandallos_Operarios_OperarioId",
                table: "OrdenEscandallos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenEscandallos_Ordenes_OrdenId",
                table: "OrdenEscandallos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenEscandallos_Secciones_SeccionId",
                table: "OrdenEscandallos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdenEscandallos",
                table: "OrdenEscandallos");

            migrationBuilder.RenameTable(
                name: "OrdenEscandallos",
                newName: "OrdenOperaciones");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenEscandallos_SeccionId",
                table: "OrdenOperaciones",
                newName: "IX_OrdenOperaciones_SeccionId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenEscandallos_OrdenId",
                table: "OrdenOperaciones",
                newName: "IX_OrdenOperaciones_OrdenId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenEscandallos_OperarioId",
                table: "OrdenOperaciones",
                newName: "IX_OrdenOperaciones_OperarioId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenEscandallos_OperacionId",
                table: "OrdenOperaciones",
                newName: "IX_OrdenOperaciones_OperacionId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenEscandallos_MaquinaId",
                table: "OrdenOperaciones",
                newName: "IX_OrdenOperaciones_MaquinaId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenEscandallos_FaseId",
                table: "OrdenOperaciones",
                newName: "IX_OrdenOperaciones_FaseId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenEscandallos_EmpresaId",
                table: "OrdenOperaciones",
                newName: "IX_OrdenOperaciones_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenEscandallos_ArticuloId",
                table: "OrdenOperaciones",
                newName: "IX_OrdenOperaciones_ArticuloId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdenOperaciones",
                table: "OrdenOperaciones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenOperaciones_Articulos_ArticuloId",
                table: "OrdenOperaciones",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenOperaciones_Empresas_EmpresaId",
                table: "OrdenOperaciones",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenOperaciones_Fases_FaseId",
                table: "OrdenOperaciones",
                column: "FaseId",
                principalTable: "Fases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenOperaciones_Maquinas_MaquinaId",
                table: "OrdenOperaciones",
                column: "MaquinaId",
                principalTable: "Maquinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenOperaciones_Operaciones_OperacionId",
                table: "OrdenOperaciones",
                column: "OperacionId",
                principalTable: "Operaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenOperaciones_Operarios_OperarioId",
                table: "OrdenOperaciones",
                column: "OperarioId",
                principalTable: "Operarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenOperaciones_Ordenes_OrdenId",
                table: "OrdenOperaciones",
                column: "OrdenId",
                principalTable: "Ordenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenOperaciones_Secciones_SeccionId",
                table: "OrdenOperaciones",
                column: "SeccionId",
                principalTable: "Secciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenOperaciones_Articulos_ArticuloId",
                table: "OrdenOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenOperaciones_Empresas_EmpresaId",
                table: "OrdenOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenOperaciones_Fases_FaseId",
                table: "OrdenOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenOperaciones_Maquinas_MaquinaId",
                table: "OrdenOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenOperaciones_Operaciones_OperacionId",
                table: "OrdenOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenOperaciones_Operarios_OperarioId",
                table: "OrdenOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenOperaciones_Ordenes_OrdenId",
                table: "OrdenOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenOperaciones_Secciones_SeccionId",
                table: "OrdenOperaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdenOperaciones",
                table: "OrdenOperaciones");

            migrationBuilder.RenameTable(
                name: "OrdenOperaciones",
                newName: "OrdenEscandallos");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenOperaciones_SeccionId",
                table: "OrdenEscandallos",
                newName: "IX_OrdenEscandallos_SeccionId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenOperaciones_OrdenId",
                table: "OrdenEscandallos",
                newName: "IX_OrdenEscandallos_OrdenId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenOperaciones_OperarioId",
                table: "OrdenEscandallos",
                newName: "IX_OrdenEscandallos_OperarioId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenOperaciones_OperacionId",
                table: "OrdenEscandallos",
                newName: "IX_OrdenEscandallos_OperacionId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenOperaciones_MaquinaId",
                table: "OrdenEscandallos",
                newName: "IX_OrdenEscandallos_MaquinaId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenOperaciones_FaseId",
                table: "OrdenEscandallos",
                newName: "IX_OrdenEscandallos_FaseId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenOperaciones_EmpresaId",
                table: "OrdenEscandallos",
                newName: "IX_OrdenEscandallos_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenOperaciones_ArticuloId",
                table: "OrdenEscandallos",
                newName: "IX_OrdenEscandallos_ArticuloId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdenEscandallos",
                table: "OrdenEscandallos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenEscandallos_Articulos_ArticuloId",
                table: "OrdenEscandallos",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenEscandallos_Empresas_EmpresaId",
                table: "OrdenEscandallos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenEscandallos_Fases_FaseId",
                table: "OrdenEscandallos",
                column: "FaseId",
                principalTable: "Fases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenEscandallos_Maquinas_MaquinaId",
                table: "OrdenEscandallos",
                column: "MaquinaId",
                principalTable: "Maquinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenEscandallos_Operaciones_OperacionId",
                table: "OrdenEscandallos",
                column: "OperacionId",
                principalTable: "Operaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenEscandallos_Operarios_OperarioId",
                table: "OrdenEscandallos",
                column: "OperarioId",
                principalTable: "Operarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenEscandallos_Ordenes_OrdenId",
                table: "OrdenEscandallos",
                column: "OrdenId",
                principalTable: "Ordenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenEscandallos_Secciones_SeccionId",
                table: "OrdenEscandallos",
                column: "SeccionId",
                principalTable: "Secciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
