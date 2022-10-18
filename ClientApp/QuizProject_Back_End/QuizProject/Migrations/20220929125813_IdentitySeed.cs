using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class IdentitySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "QuizUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d82cb833-4019-410d-9f17-a9d0b83247ee", 0, "dee0c401-908c-48cc-887a-96dc132f0d3d", "monstercattop@gmail.com", true, false, null, null, null, null, null, false, "527649c9-5747-4a95-b9e0-bea8824499ef", false, "Administrator" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "QuizUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "QuizUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "admin");
        }
    }
}
