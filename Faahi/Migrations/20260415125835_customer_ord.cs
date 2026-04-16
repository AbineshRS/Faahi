using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class customer_ord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "urget_delivery",
                table: "om_CustomerOrders",
                type: "char(1)",
                maxLength: 1,
                nullable: true,
                defaultValue: "F");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "urget_delivery",
                table: "om_CustomerOrders");
        }
    }
}
