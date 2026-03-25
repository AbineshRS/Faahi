using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class retn_purchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "exchange_rate",
                table: "im_purchase_return_header",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "freight_amount",
                table: "im_purchase_return_header",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "orginal_discount_amount",
                table: "im_purchase_return_header",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "orginal_sub_total",
                table: "im_purchase_return_header",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "orginal_tax_amount",
                table: "im_purchase_return_header",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "orginal_total_amount",
                table: "im_purchase_return_header",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "other_expenses",
                table: "im_purchase_return_header",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "plastic_bag",
                table: "im_purchase_return_header",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "orginal_line_total",
                table: "im_purchase_return_details_line",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "orginal_quantity",
                table: "im_purchase_return_details_line",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "orginal_unit_price",
                table: "im_purchase_return_details_line",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "other_expenses",
                table: "im_purchase_return_details_line",
                type: "decimal(18,4)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "exchange_rate",
                table: "im_purchase_return_header");

            migrationBuilder.DropColumn(
                name: "freight_amount",
                table: "im_purchase_return_header");

            migrationBuilder.DropColumn(
                name: "orginal_discount_amount",
                table: "im_purchase_return_header");

            migrationBuilder.DropColumn(
                name: "orginal_sub_total",
                table: "im_purchase_return_header");

            migrationBuilder.DropColumn(
                name: "orginal_tax_amount",
                table: "im_purchase_return_header");

            migrationBuilder.DropColumn(
                name: "orginal_total_amount",
                table: "im_purchase_return_header");

            migrationBuilder.DropColumn(
                name: "other_expenses",
                table: "im_purchase_return_header");

            migrationBuilder.DropColumn(
                name: "plastic_bag",
                table: "im_purchase_return_header");

            migrationBuilder.DropColumn(
                name: "orginal_line_total",
                table: "im_purchase_return_details_line");

            migrationBuilder.DropColumn(
                name: "orginal_quantity",
                table: "im_purchase_return_details_line");

            migrationBuilder.DropColumn(
                name: "orginal_unit_price",
                table: "im_purchase_return_details_line");

            migrationBuilder.DropColumn(
                name: "other_expenses",
                table: "im_purchase_return_details_line");
        }
    }
}
