using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregarTablasEstructura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estructuras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    ArticuloId = table.Column<int>(nullable: false),
                    Version = table.Column<string>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false),
                    Predeterminada = table.Column<bool>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estructuras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estructuras_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Estructuras_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "EstructurasFases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    EstructuraId = table.Column<int>(nullable: false),
                    FaseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstructurasFases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstructurasFases_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EstructurasFases_Estructuras_EstructuraId",
                        column: x => x.EstructuraId,
                        principalTable: "Estructuras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EstructurasFases_Fases_FaseId",
                        column: x => x.FaseId,
                        principalTable: "Fases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "EstructurasFasesMateriales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    EstructuraFaseId = table.Column<int>(nullable: false),
                    ArticuloId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstructurasFasesMateriales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstructurasFasesMateriales_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EstructurasFasesMateriales_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EstructurasFasesMateriales_EstructurasFases_EstructuraFaseId",
                        column: x => x.EstructuraFaseId,
                        principalTable: "EstructurasFases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estructuras_ArticuloId",
                table: "Estructuras",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Estructuras_EmpresaId",
                table: "Estructuras",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstructurasFases_EmpresaId",
                table: "EstructurasFases",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstructurasFases_EstructuraId",
                table: "EstructurasFases",
                column: "EstructuraId");

            migrationBuilder.CreateIndex(
                name: "IX_EstructurasFases_FaseId",
                table: "EstructurasFases",
                column: "FaseId");

            migrationBuilder.CreateIndex(
                name: "IX_EstructurasFasesMateriales_ArticuloId",
                table: "EstructurasFasesMateriales",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_EstructurasFasesMateriales_EmpresaId",
                table: "EstructurasFasesMateriales",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstructurasFasesMateriales_EstructuraFaseId",
                table: "EstructurasFasesMateriales",
                column: "EstructuraFaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstructurasFasesMateriales");

            migrationBuilder.DropTable(
                name: "EstructurasFases");

            migrationBuilder.DropTable(
                name: "Estructuras");
        }
    }
}
