using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class storeinv_hold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "on_hand_quantity",
                table: "im_StoreVariantInventory",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "committed_quantity",
                table: "im_StoreVariantInventory",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "on_hold",
                table: "im_StoreVariantInventory",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "F");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "on_hold",
                table: "im_StoreVariantInventory");

            migrationBuilder.AlterColumn<decimal>(
                name: "on_hand_quantity",
                table: "im_StoreVariantInventory",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "committed_quantity",
                table: "im_StoreVariantInventory",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 0m);
        }
    }
}
