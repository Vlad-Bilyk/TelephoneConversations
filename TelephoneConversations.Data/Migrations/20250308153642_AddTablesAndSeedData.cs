using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TelephoneConversations.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityID);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    SubscriberID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TelephonePoint = table.Column<string>(type: "TEXT", nullable: false),
                    IPN = table.Column<string>(type: "TEXT", nullable: false),
                    BankAccount = table.Column<string>(type: "TEXT", maxLength: 29, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.SubscriberID);
                    table.CheckConstraint("CK_Subscriber_BankAccount", "BankAccount LIKE 'UA%'");
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    TariffID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityID = table.Column<int>(type: "INTEGER", nullable: false),
                    DayPrice = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    NightPrice = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.TariffID);
                    table.ForeignKey(
                        name: "FK_Tariffs_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calls",
                columns: table => new
                {
                    CallID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubscriberID = table.Column<int>(type: "INTEGER", nullable: false),
                    CityID = table.Column<int>(type: "INTEGER", nullable: false),
                    CallDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeOfDay = table.Column<string>(type: "TEXT", nullable: false),
                    BaseCost = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    CostWithDiscount = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calls", x => x.CallID);
                    table.ForeignKey(
                        name: "FK_Calls_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calls_Subscribers_SubscriberID",
                        column: x => x.SubscriberID,
                        principalTable: "Subscribers",
                        principalColumn: "SubscriberID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TariffID = table.Column<int>(type: "INTEGER", nullable: false),
                    DurationMin = table.Column<int>(type: "INTEGER", nullable: false),
                    DurationMax = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountID);
                    table.CheckConstraint("CK_Discount_Duration", "DurationMax >= DurationMin");
                    table.ForeignKey(
                        name: "FK_Discounts_Tariffs_TariffID",
                        column: x => x.TariffID,
                        principalTable: "Tariffs",
                        principalColumn: "TariffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityID", "CityName" },
                values: new object[,]
                {
                    { 1, "Київ" },
                    { 2, "Львів" },
                    { 3, "Одеса" },
                    { 4, "Харків" },
                    { 5, "Дніпро" }
                });

            migrationBuilder.InsertData(
                table: "Subscribers",
                columns: new[] { "SubscriberID", "BankAccount", "CompanyName", "IPN", "TelephonePoint" },
                values: new object[,]
                {
                    { 1, "UA123456789012345678901234567", "Компанія A", "1234567890", "+380667898764" },
                    { 2, "UA987654321098765432109876543", "Компанія B", "0987654321", "+380232367890" },
                    { 3, "UA112233445566778899001122334", "Компанія C", "1122334455", "+380343454321" }
                });

            migrationBuilder.InsertData(
                table: "Calls",
                columns: new[] { "CallID", "BaseCost", "CallDate", "CityID", "CostWithDiscount", "Discount", "Duration", "SubscriberID", "TimeOfDay" },
                values: new object[,]
                {
                    { 1, 6.25m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6.25m, 0m, 250, 1, "день" },
                    { 2, 11.25m, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10.69m, 5m, 450, 1, "день" },
                    { 3, 8.75m, new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 7.88m, 10m, 700, 1, "ніч" },
                    { 4, 25.00m, new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 23.25m, 7m, 1250, 1, "день" },
                    { 5, 20.00m, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 17.60m, 12m, 2000, 1, "ніч" },
                    { 6, 10.83m, new DateTime(2025, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9.96m, 8m, 650, 1, "день" },
                    { 7, 20.00m, new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 17.00m, 15m, 2400, 1, "ніч" },
                    { 8, 11.67m, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 11.67m, 0m, 500, 2, "день" },
                    { 9, 11.66m, new DateTime(2025, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 11.66m, 0m, 999, 2, "ніч" },
                    { 10, 2.60m, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2.60m, 0m, 120, 2, "день" },
                    { 11, 12.99m, new DateTime(2025, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 12.99m, 0m, 1199, 2, "ніч" },
                    { 12, 17.50m, new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 15.75m, 10m, 700, 2, "день" },
                    { 13, 11.67m, new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 10.73m, 8m, 700, 3, "день" },
                    { 14, 16.67m, new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 14.17m, 15m, 2000, 3, "ніч" },
                    { 15, 6.25m, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6.25m, 0m, 250, 3, "день" }
                });

            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "TariffID", "CityID", "DayPrice", "NightPrice" },
                values: new object[,]
                {
                    { 1, 1, 1.50m, 0.75m },
                    { 2, 2, 1.20m, 0.60m },
                    { 3, 3, 1.00m, 0.50m },
                    { 4, 4, 1.40m, 0.70m },
                    { 5, 5, 1.30m, 0.65m }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "DiscountID", "DiscountRate", "DurationMax", "DurationMin", "TariffID" },
                values: new object[,]
                {
                    { 1, 5m, 10, 5, 1 },
                    { 2, 10m, 30, 10, 1 },
                    { 3, 7m, 30, 20, 2 },
                    { 4, 12m, 60, 30, 2 },
                    { 5, 8m, 15, 10, 3 },
                    { 6, 15m, 45, 20, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calls_CityID",
                table: "Calls",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Calls_SubscriberID",
                table: "Calls",
                column: "SubscriberID");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_TariffID",
                table: "Discounts",
                column: "TariffID");

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_CityID",
                table: "Tariffs",
                column: "CityID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calls");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
