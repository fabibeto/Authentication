using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeAPI.Migrations
{
    public partial class primermigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Continentes",
                columns: table => new
                {
                    ContinenteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imagen = table.Column<string>(nullable: true),
                    Denominacion = table.Column<string>(nullable: true),
                    CiudadID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continentes", x => x.ContinenteID);
                });

            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    CiudadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imagen = table.Column<string>(nullable: true),
                    Denominacion = table.Column<string>(nullable: true),
                    Cant_Habitantes = table.Column<long>(nullable: false),
                    Superficie = table.Column<long>(nullable: false),
                    Iconos_GeograficosID = table.Column<int>(nullable: false),
                    ContinenteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.CiudadID);
                    table.ForeignKey(
                        name: "FK_Ciudades_Continentes_ContinenteID",
                        column: x => x.ContinenteID,
                        principalTable: "Continentes",
                        principalColumn: "ContinenteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IconosGeograficos",
                columns: table => new
                {
                    Iconos_GeograficosID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imagen = table.Column<string>(nullable: true),
                    Denominacion = table.Column<string>(nullable: true),
                    Fecha_Creacion = table.Column<DateTime>(nullable: false),
                    Altura = table.Column<long>(nullable: false),
                    CiudadID = table.Column<int>(nullable: true),
                    ContinenteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IconosGeograficos", x => x.Iconos_GeograficosID);
                    table.ForeignKey(
                        name: "FK_IconosGeograficos_Ciudades_CiudadID",
                        column: x => x.CiudadID,
                        principalTable: "Ciudades",
                        principalColumn: "CiudadID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IconosGeograficos_Continentes_ContinenteID",
                        column: x => x.ContinenteID,
                        principalTable: "Continentes",
                        principalColumn: "ContinenteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_ContinenteID",
                table: "Ciudades",
                column: "ContinenteID");

            migrationBuilder.CreateIndex(
                name: "IX_IconosGeograficos_CiudadID",
                table: "IconosGeograficos",
                column: "CiudadID");

            migrationBuilder.CreateIndex(
                name: "IX_IconosGeograficos_ContinenteID",
                table: "IconosGeograficos",
                column: "ContinenteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IconosGeograficos");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Continentes");
        }
    }
}
