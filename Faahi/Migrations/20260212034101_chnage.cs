using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class chnage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "product_description",
                table: "im_purchase_listing_details",
                newName: "Product_title");

            migrationBuilder.AddColumn<string>(
                name: "Product_Brand",
                table: "im_purchase_listing_details",
                type: "nvarchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product_Brand",
                table: "im_purchase_listing_details");

            migrationBuilder.RenameColumn(
                name: "Product_title",
                table: "im_purchase_listing_details",
                newName: "product_description");
        }
    }
}
