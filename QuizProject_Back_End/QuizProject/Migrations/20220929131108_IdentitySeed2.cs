using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class IdentitySeed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d82cb833-4019-410d-9f17-a9d0b83247ee", 0, "d3bf4c8a-1753-4686-9d08-83e5352a9f98", "monstercattop@gmail.com", true, false, null, "MONSTERCATTOP@GMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEOu4t9io0Txgxxkl/oWmfwmafl8u3qJ7YQflp+OXZ0vdU4DTecbW8DdqOjDoWsCgsQ==", null, false, "869b4e96-9041-4517-b62b-3c9fdd1f1a45", false, "Administrator" });

            migrationBuilder.UpdateData(
                table: "QuizUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Login",
                value: "Administrator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee");

            migrationBuilder.UpdateData(
                table: "QuizUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Login",
                value: "Admin");
        }
    }
}
