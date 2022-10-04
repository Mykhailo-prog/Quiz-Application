using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class ChangetestStatEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "136d683e-4566-47a6-a90e-fa5b442356fe", "AQAAAAEAACcQAAAAELX9BscjcDpLqCY3iOJWx/KLTbu/UVCuSwQ2mlROWl0JRK8A1MPhyK85TQDVov5DaQ==", "f219ed00-e741-4215-b013-b97d5d66f8c0" });

            migrationBuilder.InsertData(
                table: "Statistics",
                columns: new[] { "Id", "AvgFirstTryResult", "AvgTryCount", "BestResult", "BestResultUser", "BestTime", "BestTimeUser", "CountOfAllTries", "MinTries", "MinTriesUser", "TestId" },
                values: new object[] { 1, 0, 0, 0, null, null, null, 0, 0, null, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
