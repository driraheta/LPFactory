using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class ModificacionTablaOperariosProduccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroMicroparadas",
                table: "OperariosProduccion");

            migrationBuilder.AlterColumn<int>(
                name: "TiempoProduccionFin",
                table: "OperariosProduccion",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TiempoPreparacionFin",
                table: "OperariosProduccion",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TiempoMicroparadasFin",
                table: "OperariosProduccion",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TiempoIncidenciasFin",
                table: "OperariosProduccion",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NumeroIncidenciasFin",
                table: "OperariosProduccion",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MermasFin",
                table: "OperariosProduccion",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CantidadFin",
                table: "OperariosProduccion",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NumeroMicroparadasFin",
                table: "OperariosProduccion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroMicroparadasFin",
                table: "OperariosProduccion");

            migrationBuilder.AlterColumn<int>(
                name: "TiempoProduccionFin",
                table: "OperariosProduccion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TiempoPreparacionFin",
                table: "OperariosProduccion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TiempoMicroparadasFin",
                table: "OperariosProduccion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TiempoIncidenciasFin",
                table: "OperariosProduccion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NumeroIncidenciasFin",
                table: "OperariosProduccion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MermasFin",
                table: "OperariosProduccion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CantidadFin",
                table: "OperariosProduccion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroMicroparadas",
                table: "OperariosProduccion",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
