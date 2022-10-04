using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class ChangeCreateTestEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CreatedTests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71146eb9-f9df-43bf-adf4-d33596f2d74a", "AQAAAAEAACcQAAAAEHwIfCBtX1CwTyReD8hbTSgfAtjl7XPGTxSHsT1iuHg/8OXf7FJbE+yMB/WSHfO1WA==", "cee4848e-8ce0-4f89-a7fc-b3a2d5ed4986" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "240b8f4d-ffd4-4b06-bff1-0920e574cb82", "AQAAAAEAACcQAAAAEJUCqRQ0ZI4FiN0UWcWC4e0J3DZgJeScC3t4GJYJZ8pgx6o2vdbPCdYivtdcXuWgpA==", "af65bc03-dde4-4426-a268-a5db9bd5be2b" });

            migrationBuilder.InsertData(
                table: "CreatedTests",
                columns: new[] { "Id", "QuizUserId", "TestId", "UserId" },
                values: new object[] { 1, null, 1, 1 });
        }
    }
}
