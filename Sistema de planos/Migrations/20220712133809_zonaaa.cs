using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_de_planos.Migrations
{
    public partial class zonaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidos_Zonas_ZonaId",
                table: "Partidos");

            migrationBuilder.DropIndex(
                name: "IX_Partidos_ZonaId",
                table: "Partidos");

            //migrationBuilder.AddColumn<int>(
            //    name: "ZonaNro",
            //    table: "Partidos",
            //    type: "int",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZonaNro",
                table: "Partidos");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_ZonaId",
                table: "Partidos",
                column: "ZonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidos_Zonas_ZonaId",
                table: "Partidos",
                column: "ZonaId",
                principalTable: "Zonas",
                principalColumn: "Id");
        }
    }
}
