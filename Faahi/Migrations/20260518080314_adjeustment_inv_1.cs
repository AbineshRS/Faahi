using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class adjeustment_inv_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "adjustment_detail_id",
                table: "temp_stock_ad_lines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "adjustment_id",
                table: "temp_stock_ad_lines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "adjustment_id",
                table: "store_inventory_ad_header",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "adjustment_detail_id",
                table: "store_inventory_ad_details",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_store_inventory_ad_header_adjustment_id",
                table: "store_inventory_ad_header",
                column: "adjustment_id");

            migrationBuilder.CreateIndex(
                name: "IX_store_inventory_ad_details_adjustment_detail_id",
                table: "store_inventory_ad_details",
                column: "adjustment_detail_id");

            migrationBuilder.AddForeignKey(
                name: "FK_store_inventory_ad_details_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "store_inventory_ad_details",
                column: "adjustment_detail_id",
                principalTable: "im_inventory_adjustment_lines",
                principalColumn: "adjustment_detail_id");

            migrationBuilder.AddForeignKey(
                name: "FK_store_inventory_ad_header_im_inventory_adjustment_header_adjustment_id",
                table: "store_inventory_ad_header",
                column: "adjustment_id",
                principalTable: "im_inventory_adjustment_header",
                principalColumn: "adjustment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_store_inventory_ad_details_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "store_inventory_ad_details");

            migrationBuilder.DropForeignKey(
                name: "FK_store_inventory_ad_header_im_inventory_adjustment_header_adjustment_id",
                table: "store_inventory_ad_header");

            migrationBuilder.DropIndex(
                name: "IX_store_inventory_ad_header_adjustment_id",
                table: "store_inventory_ad_header");

            migrationBuilder.DropIndex(
                name: "IX_store_inventory_ad_details_adjustment_detail_id",
                table: "store_inventory_ad_details");

            migrationBuilder.DropColumn(
                name: "adjustment_detail_id",
                table: "temp_stock_ad_lines");

            migrationBuilder.DropColumn(
                name: "adjustment_id",
                table: "temp_stock_ad_lines");

            migrationBuilder.DropColumn(
                name: "adjustment_id",
                table: "store_inventory_ad_header");

            migrationBuilder.DropColumn(
                name: "adjustment_detail_id",
                table: "store_inventory_ad_details");
        }
    }
}
