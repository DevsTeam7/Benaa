using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Benaa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class benaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bc79cfe-7d09-4522-bc43-27d219f22b16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c32a4445-9522-4fb0-9532-7c5e3cc69ead");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dab7e7ec-84f0-41c1-8286-57a627434ca1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f02115ef-1779-4913-8bc3-3f4edc1dd688");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "632b7826-0414-4e3c-b7d3-a3bf1eb78c09", null, "Student", null },
                    { "93e50bcb-2e48-4de8-977f-cd598482a7e0", null, "Teacher", null },
                    { "960253b0-762f-449e-b824-42ebf3ea5039", null, "Owner", null },
                    { "e9a5f849-fb84-42fa-bd1b-3f6e70ae31bf", null, "Admin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "632b7826-0414-4e3c-b7d3-a3bf1eb78c09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93e50bcb-2e48-4de8-977f-cd598482a7e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "960253b0-762f-449e-b824-42ebf3ea5039");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9a5f849-fb84-42fa-bd1b-3f6e70ae31bf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5bc79cfe-7d09-4522-bc43-27d219f22b16", null, "Student", null },
                    { "c32a4445-9522-4fb0-9532-7c5e3cc69ead", null, "Owner", null },
                    { "dab7e7ec-84f0-41c1-8286-57a627434ca1", null, "Teacher", null },
                    { "f02115ef-1779-4913-8bc3-3f4edc1dd688", null, "Admin", null }
                });
        }
    }
}
