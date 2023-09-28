using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace authentication_demo.Migrations
{
    /// <inheritdoc />
    public partial class V1_SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("8b46d95b-b3aa-4da7-897d-229b02930ca0"), null, "User", null },
                    { new Guid("e7fd9bf9-cd1c-47ca-a1d1-5d1273bd36e6"), null, "Admin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8b46d95b-b3aa-4da7-897d-229b02930ca0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e7fd9bf9-cd1c-47ca-a1d1-5d1273bd36e6"));
        }
    }
}
