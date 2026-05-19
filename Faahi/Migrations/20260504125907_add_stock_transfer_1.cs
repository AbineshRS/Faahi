using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class add_stock_transfer_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_StockTransferHeader_co_business_transfer_id",
                table: "im_StockTransferHeader");

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferHeader_business_id",
                table: "im_StockTransferHeader",
                column: "business_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_StockTransferHeader_co_business_business_id",
                table: "im_StockTransferHeader",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_StockTransferHeader_co_business_business_id",
                table: "im_StockTransferHeader");

            migrationBuilder.DropIndex(
                name: "IX_im_StockTransferHeader_business_id",
                table: "im_StockTransferHeader");

            migrationBuilder.AddForeignKey(
                name: "FK_im_StockTransferHeader_co_business_transfer_id",
                table: "im_StockTransferHeader",
                column: "transfer_id",
                principalTable: "co_business",
                principalColumn: "company_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
