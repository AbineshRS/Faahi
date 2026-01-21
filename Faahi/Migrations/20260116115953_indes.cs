using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class indes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_im_StoreVariantInventory_variant_id",
                table: "im_StoreVariantInventory",
                newName: "variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_StoreVariantInventory_store_id",
                table: "im_StoreVariantInventory",
                newName: "store_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_Products_company_id",
                table: "im_Products",
                newName: "company_id");

            migrationBuilder.CreateIndex(
                name: "attribute_id",
                table: "im_VariantAttributes",
                column: "attribute_id");

            migrationBuilder.CreateIndex(
                name: "value_id",
                table: "im_VariantAttributes",
                column: "value_id");

            migrationBuilder.CreateIndex(
                name: "variant_id",
                table: "im_VariantAttributes",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "varient_attribute_id",
                table: "im_VariantAttributes",
                column: "varient_attribute_id");

            migrationBuilder.CreateIndex(
                name: "store_variant_inventory_id",
                table: "im_StoreVariantInventory",
                column: "store_variant_inventory_id");

            migrationBuilder.CreateIndex(
                name: "description_2",
                table: "im_ProductVariants",
                column: "description_2");

            migrationBuilder.CreateIndex(
                name: "product_id",
                table: "im_ProductVariants",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "variant_id",
                table: "im_ProductVariants",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "category_id",
                table: "im_Products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "product_id",
                table: "im_Products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "sub_category_id",
                table: "im_Products",
                column: "sub_category_id");

            migrationBuilder.CreateIndex(
                name: "sub_sub_category_id",
                table: "im_Products",
                column: "sub_sub_category_id");

            migrationBuilder.CreateIndex(
                name: "image_id",
                table: "im_ProductImages",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "product_id",
                table: "im_ProductImages",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "variant_id",
                table: "im_ProductImages",
                column: "variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "attribute_id",
                table: "im_VariantAttributes");

            migrationBuilder.DropIndex(
                name: "value_id",
                table: "im_VariantAttributes");

            migrationBuilder.DropIndex(
                name: "variant_id",
                table: "im_VariantAttributes");

            migrationBuilder.DropIndex(
                name: "varient_attribute_id",
                table: "im_VariantAttributes");

            migrationBuilder.DropIndex(
                name: "store_variant_inventory_id",
                table: "im_StoreVariantInventory");

            migrationBuilder.DropIndex(
                name: "description_2",
                table: "im_ProductVariants");

            migrationBuilder.DropIndex(
                name: "product_id",
                table: "im_ProductVariants");

            migrationBuilder.DropIndex(
                name: "variant_id",
                table: "im_ProductVariants");

            migrationBuilder.DropIndex(
                name: "category_id",
                table: "im_Products");

            migrationBuilder.DropIndex(
                name: "product_id",
                table: "im_Products");

            migrationBuilder.DropIndex(
                name: "sub_category_id",
                table: "im_Products");

            migrationBuilder.DropIndex(
                name: "sub_sub_category_id",
                table: "im_Products");

            migrationBuilder.DropIndex(
                name: "image_id",
                table: "im_ProductImages");

            migrationBuilder.DropIndex(
                name: "product_id",
                table: "im_ProductImages");

            migrationBuilder.DropIndex(
                name: "variant_id",
                table: "im_ProductImages");

            migrationBuilder.RenameIndex(
                name: "variant_id",
                table: "im_StoreVariantInventory",
                newName: "IX_im_StoreVariantInventory_variant_id");

            migrationBuilder.RenameIndex(
                name: "store_id",
                table: "im_StoreVariantInventory",
                newName: "IX_im_StoreVariantInventory_store_id");

            migrationBuilder.RenameIndex(
                name: "company_id",
                table: "im_Products",
                newName: "IX_im_Products_company_id");
        }
    }
}
