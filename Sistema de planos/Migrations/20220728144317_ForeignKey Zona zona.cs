using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_de_planos.Migrations
{
    public partial class ForeignKeyZonazona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateIndex(
            //name: "IX_PartidosArba_ZonaId",
            //table: "PartidosArba",
            //column: "ZonaId");

            //migrationBuilder.AddForeignKey(
            //name: "FK_PartidosArba_Zonas_ZonaId",
            //table: "PartidosArba",
            //        column: "ZonaId",
            //        principalColumn: "Id");
            //        principalTable: "Zonas",
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PartidosArba_Zonas_ZonaId",
            //    table: "PartidosArba");

            //migrationBuilder.DropIndex(
                //name: "IX_PartidosArba_ZonaId",
                //table: "PartidosArba");
        }
    }
}
