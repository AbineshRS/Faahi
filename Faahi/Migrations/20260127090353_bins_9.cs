using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class bins_9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "listing_id",
                table: "temp_im_variants",
                newName: "detail_id");

            migrationBuilder.RenameColumn(
                name: "listing_id",
                table: "im_itemBatches",
                newName: "detail_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "detail_id",
                table: "temp_im_variants",
                newName: "listing_id");

            migrationBuilder.RenameColumn(
                name: "detail_id",
                table: "im_itemBatches",
                newName: "listing_id");
        }
    }
}
