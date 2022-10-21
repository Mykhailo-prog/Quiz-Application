using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class ChangeUserStat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTests");

            migrationBuilder.CreateTable(
                name: "UserStatistic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(nullable: true),
                    Result = table.Column<int>(nullable: false),
                    TriesCount = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    QuizUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStatistic_QuizUsers_QuizUserId",
                        column: x => x.QuizUserId,
                        principalTable: "QuizUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "240b8f4d-ffd4-4b06-bff1-0920e574cb82", "AQAAAAEAACcQAAAAEJUCqRQ0ZI4FiN0UWcWC4e0J3DZgJeScC3t4GJYJZ8pgx6o2vdbPCdYivtdcXuWgpA==", "af65bc03-dde4-4426-a268-a5db9bd5be2b" });

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistic_QuizUserId",
                table: "UserStatistic",
                column: "QuizUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStatistic");

            migrationBuilder.CreateTable(
                name: "UserTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizUserId = table.Column<int>(type: "int", nullable: true),
                    Result = table.Column<int>(type: "int", nullable: false),
                    TestTried = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TriesCount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTests_QuizUsers_QuizUserId",
                        column: x => x.QuizUserId,
                        principalTable: "QuizUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3bf4c8a-1753-4686-9d08-83e5352a9f98", "AQAAAAEAACcQAAAAEOu4t9io0Txgxxkl/oWmfwmafl8u3qJ7YQflp+OXZ0vdU4DTecbW8DdqOjDoWsCgsQ==", "869b4e96-9041-4517-b62b-3c9fdd1f1a45" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_QuizUserId",
                table: "UserTests",
                column: "QuizUserId");
        }
    }
}
