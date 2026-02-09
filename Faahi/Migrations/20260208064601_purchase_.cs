using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class purchase_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_InventoryTransactions_ap_Vendors_vendor_id",
                table: "im_InventoryTransactions");

            migrationBuilder.DropIndex(
                name: "IX_listing_code",
                table: "im_InventoryTransactions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_im_InventoryTransactions_doc_totall",
                table: "im_InventoryTransactions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_im_InventoryTransactions_sub_total",
                table: "im_InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "im_InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "discount_amount",
                table: "im_InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "doc_total",
                table: "im_InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "freight_amount",
                table: "im_InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "listing_code",
                table: "im_InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "local_referance",
                table: "im_InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "payment_mode",
                table: "im_InventoryTransactions");

            migrationBuilder.RenameColumn(
                name: "vendor_id",
                table: "im_InventoryTransactions",
                newName: "variant_id");

            migrationBuilder.RenameColumn(
                name: "tax_amount",
                table: "im_InventoryTransactions",
                newName: "unit_cost");

            migrationBuilder.RenameColumn(
                name: "supplier_invoice_no",
                table: "im_InventoryTransactions",
                newName: "trans_reason");

            migrationBuilder.RenameColumn(
                name: "supplier_invoice_date",
                table: "im_InventoryTransactions",
                newName: "trans_date");

            migrationBuilder.RenameColumn(
                name: "sub_total",
                table: "im_InventoryTransactions",
                newName: "total_cost");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "im_InventoryTransactions",
                newName: "trans_type");

            migrationBuilder.RenameColumn(
                name: "purchase_type",
                table: "im_InventoryTransactions",
                newName: "source_doc_type");

            migrationBuilder.RenameColumn(
                name: "other_expenses",
                table: "im_InventoryTransactions",
                newName: "quantity_change");

            migrationBuilder.RenameIndex(
                name: "IX_im_InventoryTransactions_vendor_id",
                table: "im_InventoryTransactions",
                newName: "IX_im_InventoryTransactions_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_InventoryTransactions_store_id",
                table: "im_InventoryTransactions",
                newName: "IX_store_id");

            migrationBuilder.AddColumn<Guid>(
                name: "batch_id",
                table: "im_InventoryTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "remarks",
                table: "im_InventoryTransactions",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryTransactions_batch_id",
                table: "im_InventoryTransactions",
                column: "batch_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_InventoryTransactions_im_ProductVariants_variant_id",
                table: "im_InventoryTransactions",
                column: "variant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_InventoryTransactions_im_itemBatches_batch_id",
                table: "im_InventoryTransactions",
                column: "batch_id",
                principalTable: "im_itemBatches",
                principalColumn: "item_batch_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_InventoryTransactions_im_ProductVariants_variant_id",
                table: "im_InventoryTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_im_InventoryTransactions_im_itemBatches_batch_id",
                table: "im_InventoryTransactions");

            migrationBuilder.DropIndex(
                name: "IX_im_InventoryTransactions_batch_id",
                table: "im_InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "batch_id",
                table: "im_InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "remarks",
                table: "im_InventoryTransactions");

            migrationBuilder.RenameColumn(
                name: "variant_id",
                table: "im_InventoryTransactions",
                newName: "vendor_id");

            migrationBuilder.RenameColumn(
                name: "unit_cost",
                table: "im_InventoryTransactions",
                newName: "tax_amount");

            migrationBuilder.RenameColumn(
                name: "trans_type",
                table: "im_InventoryTransactions",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "trans_reason",
                table: "im_InventoryTransactions",
                newName: "supplier_invoice_no");

            migrationBuilder.RenameColumn(
                name: "trans_date",
                table: "im_InventoryTransactions",
                newName: "supplier_invoice_date");

            migrationBuilder.RenameColumn(
                name: "total_cost",
                table: "im_InventoryTransactions",
                newName: "sub_total");

            migrationBuilder.RenameColumn(
                name: "source_doc_type",
                table: "im_InventoryTransactions",
                newName: "purchase_type");

            migrationBuilder.RenameColumn(
                name: "quantity_change",
                table: "im_InventoryTransactions",
                newName: "other_expenses");

            migrationBuilder.RenameIndex(
                name: "IX_store_id",
                table: "im_InventoryTransactions",
                newName: "IX_im_InventoryTransactions_store_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_InventoryTransactions_variant_id",
                table: "im_InventoryTransactions",
                newName: "IX_im_InventoryTransactions_vendor_id");

            migrationBuilder.AddColumn<DateOnly>(
                name: "created_at",
                table: "im_InventoryTransactions",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "discount_amount",
                table: "im_InventoryTransactions",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "doc_total",
                table: "im_InventoryTransactions",
                type: "decimal(19,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "freight_amount",
                table: "im_InventoryTransactions",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "listing_code",
                table: "im_InventoryTransactions",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "local_referance",
                table: "im_InventoryTransactions",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "payment_mode",
                table: "im_InventoryTransactions",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_listing_code",
                table: "im_InventoryTransactions",
                column: "listing_code");

            migrationBuilder.AddCheckConstraint(
                name: "CK_im_InventoryTransactions_doc_totall",
                table: "im_InventoryTransactions",
                sql: "[doc_total]>=0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_im_InventoryTransactions_sub_total",
                table: "im_InventoryTransactions",
                sql: "[sub_total]>=0");

            migrationBuilder.AddForeignKey(
                name: "FK_im_InventoryTransactions_ap_Vendors_vendor_id",
                table: "im_InventoryTransactions",
                column: "vendor_id",
                principalTable: "ap_Vendors",
                principalColumn: "vendor_id");
        }
    }
}
