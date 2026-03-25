using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class updateglAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "BalanceType",
                table: "gl_Accounts",
                type: "nvarchar(3)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalBalance",
                table: "gl_Accounts",
                type: "nvarchar(10)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceType",
                table: "gl_Accounts");

            migrationBuilder.DropColumn(
                name: "NormalBalance",
                table: "gl_Accounts");

            migrationBuilder.AddColumn<decimal>(
                name: "exchnage_rate",
                table: "co_avl_countries",
                type: "decimal(18,4)",
                nullable: true);
        }
    }
}
