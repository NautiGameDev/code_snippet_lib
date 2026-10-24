using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseAccountSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class fresh_proj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0802b7cb-a06e-405c-b6e9-6bf1b516e33e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3c359f6-46d0-4201-8c24-4ddc89ef2433");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51f82c97-851b-4558-87ac-52fc0dacb5d5", "51f82c97-851b-4558-87ac-52fc0dacb5d5", "User", "USER" },
                    { "6a83e9d4-a03f-4692-8f6b-6cdf372da900", "6a83e9d4-a03f-4692-8f6b-6cdf372da900", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51f82c97-851b-4558-87ac-52fc0dacb5d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a83e9d4-a03f-4692-8f6b-6cdf372da900");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0802b7cb-a06e-405c-b6e9-6bf1b516e33e", "0802b7cb-a06e-405c-b6e9-6bf1b516e33e", "User", "USER" },
                    { "d3c359f6-46d0-4201-8c24-4ddc89ef2433", "d3c359f6-46d0-4201-8c24-4ddc89ef2433", "Admin", "ADMIN" }
                });
        }
    }
}
