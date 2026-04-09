using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class invert_fixed_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "im_InventoryReservations",
                columns: table => new
                {
                    reservation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_order_line_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    batch_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    reserved_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    released_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    consumed_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
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
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_im_itemBatches_batch_id",
                        column: x => x.batch_id,
                        principalTable: "im_itemBatches",
                        principalColumn: "item_batch_id");
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_om_CustomerOrderLines_customer_order_line_id",
                        column: x => x.customer_order_line_id,
                        principalTable: "om_CustomerOrderLines",
                        principalColumn: "customer_order_line_id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_om_CustomerOrders_customer_order_id",
                        column: x => x.customer_order_id,
                        principalTable: "om_CustomerOrders",
                        principalColumn: "customer_order_id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_im_InventoryReservations_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_InventoryReservations");
        }
    }
}
