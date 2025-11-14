using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class currencys_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "default_close_time",
                table: "st_stores",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "default_currency",
                table: "st_stores",
                type: "char(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "default_invoice_init",
                table: "st_stores",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "default_invoice_template",
                table: "st_stores",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "default_quote_init",
                table: "st_stores",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "default_receipt_template",
                table: "st_stores",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "st_stores",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "last_transaction_date",
                table: "st_stores",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "low_stock_alert_email",
                table: "st_stores",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "message_on_invoice",
                table: "st_stores",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "message_on_receipt",
                table: "st_stores",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone1",
                table: "st_stores",
                type: "nvarchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone2",
                table: "st_stores",
                type: "nvarchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "plastic_bag_tax_amount",
                table: "st_stores",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "service_charge",
                table: "st_stores",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tax_activity_no",
                table: "st_stores",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tax_identification_number",
                table: "st_stores",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tax_inclusive_price",
                table: "st_stores",
                type: "char(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tax_payer_name",
                table: "st_stores",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "timezone_id",
                table: "st_stores",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "default_close_time",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "default_currency",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "default_invoice_init",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "default_invoice_template",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "default_quote_init",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "default_receipt_template",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "email",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "last_transaction_date",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "low_stock_alert_email",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "message_on_invoice",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "message_on_receipt",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "phone1",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "phone2",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "plastic_bag_tax_amount",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "service_charge",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "tax_activity_no",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "tax_identification_number",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "tax_inclusive_price",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "tax_payer_name",
                table: "st_stores");

            migrationBuilder.DropColumn(
                name: "timezone_id",
                table: "st_stores");
        }
    }
}
