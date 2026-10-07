using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromotionsCRM.Migrations
{
    /// <inheritdoc />
    public partial class insertdataforproductsandcountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "PromotionId" },
                values: new object[,]
                {
                    { 1, "Canon EOS R6 Mark II", 1 },
                    { 2, "Canon RF 24-70mm f/2.8", 1 },
                    { 3, "Canon PIXMA G650", 4 },
                    { 4, "Canon i-SENSYS MF655Cdw", 4 },
                    { 5, "Canon EOS R8", 5 },
                    { 6, "Canon PowerShot V10", 5 },
                    { 7, "Canon RF 50mm f/1.8", 6 },
                    { 8, "Canon SELPHY CP1500", 13 },
                    { 9, "Nikon Z6 III", 7 },
                    { 10, "Nikon NIKKOR Z 24-120mm", 7 },
                    { 11, "Nikon COOLPIX P950", 8 },
                    { 12, "Nikon Z fc", 9 },
                    { 13, "Nikon Z 50mm f/1.8 S", 14 },
                    { 14, "Sony WH-1000XM5", 10 },
                    { 15, "Sony WF-1000XM5", 10 },
                    { 16, "Sony SRS-XB100", 11 },
                    { 17, "Sony Alpha 7 IV", 12 },
                    { 18, "Sony ULT Field 1", 15 }
                });

            migrationBuilder.InsertData(
                table: "PromotionsCountries",
                columns: new[] { "CountryId", "PromotionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 1, 4 },
                    { 3, 4 },
                    { 2, 5 },
                    { 8, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 4, 7 },
                    { 21, 7 },
                    { 7, 8 },
                    { 24, 8 },
                    { 1, 9 },
                    { 2, 9 },
                    { 3, 9 },
                    { 4, 9 },
                    { 20, 10 },
                    { 6, 11 },
                    { 17, 11 },
                    { 1, 12 },
                    { 4, 12 },
                    { 5, 12 },
                    { 6, 12 },
                    { 7, 12 },
                    { 1, 13 },
                    { 16, 14 },
                    { 22, 14 },
                    { 26, 14 },
                    { 2, 15 },
                    { 9, 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 21, 7 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 7, 8 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 24, 8 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 4, 9 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 20, 10 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 6, 11 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 17, 11 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 1, 12 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 4, 12 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 5, 12 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 6, 12 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 7, 12 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 1, 13 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 16, 14 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 22, 14 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 26, 14 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 2, 15 });

            migrationBuilder.DeleteData(
                table: "PromotionsCountries",
                keyColumns: new[] { "CountryId", "PromotionId" },
                keyValues: new object[] { 9, 15 });
        }
    }
}
