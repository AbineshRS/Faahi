using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class retuensale_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "return_quantity",
                table: "temp_Im_Purchase_Listing_Details",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "return_reason",
                table: "temp_Im_Purchase_Listing_Details",
                type: "nvarchar(300)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "return_quantity",
                table: "temp_Im_Purchase_Listing_Details");

            migrationBuilder.DropColumn(
                name: "return_reason",
                table: "temp_Im_Purchase_Listing_Details");
        }
    }
}
