using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class tranfer_good : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "transfer_id",
                table: "im_GoodsReceiptHeaders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLines",
                table: "im_GoodsReceiptLines",
                columns: new[] { "goods_receipt_id", "store_id" });

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptHeaders",
                table: "im_GoodsReceiptHeaders",
                columns: new[] { "goods_receipt_id", "business_id", "store_id", "Goods_recipt_code", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_transfer_id_purchase_order_id",
                table: "im_GoodsReceiptHeaders",
                columns: new[] { "transfer_id", "purchase_order_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_im_GoodsReceiptHeaders_im_StockTransferHeader_transfer_id",
                table: "im_GoodsReceiptHeaders",
                column: "transfer_id",
                principalTable: "im_StockTransferHeader",
                principalColumn: "transfer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_GoodsReceiptHeaders_im_StockTransferHeader_transfer_id",
                table: "im_GoodsReceiptHeaders");

            migrationBuilder.DropIndex(
                name: "IX_im_GoodsReceiptLines",
                table: "im_GoodsReceiptLines");

            migrationBuilder.DropIndex(
                name: "IX_im_GoodsReceiptHeaders",
                table: "im_GoodsReceiptHeaders");

            migrationBuilder.DropIndex(
                name: "IX_transfer_id_purchase_order_id",
                table: "im_GoodsReceiptHeaders");

            migrationBuilder.DropColumn(
                name: "transfer_id",
                table: "im_GoodsReceiptHeaders");
        }
    }
}
