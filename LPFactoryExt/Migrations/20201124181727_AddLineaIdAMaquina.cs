using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AddLineaIdAMaquina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maquinas_Secciones_SeccionId",
                table: "Maquinas");

            migrationBuilder.AlterColumn<int>(
                name: "SeccionId",
                table: "Maquinas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LineaId",
                table: "Maquinas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_LineaId",
                table: "Maquinas",
                column: "LineaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maquinas_Lineas_LineaId",
                table: "Maquinas",
                column: "LineaId",
                principalTable: "Lineas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maquinas_Secciones_SeccionId",
                table: "Maquinas",
                column: "SeccionId",
                principalTable: "Secciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maquinas_Lineas_LineaId",
                table: "Maquinas");

            migrationBuilder.DropForeignKey(
                name: "FK_Maquinas_Secciones_SeccionId",
                table: "Maquinas");

            migrationBuilder.DropIndex(
                name: "IX_Maquinas_LineaId",
                table: "Maquinas");

            migrationBuilder.DropColumn(
                name: "LineaId",
                table: "Maquinas");

            migrationBuilder.AlterColumn<int>(
                name: "SeccionId",
                table: "Maquinas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maquinas_Secciones_SeccionId",
                table: "Maquinas",
                column: "SeccionId",
                principalTable: "Secciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
