using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EinzelhandelCoreMVC.Migrations
{
    public partial class Bon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Titel",
                table: "Produkt",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "Kunde",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Vorname = table.Column<string>(maxLength: 50, nullable: false),
                    Nachname = table.Column<string>(maxLength: 50, nullable: false),
                    Rufnummer = table.Column<string>(maxLength: 50, nullable: true),
                    Adresse = table.Column<string>(maxLength: 500, nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunde", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Bon",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    Art = table.Column<bool>(nullable: false),
                    KundeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bon", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bon_Kunde_KundeID",
                        column: x => x.KundeID,
                        principalTable: "Kunde",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detail",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Zahl = table.Column<int>(nullable: false),
                    Preis = table.Column<float>(nullable: false),
                    Ermäßigung = table.Column<decimal>(nullable: false),
                    ProduktID = table.Column<int>(nullable: true),
                    BonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Detail_Bon_BonID",
                        column: x => x.BonID,
                        principalTable: "Bon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detail_Produkt_ProduktID",
                        column: x => x.ProduktID,
                        principalTable: "Produkt",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bon_KundeID",
                table: "Bon",
                column: "KundeID");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_BonID",
                table: "Detail",
                column: "BonID");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_ProduktID",
                table: "Detail",
                column: "ProduktID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detail");

            migrationBuilder.DropTable(
                name: "Bon");

            migrationBuilder.DropTable(
                name: "Kunde");

            migrationBuilder.AlterColumn<int>(
                name: "Titel",
                table: "Produkt",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
