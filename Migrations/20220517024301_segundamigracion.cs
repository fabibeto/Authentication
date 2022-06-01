using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeAPI.Migrations
{
    public partial class segundamigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IconosGeograficos_Ciudades_CiudadID",
                table: "IconosGeograficos");

            migrationBuilder.DropForeignKey(
                name: "FK_IconosGeograficos_Continentes_ContinenteID",
                table: "IconosGeograficos");

            migrationBuilder.DropIndex(
                name: "IX_IconosGeograficos_ContinenteID",
                table: "IconosGeograficos");

            migrationBuilder.DropColumn(
                name: "ContinenteID",
                table: "IconosGeograficos");

            migrationBuilder.AlterColumn<int>(
                name: "CiudadID",
                table: "IconosGeograficos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IconosGeograficos_Ciudades_CiudadID",
                table: "IconosGeograficos",
                column: "CiudadID",
                principalTable: "Ciudades",
                principalColumn: "CiudadID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IconosGeograficos_Ciudades_CiudadID",
                table: "IconosGeograficos");

            migrationBuilder.AlterColumn<int>(
                name: "CiudadID",
                table: "IconosGeograficos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ContinenteID",
                table: "IconosGeograficos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IconosGeograficos_ContinenteID",
                table: "IconosGeograficos",
                column: "ContinenteID");

            migrationBuilder.AddForeignKey(
                name: "FK_IconosGeograficos_Ciudades_CiudadID",
                table: "IconosGeograficos",
                column: "CiudadID",
                principalTable: "Ciudades",
                principalColumn: "CiudadID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IconosGeograficos_Continentes_ContinenteID",
                table: "IconosGeograficos",
                column: "ContinenteID",
                principalTable: "Continentes",
                principalColumn: "ContinenteID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
