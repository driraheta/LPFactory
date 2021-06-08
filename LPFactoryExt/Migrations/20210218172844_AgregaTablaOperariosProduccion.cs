using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregaTablaOperariosProduccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperariosProduccion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperarioId = table.Column<int>(nullable: false),
                    LineaId = table.Column<int>(nullable: true),
                    MaquinaId = table.Column<int>(nullable: true),
                    PuestoId = table.Column<int>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    TiempoProduccionIni = table.Column<int>(nullable: false),
                    TiempoPreparacionIni = table.Column<int>(nullable: false),
                    TiempoIncidenciaIni = table.Column<int>(nullable: false),
                    TiempoMicroparadasIni = table.Column<int>(nullable: false),
                    NumeroIncidenciasIni = table.Column<int>(nullable: false),
                    NumeroMicroparadasIni = table.Column<int>(nullable: false),
                    CantidadIni = table.Column<int>(nullable: false),
                    MermasIni = table.Column<int>(nullable: false),
                    TiempoProduccionFin = table.Column<int>(nullable: false),
                    TiempoPreparacionFin = table.Column<int>(nullable: false),
                    TiempoIncidenciasFin = table.Column<int>(nullable: false),
                    TiempoMicroparadasFin = table.Column<int>(nullable: false),
                    NumeroIncidenciasFin = table.Column<int>(nullable: false),
                    NumeroMicroparadas = table.Column<int>(nullable: false),
                    CantidadFin = table.Column<int>(nullable: false),
                    MermasFin = table.Column<int>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperariosProduccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperariosProduccion_Lineas_LineaId",
                        column: x => x.LineaId,
                        principalTable: "Lineas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OperariosProduccion_Maquinas_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OperariosProduccion_Operarios_OperarioId",
                        column: x => x.OperarioId,
                        principalTable: "Operarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperariosProduccion_Puestos_PuestoId",
                        column: x => x.PuestoId,
                        principalTable: "Puestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperariosProduccion_LineaId",
                table: "OperariosProduccion",
                column: "LineaId");

            migrationBuilder.CreateIndex(
                name: "IX_OperariosProduccion_MaquinaId",
                table: "OperariosProduccion",
                column: "MaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_OperariosProduccion_OperarioId",
                table: "OperariosProduccion",
                column: "OperarioId");

            migrationBuilder.CreateIndex(
                name: "IX_OperariosProduccion_PuestoId",
                table: "OperariosProduccion",
                column: "PuestoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperariosProduccion");
        }
    }
}
