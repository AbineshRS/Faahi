using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class im_pr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_ProductVariants_im_Products_im_Productsproduct_id",
                table: "im_ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_im_StoreVariantInventory_im_VariantAttributes_im_VariantAttributesvarient_attribute_id",
                table: "im_StoreVariantInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_im_VariantAttributes_im_ProductVariants_im_ProductVariantsvariant_id",
                table: "im_VariantAttributes");

            migrationBuilder.DropIndex(
                name: "IX_im_VariantAttributes_im_ProductVariantsvariant_id",
                table: "im_VariantAttributes");

            migrationBuilder.DropIndex(
                name: "IX_im_StoreVariantInventory_im_VariantAttributesvarient_attribute_id",
                table: "im_StoreVariantInventory");

            migrationBuilder.DropIndex(
                name: "IX_im_ProductVariants_im_Productsproduct_id",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "im_VariantAttributesvarient_attribute_id",
                table: "im_StoreVariantInventory");

            migrationBuilder.DropColumn(
                name: "im_Productsproduct_id",
                table: "im_ProductVariants");

            migrationBuilder.RenameColumn(
                name: "im_ProductVariantsvariant_id",
                table: "im_VariantAttributes",
                newName: "variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "variant_id",
                table: "im_VariantAttributes",
                newName: "im_ProductVariantsvariant_id");

            migrationBuilder.AddColumn<Guid>(
                name: "im_VariantAttributesvarient_attribute_id",
                table: "im_StoreVariantInventory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "im_Productsproduct_id",
                table: "im_ProductVariants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_VariantAttributes_im_ProductVariantsvariant_id",
                table: "im_VariantAttributes",
                column: "im_ProductVariantsvariant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StoreVariantInventory_im_VariantAttributesvarient_attribute_id",
                table: "im_StoreVariantInventory",
                column: "im_VariantAttributesvarient_attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductVariants_im_Productsproduct_id",
                table: "im_ProductVariants",
                column: "im_Productsproduct_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_ProductVariants_im_Products_im_Productsproduct_id",
                table: "im_ProductVariants",
                column: "im_Productsproduct_id",
                principalTable: "im_Products",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_StoreVariantInventory_im_VariantAttributes_im_VariantAttributesvarient_attribute_id",
                table: "im_StoreVariantInventory",
                column: "im_VariantAttributesvarient_attribute_id",
                principalTable: "im_VariantAttributes",
                principalColumn: "varient_attribute_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_VariantAttributes_im_ProductVariants_im_ProductVariantsvariant_id",
                table: "im_VariantAttributes",
                column: "im_ProductVariantsvariant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");
        }
    }
}
