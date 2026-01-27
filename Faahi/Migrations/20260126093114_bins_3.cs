using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class bins_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "batch_id",
                table: "im_itemBatches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "super_abi",
                columns: table => new
                {
                    description = table.Column<string>(type: "varchar(255)", nullable: false),
                    next_key = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_super_abi", x => x.description);
                });

            migrationBuilder.CreateTable(
                name: "temp_im_ItemBatches",
                columns: table => new
                {
                    item_batch_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    batch_id = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_temp_im_ItemBatches", x => x.item_batch_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "super_abi");

            migrationBuilder.DropTable(
                name: "temp_im_ItemBatches");

            migrationBuilder.DropColumn(
                name: "batch_id",
                table: "im_itemBatches");
        }
    }
}
