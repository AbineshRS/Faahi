using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class im_prodt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_product_subvariant_im_ProductVariants_im_ProductVariantsvariant_id",
                table: "im_product_subvariant");

            migrationBuilder.DropIndex(
                name: "IX_im_product_subvariant_im_ProductVariantsvariant_id",
                table: "im_product_subvariant");

            migrationBuilder.DropColumn(
                name: "allow_below_Zero",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "chargeable_weight_kg",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "color",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "height_cm",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "length_cm",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "price",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "weight_kg",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "width_cm",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "HS_CODE",
                table: "im_Products");

            migrationBuilder.DropColumn(
                name: "tax_class",
                table: "im_Products");

            migrationBuilder.DropColumn(
                name: "im_ProductVariantsvariant_id",
                table: "im_product_subvariant");

            migrationBuilder.RenameColumn(
                name: "stock_quantity",
                table: "im_ProductVariants",
                newName: "base_price");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "im_ProductVariants",
                newName: "barcovendor_part_number");

            migrationBuilder.RenameColumn(
                name: "low_stock_alert",
                table: "im_ProductVariants",
                newName: "is_default");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "im_Products",
                newName: "track_expiry");

            migrationBuilder.RenameColumn(
                name: "katta",
                table: "im_Products",
                newName: "stock_flag");

            migrationBuilder.RenameColumn(
                name: "iqnore_decimal_qty",
                table: "im_Products",
                newName: "restrict_deciaml_qty");

            migrationBuilder.RenameColumn(
                name: "free_item",
                table: "im_Products",
                newName: "published");

            migrationBuilder.RenameColumn(
                name: "consign_item",
                table: "im_Products",
                newName: "low_stock_alert");

            migrationBuilder.AlterColumn<string>(
                name: "barcode",
                table: "im_ProductVariants",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "vendor_Code",
                table: "im_Products",
                type: "varchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "allow_below_zero",
                table: "im_Products",
                type: "char(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fixed_price",
                table: "im_Products",
                type: "char(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "has_free_item",
                table: "im_Products",
                type: "char(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "is_multi_unit",
                table: "im_Products",
                type: "char(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "kitchen_type",
                table: "im_Products",
                type: "varchar(32)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tax_class_id",
                table: "im_Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "im_ProductAttributes",
                columns: table => new
                {
                    attribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_ProductAttributes", x => x.attribute_id);
                });

            migrationBuilder.CreateTable(
                name: "im_VariantAttributes",
                columns: table => new
                {
                    varient_attribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    value_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    im_ProductVariantsvariant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_VariantAttributes", x => x.varient_attribute_id);
                    table.ForeignKey(
                        name: "FK_im_VariantAttributes_im_ProductVariants_im_ProductVariantsvariant_id",
                        column: x => x.im_ProductVariantsvariant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                });

            migrationBuilder.CreateTable(
                name: "im_AttributeValues",
                columns: table => new
                {
                    value_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    attribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    value = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: true),
                    im_ProductAttributesattribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_AttributeValues", x => x.value_id);
                    table.ForeignKey(
                        name: "FK_im_AttributeValues_im_ProductAttributes_im_ProductAttributesattribute_id",
                        column: x => x.im_ProductAttributesattribute_id,
                        principalTable: "im_ProductAttributes",
                        principalColumn: "attribute_id");
                });

            migrationBuilder.CreateTable(
                name: "im_StoreVariantInventory",
                columns: table => new
                {
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    on_hand_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    committed_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    bin_number = table.Column<string>(type: "nvarchar(24)", nullable: true),
                    im_VariantAttributesvarient_attribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_StoreVariantInventory", x => x.store_variant_inventory_id);
                    table.ForeignKey(
                        name: "FK_im_StoreVariantInventory_im_VariantAttributes_im_VariantAttributesvarient_attribute_id",
                        column: x => x.im_VariantAttributesvarient_attribute_id,
                        principalTable: "im_VariantAttributes",
                        principalColumn: "varient_attribute_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_Products_company_id",
                table: "im_Products",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_Products_title",
                table: "im_Products",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "IX_im_AttributeValues_im_ProductAttributesattribute_id",
                table: "im_AttributeValues",
                column: "im_ProductAttributesattribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductAttributes_company_id",
                table: "im_ProductAttributes",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductAttributes_name",
                table: "im_ProductAttributes",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_im_StoreVariantInventory_im_VariantAttributesvarient_attribute_id",
                table: "im_StoreVariantInventory",
                column: "im_VariantAttributesvarient_attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StoreVariantInventory_store_id",
                table: "im_StoreVariantInventory",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StoreVariantInventory_variant_id",
                table: "im_StoreVariantInventory",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_VariantAttributes_im_ProductVariantsvariant_id",
                table: "im_VariantAttributes",
                column: "im_ProductVariantsvariant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_AttributeValues");

            migrationBuilder.DropTable(
                name: "im_StoreVariantInventory");

            migrationBuilder.DropTable(
                name: "im_ProductAttributes");

            migrationBuilder.DropTable(
                name: "im_VariantAttributes");

            migrationBuilder.DropIndex(
                name: "IX_im_Products_company_id",
                table: "im_Products");

            migrationBuilder.DropIndex(
                name: "IX_im_Products_title",
                table: "im_Products");

            migrationBuilder.DropColumn(
                name: "allow_below_zero",
                table: "im_Products");

            migrationBuilder.DropColumn(
                name: "fixed_price",
                table: "im_Products");

            migrationBuilder.DropColumn(
                name: "has_free_item",
                table: "im_Products");

            migrationBuilder.DropColumn(
                name: "is_multi_unit",
                table: "im_Products");

            migrationBuilder.DropColumn(
                name: "kitchen_type",
                table: "im_Products");

            migrationBuilder.DropColumn(
                name: "tax_class_id",
                table: "im_Products");

            migrationBuilder.RenameColumn(
                name: "is_default",
                table: "im_ProductVariants",
                newName: "low_stock_alert");

            migrationBuilder.RenameColumn(
                name: "base_price",
                table: "im_ProductVariants",
                newName: "stock_quantity");

            migrationBuilder.RenameColumn(
                name: "barcovendor_part_number",
                table: "im_ProductVariants",
                newName: "size");

            migrationBuilder.RenameColumn(
                name: "track_expiry",
                table: "im_Products",
                newName: "stock");

            migrationBuilder.RenameColumn(
                name: "stock_flag",
                table: "im_Products",
                newName: "katta");

            migrationBuilder.RenameColumn(
                name: "restrict_deciaml_qty",
                table: "im_Products",
                newName: "iqnore_decimal_qty");

            migrationBuilder.RenameColumn(
                name: "published",
                table: "im_Products",
                newName: "free_item");

            migrationBuilder.RenameColumn(
                name: "low_stock_alert",
                table: "im_Products",
                newName: "consign_item");

            migrationBuilder.AlterColumn<string>(
                name: "barcode",
                table: "im_ProductVariants",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "allow_below_Zero",
                table: "im_ProductVariants",
                type: "char(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "chargeable_weight_kg",
                table: "im_ProductVariants",
                type: "decimal(16,4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "im_ProductVariants",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "height_cm",
                table: "im_ProductVariants",
                type: "decimal(16,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "length_cm",
                table: "im_ProductVariants",
                type: "decimal(16,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "im_ProductVariants",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "weight_kg",
                table: "im_ProductVariants",
                type: "decimal(16,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "width_cm",
                table: "im_ProductVariants",
                type: "decimal(16,4)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "vendor_Code",
                table: "im_Products",
                type: "varchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HS_CODE",
                table: "im_Products",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tax_class",
                table: "im_Products",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "im_ProductVariantsvariant_id",
                table: "im_product_subvariant",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_product_subvariant_im_ProductVariantsvariant_id",
                table: "im_product_subvariant",
                column: "im_ProductVariantsvariant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_product_subvariant_im_ProductVariants_im_ProductVariantsvariant_id",
                table: "im_product_subvariant",
                column: "im_ProductVariantsvariant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");
        }
    }
}
