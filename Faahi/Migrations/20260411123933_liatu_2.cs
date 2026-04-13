using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class liatu_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "doc_type",
                table: "so_SalesHeaders",
                type: "varchar(30)",
                nullable: true,
                defaultValue: "SALE",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldDefaultValue: "SALE");

            migrationBuilder.AddColumn<Guid>(
                name: "store_variant_inventory_id",
                table: "om_CustomerOrderLines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrderLines_store_variant_inventory_id",
                table: "om_CustomerOrderLines",
                column: "store_variant_inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrderLines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "om_CustomerOrderLines",
                column: "store_variant_inventory_id",
                principalTable: "im_StoreVariantInventory",
                principalColumn: "store_variant_inventory_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrderLines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "om_CustomerOrderLines");

            migrationBuilder.DropIndex(
                name: "IX_om_CustomerOrderLines_store_variant_inventory_id",
                table: "om_CustomerOrderLines");

            migrationBuilder.DropColumn(
                name: "store_variant_inventory_id",
                table: "om_CustomerOrderLines");

            migrationBuilder.AlterColumn<string>(
                name: "doc_type",
                table: "so_SalesHeaders",
                type: "varchar(10)",
                nullable: true,
                defaultValue: "SALE",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true,
                oldDefaultValue: "SALE");
        }
    }
}
