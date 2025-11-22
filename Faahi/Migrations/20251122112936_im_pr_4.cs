using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class im_pr_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "im_ProductImages",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "im_ProductVariantsvariant_id",
                table: "im_ProductImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductImages_im_ProductVariantsvariant_id",
                table: "im_ProductImages",
                column: "im_ProductVariantsvariant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_ProductImages_im_ProductVariants_im_ProductVariantsvariant_id",
                table: "im_ProductImages",
                column: "im_ProductVariantsvariant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_ProductImages_im_ProductVariants_im_ProductVariantsvariant_id",
                table: "im_ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_im_ProductImages_im_ProductVariantsvariant_id",
                table: "im_ProductImages");

            migrationBuilder.DropColumn(
                name: "im_ProductVariantsvariant_id",
                table: "im_ProductImages");

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "im_ProductImages",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);
        }
    }
}
