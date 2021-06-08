using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class ArreglaLoteReferenciaStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoteNumeroSerie",
                table: "Stocks");

            migrationBuilder.AddColumn<string>(
                name: "Lote",
                table: "Stocks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Referencia",
                table: "Stocks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lote",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Referencia",
                table: "Stocks");

            migrationBuilder.AddColumn<string>(
                name: "LoteNumeroSerie",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
