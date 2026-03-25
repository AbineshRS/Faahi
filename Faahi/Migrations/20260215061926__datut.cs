using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class _datut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_so_SalesLines_Amounts",
                table: "so_SalesLines");

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_so_SalesLines_Amounts",
                table: "so_SalesLines",
                sql: "unit_price >= 0 AND discount_amount >= 0 AND tax_amount >= 0 AND discount_percent >= 0 AND fx_rate_to_base > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_so_SalesLines_Amounts",
                table: "so_SalesLines");

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "CK_so_SalesLines_Amounts",
                table: "so_SalesLines",
                sql: " unit_price >= 0 AND discount_amount >= 0 AND tax_amount >= 0 AND discount_percent >= 0 AND fx_rate_to_base > 0");
        }
    }
}
