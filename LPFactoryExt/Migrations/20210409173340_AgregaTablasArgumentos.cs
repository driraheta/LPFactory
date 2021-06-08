using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregaTablasArgumentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Argumentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Argumentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Argumentos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ArgumentoValores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    ArgumentoId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArgumentoValores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArgumentoValores_Argumentos_ArgumentoId",
                        column: x => x.ArgumentoId,
                        principalTable: "Argumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ArgumentoValores_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TiemposCambio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    ArgumentoId = table.Column<int>(nullable: false),
                    ValorActualId = table.Column<int>(nullable: false),
                    ValorSiguienteId = table.Column<int>(nullable: false),
                    Tiempo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiemposCambio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiemposCambio_Argumentos_ArgumentoId",
                        column: x => x.ArgumentoId,
                        principalTable: "Argumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TiemposCambio_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TiemposCambio_ArgumentoValores_ValorActualId",
                        column: x => x.ValorActualId,
                        principalTable: "ArgumentoValores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TiemposCambio_ArgumentoValores_ValorSiguienteId",
                        column: x => x.ValorSiguienteId,
                        principalTable: "ArgumentoValores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Argumentos_EmpresaId",
                table: "Argumentos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ArgumentoValores_ArgumentoId",
                table: "ArgumentoValores",
                column: "ArgumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArgumentoValores_EmpresaId",
                table: "ArgumentoValores",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_TiemposCambio_ArgumentoId",
                table: "TiemposCambio",
                column: "ArgumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiemposCambio_EmpresaId",
                table: "TiemposCambio",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_TiemposCambio_ValorActualId",
                table: "TiemposCambio",
                column: "ValorActualId");

            migrationBuilder.CreateIndex(
                name: "IX_TiemposCambio_ValorSiguienteId",
                table: "TiemposCambio",
                column: "ValorSiguienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiemposCambio");

            migrationBuilder.DropTable(
                name: "ArgumentoValores");

            migrationBuilder.DropTable(
                name: "Argumentos");
        }
    }
}
