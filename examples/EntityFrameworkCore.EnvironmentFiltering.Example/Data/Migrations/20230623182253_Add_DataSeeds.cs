using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityFrameworkCore.EnvironmentFiltering.Example.Migrations
{
    /// <inheritdoc />
    public partial class Add_DataSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "chuck@norris.com", "Chuck Norris" },
                    { 2, "minnie@mouse.com", "Minnie Mouse" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Laughing Gas in a Can", 10.99m },
                    { 2, "Invisible Ink Pen", 19.99m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "OrderNumber" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 6, 23, 18, 22, 53, 875, DateTimeKind.Utc).AddTicks(5437), "ORD001" },
                    { 2, 2, new DateTime(2023, 6, 23, 18, 22, 53, 875, DateTimeKind.Utc).AddTicks(5442), "ORD002" }
                }).ExcludeProductionEnvironment();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
