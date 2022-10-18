using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class ChangeCreateTestEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreatedTests_QuizUsers_QuizUserId",
                table: "CreatedTests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CreatedTests");

            migrationBuilder.AlterColumn<int>(
                name: "QuizUserId",
                table: "CreatedTests",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fff4600b-f342-4359-a19e-1571493b5f49", "AQAAAAEAACcQAAAAEI4O3DUCnqLoDsg3FHDvMUHUOtoD1OXvYGBQtdeXC0+RX9EetAj6Y9zMZffk4nRSKw==", "7331ec76-6de1-4ce9-b86d-a3a71be8b602" });

            migrationBuilder.InsertData(
                table: "CreatedTests",
                columns: new[] { "Id", "QuizUserId", "TestId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_CreatedTests_QuizUsers_QuizUserId",
                table: "CreatedTests",
                column: "QuizUserId",
                principalTable: "QuizUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreatedTests_QuizUsers_QuizUserId",
                table: "CreatedTests");

            migrationBuilder.DeleteData(
                table: "CreatedTests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "QuizUserId",
                table: "CreatedTests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CreatedTests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71146eb9-f9df-43bf-adf4-d33596f2d74a", "AQAAAAEAACcQAAAAEHwIfCBtX1CwTyReD8hbTSgfAtjl7XPGTxSHsT1iuHg/8OXf7FJbE+yMB/WSHfO1WA==", "cee4848e-8ce0-4f89-a7fc-b3a2d5ed4986" });

            migrationBuilder.AddForeignKey(
                name: "FK_CreatedTests_QuizUsers_QuizUserId",
                table: "CreatedTests",
                column: "QuizUserId",
                principalTable: "QuizUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
