using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class lot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "im_InventoryLedger",
                columns: table => new
                {
                    ledger_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    transaction_type = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    unit_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    currency = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    source_doc_type = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    source_doc_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    source_doc_line = table.Column<int>(type: "int", nullable: true),
                    reference_note = table.Column<string>(type: "varchar(400)", nullable: true),
                    cost_method = table.Column<string>(type: "varchar(50)", nullable: true),
                    transaction_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_InventoryLedger", x => x.ledger_id);
                });

            migrationBuilder.CreateTable(
                name: "im_Lots",
                columns: table => new
                {
                    lot_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    lot_code = table.Column<string>(type: "varchar(80)", nullable: true),
                    mfg_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    note = table.Column<string>(type: "varchar(200)", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    committed_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    consign_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    promo_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    is_on_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_Lots", x => x.lot_id);
                });

            migrationBuilder.CreateTable(
                name: "im_SellerInventory",
                columns: table => new
                {
                    seller_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    stock_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    reorder_level = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    sales_count = table.Column<int>(type: "int", nullable: true),
                    Rack_no = table.Column<string>(type: "varchar(25)", nullable: true),
                    bin_number = table.Column<string>(type: "varchar(25)", nullable: true),
                    Consignment_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    committed_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    on_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    allow_Inter_Location_Transfer = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    sales_on_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_SellerInventory", x => x.seller_inventory_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryLedger_source_doc_id",
                table: "im_InventoryLedger",
                column: "source_doc_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryLedger_source_doc_type",
                table: "im_InventoryLedger",
                column: "source_doc_type");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryLedger_transaction_date",
                table: "im_InventoryLedger",
                column: "transaction_date");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryLedger_user_id",
                table: "im_InventoryLedger",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryLedger_variant_id",
                table: "im_InventoryLedger",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_Lots_expiry_date",
                table: "im_Lots",
                column: "expiry_date");

            migrationBuilder.CreateIndex(
                name: "IX_im_Lots_is_on_hold",
                table: "im_Lots",
                column: "is_on_hold");

            migrationBuilder.CreateIndex(
                name: "IX_im_Lots_lot_code",
                table: "im_Lots",
                column: "lot_code");

            migrationBuilder.CreateIndex(
                name: "IX_im_Lots_variant_id",
                table: "im_Lots",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_SellerInventory_store_id",
                table: "im_SellerInventory",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_SellerInventory_user_id",
                table: "im_SellerInventory",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_SellerInventory_variant_id",
                table: "im_SellerInventory",
                column: "variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_InventoryLedger");

            migrationBuilder.DropTable(
                name: "im_Lots");

            migrationBuilder.DropTable(
                name: "im_SellerInventory");
        }
    }
}
