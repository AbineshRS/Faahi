using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class fullfilme_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "total_delivered_qty",
                table: "om_FulfillmentOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "total_ordered_qty",
                table: "om_FulfillmentOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "total_rejected_qty",
                table: "om_FulfillmentOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "total_reserved_qty",
                table: "om_FulfillmentOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "total_returned_qty",
                table: "om_FulfillmentOrders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_delivered_qty",
                table: "om_FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "total_ordered_qty",
                table: "om_FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "total_rejected_qty",
                table: "om_FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "total_reserved_qty",
                table: "om_FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "total_returned_qty",
                table: "om_FulfillmentOrders");
        }
    }
}
