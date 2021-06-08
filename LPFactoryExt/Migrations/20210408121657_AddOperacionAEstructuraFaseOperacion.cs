using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AddOperacionAEstructuraFaseOperacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OperacionId",
                table: "EstructurasFasesOperaciones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EstructurasFasesOperaciones_OperacionId",
                table: "EstructurasFasesOperaciones",
                column: "OperacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstructurasFasesOperaciones_Operaciones_OperacionId",
                table: "EstructurasFasesOperaciones",
                column: "OperacionId",
                principalTable: "Operaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstructurasFasesOperaciones_Operaciones_OperacionId",
                table: "EstructurasFasesOperaciones");

            migrationBuilder.DropIndex(
                name: "IX_EstructurasFasesOperaciones_OperacionId",
                table: "EstructurasFasesOperaciones");

            migrationBuilder.DropColumn(
                name: "OperacionId",
                table: "EstructurasFasesOperaciones");
        }
    }
}
