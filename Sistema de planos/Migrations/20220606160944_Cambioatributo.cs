using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_de_planos.Migrations
{
    public partial class Cambioatributo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Decripcion",
                table: "Estados",
                newName: "Descripcion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Estados",
                newName: "Decripcion");
        }
    }
}
