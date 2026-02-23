using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class so_sales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "so_SalesHeaders",
                columns: table => new
                {
                    sales_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    payment_term_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    membership_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sales_no = table.Column<long>(type: "bigint", nullable: false),
                    invoice_no = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    purchase_order_no = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    sales_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    doc_type = table.Column<string>(type: "varchar(10)", nullable: true),
                    due_date = table.Column<DateOnly>(type: "date", nullable: true),
                    tax_percent = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    service_charge_percent = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    sales_mode = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    quick_customer = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    reference_no = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    sales_on_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    id_card_no = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    age = table.Column<int>(type: "int", nullable: true),
                    table_no = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    number_of_pax = table.Column<int>(type: "int", nullable: true),
                    doc_currency_code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    base_currency_code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    fx_rate_to_base = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    fx_rate_date = table.Column<DateOnly>(type: "date", nullable: true),
                    fx_source = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    sub_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    service_charge = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    tax_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    grand_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_plastic_bag = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_taxable_value = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_zero_value = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_exempted_value = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_charge_customer = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_plastic_bag_tax = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    sub_total_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_total_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    tax_total_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    grand_total_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_taxable_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_zero_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_exempted_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_charge_customer_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    service_charge_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_plastic_bag_tax_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_charge_bank_marchant = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    transaction_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    amount_paid_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    change_given_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    change_given_doc = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    balance_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    datetime = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_so_SalesHeaders", x => x.sales_id);
                    table.CheckConstraint("CK_so_SalesHeaders_Totals_NonNegative", "sub_total >= 0 AND discount_total >= 0 AND tax_total >= 0 AND grand_total >= 0 AND sub_total_base >= 0 AND discount_total_base >= 0 AND tax_total_base >= 0 AND grand_total_base >= 0");
                    table.ForeignKey(
                        name: "FK_so_SalesHeaders_co_business_company_id",
                        column: x => x.company_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_so_SalesHeaders_st_Parties_customer_id",
                        column: x => x.customer_id,
                        principalTable: "st_Parties",
                        principalColumn: "party_id");
                    table.ForeignKey(
                        name: "FK_so_SalesHeaders_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "so_SalesLines",
                columns: table => new
                {
                    sales_line_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sales_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    barcode = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    product_sku = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    track_expiry = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    item_description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    line_discount_amount = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    stock_item = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    consignment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    consignment_det_id = table.Column<int>(type: "int", nullable: true),
                    consignment_billed = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    batch_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    batch_id_int = table.Column<int>(type: "int", nullable: true),
                    batch_name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    insurance_code = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    doctor_consent = table.Column<string>(type: "char(1)", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    unit_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_percent = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    original_price_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    tax_class = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    returned_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    original_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    doc_currency_code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    base_currency_code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    fx_rate_to_base = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    unit_price_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_amount_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    tax_amount_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    unit_discount_amount_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    line_total_base = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_so_SalesLines", x => x.sales_line_id);
                    table.CheckConstraint("CK_so_SalesLines_Amounts", " unit_price >= 0 AND discount_amount >= 0 AND tax_amount >= 0 AND discount_percent >= 0 AND fx_rate_to_base > 0");
                    table.ForeignKey(
                        name: "FK_so_SalesLines_co_business_company_id",
                        column: x => x.company_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_so_SalesLines_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                    table.ForeignKey(
                        name: "FK_so_SalesLines_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_so_SalesLines_so_SalesHeaders_sales_id",
                        column: x => x.sales_id,
                        principalTable: "so_SalesHeaders",
                        principalColumn: "sales_id");
                    table.ForeignKey(
                        name: "FK_so_SalesLines_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_company_id",
                table: "so_SalesHeaders",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_id",
                table: "so_SalesHeaders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_due_date",
                table: "so_SalesHeaders",
                column: "due_date");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_no",
                table: "so_SalesHeaders",
                column: "invoice_no");

            migrationBuilder.CreateIndex(
                name: "IX_sales_date ",
                table: "so_SalesHeaders",
                column: "sales_date");

            migrationBuilder.CreateIndex(
                name: "IX_sales_id",
                table: "so_SalesHeaders",
                column: "sales_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sales_no",
                table: "so_SalesHeaders",
                column: "sales_no");

            migrationBuilder.CreateIndex(
                name: "IX_store_id",
                table: "so_SalesHeaders",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_barcode",
                table: "so_SalesLines",
                column: "barcode");

            migrationBuilder.CreateIndex(
                name: "IX_company_id",
                table: "so_SalesLines",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_id",
                table: "so_SalesLines",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_sku",
                table: "so_SalesLines",
                column: "product_sku");

            migrationBuilder.CreateIndex(
                name: "IX_sales_id",
                table: "so_SalesLines",
                column: "sales_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_line_id",
                table: "so_SalesLines",
                column: "sales_line_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_store_id",
                table: "so_SalesLines",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_variant_id",
                table: "so_SalesLines",
                column: "variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "so_SalesLines");

            migrationBuilder.DropTable(
                name: "so_SalesHeaders");
        }
    }
}
