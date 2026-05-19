using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class adjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventory_adjustment_header",
                columns: table => new
                {
                    adjustment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    adjustment_code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    adjustment_type = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    total_items = table.Column<int>(type: "int", nullable: false),
                    total_negative_value = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    total_positive_value = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    total_adjustment_value = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    approved_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    transaction_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    approved_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_posted = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false, defaultValue: "F"),
                    status = table.Column<string>(type: "nvarchar(30)", nullable: false, defaultValue: "PENDING")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_adjustment_header", x => x.adjustment_id);
                    table.ForeignKey(
                        name: "FK_inventory_adjustment_header_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventory_adjustment_lines",
                columns: table => new
                {
                    adjustment_detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    adjustment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    barcode = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    batch_number = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    expiry_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    track_expiry = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    system_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    counted_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    adjusted_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    average_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    total_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    is_posted = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false, defaultValue: "F"),
                    status = table.Column<string>(type: "nvarchar(30)", nullable: false, defaultValue: "PENDING")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_adjustment_lines", x => x.adjustment_detail_id);
                    table.ForeignKey(
                        name: "FK_inventory_adjustment_lines_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                    table.ForeignKey(
                        name: "FK_inventory_adjustment_lines_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_adjustment_lines_im_StoreVariantInventory_store_variant_inventory_id",
                        column: x => x.store_variant_inventory_id,
                        principalTable: "im_StoreVariantInventory",
                        principalColumn: "store_variant_inventory_id");
                    table.ForeignKey(
                        name: "FK_inventory_adjustment_lines_inventory_adjustment_header_adjustment_id",
                        column: x => x.adjustment_id,
                        principalTable: "inventory_adjustment_header",
                        principalColumn: "adjustment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_inventory_adjustment_header",
                table: "inventory_adjustment_header",
                columns: new[] { "adjustment_id", "adjustment_code", "store_id" });

            migrationBuilder.CreateIndex(
                name: "IX_inventory_adjustment_header_store_id",
                table: "inventory_adjustment_header",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_adjustment_lines",
                table: "inventory_adjustment_lines",
                columns: new[] { "adjustment_detail_id", "product_id", "variant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_inventory_adjustment_lines_adjustment_id",
                table: "inventory_adjustment_lines",
                column: "adjustment_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_adjustment_lines_product_id",
                table: "inventory_adjustment_lines",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_adjustment_lines_store_variant_inventory_id",
                table: "inventory_adjustment_lines",
                column: "store_variant_inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_adjustment_lines_variant_id",
                table: "inventory_adjustment_lines",
                column: "variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventory_adjustment_lines");

            migrationBuilder.DropTable(
                name: "inventory_adjustment_header");
        }
    }
}
