using Microsoft.EntityFrameworkCore.Migrations;

namespace EinzelhandelCoreMVC.Migrations
{
    public partial class Geschlecht : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Geschlecht",
                table: "Kunde",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Geschlecht",
                table: "Kunde");
        }
    }
}
