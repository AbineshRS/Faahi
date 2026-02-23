using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class _datut_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_so_SalesLines_batch_id",
                table: "so_SalesLines",
                column: "batch_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesLines_im_itemBatches_batch_id",
                table: "so_SalesLines",
                column: "batch_id",
                principalTable: "im_itemBatches",
                principalColumn: "item_batch_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesLines_im_itemBatches_batch_id",
                table: "so_SalesLines");

            migrationBuilder.DropIndex(
                name: "IX_so_SalesLines_batch_id",
                table: "so_SalesLines");
        }
    }
}
