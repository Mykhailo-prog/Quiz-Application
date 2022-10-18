using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class changeTestStat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvgFirstTryResult", "BestResult", "CountOfAllTries", "MinTries" },
                values: new object[] { 0, 0, 0, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvgFirstTryResult", "BestResult", "CountOfAllTries", "MinTries" },
                values: new object[] { null, null, null, null });
        }
    }
}
