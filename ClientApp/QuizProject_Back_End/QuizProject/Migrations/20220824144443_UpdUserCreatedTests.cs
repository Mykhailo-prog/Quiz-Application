using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class UpdUserCreatedTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCreatedTest_Tests_TestId",
                table: "UserCreatedTest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCreatedTest_Users_UserId",
                table: "UserCreatedTest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCreatedTest",
                table: "UserCreatedTest");

            migrationBuilder.RenameTable(
                name: "UserCreatedTest",
                newName: "CreatedTests");

            migrationBuilder.RenameIndex(
                name: "IX_UserCreatedTest_UserId",
                table: "CreatedTests",
                newName: "IX_CreatedTests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCreatedTest_TestId",
                table: "CreatedTests",
                newName: "IX_CreatedTests_TestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreatedTests",
                table: "CreatedTests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreatedTests_Tests_TestId",
                table: "CreatedTests",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreatedTests_Users_UserId",
                table: "CreatedTests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreatedTests_Tests_TestId",
                table: "CreatedTests");

            migrationBuilder.DropForeignKey(
                name: "FK_CreatedTests_Users_UserId",
                table: "CreatedTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CreatedTests",
                table: "CreatedTests");

            migrationBuilder.RenameTable(
                name: "CreatedTests",
                newName: "UserCreatedTest");

            migrationBuilder.RenameIndex(
                name: "IX_CreatedTests_UserId",
                table: "UserCreatedTest",
                newName: "IX_UserCreatedTest_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CreatedTests_TestId",
                table: "UserCreatedTest",
                newName: "IX_UserCreatedTest_TestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCreatedTest",
                table: "UserCreatedTest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCreatedTest_Tests_TestId",
                table: "UserCreatedTest",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCreatedTest_Users_UserId",
                table: "UserCreatedTest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
