using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class adjeustment_inv_1_tab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_store_inventory_ad_details_im_ProductVariants_variant_id",
                table: "store_inventory_ad_details");

            migrationBuilder.DropForeignKey(
                name: "FK_store_inventory_ad_details_im_Products_product_id",
                table: "store_inventory_ad_details");

            migrationBuilder.DropForeignKey(
                name: "FK_store_inventory_ad_details_im_StoreVariantInventory_store_variant_inventory_id",
                table: "store_inventory_ad_details");

            migrationBuilder.DropForeignKey(
                name: "FK_store_inventory_ad_details_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "store_inventory_ad_details");

            migrationBuilder.DropForeignKey(
                name: "FK_store_inventory_ad_details_store_inventory_ad_header_store_inventory_ad_id",
                table: "store_inventory_ad_details");

            migrationBuilder.DropForeignKey(
                name: "FK_store_inventory_ad_header_im_inventory_adjustment_header_adjustment_id",
                table: "store_inventory_ad_header");

            migrationBuilder.DropForeignKey(
                name: "FK_store_inventory_ad_header_st_stores_store_id",
                table: "store_inventory_ad_header");

            migrationBuilder.DropPrimaryKey(
                name: "PK_store_inventory_ad_header",
                table: "store_inventory_ad_header");

            migrationBuilder.DropPrimaryKey(
                name: "PK_store_inventory_ad_details",
                table: "store_inventory_ad_details");

            migrationBuilder.RenameTable(
                name: "store_inventory_ad_header",
                newName: "im_store_inventory_ad_header");

            migrationBuilder.RenameTable(
                name: "store_inventory_ad_details",
                newName: "im_store_inventory_ad_details");

            migrationBuilder.RenameIndex(
                name: "IX_store_inventory_ad_header_store_id",
                table: "im_store_inventory_ad_header",
                newName: "IX_im_store_inventory_ad_header_store_id");

            migrationBuilder.RenameIndex(
                name: "IX_store_inventory_ad_header_adjustment_id",
                table: "im_store_inventory_ad_header",
                newName: "IX_im_store_inventory_ad_header_adjustment_id");

            migrationBuilder.RenameIndex(
                name: "IX_store_inventory_ad_details_variant_id",
                table: "im_store_inventory_ad_details",
                newName: "IX_im_store_inventory_ad_details_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_store_inventory_ad_details_store_variant_inventory_id",
                table: "im_store_inventory_ad_details",
                newName: "IX_im_store_inventory_ad_details_store_variant_inventory_id");

            migrationBuilder.RenameIndex(
                name: "IX_store_inventory_ad_details_store_inventory_ad_id",
                table: "im_store_inventory_ad_details",
                newName: "IX_im_store_inventory_ad_details_store_inventory_ad_id");

            migrationBuilder.RenameIndex(
                name: "IX_store_inventory_ad_details_product_id",
                table: "im_store_inventory_ad_details",
                newName: "IX_im_store_inventory_ad_details_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_store_inventory_ad_details_adjustment_detail_id",
                table: "im_store_inventory_ad_details",
                newName: "IX_im_store_inventory_ad_details_adjustment_detail_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_im_store_inventory_ad_header",
                table: "im_store_inventory_ad_header",
                column: "store_inventory_ad_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_im_store_inventory_ad_details",
                table: "im_store_inventory_ad_details",
                column: "store_inventory_detail_ad_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_store_inventory_ad_details_im_ProductVariants_variant_id",
                table: "im_store_inventory_ad_details",
                column: "variant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_store_inventory_ad_details_im_Products_product_id",
                table: "im_store_inventory_ad_details",
                column: "product_id",
                principalTable: "im_Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_im_store_inventory_ad_details_im_StoreVariantInventory_store_variant_inventory_id",
                table: "im_store_inventory_ad_details",
                column: "store_variant_inventory_id",
                principalTable: "im_StoreVariantInventory",
                principalColumn: "store_variant_inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_store_inventory_ad_details_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "im_store_inventory_ad_details",
                column: "adjustment_detail_id",
                principalTable: "im_inventory_adjustment_lines",
                principalColumn: "adjustment_detail_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_store_inventory_ad_details_im_store_inventory_ad_header_store_inventory_ad_id",
                table: "im_store_inventory_ad_details",
                column: "store_inventory_ad_id",
                principalTable: "im_store_inventory_ad_header",
                principalColumn: "store_inventory_ad_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_im_store_inventory_ad_header_im_inventory_adjustment_header_adjustment_id",
                table: "im_store_inventory_ad_header",
                column: "adjustment_id",
                principalTable: "im_inventory_adjustment_header",
                principalColumn: "adjustment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_store_inventory_ad_header_st_stores_store_id",
                table: "im_store_inventory_ad_header",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_store_inventory_ad_details_im_ProductVariants_variant_id",
                table: "im_store_inventory_ad_details");

            migrationBuilder.DropForeignKey(
                name: "FK_im_store_inventory_ad_details_im_Products_product_id",
                table: "im_store_inventory_ad_details");

            migrationBuilder.DropForeignKey(
                name: "FK_im_store_inventory_ad_details_im_StoreVariantInventory_store_variant_inventory_id",
                table: "im_store_inventory_ad_details");

            migrationBuilder.DropForeignKey(
                name: "FK_im_store_inventory_ad_details_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "im_store_inventory_ad_details");

            migrationBuilder.DropForeignKey(
                name: "FK_im_store_inventory_ad_details_im_store_inventory_ad_header_store_inventory_ad_id",
                table: "im_store_inventory_ad_details");

            migrationBuilder.DropForeignKey(
                name: "FK_im_store_inventory_ad_header_im_inventory_adjustment_header_adjustment_id",
                table: "im_store_inventory_ad_header");

            migrationBuilder.DropForeignKey(
                name: "FK_im_store_inventory_ad_header_st_stores_store_id",
                table: "im_store_inventory_ad_header");

            migrationBuilder.DropPrimaryKey(
                name: "PK_im_store_inventory_ad_header",
                table: "im_store_inventory_ad_header");

            migrationBuilder.DropPrimaryKey(
                name: "PK_im_store_inventory_ad_details",
                table: "im_store_inventory_ad_details");

            migrationBuilder.RenameTable(
                name: "im_store_inventory_ad_header",
                newName: "store_inventory_ad_header");

            migrationBuilder.RenameTable(
                name: "im_store_inventory_ad_details",
                newName: "store_inventory_ad_details");

            migrationBuilder.RenameIndex(
                name: "IX_im_store_inventory_ad_header_store_id",
                table: "store_inventory_ad_header",
                newName: "IX_store_inventory_ad_header_store_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_store_inventory_ad_header_adjustment_id",
                table: "store_inventory_ad_header",
                newName: "IX_store_inventory_ad_header_adjustment_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_store_inventory_ad_details_variant_id",
                table: "store_inventory_ad_details",
                newName: "IX_store_inventory_ad_details_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_store_inventory_ad_details_store_variant_inventory_id",
                table: "store_inventory_ad_details",
                newName: "IX_store_inventory_ad_details_store_variant_inventory_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_store_inventory_ad_details_store_inventory_ad_id",
                table: "store_inventory_ad_details",
                newName: "IX_store_inventory_ad_details_store_inventory_ad_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_store_inventory_ad_details_product_id",
                table: "store_inventory_ad_details",
                newName: "IX_store_inventory_ad_details_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_store_inventory_ad_details_adjustment_detail_id",
                table: "store_inventory_ad_details",
                newName: "IX_store_inventory_ad_details_adjustment_detail_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_store_inventory_ad_header",
                table: "store_inventory_ad_header",
                column: "store_inventory_ad_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_store_inventory_ad_details",
                table: "store_inventory_ad_details",
                column: "store_inventory_detail_ad_id");

            migrationBuilder.AddForeignKey(
                name: "FK_store_inventory_ad_details_im_ProductVariants_variant_id",
                table: "store_inventory_ad_details",
                column: "variant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_store_inventory_ad_details_im_Products_product_id",
                table: "store_inventory_ad_details",
                column: "product_id",
                principalTable: "im_Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_store_inventory_ad_details_im_StoreVariantInventory_store_variant_inventory_id",
                table: "store_inventory_ad_details",
                column: "store_variant_inventory_id",
                principalTable: "im_StoreVariantInventory",
                principalColumn: "store_variant_inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_store_inventory_ad_details_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "store_inventory_ad_details",
                column: "adjustment_detail_id",
                principalTable: "im_inventory_adjustment_lines",
                principalColumn: "adjustment_detail_id");

            migrationBuilder.AddForeignKey(
                name: "FK_store_inventory_ad_details_store_inventory_ad_header_store_inventory_ad_id",
                table: "store_inventory_ad_details",
                column: "store_inventory_ad_id",
                principalTable: "store_inventory_ad_header",
                principalColumn: "store_inventory_ad_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_store_inventory_ad_header_im_inventory_adjustment_header_adjustment_id",
                table: "store_inventory_ad_header",
                column: "adjustment_id",
                principalTable: "im_inventory_adjustment_header",
                principalColumn: "adjustment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_store_inventory_ad_header_st_stores_store_id",
                table: "store_inventory_ad_header",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
