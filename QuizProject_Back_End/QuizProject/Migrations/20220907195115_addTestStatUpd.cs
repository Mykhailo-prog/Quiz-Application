using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class addTestStatUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestStatistic_Tests_TestId",
                table: "TestStatistic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestStatistic",
                table: "TestStatistic");

            migrationBuilder.RenameTable(
                name: "TestStatistic",
                newName: "Statistics");

            migrationBuilder.RenameIndex(
                name: "IX_TestStatistic_TestId",
                table: "Statistics",
                newName: "IX_Statistics_TestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statistics",
                table: "Statistics",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Statistics",
                columns: new[] { "Id", "AvgFirstTryResult", "BestResult", "BestResultUser", "BestTime", "BestTimeUser", "CountOfAllTries", "MinTries", "MinTriesUser", "TestId" },
                values: new object[] { 1, null, null, null, null, null, null, null, null, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Tests_TestId",
                table: "Statistics",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Tests_TestId",
                table: "Statistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statistics",
                table: "Statistics");

            migrationBuilder.DeleteData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "Statistics",
                newName: "TestStatistic");

            migrationBuilder.RenameIndex(
                name: "IX_Statistics_TestId",
                table: "TestStatistic",
                newName: "IX_TestStatistic_TestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestStatistic",
                table: "TestStatistic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestStatistic_Tests_TestId",
                table: "TestStatistic",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
