using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class varient_qty_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "varient_quantity",
                table: "im_purchase_listing_details");

            migrationBuilder.AlterColumn<decimal>(
                name: "variant_qty",
                table: "im_purchase_listing_details",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "variant_qty",
                table: "im_purchase_listing_details",
                type: "nvarchar(10)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "varient_quantity",
                table: "im_purchase_listing_details",
                type: "decimal(18,4)",
                nullable: true);
        }
    }
}
