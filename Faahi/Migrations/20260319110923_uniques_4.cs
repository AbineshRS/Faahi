using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class uniques_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "temp_Im_Purchase_Listing_Details",
                columns: table => new
                {
                    temp_detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    listing_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sub_variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    tax_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_name = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    unit_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    freight_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    other_expenses = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    line_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    notes = table.Column<string>(type: "varchar(400)", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_varient = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    variant_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    batch_no = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    bin_no = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Product_Brand = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Product_title = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    barcode = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    sku = table.Column<string>(type: "varchar(50)", nullable: true),
                    new_item = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    base_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    selling_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    line_net_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    line_unit_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temp_Im_Purchase_Listing_Details", x => x.temp_detail_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "temp_Im_Purchase_Listing_Details");
        }
    }
}
