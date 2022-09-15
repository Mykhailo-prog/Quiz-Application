using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class addTestStatistic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestStatistic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestTime = table.Column<string>(nullable: true),
                    BestTimeUser = table.Column<string>(nullable: true),
                    BestResult = table.Column<int>(nullable: true),
                    BestResultUser = table.Column<string>(nullable: true),
                    AvgFirstTryResult = table.Column<int>(nullable: true),
                    MinTries = table.Column<int>(nullable: true),
                    MinTriesUser = table.Column<string>(nullable: true),
                    CountOfAllTries = table.Column<int>(nullable: true),
                    TestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestStatistic_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestStatistic_TestId",
                table: "TestStatistic",
                column: "TestId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestStatistic");
        }
    }
}
