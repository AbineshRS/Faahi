using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class avgs_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "average_cost",
                table: "im_StoreVariantInventory");

            migrationBuilder.DropColumn(
                name: "last_cost",
                table: "im_StoreVariantInventory");

            migrationBuilder.AddColumn<Guid>(
                name: "store_variant_inventory_id",
                table: "im_purchase_listing_details",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "store_variant_inventory_id",
                table: "im_purchase_listing_details");

            migrationBuilder.AddColumn<decimal>(
                name: "average_cost",
                table: "im_StoreVariantInventory",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "last_cost",
                table: "im_StoreVariantInventory",
                type: "decimal(18,4)",
                nullable: true);
        }
    }
}
