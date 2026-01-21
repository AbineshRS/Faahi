using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class tpm_var_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "average_cost",
                table: "im_ProductVariants",
                type: "decimal(16,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "chargeable_weight_kg",
                table: "im_ProductVariants",
                type: "decimal(16,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "height_cm",
                table: "im_ProductVariants",
                type: "decimal(16,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "length_cm",
                table: "im_ProductVariants",
                type: "decimal(16,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "weight_kg",
                table: "im_ProductVariants",
                type: "decimal(16,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "width_cm",
                table: "im_ProductVariants",
                type: "decimal(16,4)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "average_cost",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "chargeable_weight_kg",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "height_cm",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "length_cm",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "weight_kg",
                table: "im_ProductVariants");

            migrationBuilder.DropColumn(
                name: "width_cm",
                table: "im_ProductVariants");
        }
    }
}
