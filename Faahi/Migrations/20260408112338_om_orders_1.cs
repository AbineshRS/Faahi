using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class om_orders_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrderLines_im_ProductVariants_variant_id",
                table: "om_CustomerOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrderLines_im_Products_product_id",
                table: "om_CustomerOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrderLines_om_CustomerOrders_customer_order_id",
                table: "om_CustomerOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrders_co_business_business_id",
                table: "om_CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrders_st_stores_store_id",
                table: "om_CustomerOrders");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_amount",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "sub_total",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "store_id",
                table: "om_CustomerOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "payment_status",
                table: "om_CustomerOrders",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "UNPAID",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldNullable: true,
                oldDefaultValue: "UNPAID");

            migrationBuilder.AlterColumn<decimal>(
                name: "other_charges",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "order_status",
                table: "om_CustomerOrders",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "NEW",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldNullable: true,
                oldDefaultValue: "NEW");

            migrationBuilder.AlterColumn<DateTime>(
                name: "order_date",
                table: "om_CustomerOrders",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<decimal>(
                name: "grand_total",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "fulfillment_status",
                table: "om_CustomerOrders",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "PENDING",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldNullable: true,
                oldDefaultValue: "PENDING");

            migrationBuilder.AlterColumn<string>(
                name: "expected_payment_method",
                table: "om_CustomerOrders",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "COD",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldNullable: true,
                oldDefaultValue: "COD");

            migrationBuilder.AlterColumn<decimal>(
                name: "exchange_rate",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_amount",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "delivery_status",
                table: "om_CustomerOrders",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "PENDING",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldNullable: true,
                oldDefaultValue: "PENDING");

            migrationBuilder.AlterColumn<decimal>(
                name: "delivery_charge",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "currency_code",
                table: "om_CustomerOrders",
                type: "nvarchar(15)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "business_id",
                table: "om_CustomerOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "variant_id",
                table: "om_CustomerOrderLines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_amount",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "returned_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "reserved_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "product_id",
                table: "om_CustomerOrderLines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "packed_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ordered_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "line_total",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "dispatched_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_amount",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "delivered_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "customer_order_id",
                table: "om_CustomerOrderLines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "cancelled_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrderLines_im_ProductVariants_variant_id",
                table: "om_CustomerOrderLines",
                column: "variant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrderLines_im_Products_product_id",
                table: "om_CustomerOrderLines",
                column: "product_id",
                principalTable: "im_Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrderLines_om_CustomerOrders_customer_order_id",
                table: "om_CustomerOrderLines",
                column: "customer_order_id",
                principalTable: "om_CustomerOrders",
                principalColumn: "customer_order_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrders_co_business_business_id",
                table: "om_CustomerOrders",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrders_st_stores_store_id",
                table: "om_CustomerOrders",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrderLines_im_ProductVariants_variant_id",
                table: "om_CustomerOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrderLines_im_Products_product_id",
                table: "om_CustomerOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrderLines_om_CustomerOrders_customer_order_id",
                table: "om_CustomerOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrders_co_business_business_id",
                table: "om_CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrders_st_stores_store_id",
                table: "om_CustomerOrders");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_amount",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "sub_total",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "store_id",
                table: "om_CustomerOrders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "payment_status",
                table: "om_CustomerOrders",
                type: "nvarchar(30)",
                nullable: true,
                defaultValue: "UNPAID",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldDefaultValue: "UNPAID");

            migrationBuilder.AlterColumn<decimal>(
                name: "other_charges",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "order_status",
                table: "om_CustomerOrders",
                type: "nvarchar(30)",
                nullable: true,
                defaultValue: "NEW",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldDefaultValue: "NEW");

            migrationBuilder.AlterColumn<DateTime>(
                name: "order_date",
                table: "om_CustomerOrders",
                type: "datetime",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<decimal>(
                name: "grand_total",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "fulfillment_status",
                table: "om_CustomerOrders",
                type: "nvarchar(30)",
                nullable: true,
                defaultValue: "PENDING",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldDefaultValue: "PENDING");

            migrationBuilder.AlterColumn<string>(
                name: "expected_payment_method",
                table: "om_CustomerOrders",
                type: "nvarchar(30)",
                nullable: true,
                defaultValue: "COD",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldDefaultValue: "COD");

            migrationBuilder.AlterColumn<decimal>(
                name: "exchange_rate",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_amount",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "delivery_status",
                table: "om_CustomerOrders",
                type: "nvarchar(30)",
                nullable: true,
                defaultValue: "PENDING",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldDefaultValue: "PENDING");

            migrationBuilder.AlterColumn<decimal>(
                name: "delivery_charge",
                table: "om_CustomerOrders",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "currency_code",
                table: "om_CustomerOrders",
                type: "nvarchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.AlterColumn<Guid>(
                name: "business_id",
                table: "om_CustomerOrders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "variant_id",
                table: "om_CustomerOrderLines",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_amount",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "returned_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "reserved_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "product_id",
                table: "om_CustomerOrderLines",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "packed_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ordered_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "line_total",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "dispatched_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_amount",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "delivered_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "customer_order_id",
                table: "om_CustomerOrderLines",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "cancelled_qty",
                table: "om_CustomerOrderLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrderLines_im_ProductVariants_variant_id",
                table: "om_CustomerOrderLines",
                column: "variant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrderLines_im_Products_product_id",
                table: "om_CustomerOrderLines",
                column: "product_id",
                principalTable: "im_Products",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrderLines_om_CustomerOrders_customer_order_id",
                table: "om_CustomerOrderLines",
                column: "customer_order_id",
                principalTable: "om_CustomerOrders",
                principalColumn: "customer_order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrders_co_business_business_id",
                table: "om_CustomerOrders",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrders_st_stores_store_id",
                table: "om_CustomerOrders",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id");
        }
    }
}
