using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class salesss_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "im_GoodsReceiptHeaders",
                columns: table => new
                {
                    goods_receipt_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    purchase_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    purchase_entry_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    warehouse_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    received_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    posted_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    cancelled_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    receipt_no = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    receipt_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    supplier_invoice_no = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    supplier_do_no = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    supplier_ref_no = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    subtotal = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    total_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    posted_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    remarks = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    is_posted = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false, defaultValue: "F"),
                    is_cancelled = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false, defaultValue: "F")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_GoodsReceiptHeaders", x => x.goods_receipt_id);
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptHeaders_ap_Vendors_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "ap_Vendors",
                        principalColumn: "vendor_id");
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptHeaders_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptHeaders_im_purchase_listing_purchase_order_id",
                        column: x => x.purchase_order_id,
                        principalTable: "im_purchase_listing",
                        principalColumn: "listing_id");
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptHeaders_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "im_GoodsReceiptLineBatches",
                columns: table => new
                {
                    goods_receipt_line_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    batch_no = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    manufacture_date = table.Column<DateOnly>(type: "date", nullable: true),
                    received_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    free_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    rejected_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    accepted_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    unit_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    net_unit_cost = table.Column<decimal>(type: "decimal(18,6)", nullable: false, defaultValue: 0m),
                    remarks = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_GoodsReceiptLineBatches", x => x.goods_receipt_line_id);
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptLineBatches_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptLineBatches_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptLineBatches_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "im_GoodsReceiptLines",
                columns: table => new
                {
                    goods_receipt_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    uom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_code = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    line_no = table.Column<int>(type: "int", nullable: false),
                    ordered_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    received_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    free_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    rejected_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    accepted_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    unit_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    discount_percent = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    tax_percent = table.Column<decimal>(type: "decimal(9,4)", nullable: false, defaultValue: 0m),
                    line_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    net_unit_cost = table.Column<decimal>(type: "decimal(18,6)", nullable: false, defaultValue: 0m),
                    net_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    batch_no = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    manufacture_date = table.Column<DateOnly>(type: "date", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    im_GoodsReceiptHeadersgoods_receipt_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_GoodsReceiptLines", x => x.goods_receipt_id);
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptLines_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptLines_im_GoodsReceiptHeaders_im_GoodsReceiptHeadersgoods_receipt_id",
                        column: x => x.im_GoodsReceiptHeadersgoods_receipt_id,
                        principalTable: "im_GoodsReceiptHeaders",
                        principalColumn: "goods_receipt_id");
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptLines_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptLines_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptLines_im_UnitsOfMeasures_uom_id",
                        column: x => x.uom_id,
                        principalTable: "im_UnitsOfMeasures",
                        principalColumn: "uom_id");
                    table.ForeignKey(
                        name: "FK_im_GoodsReceiptLines_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_goods_receipt_id",
                table: "im_GoodsReceiptHeaders",
                column: "goods_receipt_id",
                unique: true,
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptHeaders_purchase_order_id",
                table: "im_GoodsReceiptHeaders",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptHeaders_status",
                table: "im_GoodsReceiptHeaders",
                columns: new[] { "business_id", "store_id", "status", "receipt_date" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptHeaders_store_id",
                table: "im_GoodsReceiptHeaders",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptHeaders_supplier",
                table: "im_GoodsReceiptHeaders",
                columns: new[] { "supplier_id", "receipt_date" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLineBatches_line",
                table: "im_GoodsReceiptLineBatches",
                column: "goods_receipt_line_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLineBatches_store_id",
                table: "im_GoodsReceiptLineBatches",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLineBatches_variant_batch",
                table: "im_GoodsReceiptLineBatches",
                columns: new[] { "business_id", "store_id", "variant_id", "batch_no", "expiry_date" });

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLineBatches_variant_id",
                table: "im_GoodsReceiptLineBatches",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_goods_receipt_id",
                table: "im_GoodsReceiptLines",
                column: "goods_receipt_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLines_im_GoodsReceiptHeadersgoods_receipt_id",
                table: "im_GoodsReceiptLines",
                column: "im_GoodsReceiptHeadersgoods_receipt_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLines_product_id",
                table: "im_GoodsReceiptLines",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLines_receipt",
                table: "im_GoodsReceiptLines",
                column: "line_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLines_store_id",
                table: "im_GoodsReceiptLines",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLines_uom_id",
                table: "im_GoodsReceiptLines",
                column: "uom_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLines_variant",
                table: "im_GoodsReceiptLines",
                columns: new[] { "business_id", "store_id", "variant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLines_variant_id",
                table: "im_GoodsReceiptLines",
                column: "variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_GoodsReceiptLineBatches");

            migrationBuilder.DropTable(
                name: "im_GoodsReceiptLines");

            migrationBuilder.DropTable(
                name: "im_GoodsReceiptHeaders");
        }
    }
}
