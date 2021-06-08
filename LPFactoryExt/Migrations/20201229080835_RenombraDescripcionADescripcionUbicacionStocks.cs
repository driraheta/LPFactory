using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class RenombraDescripcionADescripcionUbicacionStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Descripcion",
            //    table: "Stocks");

            //migrationBuilder.AddColumn<string>(
            //    name: "DescripcionUbicacion",
            //    table: "Stocks",
            //    nullable: true);

            migrationBuilder.RenameColumn("Descripcion", "Stocks", "DescripcionUbicacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "DescripcionUbicacion",
            //    table: "Stocks");

            //migrationBuilder.AddColumn<string>(
            //    name: "Descripcion",
            //    table: "Stocks",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.RenameColumn("DescripcionUbicacion", "Stocks", "Descripcion");
        }
    }
}
