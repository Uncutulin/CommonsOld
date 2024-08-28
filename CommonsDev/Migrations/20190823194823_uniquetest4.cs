using Microsoft.EntityFrameworkCore.Migrations;

namespace CommonsDev.Migrations
{
    public partial class uniquetest4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tests_Name",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_Doc_TipoDoc",
                table: "Tests");

            migrationBuilder.AlterColumn<string>(
                name: "TipoDoc",
                table: "Tests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Doc",
                table: "Tests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_Name",
                table: "Tests",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_Doc_TipoDoc",
                table: "Tests",
                columns: new[] { "Doc", "TipoDoc" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tests_Name",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_Doc_TipoDoc",
                table: "Tests");

            migrationBuilder.AlterColumn<string>(
                name: "TipoDoc",
                table: "Tests",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tests",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Doc",
                table: "Tests",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Tests_Name",
                table: "Tests",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_Doc_TipoDoc",
                table: "Tests",
                columns: new[] { "Doc", "TipoDoc" },
                unique: true,
                filter: "[Doc] IS NOT NULL AND [TipoDoc] IS NOT NULL");
        }
    }
}
