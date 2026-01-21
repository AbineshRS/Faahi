using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class lists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "is_varient",
                table: "im_purchase_listing_details",
                type: "char(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "varient_quantity",
                table: "im_purchase_listing_details",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "local_referance",
                table: "im_purchase_listing",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "detail_id",
                table: "im_purchase_listing_details",
                column: "detail_id");

            migrationBuilder.CreateIndex(
                name: "listing_id",
                table: "im_purchase_listing_details",
                column: "listing_id");

            migrationBuilder.CreateIndex(
                name: "product_id",
                table: "im_purchase_listing_details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "sub_variant_id",
                table: "im_purchase_listing_details",
                column: "sub_variant_id");

            migrationBuilder.CreateIndex(
                name: "listing_id",
                table: "im_purchase_listing",
                column: "listing_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "site_id",
                table: "im_purchase_listing",
                column: "site_id");

            migrationBuilder.CreateIndex(
                name: "vendor_id",
                table: "im_purchase_listing",
                column: "vendor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "detail_id",
                table: "im_purchase_listing_details");

            migrationBuilder.DropIndex(
                name: "listing_id",
                table: "im_purchase_listing_details");

            migrationBuilder.DropIndex(
                name: "product_id",
                table: "im_purchase_listing_details");

            migrationBuilder.DropIndex(
                name: "sub_variant_id",
                table: "im_purchase_listing_details");

            migrationBuilder.DropIndex(
                name: "listing_id",
                table: "im_purchase_listing");

            migrationBuilder.DropIndex(
                name: "site_id",
                table: "im_purchase_listing");

            migrationBuilder.DropIndex(
                name: "vendor_id",
                table: "im_purchase_listing");

            migrationBuilder.DropColumn(
                name: "is_varient",
                table: "im_purchase_listing_details");

            migrationBuilder.DropColumn(
                name: "varient_quantity",
                table: "im_purchase_listing_details");

            migrationBuilder.DropColumn(
                name: "local_referance",
                table: "im_purchase_listing");
        }
    }
}
