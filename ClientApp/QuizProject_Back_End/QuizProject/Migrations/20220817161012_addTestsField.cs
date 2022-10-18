using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class addTestsField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "question",
                table: "Questions",
                newName: "Quest");

            migrationBuilder.RenameColumn(
                name: "answer",
                table: "Answers",
                newName: "Ans");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tests",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "Users",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "login",
                table: "Users",
                newName: "Login");

            migrationBuilder.RenameColumn(
                name: "correctAnswer",
                table: "Questions",
                newName: "CorrectAnswer");

            migrationBuilder.AddColumn<int>(
                name: "Tests",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tests_TestId",
                table: "Questions",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quest",
                table: "Questions",
                newName: "question");

            migrationBuilder.RenameColumn(
                name: "Ans",
                table: "Answers",
                newName: "answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tests_TestId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Tests",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Users",
                newName: "score");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "login");

            migrationBuilder.RenameColumn(
                name: "CorrectAnswer",
                table: "Questions",
                newName: "correctAnswer");

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tests",
                table: "Questions",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
