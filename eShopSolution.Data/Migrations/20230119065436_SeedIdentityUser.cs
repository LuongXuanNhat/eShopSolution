using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class SeedIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("a18be9c0-aa65-4af8-bd17-00bd9344e575"), "8dfb3da4-f2e5-42f3-8242-fa0640fdc7c6", "Administrator Role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("d1f771da-b318-42f8-a003-5a15614216f5"), new Guid("a18be9c0-aa65-4af8-bd17-00bd9344e575") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("d1f771da-b318-42f8-a003-5a15614216f5"), 0, "a2288d6f-a9ea-4bbb-a0ce-0245d18f83a5", new DateTime(2023, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "onionwebdev@gmail.com", true, "Nhat", "Luong Xuan", false, null, "onionwebdev@gmail.com", "admin", "AQAAAAEAACcQAAAAEID+9M0nWI/Ntl7SYcYnBZuDCQXANnxqKtTS4RDFYjFSF6zqGgJLI2opxlJ7XjxtLg==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 1, 19, 13, 54, 35, 637, DateTimeKind.Local).AddTicks(4383));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a18be9c0-aa65-4af8-bd17-00bd9344e575"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("d1f771da-b318-42f8-a003-5a15614216f5"), new Guid("a18be9c0-aa65-4af8-bd17-00bd9344e575") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d1f771da-b318-42f8-a003-5a15614216f5"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 1, 19, 13, 20, 30, 924, DateTimeKind.Local).AddTicks(4329));
        }
    }
}
