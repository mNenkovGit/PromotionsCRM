using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromotionsCRM.Migrations
{
    /// <inheritdoc />
    public partial class NewSeedOfPromotions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "ClientId", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 13, 1, new DateTime(2027, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canon Valentine's Print Deal", new DateTime(2027, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 2, new DateTime(2027, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nikon Spring Wildlife Promo", new DateTime(2027, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 3, new DateTime(2027, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sony Summer Sound Festival", new DateTime(2027, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
