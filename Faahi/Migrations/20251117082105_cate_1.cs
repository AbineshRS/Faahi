using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class cate_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "item_subclass_id",
                table: "im_Products",
                newName: "parent_id");

            migrationBuilder.RenameColumn(
                name: "item_class_id",
                table: "im_Products",
                newName: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "im_Products",
                newName: "item_subclass_id");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "im_Products",
                newName: "item_class_id");
        }
    }
}
