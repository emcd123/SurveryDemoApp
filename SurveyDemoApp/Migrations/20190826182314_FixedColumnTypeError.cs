using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyDemoApp.Migrations
{
    public partial class FixedColumnTypeError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Survey",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "Survey",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
