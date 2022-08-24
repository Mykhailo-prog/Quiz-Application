using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class InitDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tests",
                columns: new[] { "TestId", "Name" },
                values: new object[] { 1, "C# Beginers Test" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Password", "Score" },
                values: new object[] { 1, "Admin", "admin", 0 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CorrectAnswer", "Quest", "TestId" },
                values: new object[] { 1, "Private", "What access modifier make component available only within it's class of struct?", 1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CorrectAnswer", "Quest", "TestId" },
                values: new object[] { 2, "Class", "What of these types is Referense type?", 1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CorrectAnswer", "Quest", "TestId" },
                values: new object[] { 3, "ref", "What modifier can pass parameters by reference?", 1 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "Ans", "QuestionId" },
                values: new object[,]
                {
                    { 101, "Private", 1 },
                    { 102, "Private Protected", 1 },
                    { 103, "Protected", 1 },
                    { 104, "Internal", 1 },
                    { 105, "Public", 1 },
                    { 106, "Bool", 2 },
                    { 107, "Integer", 2 },
                    { 108, "Class", 2 },
                    { 109, "Structure", 2 },
                    { 110, "Char", 2 },
                    { 111, "null", 3 },
                    { 112, "ref", 3 },
                    { 113, "public", 3 },
                    { 114, "static", 3 },
                    { 115, "<T>", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "TestId",
                keyValue: 1);
        }
    }
}
