using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class btch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uom_id",
                table: "im_purchase_listing_details");

            migrationBuilder.AddColumn<string>(
                name: "batch_no",
                table: "im_purchase_listing_details",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bin_no",
                table: "im_purchase_listing_details",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uom_name",
                table: "im_purchase_listing_details",
                type: "nvarchar(20)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "batch_no",
                table: "im_purchase_listing_details");

            migrationBuilder.DropColumn(
                name: "bin_no",
                table: "im_purchase_listing_details");

            migrationBuilder.DropColumn(
                name: "uom_name",
                table: "im_purchase_listing_details");

            migrationBuilder.AddColumn<Guid>(
                name: "uom_id",
                table: "im_purchase_listing_details",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
