using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class om_orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "source_code",
                table: "om_OrderSources",
                type: "nvarchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");

            migrationBuilder.CreateTable(
                name: "om_CustomerOrders",
                columns: table => new
                {
                    customer_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    source_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    customer_profile_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    party_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    order_no = table.Column<long>(type: "bigint", nullable: true),
                    order_reference_no = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    order_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    expected_payment_method = table.Column<string>(type: "nvarchar(30)", nullable: true, defaultValue: "COD"),
                    payment_status = table.Column<string>(type: "nvarchar(30)", nullable: true, defaultValue: "UNPAID"),
                    order_status = table.Column<string>(type: "nvarchar(30)", nullable: true, defaultValue: "NEW"),
                    fulfillment_status = table.Column<string>(type: "nvarchar(30)", nullable: true, defaultValue: "PENDING"),
                    delivery_status = table.Column<string>(type: "nvarchar(30)", nullable: true, defaultValue: "PENDING"),
                    currency_code = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    exchange_rate = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    sub_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    delivery_charge = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    other_charges = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    grand_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    delivery_contact_name = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    delivery_contact_no = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    delivery_address1 = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    delivery_address2 = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    delivery_area = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    delivery_city = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    delivery_postal_code = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    delivery_latitude = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    delivery_longitude = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    internal_notes = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    confirmed_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    confirmed_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    cancelled_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    cancelled_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    cancellation_reason = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_om_CustomerOrders", x => x.customer_order_id);
                    table.ForeignKey(
                        name: "FK_om_CustomerOrders_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_om_CustomerOrders_mk_customer_profiles_customer_profile_id",
                        column: x => x.customer_profile_id,
                        principalTable: "mk_customer_profiles",
                        principalColumn: "customer_profile_id");
                    table.ForeignKey(
                        name: "FK_om_CustomerOrders_om_OrderSources_source_id",
                        column: x => x.source_id,
                        principalTable: "om_OrderSources",
                        principalColumn: "source_id");
                    table.ForeignKey(
                        name: "FK_om_CustomerOrders_st_Parties_party_id",
                        column: x => x.party_id,
                        principalTable: "st_Parties",
                        principalColumn: "party_id");
                    table.ForeignKey(
                        name: "FK_om_CustomerOrders_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateTable(
                name: "om_CustomerOrderLines",
                columns: table => new
                {
                    customer_order_line_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    line_no = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    batch_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ordered_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    reserved_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    picked_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    packed_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    dispatched_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    delivered_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    returned_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    cancelled_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    unit_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    line_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    remarks = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    line_status = table.Column<string>(type: "nvarchar(30)", nullable: true, defaultValue: "OPEN")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_om_CustomerOrderLines", x => x.customer_order_line_id);
                    table.ForeignKey(
                        name: "FK_om_CustomerOrderLines_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                    table.ForeignKey(
                        name: "FK_om_CustomerOrderLines_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_om_CustomerOrderLines_im_UnitsOfMeasures_uom_id",
                        column: x => x.uom_id,
                        principalTable: "im_UnitsOfMeasures",
                        principalColumn: "uom_id");
                    table.ForeignKey(
                        name: "FK_om_CustomerOrderLines_im_itemBatches_batch_id",
                        column: x => x.batch_id,
                        principalTable: "im_itemBatches",
                        principalColumn: "item_batch_id");
                    table.ForeignKey(
                        name: "FK_om_CustomerOrderLines_om_CustomerOrders_customer_order_id",
                        column: x => x.customer_order_id,
                        principalTable: "om_CustomerOrders",
                        principalColumn: "customer_order_id");
                });

            migrationBuilder.CreateTable(
                name: "om_OrderStatusHistories",
                columns: table => new
                {
                    order_status_history_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    old_status = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    new_status = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    status_type = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    changed_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    changed_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_om_OrderStatusHistories", x => x.order_status_history_id);
                    table.ForeignKey(
                        name: "FK_om_OrderStatusHistories_om_CustomerOrders_customer_order_id",
                        column: x => x.customer_order_id,
                        principalTable: "om_CustomerOrders",
                        principalColumn: "customer_order_id");
                });

            migrationBuilder.CreateTable(
                name: "im_InventoryReservations",
                columns: table => new
                {
                    reservation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    customer_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    customer_order_line_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    batch_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    reserved_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    released_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    consumed_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true, defaultValue: 0m),
                    reserved_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    released_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    consumed_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    expires_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    reservation_status = table.Column<string>(type: "nvarchar(20)", nullable: true, defaultValue: "ACTIVE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_InventoryReservations", x => x.reservation_id);
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_im_itemBatches_batch_id",
                        column: x => x.batch_id,
                        principalTable: "im_itemBatches",
                        principalColumn: "item_batch_id");
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_om_CustomerOrderLines_customer_order_line_id",
                        column: x => x.customer_order_line_id,
                        principalTable: "om_CustomerOrderLines",
                        principalColumn: "customer_order_line_id");
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_om_CustomerOrders_customer_order_id",
                        column: x => x.customer_order_id,
                        principalTable: "om_CustomerOrders",
                        principalColumn: "customer_order_id");
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryReservations_batch_id",
                table: "im_InventoryReservations",
                column: "batch_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryReservations_business_id",
                table: "im_InventoryReservations",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryReservations_customer_order_line_id",
                table: "im_InventoryReservations",
                column: "customer_order_line_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryReservations_order",
                table: "im_InventoryReservations",
                columns: new[] { "customer_order_id", "reservation_status" });

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryReservations_product_id",
                table: "im_InventoryReservations",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryReservations_store_variant_status",
                table: "im_InventoryReservations",
                columns: new[] { "store_id", "variant_id", "reservation_status" });

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryReservations_variant_id",
                table: "im_InventoryReservations",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrderLines_batch_id",
                table: "om_CustomerOrderLines",
                column: "batch_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrderLines_product_id",
                table: "om_CustomerOrderLines",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrderLines_uom_id",
                table: "om_CustomerOrderLines",
                column: "uom_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrderLines_variant",
                table: "om_CustomerOrderLines",
                columns: new[] { "variant_id", "line_status", "customer_order_id", "ordered_qty", "reserved_qty", "delivered_qty" });

            migrationBuilder.CreateIndex(
                name: "UX_om_CustomerOrderLines_order_line_no",
                table: "om_CustomerOrderLines",
                columns: new[] { "customer_order_id", "line_no" });

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrders_BusinessId_StoreId_OrderDate",
                table: "om_CustomerOrders",
                columns: new[] { "business_id", "store_id", "order_date" },
                descending: new[] { false, false, true });

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrders_customer_order_id",
                table: "om_CustomerOrders",
                column: "customer_order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrders_customer_profile_id",
                table: "om_CustomerOrders",
                column: "customer_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrders_order_status",
                table: "om_CustomerOrders",
                columns: new[] { "business_id", "store_id", "order_status", "delivery_status", "fulfillment_status" });

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrders_party_id",
                table: "om_CustomerOrders",
                column: "party_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrders_source_id",
                table: "om_CustomerOrders",
                column: "source_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrders_store_id",
                table: "om_CustomerOrders",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "UX_om_CustomerOrders_business_store_order_no",
                table: "om_CustomerOrders",
                columns: new[] { "business_id", "store_id", "order_no" });

            migrationBuilder.CreateIndex(
                name: "IX_om_OrderStatusHistory_order_changed_at",
                table: "om_OrderStatusHistories",
                columns: new[] { "customer_order_id", "changed_at" },
                descending: new[] { false, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_InventoryReservations");

            migrationBuilder.DropTable(
                name: "om_OrderStatusHistories");

            migrationBuilder.DropTable(
                name: "om_CustomerOrderLines");

            migrationBuilder.DropTable(
                name: "om_CustomerOrders");

            migrationBuilder.AlterColumn<string>(
                name: "source_code",
                table: "om_OrderSources",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldNullable: true);
        }
    }
}
