using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class adjeustment_inv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "store_inventory_ad_header",
                columns: table => new
                {
                    store_inventory_ad_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    inventory_code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total_items = table.Column<int>(type: "int", nullable: false),
                    total_negative_value = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    total_positive_value = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    total_item_adjusted = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    total_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    status = table.Column<string>(type: "nvarchar(30)", nullable: false, defaultValue: "PENDING")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_store_inventory_ad_header", x => x.store_inventory_ad_id);
                    table.ForeignKey(
                        name: "FK_store_inventory_ad_header_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "temp_stock_ad_lines",
                columns: table => new
                {
                    tem_ad_line_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    barcode = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    sku = table.Column<string>(type: "varchar(50)", nullable: true),
                    title = table.Column<string>(type: "varchar(200)", nullable: true),
                    batch_number = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    expiry_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    counted_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    adjusted_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    average_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    total_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temp_stock_ad_lines", x => x.tem_ad_line_id);
                    table.ForeignKey(
                        name: "FK_temp_stock_ad_lines_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                    table.ForeignKey(
                        name: "FK_temp_stock_ad_lines_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_temp_stock_ad_lines_im_StoreVariantInventory_store_variant_inventory_id",
                        column: x => x.store_variant_inventory_id,
                        principalTable: "im_StoreVariantInventory",
                        principalColumn: "store_variant_inventory_id");
                    table.ForeignKey(
                        name: "FK_temp_stock_ad_lines_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "store_inventory_ad_details",
                columns: table => new
                {
                    store_inventory_detail_ad_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    store_inventory_ad_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    barcode = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    batch_number = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    expiry_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    system_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    adjusted_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    average_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    total_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    status = table.Column<string>(type: "nvarchar(30)", nullable: false, defaultValue: "PENDING")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_store_inventory_ad_details", x => x.store_inventory_detail_ad_id);
                    table.ForeignKey(
                        name: "FK_store_inventory_ad_details_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                    table.ForeignKey(
                        name: "FK_store_inventory_ad_details_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_store_inventory_ad_details_im_StoreVariantInventory_store_variant_inventory_id",
                        column: x => x.store_variant_inventory_id,
                        principalTable: "im_StoreVariantInventory",
                        principalColumn: "store_variant_inventory_id");
                    table.ForeignKey(
                        name: "FK_store_inventory_ad_details_store_inventory_ad_header_store_inventory_ad_id",
                        column: x => x.store_inventory_ad_id,
                        principalTable: "store_inventory_ad_header",
                        principalColumn: "store_inventory_ad_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_store_inventory_ad_details_product_id",
                table: "store_inventory_ad_details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_store_inventory_ad_details_store_inventory_ad_id",
                table: "store_inventory_ad_details",
                column: "store_inventory_ad_id");

            migrationBuilder.CreateIndex(
                name: "IX_store_inventory_ad_details_store_variant_inventory_id",
                table: "store_inventory_ad_details",
                column: "store_variant_inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_store_inventory_ad_details_variant_id",
                table: "store_inventory_ad_details",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "store_inventory_ad_header",
                table: "store_inventory_ad_details",
                column: "store_inventory_detail_ad_id");

            migrationBuilder.CreateIndex(
                name: "IX_store_inventory_ad_header_store_id",
                table: "store_inventory_ad_header",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "store_inventory_ad_header",
                table: "store_inventory_ad_header",
                columns: new[] { "store_inventory_ad_id", "store_id", "inventory_code" });

            migrationBuilder.CreateIndex(
                name: "IX_temp_stock_ad_lines_product_id",
                table: "temp_stock_ad_lines",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_temp_stock_ad_lines_store_id",
                table: "temp_stock_ad_lines",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_temp_stock_ad_lines_store_variant_inventory_id",
                table: "temp_stock_ad_lines",
                column: "store_variant_inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_temp_stock_ad_lines_variant_id",
                table: "temp_stock_ad_lines",
                column: "variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "store_inventory_ad_details");

            migrationBuilder.DropTable(
                name: "temp_stock_ad_lines");

            migrationBuilder.DropTable(
                name: "store_inventory_ad_header");
        }
    }
}
