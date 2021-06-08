using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregarTablasOrdenes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_FamiliasArticulos_FamiliaArticuloId",
                table: "Articulos");

            migrationBuilder.DropTable(
                name: "FamiliasArticulos");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_FamiliaArticuloId",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "FamiliaArticuloId",
                table: "Articulos");

            migrationBuilder.AddColumn<int>(
                name: "ArticuloFamiliaId",
                table: "Articulos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArticuloFamilias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(nullable: false),
                    Actividad = table.Column<int>(nullable: false),
                    Activo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticuloFamilias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Entrada = table.Column<bool>(nullable: false),
                    OtAbierta = table.Column<bool>(nullable: false),
                    SeccionId = table.Column<int>(nullable: false),
                    HoraEntrada1 = table.Column<int>(nullable: false),
                    MinutoEntrada1 = table.Column<int>(nullable: false),
                    HoraSalida1 = table.Column<int>(nullable: false),
                    MinutoSalida1 = table.Column<int>(nullable: false),
                    HoraEntrada2 = table.Column<int>(nullable: false),
                    MinutoEntrada2 = table.Column<int>(nullable: false),
                    HoraSalida2 = table.Column<int>(nullable: false),
                    MinutoSalida2 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operarios_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(nullable: false),
                    ArticuloId = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Prioridad = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    MermasTeoricas = table.Column<int>(nullable: false),
                    MermasReales = table.Column<int>(nullable: false),
                    UbicacionDestinoId = table.Column<int>(nullable: false),
                    Producido = table.Column<int>(nullable: false),
                    Pendiente = table.Column<int>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenes_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Ordenes_Ubicaciones_UbicacionDestinoId",
                        column: x => x.UbicacionDestinoId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OrdenEscandallos",
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
                    ArticuloId = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    TiempoPreparacion = table.Column<int>(nullable: false),
                    TiempoProduccion = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    MermasTeoricas = table.Column<int>(nullable: false),
                    OperarioId = table.Column<int>(nullable: false),
                    TiempoPreparacionReal = table.Column<int>(nullable: false),
                    TiempoProduccionReal = table.Column<int>(nullable: false),
                    CantidadProducida = table.Column<int>(nullable: false),
                    MermasReales = table.Column<int>(nullable: false),
                    TiempoIncidencias = table.Column<int>(nullable: false),
                    NumeroIncidencias = table.Column<int>(nullable: false),
                    TiempoMicroparadas = table.Column<int>(nullable: false),
                    NumeroMicroparadas = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenEscandallos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenEscandallos_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenEscandallos_Fases_FaseId",
                        column: x => x.FaseId,
                        principalTable: "Fases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenEscandallos_Maquinas_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenEscandallos_Operaciones_OperacionId",
                        column: x => x.OperacionId,
                        principalTable: "Operaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenEscandallos_Operarios_OperarioId",
                        column: x => x.OperarioId,
                        principalTable: "Operarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenEscandallos_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenEscandallos_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OrdenMateriales",
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
                    Descripcion = table.Column<string>(nullable: true),
                    Lote = table.Column<string>(nullable: true),
                    UbicacionOrigenId = table.Column<int>(nullable: false),
                    UbicacionDestinoId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    CantidadReal = table.Column<int>(nullable: false),
                    MermasTeoricas = table.Column<int>(nullable: false),
                    MermasReales = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenMateriales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenMateriales_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenMateriales_Fases_FaseId",
                        column: x => x.FaseId,
                        principalTable: "Fases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenMateriales_Maquinas_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenMateriales_Operaciones_OperacionId",
                        column: x => x.OperacionId,
                        principalTable: "Operaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenMateriales_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenMateriales_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenMateriales_Ubicaciones_UbicacionDestinoId",
                        column: x => x.UbicacionDestinoId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdenMateriales_Ubicaciones_UbicacionOrigenId",
                        column: x => x.UbicacionOrigenId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_ArticuloFamiliaId",
                table: "Articulos",
                column: "ArticuloFamiliaId");

            migrationBuilder.CreateIndex(
                name: "IX_Operarios_SeccionId",
                table: "Operarios",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ArticuloId",
                table: "Ordenes",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_UbicacionDestinoId",
                table: "Ordenes",
                column: "UbicacionDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenEscandallos_ArticuloId",
                table: "OrdenEscandallos",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenEscandallos_FaseId",
                table: "OrdenEscandallos",
                column: "FaseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenEscandallos_MaquinaId",
                table: "OrdenEscandallos",
                column: "MaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenEscandallos_OperacionId",
                table: "OrdenEscandallos",
                column: "OperacionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenEscandallos_OperarioId",
                table: "OrdenEscandallos",
                column: "OperarioId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenEscandallos_OrdenId",
                table: "OrdenEscandallos",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenEscandallos_SeccionId",
                table: "OrdenEscandallos",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenMateriales_ArticuloId",
                table: "OrdenMateriales",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenMateriales_FaseId",
                table: "OrdenMateriales",
                column: "FaseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenMateriales_MaquinaId",
                table: "OrdenMateriales",
                column: "MaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenMateriales_OperacionId",
                table: "OrdenMateriales",
                column: "OperacionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenMateriales_OrdenId",
                table: "OrdenMateriales",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenMateriales_SeccionId",
                table: "OrdenMateriales",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenMateriales_UbicacionDestinoId",
                table: "OrdenMateriales",
                column: "UbicacionDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenMateriales_UbicacionOrigenId",
                table: "OrdenMateriales",
                column: "UbicacionOrigenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_ArticuloFamilias_ArticuloFamiliaId",
                table: "Articulos",
                column: "ArticuloFamiliaId",
                principalTable: "ArticuloFamilias",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_ArticuloFamilias_ArticuloFamiliaId",
                table: "Articulos");

            migrationBuilder.DropTable(
                name: "ArticuloFamilias");

            migrationBuilder.DropTable(
                name: "OrdenEscandallos");

            migrationBuilder.DropTable(
                name: "OrdenMateriales");

            migrationBuilder.DropTable(
                name: "Operarios");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_ArticuloFamiliaId",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "ArticuloFamiliaId",
                table: "Articulos");

            migrationBuilder.AddColumn<int>(
                name: "FamiliaArticuloId",
                table: "Articulos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FamiliasArticulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Actividad = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamiliasArticulos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_FamiliaArticuloId",
                table: "Articulos",
                column: "FamiliaArticuloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_FamiliasArticulos_FamiliaArticuloId",
                table: "Articulos",
                column: "FamiliaArticuloId",
                principalTable: "FamiliasArticulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
