using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizProject.Migrations
{
    public partial class SeedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a83cb833-4019-510d-9f17-a9d0b83540ee", "1", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed6a7ffc-d690-4a46-b6ae-72cae474c830", "AQAAAAEAACcQAAAAEIEtR1ucmbbJMJHGwVSBApybXYC3LRQpi12+zl4E+7gF165IcUlwRLrLxeM9JFSMVQ==", "fd74b806-67be-43e9-9344-17e4ecb94334" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "d82cb833-4019-410d-9f17-a9d0b83247ee", "a83cb833-4019-510d-9f17-a9d0b83540ee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "d82cb833-4019-410d-9f17-a9d0b83247ee", "a83cb833-4019-510d-9f17-a9d0b83540ee" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a83cb833-4019-510d-9f17-a9d0b83540ee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d82cb833-4019-410d-9f17-a9d0b83247ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "136d683e-4566-47a6-a90e-fa5b442356fe", "AQAAAAEAACcQAAAAELX9BscjcDpLqCY3iOJWx/KLTbu/UVCuSwQ2mlROWl0JRK8A1MPhyK85TQDVov5DaQ==", "f219ed00-e741-4215-b013-b97d5d66f8c0" });
        }
    }
}
