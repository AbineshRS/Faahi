using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class _datut_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_discount_amount_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_amount_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_amount",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "returned_quantity",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "original_quantity",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "original_price_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "line_total_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "line_discount_amount",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "fx_rate_to_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_percent",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_amount_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_amount",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "detected_qty",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_discount_amount_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_amount_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_amount",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "returned_quantity",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "original_quantity",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "original_price_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "line_total_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "line_discount_amount",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "fx_rate_to_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_percent",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_amount_base",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "discount_amount",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "detected_qty",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValueSql: "0");
        }
    }
}
