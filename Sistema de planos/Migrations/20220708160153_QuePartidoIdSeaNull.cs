using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_de_planos.Migrations
{
    public partial class QuePartidoIdSeaNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planos_Partidos_PartidoId",
                table: "Planos");

            migrationBuilder.AlterColumn<int>(
                name: "PartidoId",
                table: "Planos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Planos_Partidos_PartidoId",
                table: "Planos",
                column: "PartidoId",
                principalTable: "Partidos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planos_Partidos_PartidoId",
                table: "Planos");

            migrationBuilder.AlterColumn<int>(
                name: "PartidoId",
                table: "Planos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Planos_Partidos_PartidoId",
                table: "Planos",
                column: "PartidoId",
                principalTable: "Partidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
