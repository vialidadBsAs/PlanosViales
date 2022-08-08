using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_de_planos.Migrations
{
    public partial class _270702 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Planos_Partidos_PartidoId",
            //    table: "Planos");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Planos_PartidosArba_PartidoId",
            //    table: "Planos",
            //    column: "PartidoId",
            //    principalTable: "PartidosArba",
            //    principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Planos_PartidosArba_PartidoId",
            //    table: "Planos");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Planos_Partidos_PartidoId",
            //    table: "Planos",
            //    column: "PartidoId",
            //    principalTable: "Partidos",
            //    principalColumn: "Id");
        }
    }
}
