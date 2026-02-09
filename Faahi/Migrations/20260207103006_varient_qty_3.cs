using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class varient_qty_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "im_InventoryTransactions",
                columns: table => new
                {
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    listing_code = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    listing_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    vendor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateOnly>(type: "date", nullable: true),
                    payment_mode = table.Column<string>(type: "varchar(20)", nullable: true),
                    purchase_type = table.Column<string>(type: "varchar(20)", nullable: true),
                    supplier_invoice_no = table.Column<string>(type: "varchar(50)", nullable: true),
                    supplier_invoice_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    sub_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    freight_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    other_expenses = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true),
                    doc_total = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    local_referance = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_InventoryTransactions", x => x.transaction_id);
                    table.CheckConstraint("CK_im_InventoryTransactions_doc_totall", "[doc_total]>=0");
                    table.CheckConstraint("CK_im_InventoryTransactions_sub_total", "[sub_total]>=0");
                    table.ForeignKey(
                        name: "FK_im_InventoryTransactions_ap_Vendors_vendor_id",
                        column: x => x.vendor_id,
                        principalTable: "ap_Vendors",
                        principalColumn: "vendor_id");
                    table.ForeignKey(
                        name: "FK_im_InventoryTransactions_im_purchase_listing_listing_id",
                        column: x => x.listing_id,
                        principalTable: "im_purchase_listing",
                        principalColumn: "listing_id");
                    table.ForeignKey(
                        name: "FK_im_InventoryTransactions_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryTransactions_store_id",
                table: "im_InventoryTransactions",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryTransactions_vendor_id",
                table: "im_InventoryTransactions",
                column: "vendor_id");

            migrationBuilder.CreateIndex(
                name: "IX_listing_code",
                table: "im_InventoryTransactions",
                column: "listing_code");

            migrationBuilder.CreateIndex(
                name: "IX_listing_id",
                table: "im_InventoryTransactions",
                column: "listing_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_id",
                table: "im_InventoryTransactions",
                column: "transaction_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_InventoryTransactions");
        }
    }
}
