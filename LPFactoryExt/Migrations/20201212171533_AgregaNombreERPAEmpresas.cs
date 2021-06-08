using Microsoft.EntityFrameworkCore.Migrations;

namespace LPFactory.Migrations
{
    public partial class AgregaNombreERPAEmpresas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreERP",
                table: "Empresas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreERP",
                table: "Empresas");
        }
    }
}
