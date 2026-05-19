using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class random_reject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "im_random_Stock_reject",
                columns: table => new
                {
                    stock_reject_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    adjustment_detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    barcode = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    sku = table.Column<string>(type: "varchar(50)", nullable: true),
                    title = table.Column<string>(type: "varchar(200)", nullable: true),
                    batch_number = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    expiry_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    track_expiry = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    line_no = table.Column<int>(type: "int", nullable: false),
                    rejected_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    system_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    counted_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    adjusted_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    average_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    total_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    is_posted = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false, defaultValue: "T"),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    status = table.Column<string>(type: "nvarchar(30)", nullable: false, defaultValue: "POSTED")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_random_Stock_reject", x => x.stock_reject_id);
                    table.ForeignKey(
                        name: "FK_im_random_Stock_reject_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                    table.ForeignKey(
                        name: "FK_im_random_Stock_reject_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_im_random_Stock_reject_im_StoreVariantInventory_store_variant_inventory_id",
                        column: x => x.store_variant_inventory_id,
                        principalTable: "im_StoreVariantInventory",
                        principalColumn: "store_variant_inventory_id");
                    table.ForeignKey(
                        name: "FK_im_random_Stock_reject_im_inventory_adjustment_lines_adjustment_detail_id",
                        column: x => x.adjustment_detail_id,
                        principalTable: "im_inventory_adjustment_lines",
                        principalColumn: "adjustment_detail_id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_random_Stock_reject_adjustment_detail_id",
                table: "im_random_Stock_reject",
                column: "adjustment_detail_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_random_Stock_reject_product_id",
                table: "im_random_Stock_reject",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_random_Stock_reject_store_variant_inventory_id",
                table: "im_random_Stock_reject",
                column: "store_variant_inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_random_Stock_reject_variant_id",
                table: "im_random_Stock_reject",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_adjustment_lines",
                table: "im_random_Stock_reject",
                columns: new[] { "stock_reject_id", "product_id", "variant_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_random_Stock_reject");
        }
    }
}
