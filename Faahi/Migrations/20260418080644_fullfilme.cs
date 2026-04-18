using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class fullfilme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "om_FulfillmentOrders",
                columns: table => new
                {
                    fulfillment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fulfillment_no = table.Column<long>(type: "bigint", nullable: false),
                    picked_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    packed_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    pick_started_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    pick_completed_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    packed_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    ready_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    cancel_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    out_for_delivery_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    failed_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    returned_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    collected_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    proof_of_delivery_image_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    failure_reason = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    fulfillment_status = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_om_FulfillmentOrders", x => x.fulfillment_id);
                    table.ForeignKey(
                        name: "FK_om_FulfillmentOrders_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_om_FulfillmentOrders_om_CustomerOrders_customer_order_id",
                        column: x => x.customer_order_id,
                        principalTable: "om_CustomerOrders",
                        principalColumn: "customer_order_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_om_FulfillmentOrders_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "om_FulfillmentLines",
                columns: table => new
                {
                    fulfillment_line_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fulfillment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_order_line_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    batch_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    line_no = table.Column<int>(type: "int", nullable: true),
                    ordered_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    reserved_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    picked_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    packed_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    delivered_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    returned_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    rejected_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    remarks = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    line_status = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    om_FulfillmentOrdersfulfillment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_om_FulfillmentLines", x => x.fulfillment_line_id);
                    table.ForeignKey(
                        name: "FK_om_FulfillmentLines_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_om_FulfillmentLines_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_om_FulfillmentLines_im_StoreVariantInventory_store_variant_inventory_id",
                        column: x => x.store_variant_inventory_id,
                        principalTable: "im_StoreVariantInventory",
                        principalColumn: "store_variant_inventory_id");
                    table.ForeignKey(
                        name: "FK_om_FulfillmentLines_im_itemBatches_batch_id",
                        column: x => x.batch_id,
                        principalTable: "im_itemBatches",
                        principalColumn: "item_batch_id");
                    table.ForeignKey(
                        name: "FK_om_FulfillmentLines_om_FulfillmentOrders_fulfillment_id",
                        column: x => x.fulfillment_id,
                        principalTable: "om_FulfillmentOrders",
                        principalColumn: "fulfillment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_om_FulfillmentLines_om_FulfillmentOrders_om_FulfillmentOrdersfulfillment_id",
                        column: x => x.om_FulfillmentOrdersfulfillment_id,
                        principalTable: "om_FulfillmentOrders",
                        principalColumn: "fulfillment_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_line_statuss",
                table: "om_FulfillmentLines",
                columns: new[] { "rejected_qty", "line_status" });

            migrationBuilder.CreateIndex(
                name: "IX_om_FulfillmentLines",
                table: "om_FulfillmentLines",
                columns: new[] { "fulfillment_line_id", "fulfillment_id", "product_id", "store_variant_inventory_id", "batch_id" });

            migrationBuilder.CreateIndex(
                name: "IX_om_FulfillmentLines_batch_id",
                table: "om_FulfillmentLines",
                column: "batch_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_FulfillmentLines_fulfillment_id",
                table: "om_FulfillmentLines",
                column: "fulfillment_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_FulfillmentLines_om_FulfillmentOrdersfulfillment_id",
                table: "om_FulfillmentLines",
                column: "om_FulfillmentOrdersfulfillment_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_FulfillmentLines_product_id",
                table: "om_FulfillmentLines",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_FulfillmentLines_store_variant_inventory_id",
                table: "om_FulfillmentLines",
                column: "store_variant_inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_FulfillmentLines_variant_id",
                table: "om_FulfillmentLines",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_fulfillment_no",
                table: "om_FulfillmentOrders",
                column: "fulfillment_no");

            migrationBuilder.CreateIndex(
                name: "IX_om_FulfillmentOrders",
                table: "om_FulfillmentOrders",
                columns: new[] { "fulfillment_id", "business_id", "store_id", "customer_order_id" });

            migrationBuilder.CreateIndex(
                name: "IX_om_FulfillmentOrders_business_id",
                table: "om_FulfillmentOrders",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_FulfillmentOrders_customer_order_id",
                table: "om_FulfillmentOrders",
                column: "customer_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_om_FulfillmentOrders_store_id",
                table: "om_FulfillmentOrders",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders",
                table: "om_FulfillmentOrders",
                columns: new[] { "picked_by", "pick_started_at", "pick_completed_at", "packed_at", "ready_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "om_FulfillmentLines");

            migrationBuilder.DropTable(
                name: "om_FulfillmentOrders");
        }
    }
}
