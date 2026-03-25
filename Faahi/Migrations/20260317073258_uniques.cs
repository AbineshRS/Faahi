using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class uniques : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_im_GoodsReceiptLines_receipt",
                table: "im_GoodsReceiptLines");

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLines_receipt",
                table: "im_GoodsReceiptLines",
                column: "line_no");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_im_GoodsReceiptLines_receipt",
                table: "im_GoodsReceiptLines");

            migrationBuilder.CreateIndex(
                name: "IX_im_GoodsReceiptLines_receipt",
                table: "im_GoodsReceiptLines",
                column: "line_no",
                unique: true);
        }
    }
}
