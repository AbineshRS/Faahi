using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class _datut_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "so_SalesLines",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValueSql: "0");
        }
    }
}
