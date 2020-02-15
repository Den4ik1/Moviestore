using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesShop.Migrations
{
    public partial class FixTitleCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Film_Country_CountryId",
                table: "Film");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Film",
                newName: "CountrysId");

            migrationBuilder.RenameIndex(
                name: "IX_Film_CountryId",
                table: "Film",
                newName: "IX_Film_CountrysId");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Country",
                newName: "NameOfTheCountry");

            migrationBuilder.AddForeignKey(
                name: "FK_Film_Country_CountrysId",
                table: "Film",
                column: "CountrysId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Film_Country_CountrysId",
                table: "Film");

            migrationBuilder.RenameColumn(
                name: "CountrysId",
                table: "Film",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Film_CountrysId",
                table: "Film",
                newName: "IX_Film_CountryId");

            migrationBuilder.RenameColumn(
                name: "NameOfTheCountry",
                table: "Country",
                newName: "Country");

            migrationBuilder.AddForeignKey(
                name: "FK_Film_Country_CountryId",
                table: "Film",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
