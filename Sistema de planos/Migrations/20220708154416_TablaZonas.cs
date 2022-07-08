using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_de_planos.Migrations
{
    public partial class TablaZonas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZonaId",
                table: "Partidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Zonas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nro = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zonas", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidos_Zonas_ZonaId",
                table: "Partidos");

            migrationBuilder.DropTable(
                name: "Zonas");

            migrationBuilder.DropIndex(
                name: "IX_Partidos_ZonaId",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "ZonaId",
                table: "Partidos");
        }
    }
}
