using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class qo_valid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "qo_attention",
                table: "so_SalesHeaders",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qo_delivery",
                table: "so_SalesHeaders",
                type: "nvarchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qo_validity",
                table: "so_SalesHeaders",
                type: "nvarchar(30)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "qo_attention",
                table: "so_SalesHeaders");

            migrationBuilder.DropColumn(
                name: "qo_delivery",
                table: "so_SalesHeaders");

            migrationBuilder.DropColumn(
                name: "qo_validity",
                table: "so_SalesHeaders");
        }
    }
}
