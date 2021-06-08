using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregarCodigoOrdenYCodigoArticuloAOrdenOperaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoOrden",
                table: "OrdenOperaciones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CogidoArticulo",
                table: "OrdenOperaciones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoOrden",
                table: "OrdenOperaciones");

            migrationBuilder.DropColumn(
                name: "CogidoArticulo",
                table: "OrdenOperaciones");
        }
    }
}
