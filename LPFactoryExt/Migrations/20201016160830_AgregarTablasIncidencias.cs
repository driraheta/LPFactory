using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregarTablasIncidencias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncidenciaTipos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidenciaTipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenId = table.Column<int>(nullable: false),
                    SeccionId = table.Column<int>(nullable: false),
                    FaseId = table.Column<int>(nullable: false),
                    OperacionId = table.Column<int>(nullable: false),
                    MaquinaId = table.Column<int>(nullable: false),
                    Secuencia = table.Column<int>(nullable: false),
                    NumeroLinea = table.Column<int>(nullable: false),
                    ArticuloId = table.Column<int>(nullable: false),
                    IncidenciaTipoId = table.Column<int>(nullable: false),
                    Tiempo = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidencias_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Incidencias_Fases_FaseId",
                        column: x => x.FaseId,
                        principalTable: "Fases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Incidencias_IncidenciaTipos_IncidenciaTipoId",
                        column: x => x.IncidenciaTipoId,
                        principalTable: "IncidenciaTipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Incidencias_Maquinas_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Incidencias_Operaciones_OperacionId",
                        column: x => x.OperacionId,
                        principalTable: "Operaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Incidencias_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Incidencias_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_ArticuloId",
                table: "Incidencias",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_FaseId",
                table: "Incidencias",
                column: "FaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_IncidenciaTipoId",
                table: "Incidencias",
                column: "IncidenciaTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_MaquinaId",
                table: "Incidencias",
                column: "MaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_OperacionId",
                table: "Incidencias",
                column: "OperacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_OrdenId",
                table: "Incidencias",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_SeccionId",
                table: "Incidencias",
                column: "SeccionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropTable(
                name: "IncidenciaTipos");
        }
    }
}
