using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class tpm_var_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "committed_quantity",
                table: "temp_im_variants",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "base_price",
                table: "temp_im_variants",
                newName: "cost_price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "temp_im_variants",
                newName: "committed_quantity");

            migrationBuilder.RenameColumn(
                name: "cost_price",
                table: "temp_im_variants",
                newName: "base_price");
        }
    }
}
