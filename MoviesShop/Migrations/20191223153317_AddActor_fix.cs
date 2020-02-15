using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesShop.Migrations
{
    public partial class AddActor_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Country_CountrysId",
                table: "Actor");

            migrationBuilder.DropIndex(
                name: "IX_Actor_CountrysId",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "CountrysId",
                table: "Actor");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Actor",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actor_CountryId",
                table: "Actor",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Country_CountryId",
                table: "Actor",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Country_CountryId",
                table: "Actor");

            migrationBuilder.DropIndex(
                name: "IX_Actor_CountryId",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Actor");

            migrationBuilder.AddColumn<int>(
                name: "CountrysId",
                table: "Actor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Actor_CountrysId",
                table: "Actor",
                column: "CountrysId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Country_CountrysId",
                table: "Actor",
                column: "CountrysId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
