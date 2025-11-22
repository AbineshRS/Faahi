using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class im_pr_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "im_Products",
                newName: "sub_sub_category_id");

            migrationBuilder.AddColumn<Guid>(
                name: "sub_category_id",
                table: "im_Products",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sub_category_id",
                table: "im_Products");

            migrationBuilder.RenameColumn(
                name: "sub_sub_category_id",
                table: "im_Products",
                newName: "parent_id");
        }
    }
}
