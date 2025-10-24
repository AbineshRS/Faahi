using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class co_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "createdSites_users",
                table: "co_business",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sites_users_allowed",
                table: "co_business",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdSites_users",
                table: "co_business");

            migrationBuilder.DropColumn(
                name: "sites_users_allowed",
                table: "co_business");
        }
    }
}
