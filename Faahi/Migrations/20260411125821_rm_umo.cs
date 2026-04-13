using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class rm_umo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrderLines_im_UnitsOfMeasures_uom_id",
                table: "om_CustomerOrderLines");

            migrationBuilder.DropIndex(
                name: "IX_om_CustomerOrderLines_uom_id",
                table: "om_CustomerOrderLines");

            migrationBuilder.DropColumn(
                name: "uom_id",
                table: "om_CustomerOrderLines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "uom_id",
                table: "om_CustomerOrderLines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_om_CustomerOrderLines_uom_id",
                table: "om_CustomerOrderLines",
                column: "uom_id");

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrderLines_im_UnitsOfMeasures_uom_id",
                table: "om_CustomerOrderLines",
                column: "uom_id",
                principalTable: "im_UnitsOfMeasures",
                principalColumn: "uom_id");
        }
    }
}
