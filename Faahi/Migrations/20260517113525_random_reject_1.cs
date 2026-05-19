using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class random_reject_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_random_Stock_reject_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "im_random_Stock_reject");

            migrationBuilder.AlterColumn<Guid>(
                name: "adjustment_detail_id",
                table: "im_random_Stock_reject",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_im_random_Stock_reject_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "im_random_Stock_reject",
                column: "adjustment_detail_id",
                principalTable: "im_inventory_adjustment_lines",
                principalColumn: "adjustment_detail_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_random_Stock_reject_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "im_random_Stock_reject");

            migrationBuilder.AlterColumn<Guid>(
                name: "adjustment_detail_id",
                table: "im_random_Stock_reject",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_im_random_Stock_reject_im_inventory_adjustment_lines_adjustment_detail_id",
                table: "im_random_Stock_reject",
                column: "adjustment_detail_id",
                principalTable: "im_inventory_adjustment_lines",
                principalColumn: "adjustment_detail_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
