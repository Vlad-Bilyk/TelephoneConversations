using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelephoneConversations.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCorrectValidationForSubscribers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TelephonePoint",
                table: "Subscribers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "IPN",
                table: "Subscribers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "SubscriberID",
                keyValue: 1,
                column: "TelephonePoint",
                value: "+380667898764");

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "SubscriberID",
                keyValue: 2,
                column: "TelephonePoint",
                value: "+380232367890");

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "SubscriberID",
                keyValue: 3,
                column: "TelephonePoint",
                value: "+380343454321");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TelephonePoint",
                table: "Subscribers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IPN",
                table: "Subscribers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "SubscriberID",
                keyValue: 1,
                column: "TelephonePoint",
                value: "12345");

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "SubscriberID",
                keyValue: 2,
                column: "TelephonePoint",
                value: "67890");

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "SubscriberID",
                keyValue: 3,
                column: "TelephonePoint",
                value: "54321");
        }
    }
}
