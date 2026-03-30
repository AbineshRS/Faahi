using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class accountmappingchar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsRequired",
                table: "gl_AccountMapping",
                type: "Char(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "IsActive",
                table: "gl_AccountMapping",
                type: "Char(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsRequired",
                table: "gl_AccountMapping",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Char(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "gl_AccountMapping",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Char(1)");
        }
    }
}
