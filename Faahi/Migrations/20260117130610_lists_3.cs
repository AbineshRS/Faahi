using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class lists_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uom_id",
                table: "im_ProductVariants");

            migrationBuilder.AddColumn<string>(
                name: "uom_name",
                table: "im_ProductVariants",
                type: "varchar(20)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uom_name",
                table: "im_ProductVariants");

            migrationBuilder.AddColumn<Guid>(
                name: "uom_id",
                table: "im_ProductVariants",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
