using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class exc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "category_id",
                table: "im_purchase_listing_details",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "im_purchase_listing_details",
                type: "char(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "sub_category_id",
                table: "im_purchase_listing_details",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "sub_sub_category_id",
                table: "im_purchase_listing_details",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tax_class_id",
                table: "im_purchase_listing_details",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category_id",
                table: "im_purchase_listing_details");

            migrationBuilder.DropColumn(
                name: "status",
                table: "im_purchase_listing_details");

            migrationBuilder.DropColumn(
                name: "sub_category_id",
                table: "im_purchase_listing_details");

            migrationBuilder.DropColumn(
                name: "sub_sub_category_id",
                table: "im_purchase_listing_details");

            migrationBuilder.DropColumn(
                name: "tax_class_id",
                table: "im_purchase_listing_details");
        }
    }
}
