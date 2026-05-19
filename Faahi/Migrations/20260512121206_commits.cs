using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class commits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "im_InventoryCommitments",
                columns: table => new
                {
                    commitment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    source_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    source_type = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    source_no = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    committed_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    action_type = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_InventoryCommitments", x => x.commitment_id);
                    table.ForeignKey(
                        name: "FK_im_InventoryCommitments_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_im_InventoryCommitments_im_ProductVariants_variant_id",
                        column: x => x.variant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                    table.ForeignKey(
                        name: "FK_im_InventoryCommitments_im_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_im_InventoryCommitments_im_StoreVariantInventory_store_variant_inventory_id",
                        column: x => x.store_variant_inventory_id,
                        principalTable: "im_StoreVariantInventory",
                        principalColumn: "store_variant_inventory_id");
                    table.ForeignKey(
                        name: "FK_im_InventoryCommitments_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryCommitments",
                table: "im_InventoryCommitments",
                columns: new[] { "commitment_id", "product_id", "variant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryCommitments_business_id",
                table: "im_InventoryCommitments",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryCommitments_product_id",
                table: "im_InventoryCommitments",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryCommitments_store_id",
                table: "im_InventoryCommitments",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryCommitments_store_variant_inventory_id",
                table: "im_InventoryCommitments",
                column: "store_variant_inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryCommitments_variant_id",
                table: "im_InventoryCommitments",
                column: "variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_InventoryCommitments");
        }
    }
}
