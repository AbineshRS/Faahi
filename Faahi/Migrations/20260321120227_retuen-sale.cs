using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class retuensale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "return_quantity",
                table: "im_purchase_listing_details",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "im_purchase_return_header",
                columns: table => new
                {
                    return_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    return_code = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    listing_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    site_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    vendor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    return_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    return_type = table.Column<string>(type: "varchar(20)", nullable: true),
                    supplier_return_ref = table.Column<string>(type: "varchar(50)", nullable: true),
                    reason = table.Column<string>(type: "varchar(400)", nullable: true),
                    sub_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true),
                    notes = table.Column<string>(type: "varchar(400)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_purchase_return_header", x => x.return_id);
                    table.ForeignKey(
                        name: "FK_im_purchase_return_header_ap_Vendors_vendor_id",
                        column: x => x.vendor_id,
                        principalTable: "ap_Vendors",
                        principalColumn: "vendor_id");
                    table.ForeignKey(
                        name: "FK_im_purchase_return_header_st_stores_site_id",
                        column: x => x.site_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateTable(
                name: "im_purchase_return_details_line",
                columns: table => new
                {
                    return_detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    return_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sub_variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_name = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    return_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    unit_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    line_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    batch_no = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    return_reason = table.Column<string>(type: "varchar(200)", nullable: true),
                    product_brand = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    product_title = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    barcode = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    sku = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_purchase_return_details_line", x => x.return_detail_id);
                    table.ForeignKey(
                        name: "FK_im_purchase_return_details_line_im_ProductVariants_sub_variant_id",
                        column: x => x.sub_variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                    table.ForeignKey(
                        name: "FK_im_purchase_return_details_line_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_im_purchase_return_details_line_im_StoreVariantInventory_store_variant_inventory_id",
                        column: x => x.store_variant_inventory_id,
                        principalTable: "im_StoreVariantInventory",
                        principalColumn: "store_variant_inventory_id");
                    table.ForeignKey(
                        name: "FK_im_purchase_return_details_line_im_purchase_return_header_return_id",
                        column: x => x.return_id,
                        principalTable: "im_purchase_return_header",
                        principalColumn: "return_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_purchase_return_details_line_store_variant_inventory_id",
                table: "im_purchase_return_details_line",
                column: "store_variant_inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_purchase_return_details_line_sub_variant_id",
                table: "im_purchase_return_details_line",
                column: "sub_variant_id");

            migrationBuilder.CreateIndex(
                name: "product_id",
                table: "im_purchase_return_details_line",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "return_detail_id",
                table: "im_purchase_return_details_line",
                column: "return_detail_id");

            migrationBuilder.CreateIndex(
                name: "return_id",
                table: "im_purchase_return_details_line",
                column: "return_id");

            migrationBuilder.CreateIndex(
                name: "return_id",
                table: "im_purchase_return_header",
                column: "return_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "site_id",
                table: "im_purchase_return_header",
                column: "site_id");

            migrationBuilder.CreateIndex(
                name: "vendor_id",
                table: "im_purchase_return_header",
                column: "vendor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_purchase_return_details_line");

            migrationBuilder.DropTable(
                name: "im_purchase_return_header");

            migrationBuilder.DropColumn(
                name: "return_quantity",
                table: "im_purchase_listing_details");
        }
    }
}
