using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class ChangetestStatEntity1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c60da73-7ebc-4250-a4cb-436918e6ec53", "AQAAAAEAACcQAAAAECn3bCHViICV45sglBl46btTtg5drbhsVWEihOt9THImP1ZXMxvqAdc+1/3pLF5tDQ==", "c09c2ffb-442e-41cf-9ebe-f526340f0e3f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fff4600b-f342-4359-a19e-1571493b5f49", "AQAAAAEAACcQAAAAEI4O3DUCnqLoDsg3FHDvMUHUOtoD1OXvYGBQtdeXC0+RX9EetAj6Y9zMZffk4nRSKw==", "7331ec76-6de1-4ce9-b86d-a3a71be8b602" });

            migrationBuilder.InsertData(
                table: "Statistics",
                columns: new[] { "Id", "AvgFirstTryResult", "AvgTryCount", "BestResult", "BestResultUser", "BestTime", "BestTimeUser", "CountOfAllTries", "MinTries", "MinTriesUser", "TestId" },
                values: new object[] { 1, 0, 0, 0, null, null, null, 0, 0, null, 1 });
        }
    }
}
