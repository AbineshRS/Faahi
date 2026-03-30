using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class glaccountchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_gl_AccountDefaults_CompanyPurpose",
                table: "gl_AccountMapping",
                newName: "UQ_gl_AccountMapping_CompanyStoreModulePurpose");

            migrationBuilder.AlterColumn<string>(
                name: "IsRequired",
                table: "gl_AccountMapping",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Char(1)");

            migrationBuilder.AlterColumn<string>(
                name: "IsActive",
                table: "gl_AccountMapping",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Char(1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_gl_AccountMapping_CompanyStoreModulePurpose",
                table: "gl_AccountMapping",
                newName: "UQ_gl_AccountDefaults_CompanyPurpose");

            migrationBuilder.AlterColumn<string>(
                name: "IsRequired",
                table: "gl_AccountMapping",
                type: "Char(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "IsActive",
                table: "gl_AccountMapping",
                type: "Char(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1);
        }
    }
}
