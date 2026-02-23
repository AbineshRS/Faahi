using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class so_head_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_InventoryTransactions_so_SalesLines_sales_id",
                table: "im_InventoryTransactions");

            migrationBuilder.RenameColumn(
                name: "sales_id",
                table: "im_InventoryTransactions",
                newName: "sales_line_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_InventoryTransactions_sales_id",
                table: "im_InventoryTransactions",
                newName: "IX_im_InventoryTransactions_sales_line_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_InventoryTransactions_so_SalesLines_sales_line_id",
                table: "im_InventoryTransactions",
                column: "sales_line_id",
                principalTable: "so_SalesLines",
                principalColumn: "sales_line_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_InventoryTransactions_so_SalesLines_sales_line_id",
                table: "im_InventoryTransactions");

            migrationBuilder.RenameColumn(
                name: "sales_line_id",
                table: "im_InventoryTransactions",
                newName: "sales_id");

            migrationBuilder.RenameIndex(
                name: "IX_im_InventoryTransactions_sales_line_id",
                table: "im_InventoryTransactions",
                newName: "IX_im_InventoryTransactions_sales_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_InventoryTransactions_so_SalesLines_sales_id",
                table: "im_InventoryTransactions",
                column: "sales_id",
                principalTable: "so_SalesLines",
                principalColumn: "sales_line_id");
        }
    }
}
