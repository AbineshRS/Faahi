using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class add_stock_transfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "transfer_line_id",
                table: "im_InventoryTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "im_StockTransferHeader",
                columns: table => new
                {
                    transfer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transfer_code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    from_store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    to_store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    approved_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    approved_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_StockTransferHeader", x => x.transfer_id);
                    table.ForeignKey(
                        name: "FK_im_StockTransferHeader_co_business_transfer_id",
                        column: x => x.transfer_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_im_StockTransferHeader_st_stores_from_store_id",
                        column: x => x.from_store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_im_StockTransferHeader_st_stores_to_store_id",
                        column: x => x.to_store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "im_StockTransferLines",
                columns: table => new
                {
                    transfer_line_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transfer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    item_batch_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    batch_number = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    average_cost = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    line_total = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_StockTransferLines", x => x.transfer_line_id);
                    table.ForeignKey(
                        name: "FK_im_StockTransferLines_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_im_StockTransferLines_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_im_StockTransferLines_im_StockTransferHeader_transfer_id",
                        column: x => x.transfer_id,
                        principalTable: "im_StockTransferHeader",
                        principalColumn: "transfer_id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_im_StockTransferLines_im_StoreVariantInventory_store_variant_inventory_id",
                        column: x => x.store_variant_inventory_id,
                        principalTable: "im_StoreVariantInventory",
                        principalColumn: "store_variant_inventory_id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_im_StockTransferLines_im_itemBatches_item_batch_id",
                        column: x => x.item_batch_id,
                        principalTable: "im_itemBatches",
                        principalColumn: "item_batch_id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryTransactions_transfer_line_id",
                table: "im_InventoryTransactions",
                column: "transfer_line_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferHeader_from_store_id",
                table: "im_StockTransferHeader",
                column: "from_store_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferHeader_to_store_id",
                table: "im_StockTransferHeader",
                column: "to_store_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferLines_item_batch_id",
                table: "im_StockTransferLines",
                column: "item_batch_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferLines_product_id",
                table: "im_StockTransferLines",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferLines_store_variant_inventory_id",
                table: "im_StockTransferLines",
                column: "store_variant_inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferLines_transfer_id",
                table: "im_StockTransferLines",
                column: "transfer_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferLines_variant_id",
                table: "im_StockTransferLines",
                column: "variant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_InventoryTransactions_im_StockTransferLines_transfer_line_id",
                table: "im_InventoryTransactions",
                column: "transfer_line_id",
                principalTable: "im_StockTransferLines",
                principalColumn: "transfer_line_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_InventoryTransactions_im_StockTransferLines_transfer_line_id",
                table: "im_InventoryTransactions");

            migrationBuilder.DropTable(
                name: "im_StockTransferLines");

            migrationBuilder.DropTable(
                name: "im_StockTransferHeader");

            migrationBuilder.DropIndex(
                name: "IX_im_InventoryTransactions_transfer_line_id",
                table: "im_InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "transfer_line_id",
                table: "im_InventoryTransactions");
        }
    }
}
