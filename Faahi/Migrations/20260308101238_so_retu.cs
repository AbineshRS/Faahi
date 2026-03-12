using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class so_retu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "return_qty",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.CreateTable(
                name: "so_SalesReturnHeaders",
                columns: table => new
                {
                    sales_return_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sales_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    payment_term_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    return_no = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    return_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    doc_type = table.Column<string>(type: "varchar(10)", nullable: true),
                    return_type = table.Column<string>(type: "varchar(10)", nullable: true),
                    return_reason = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    doc_currency_code = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    base_currency_code = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    fx_rate_to_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    sub_total = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    discount_total = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    tax_total = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    grand_total = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    sub_total_base = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    discount_total_base = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    tax_total_base = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    grand_total_base = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    notes = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(130)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_so_SalesReturnHeaders", x => x.sales_return_id);
                    table.ForeignKey(
                        name: "FK_so_SalesReturnHeaders_ar_Customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "ar_Customers",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK_so_SalesReturnHeaders_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_so_SalesReturnHeaders_so_Payment_Types_payment_term_id",
                        column: x => x.payment_term_id,
                        principalTable: "so_Payment_Types",
                        principalColumn: "payment_type_id");
                    table.ForeignKey(
                        name: "FK_so_SalesReturnHeaders_so_SalesHeaders_sales_id",
                        column: x => x.sales_id,
                        principalTable: "so_SalesHeaders",
                        principalColumn: "sales_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_so_SalesReturnHeaders_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "so_SalesReturnLines",
                columns: table => new
                {
                    sales_return_line_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sales_return_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    batch_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    barcode = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    product_sku = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    track_expiry = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    item_description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    return_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    unit_price = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    discount_percent = table.Column<decimal>(type: "decimal(6,2)", nullable: false, defaultValue: 0m),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    line_total = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    unit_price_base = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    discount_amount_base = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    tax_amount_base = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    line_total_base = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    tax_class = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    return_reason = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    line_status = table.Column<string>(type: "varchar(10)", nullable: true),
                    so_SalesReturnHeaderssales_return_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_so_SalesReturnLines", x => x.sales_return_line_id);
                    table.ForeignKey(
                        name: "FK_so_SalesReturnLines_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_so_SalesReturnLines_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                    table.ForeignKey(
                        name: "FK_so_SalesReturnLines_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_so_SalesReturnLines_im_StoreVariantInventory_store_variant_inventory_id",
                        column: x => x.store_variant_inventory_id,
                        principalTable: "im_StoreVariantInventory",
                        principalColumn: "store_variant_inventory_id");
                    table.ForeignKey(
                        name: "FK_so_SalesReturnLines_im_itemBatches_batch_id",
                        column: x => x.batch_id,
                        principalTable: "im_itemBatches",
                        principalColumn: "item_batch_id");
                    table.ForeignKey(
                        name: "FK_so_SalesReturnLines_so_SalesReturnHeaders_sales_return_id",
                        column: x => x.sales_return_id,
                        principalTable: "so_SalesReturnHeaders",
                        principalColumn: "sales_return_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_so_SalesReturnLines_so_SalesReturnHeaders_so_SalesReturnHeaderssales_return_id",
                        column: x => x.so_SalesReturnHeaderssales_return_id,
                        principalTable: "so_SalesReturnHeaders",
                        principalColumn: "sales_return_id");
                    table.ForeignKey(
                        name: "FK_so_SalesReturnLines_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_business_id",
                table: "so_SalesReturnHeaders",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_return_no",
                table: "so_SalesReturnHeaders",
                column: "return_no");

            migrationBuilder.CreateIndex(
                name: "IX_sales_id",
                table: "so_SalesReturnHeaders",
                column: "sales_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_return_id",
                table: "so_SalesReturnHeaders",
                column: "sales_return_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_so_SalesReturnHeaders_customer_id",
                table: "so_SalesReturnHeaders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_so_SalesReturnHeaders_payment_term_id",
                table: "so_SalesReturnHeaders",
                column: "payment_term_id");

            migrationBuilder.CreateIndex(
                name: "IX_store_id",
                table: "so_SalesReturnHeaders",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_business_id",
                table: "so_SalesReturnLines",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_return_id",
                table: "so_SalesReturnLines",
                column: "sales_return_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_return_line_id",
                table: "so_SalesReturnLines",
                column: "sales_return_line_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_so_SalesReturnLines_batch_id",
                table: "so_SalesReturnLines",
                column: "batch_id");

            migrationBuilder.CreateIndex(
                name: "IX_so_SalesReturnLines_product_id",
                table: "so_SalesReturnLines",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_so_SalesReturnLines_so_SalesReturnHeaderssales_return_id",
                table: "so_SalesReturnLines",
                column: "so_SalesReturnHeaderssales_return_id");

            migrationBuilder.CreateIndex(
                name: "IX_so_SalesReturnLines_store_id",
                table: "so_SalesReturnLines",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_so_SalesReturnLines_store_variant_inventory_id",
                table: "so_SalesReturnLines",
                column: "store_variant_inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_so_SalesReturnLines_variant_id",
                table: "so_SalesReturnLines",
                column: "variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "so_SalesReturnLines");

            migrationBuilder.DropTable(
                name: "so_SalesReturnHeaders");

            migrationBuilder.DropColumn(
                name: "return_qty",
                table: "so_SalesLines");
        }
    }
}
