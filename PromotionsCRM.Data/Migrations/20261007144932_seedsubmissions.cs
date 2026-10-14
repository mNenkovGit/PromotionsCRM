using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromotionsCRM.Migrations
{
    /// <inheritdoc />
    public partial class seedsubmissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CountryId", "Email", "FirstName", "LastName", "PhoneNumber", "RegisteredOn" },
                values: new object[,]
                {
                    { 1, 1, "ivan.petrov@example.com", "Ivan", "Petrov", "359-888-1234", new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "maria.g@example.com", "Maria", "Georgieva", "359-877-5678", new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, "james.smith@example.com", "James", "Smith", "447-700-9001", new DateTime(2026, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, "anna.muller@example.com", "Anna", "Muller", "491-512-3456", new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 20, "kenji.tanaka@example.com", "Kenji", "Tanaka", "819-012-3456", new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "Id", "CustomerId", "ProcessedOn", "ProductId", "PromotionId", "PurchaseDate", "Status", "SubmittedOn", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 2, new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, 3, null, 3, 4, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, 1, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, 4, new DateTime(2026, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 6, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 6, 5, null, 9, 7, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 7, 2, new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 7, new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 8, 3, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 8, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 9, 4, new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 10, new DateTime(2026, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 10, 5, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 11, new DateTime(2026, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
