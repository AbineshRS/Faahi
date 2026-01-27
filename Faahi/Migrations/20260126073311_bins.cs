using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class bins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "im_bin_locations",
                columns: table => new
                {
                    bin_location_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    bin_code = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_bin_locations", x => x.bin_location_id);
                });

            migrationBuilder.CreateTable(
                name: "im_itemBatches",
                columns: table => new
                {
                    item_batch_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    batch_number = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    received_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    on_hand_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    reserved_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    unit_cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    total_cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    batch_promo_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    promo_from_date = table.Column<DateOnly>(type: "date", nullable: true),
                    promo_to_date = table.Column<DateOnly>(type: "date", nullable: true),
                    batch_on_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    is_expired = table.Column<DateOnly>(type: "date", nullable: true),
                    is_active = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    received_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    reference_doc = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_itemBatches", x => x.item_batch_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_batch_on_hold",
                table: "im_itemBatches",
                column: "batch_on_hold");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyId",
                table: "im_itemBatches",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_expiry_date",
                table: "im_itemBatches",
                column: "expiry_date");

            migrationBuilder.CreateIndex(
                name: "IX_is_active",
                table: "im_itemBatches",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "IX_item_batch_id",
                table: "im_itemBatches",
                column: "item_batch_id");

            migrationBuilder.CreateIndex(
                name: "IX_on_hand_quantity",
                table: "im_itemBatches",
                column: "on_hand_quantity");

            migrationBuilder.CreateIndex(
                name: "IX_store_id",
                table: "im_itemBatches",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_variant_id",
                table: "im_itemBatches",
                column: "variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_bin_locations");

            migrationBuilder.DropTable(
                name: "im_itemBatches");
        }
    }
}
