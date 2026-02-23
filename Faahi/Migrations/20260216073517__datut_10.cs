using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class _datut_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "line_total",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_charge_customer_base",
                table: "so_SalesHeaders",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<long>(
                name: "sales_no",
                table: "so_SalesHeaders",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "line_total",
                table: "so_SalesLines");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_charge_customer_base",
                table: "so_SalesHeaders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "sales_no",
                table: "so_SalesHeaders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
