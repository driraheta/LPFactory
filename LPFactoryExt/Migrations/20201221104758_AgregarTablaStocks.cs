using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregarTablaStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    AlmacenId = table.Column<int>(nullable: false),
                    CodigoAlmacen = table.Column<string>(nullable: true),
                    DescripcionAlmacen = table.Column<string>(nullable: true),
                    UbicacionId = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    X = table.Column<string>(nullable: true),
                    Y = table.Column<string>(nullable: true),
                    Z = table.Column<string>(nullable: true),
                    ArticuloId = table.Column<int>(nullable: false),
                    CodigoArticulo = table.Column<string>(nullable: true),
                    DescripcionArticulo = table.Column<string>(nullable: true),
                    LoteNumeroSerie = table.Column<string>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Almacenes_AlmacenId",
                        column: x => x.AlmacenId,
                        principalTable: "Almacenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Stocks_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Stocks_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Stocks_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_AlmacenId",
                table: "Stocks",
                column: "AlmacenId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ArticuloId",
                table: "Stocks",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_EmpresaId",
                table: "Stocks",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_UbicacionId",
                table: "Stocks",
                column: "UbicacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
