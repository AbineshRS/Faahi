using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class listing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "im_purchase_listing",
                columns: table => new
                {
                    listing_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    site_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    vendor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    payment_mode = table.Column<string>(type: "varchar(20)", nullable: true),
                    purchase_type = table.Column<string>(type: "varchar(20)", nullable: true),
                    supplier_invoice_no = table.Column<string>(type: "varchar(50)", nullable: true),
                    supplier_invoice_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(10)", nullable: true),
                    exchange_rate = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    sub_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    freight_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    other_expenses = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    received_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    notes = table.Column<string>(type: "varchar(400)", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_purchase_listing", x => x.listing_id);
                });

            migrationBuilder.CreateTable(
                name: "im_purchase_listing_details",
                columns: table => new
                {
                    detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    listing_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sub_variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    unit_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    freight_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    other_expenses = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    line_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    notes = table.Column<string>(type: "varchar(400)", nullable: true),
                    expiry_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    im_purchase_listinglisting_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_purchase_listing_details", x => x.detail_id);
                    table.ForeignKey(
                        name: "FK_im_purchase_listing_details_im_purchase_listing_im_purchase_listinglisting_id",
                        column: x => x.im_purchase_listinglisting_id,
                        principalTable: "im_purchase_listing",
                        principalColumn: "listing_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_purchase_listing_details_im_purchase_listinglisting_id",
                table: "im_purchase_listing_details",
                column: "im_purchase_listinglisting_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_purchase_listing_details");

            migrationBuilder.DropTable(
                name: "im_purchase_listing");
        }
    }
}
