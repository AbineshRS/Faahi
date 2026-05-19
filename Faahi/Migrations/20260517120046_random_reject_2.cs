using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class random_reject_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_random_Stock_reject_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "im_random_Stock_reject");

            migrationBuilder.DropIndex(
                name: "IX_im_random_Stock_reject_adjustment_detail_id",
                table: "im_random_Stock_reject");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_im_random_Stock_reject_adjustment_detail_id",
                table: "im_random_Stock_reject",
                column: "adjustment_detail_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_random_Stock_reject_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "im_random_Stock_reject",
                column: "adjustment_detail_id",
                principalTable: "im_inventory_adjustment_lines",
                principalColumn: "adjustment_detail_id");
        }
    }
}
