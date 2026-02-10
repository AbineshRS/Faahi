using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class pyas_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "vendor_Code",
                table: "im_Products",
                newName: "vendor_id");

            migrationBuilder.AddColumn<string>(
                name: "item_kit",
                table: "im_Products",
                type: "char(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "on_hold",
                table: "im_Products",
                type: "char(1)",
                maxLength: 1,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "item_kit",
                table: "im_Products");

            migrationBuilder.DropColumn(
                name: "on_hold",
                table: "im_Products");

            migrationBuilder.RenameColumn(
                name: "vendor_id",
                table: "im_Products",
                newName: "vendor_Code");
        }
    }
}
