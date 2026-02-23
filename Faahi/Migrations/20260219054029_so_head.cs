using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class so_head : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "sales_id",
                table: "im_InventoryTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryTransactions_sales_id",
                table: "im_InventoryTransactions",
                column: "sales_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_InventoryTransactions_so_SalesLines_sales_id",
                table: "im_InventoryTransactions",
                column: "sales_id",
                principalTable: "so_SalesLines",
                principalColumn: "sales_line_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_InventoryTransactions_so_SalesLines_sales_id",
                table: "im_InventoryTransactions");

            migrationBuilder.DropIndex(
                name: "IX_im_InventoryTransactions_sales_id",
                table: "im_InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "sales_id",
                table: "im_InventoryTransactions");
        }
    }
}
