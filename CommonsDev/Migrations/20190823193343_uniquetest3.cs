using Microsoft.EntityFrameworkCore.Migrations;

namespace CommonsDev.Migrations
{
    public partial class uniquetest3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Doc",
                table: "Tests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoDoc",
                table: "Tests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_Doc_TipoDoc",
                table: "Tests",
                columns: new[] { "Doc", "TipoDoc" },
                unique: true,
                filter: "[Doc] IS NOT NULL AND [TipoDoc] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tests_Doc_TipoDoc",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Doc",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TipoDoc",
                table: "Tests");
        }
    }
}
