using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class rol_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "sales_id",
                table: "om_CustomerOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrders_sales_id",
                table: "om_CustomerOrders",
                column: "sales_id");

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrders_so_SalesHeaders_sales_id",
                table: "om_CustomerOrders",
                column: "sales_id",
                principalTable: "so_SalesHeaders",
                principalColumn: "sales_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrders_so_SalesHeaders_sales_id",
                table: "om_CustomerOrders");

            migrationBuilder.DropIndex(
                name: "IX_om_CustomerOrders_sales_id",
                table: "om_CustomerOrders");

            migrationBuilder.DropColumn(
                name: "sales_id",
                table: "om_CustomerOrders");
        }
    }
}
