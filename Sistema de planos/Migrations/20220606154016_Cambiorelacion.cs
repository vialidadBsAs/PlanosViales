using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_de_planos.Migrations
{
    public partial class Cambiorelacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadoHistorico");

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Historicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_EstadoId",
                table: "Historicos",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Historicos_Estados_EstadoId",
                table: "Historicos",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historicos_Estados_EstadoId",
                table: "Historicos");

            migrationBuilder.DropIndex(
                name: "IX_Historicos_EstadoId",
                table: "Historicos");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Historicos");

            migrationBuilder.CreateTable(
                name: "EstadoHistorico",
                columns: table => new
                {
                    EstadosId = table.Column<int>(type: "int", nullable: false),
                    HistoricosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoHistorico", x => new { x.EstadosId, x.HistoricosId });
                    table.ForeignKey(
                        name: "FK_EstadoHistorico_Estados_EstadosId",
                        column: x => x.EstadosId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstadoHistorico_Historicos_HistoricosId",
                        column: x => x.HistoricosId,
                        principalTable: "Historicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstadoHistorico_HistoricosId",
                table: "EstadoHistorico",
                column: "HistoricosId");
        }
    }
}
