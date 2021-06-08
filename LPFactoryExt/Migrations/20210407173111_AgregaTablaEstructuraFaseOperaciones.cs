using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregaTablaEstructuraFaseOperaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstructuraFaseOperaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    EstructuraFaseId = table.Column<int>(nullable: false),
                    MaquinaId = table.Column<int>(nullable: false),
                    TiempoPreparacion = table.Column<int>(nullable: false),
                    TiempoProduccion = table.Column<int>(nullable: false),
                    MermasTeoricas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstructuraFaseOperaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstructuraFaseOperaciones_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstructuraFaseOperaciones_EstructurasFases_EstructuraFaseId",
                        column: x => x.EstructuraFaseId,
                        principalTable: "EstructurasFases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstructuraFaseOperaciones_Maquinas_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstructuraFaseOperaciones_EmpresaId",
                table: "EstructuraFaseOperaciones",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstructuraFaseOperaciones_EstructuraFaseId",
                table: "EstructuraFaseOperaciones",
                column: "EstructuraFaseId");

            migrationBuilder.CreateIndex(
                name: "IX_EstructuraFaseOperaciones_MaquinaId",
                table: "EstructuraFaseOperaciones",
                column: "MaquinaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstructuraFaseOperaciones");
        }
    }
}
