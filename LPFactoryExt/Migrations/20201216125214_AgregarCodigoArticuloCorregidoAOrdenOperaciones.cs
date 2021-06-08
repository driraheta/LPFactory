using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregarCodigoArticuloCorregidoAOrdenOperaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CogidoArticulo",
                table: "OrdenOperaciones");

            migrationBuilder.AddColumn<string>(
                name: "CodigoArticulo",
                table: "OrdenOperaciones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoArticulo",
                table: "OrdenOperaciones");

            migrationBuilder.AddColumn<string>(
                name: "CogidoArticulo",
                table: "OrdenOperaciones",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
