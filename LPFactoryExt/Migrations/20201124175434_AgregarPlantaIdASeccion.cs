using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregarPlantaIdASeccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlantaId",
                table: "Secciones",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_PlantaId",
                table: "Secciones",
                column: "PlantaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Secciones_Plantas_PlantaId",
                table: "Secciones",
                column: "PlantaId",
                principalTable: "Plantas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secciones_Plantas_PlantaId",
                table: "Secciones");

            migrationBuilder.DropIndex(
                name: "IX_Secciones_PlantaId",
                table: "Secciones");

            migrationBuilder.DropColumn(
                name: "PlantaId",
                table: "Secciones");
        }
    }
}
