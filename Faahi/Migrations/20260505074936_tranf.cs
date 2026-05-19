using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class tranf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_StockTransferLines_im_itemBatches_item_batch_id",
                table: "im_StockTransferLines");

            migrationBuilder.DropIndex(
                name: "IX_im_StockTransferHeader_from_store_id",
                table: "im_StockTransferHeader");

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "im_StockTransferLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "im_StockTransferLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "line_total",
                table: "im_StockTransferLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<Guid>(
                name: "item_batch_id",
                table: "im_StockTransferLines",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "im_StockTransferLines",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<decimal>(
                name: "average_cost",
                table: "im_StockTransferLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,4)");

            migrationBuilder.AlterColumn<Guid>(
                name: "transfer_line_id",
                table: "im_StockTransferLines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_quantity",
                table: "im_StockTransferHeader",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_amount",
                table: "im_StockTransferHeader",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "im_StockTransferHeader",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "Draft",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "im_StockTransferHeader",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<Guid>(
                name: "transfer_id",
                table: "im_StockTransferHeader",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferLines",
                table: "im_StockTransferLines",
                columns: new[] { "transfer_line_id", "product_id", "variant_id", "store_variant_inventory_id", "item_batch_id" });

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferHeader",
                table: "im_StockTransferHeader",
                columns: new[] { "transfer_id", "from_store_id", "to_store_id" });

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferStore",
                table: "im_StockTransferHeader",
                columns: new[] { "from_store_id", "to_store_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_im_StockTransferLines_im_itemBatches_item_batch_id",
                table: "im_StockTransferLines",
                column: "item_batch_id",
                principalTable: "im_itemBatches",
                principalColumn: "item_batch_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_StockTransferLines_im_itemBatches_item_batch_id",
                table: "im_StockTransferLines");

            migrationBuilder.DropIndex(
                name: "IX_im_StockTransferLines",
                table: "im_StockTransferLines");

            migrationBuilder.DropIndex(
                name: "IX_im_StockTransferHeader",
                table: "im_StockTransferHeader");

            migrationBuilder.DropIndex(
                name: "IX_im_StockTransferStore",
                table: "im_StockTransferHeader");

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "im_StockTransferLines",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "im_StockTransferLines",
                type: "decimal(16,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "line_total",
                table: "im_StockTransferLines",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "item_batch_id",
                table: "im_StockTransferLines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "im_StockTransferLines",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<decimal>(
                name: "average_cost",
                table: "im_StockTransferLines",
                type: "decimal(16,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "transfer_line_id",
                table: "im_StockTransferLines",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_quantity",
                table: "im_StockTransferHeader",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "total_amount",
                table: "im_StockTransferHeader",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "im_StockTransferHeader",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldDefaultValue: "Draft");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "im_StockTransferHeader",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "transfer_id",
                table: "im_StockTransferHeader",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.CreateIndex(
                name: "IX_im_StockTransferHeader_from_store_id",
                table: "im_StockTransferHeader",
                column: "from_store_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_StockTransferLines_im_itemBatches_item_batch_id",
                table: "im_StockTransferLines",
                column: "item_batch_id",
                principalTable: "im_itemBatches",
                principalColumn: "item_batch_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
