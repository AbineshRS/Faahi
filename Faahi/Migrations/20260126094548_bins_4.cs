using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class bins_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "temp_im_ItemBatches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "temp_im_ItemBatches",
                columns: table => new
                {
                    item_batch_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    batch_id = table.Column<int>(type: "int", nullable: true),
                    batch_number = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    batch_on_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    batch_promo_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_active = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    is_expired = table.Column<DateOnly>(type: "date", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    on_hand_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    promo_from_date = table.Column<DateOnly>(type: "date", nullable: true),
                    promo_to_date = table.Column<DateOnly>(type: "date", nullable: true),
                    received_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    received_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    reference_doc = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    reserved_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    total_cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    unit_cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temp_im_ItemBatches", x => x.item_batch_id);
                });
        }
    }
}
