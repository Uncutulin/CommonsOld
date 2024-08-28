using Microsoft.EntityFrameworkCore.Migrations;

namespace CommonsDev.Migrations
{
    public partial class uniquetest1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_Name",
                table: "Tests",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tests_Name",
                table: "Tests");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
