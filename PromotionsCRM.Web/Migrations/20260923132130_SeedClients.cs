using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromotionsCRM.Migrations
{
    /// <inheritdoc />
    public partial class SeedClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "CountryId", "Email", "Name", "PhoneNumber", "RegisteredOn", "Website" },
                values: new object[,]
                {
                    { 1, "123 Canon Street", 1, "canon@example.com", "Canon", "123-456-7890", new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "www.canon.com" },
                    { 2, "456 Nikon Avenue", 1, "nikon@example.com", "Nikon", "987-654-3210", new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "www.nikon.com" },
                    { 3, "789 Sony Boulevard", 1, "sony@example.com", "Sony", "555-555-5555", new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "www.sony.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
