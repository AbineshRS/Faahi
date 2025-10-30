using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class changes_maxs_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "parent_id",
                table: "im_ProductCategories",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "im_ProductCategories");
        }
    }
}
