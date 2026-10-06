using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromotionsCRM.Migrations
{
    /// <inheritdoc />
    public partial class SeedMorePromotions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "ClientId", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 4, 1, new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canon Back to School", new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, new DateTime(2026, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canon Black Friday", new DateTime(2026, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canon Spring Photo Fest", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 2, new DateTime(2026, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nikon Autumn Lens Offer", new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 2, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nikon Summer Adventure", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 2, new DateTime(2027, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nikon New Year Bundle", new DateTime(2026, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 3, new DateTime(2026, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sony Audio Week", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 3, new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sony Easter Promo", new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 3, new DateTime(2027, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sony Winter Cashback", new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }

                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
