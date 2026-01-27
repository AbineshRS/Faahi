using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class bins_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_im_itemBatches_expiry_date",
                table: "im_itemBatches",
                sql: "[expiry_date] > GETDATE()");

            migrationBuilder.AddCheckConstraint(
                name: "CK_im_itemBatches_on_hand_quantity",
                table: "im_itemBatches",
                sql: "[on_hand_quantity] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_im_itemBatches_unit_cost",
                table: "im_itemBatches",
                sql: "[unit_cost] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_im_itemBatches_expiry_date",
                table: "im_itemBatches");

            migrationBuilder.DropCheckConstraint(
                name: "CK_im_itemBatches_on_hand_quantity",
                table: "im_itemBatches");

            migrationBuilder.DropCheckConstraint(
                name: "CK_im_itemBatches_unit_cost",
                table: "im_itemBatches");
        }
    }
}
