using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromotionsCRM.Migrations
{
    /// <inheritdoc />
    public partial class SeedPromotions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "ClientId", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canon Summer Sale", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2027, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nikon Winter Clearance", new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sony Holiday Deals", new DateTime(2026, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
