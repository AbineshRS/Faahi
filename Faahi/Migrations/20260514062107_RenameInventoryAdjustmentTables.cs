using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class RenameInventoryAdjustmentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_adjustment_header_st_stores_store_id",
                table: "inventory_adjustment_header");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_adjustment_lines_im_ProductVariants_variant_id",
                table: "inventory_adjustment_lines");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_adjustment_lines_im_Products_product_id",
                table: "inventory_adjustment_lines");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_adjustment_lines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "inventory_adjustment_lines");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_adjustment_lines_inventory_adjustment_header_adjustment_id",
                table: "inventory_adjustment_lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory_adjustment_lines",
                table: "inventory_adjustment_lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory_adjustment_header",
                table: "inventory_adjustment_header");

            migrationBuilder.RenameTable(
                name: "inventory_adjustment_lines",
                newName: "im_inventory_adjustment_lines");

            migrationBuilder.RenameTable(
                name: "inventory_adjustment_header",
                newName: "im_inventory_adjustment_header");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_adjustment_lines_variant_id",
                table: "im_inventory_adjustment_lines",
                newName: "IX_im_inventory_adjustment_lines_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_adjustment_lines_store_variant_inventory_id",
                table: "im_inventory_adjustment_lines",
                newName: "IX_im_inventory_adjustment_lines_store_variant_inventory_id");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_adjustment_lines_product_id",
                table: "im_inventory_adjustment_lines",
                newName: "IX_im_inventory_adjustment_lines_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_adjustment_lines_adjustment_id",
                table: "im_inventory_adjustment_lines",
                newName: "IX_im_inventory_adjustment_lines_adjustment_id");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_adjustment_header_store_id",
                table: "im_inventory_adjustment_header",
                newName: "IX_im_inventory_adjustment_header_store_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_im_inventory_adjustment_lines",
                table: "im_inventory_adjustment_lines",
                column: "adjustment_detail_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_im_inventory_adjustment_header",
                table: "im_inventory_adjustment_header",
                column: "adjustment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_inventory_adjustment_header_st_stores_store_id",
                table: "im_inventory_adjustment_header",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_im_inventory_adjustment_lines_im_ProductVariants_variant_id",
                table: "im_inventory_adjustment_lines",
                column: "variant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_inventory_adjustment_lines_im_Products_product_id",
                table: "im_inventory_adjustment_lines",
                column: "product_id",
                principalTable: "im_Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_im_inventory_adjustment_lines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "im_inventory_adjustment_lines",
                column: "store_variant_inventory_id",
                principalTable: "im_StoreVariantInventory",
                principalColumn: "store_variant_inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_inventory_adjustment_lines_im_inventory_adjustment_header_adjustment_id",
                table: "im_inventory_adjustment_lines",
                column: "adjustment_id",
                principalTable: "im_inventory_adjustment_header",
                principalColumn: "adjustment_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_inventory_adjustment_header_st_stores_store_id",
                table: "im_inventory_adjustment_header");

            migrationBuilder.DropForeignKey(
                name: "FK_im_inventory_adjustment_lines_im_ProductVariants_variant_id",
                table: "im_inventory_adjustment_lines");

            migrationBuilder.DropForeignKey(
                name: "FK_im_inventory_adjustment_lines_im_Products_product_id",
                table: "im_inventory_adjustment_lines");

            migrationBuilder.DropForeignKey(
                name: "FK_im_inventory_adjustment_lines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "im_inventory_adjustment_lines");

            migrationBuilder.DropForeignKey(
                name: "FK_im_inventory_adjustment_lines_im_inventory_adjustment_header_adjustment_id",
                table: "im_inventory_adjustment_lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_im_inventory_adjustment_lines",
                table: "im_inventory_adjustment_lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_im_inventory_adjustment_header",
                table: "im_inventory_adjustment_header");

            migrationBuilder.RenameTable(
                name: "im_inventory_adjustment_lines",
                newName: "inventory_adjustment_lines");

            migrationBuilder.RenameTable(
                name: "im_inventory_adjustment_header",
                newName: "inventory_adjustment_header");

            migrationBuilder.RenameIndex(
                name: "IX_im_inventory_adjustment_lines_variant_id",
                table: "inventory_adjustment_lines",
                newName: "IX_inventory_adjustment_lines_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_inventory_adjustment_lines_store_variant_inventory_id",
                table: "inventory_adjustment_lines",
                newName: "IX_inventory_adjustment_lines_store_variant_inventory_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_inventory_adjustment_lines_product_id",
                table: "inventory_adjustment_lines",
                newName: "IX_inventory_adjustment_lines_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_inventory_adjustment_lines_adjustment_id",
                table: "inventory_adjustment_lines",
                newName: "IX_inventory_adjustment_lines_adjustment_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_inventory_adjustment_header_store_id",
                table: "inventory_adjustment_header",
                newName: "IX_inventory_adjustment_header_store_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory_adjustment_lines",
                table: "inventory_adjustment_lines",
                column: "adjustment_detail_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory_adjustment_header",
                table: "inventory_adjustment_header",
                column: "adjustment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_adjustment_header_st_stores_store_id",
                table: "inventory_adjustment_header",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_adjustment_lines_im_ProductVariants_variant_id",
                table: "inventory_adjustment_lines",
                column: "variant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_adjustment_lines_im_Products_product_id",
                table: "inventory_adjustment_lines",
                column: "product_id",
                principalTable: "im_Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_adjustment_lines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "inventory_adjustment_lines",
                column: "store_variant_inventory_id",
                principalTable: "im_StoreVariantInventory",
                principalColumn: "store_variant_inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_adjustment_lines_inventory_adjustment_header_adjustment_id",
                table: "inventory_adjustment_lines",
                column: "adjustment_id",
                principalTable: "inventory_adjustment_header",
                principalColumn: "adjustment_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
