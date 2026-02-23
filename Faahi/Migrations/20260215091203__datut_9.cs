using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class _datut_9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "store_variant_inventory_id",
                table: "so_SalesLines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_so_SalesLines_store_variant_inventory_id",
                table: "so_SalesLines",
                column: "store_variant_inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesLines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "so_SalesLines",
                column: "store_variant_inventory_id",
                principalTable: "im_StoreVariantInventory",
                principalColumn: "store_variant_inventory_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesLines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "so_SalesLines");

            migrationBuilder.DropIndex(
                name: "IX_so_SalesLines_store_variant_inventory_id",
                table: "so_SalesLines");

            migrationBuilder.DropColumn(
                name: "store_variant_inventory_id",
                table: "so_SalesLines");
        }
    }
}
