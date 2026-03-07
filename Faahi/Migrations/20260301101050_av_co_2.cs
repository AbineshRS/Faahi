using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class av_co_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "is_mutiple_payment",
                table: "so_SalesHeaders",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "F");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_mutiple_payment",
                table: "so_SalesHeaders");
        }
    }
}
