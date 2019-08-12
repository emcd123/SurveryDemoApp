using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyDemoApp.Migrations
{
    public partial class AddedColumnToQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionNo",
                table: "Question",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionNo",
                table: "Question");
        }
    }
}
