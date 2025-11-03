using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class im_ProductCategories_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "category_name",
                table: "im_ProductCategories",
                type: "nvarchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductCategories_category_name",
                table: "im_ProductCategories",
                column: "category_name");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductCategories_is_active",
                table: "im_ProductCategories",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductCategories_Level",
                table: "im_ProductCategories",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductCategories_parent_id",
                table: "im_ProductCategories",
                column: "parent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_im_ProductCategories_category_name",
                table: "im_ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_im_ProductCategories_is_active",
                table: "im_ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_im_ProductCategories_Level",
                table: "im_ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_im_ProductCategories_parent_id",
                table: "im_ProductCategories");

            migrationBuilder.AlterColumn<string>(
                name: "category_name",
                table: "im_ProductCategories",
                type: "varchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldNullable: true);
        }
    }
}
