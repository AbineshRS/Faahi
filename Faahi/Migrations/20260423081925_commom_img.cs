using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class commom_img : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "is_active",
                table: "mk_blacklisted_numbers",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "T",
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldMaxLength: 1,
                oldDefaultValue: "T");

            migrationBuilder.CreateTable(
                name: "sys_Images",
                columns: table => new
                {
                    image_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    source_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    source_type = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    image_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    status = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "T")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Images", x => x.image_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_image_url",
                table: "sys_Images",
                column: "source_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Images",
                table: "sys_Images",
                columns: new[] { "image_id", "source_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_Images");

            migrationBuilder.AlterColumn<string>(
                name: "is_active",
                table: "mk_blacklisted_numbers",
                type: "nchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "T",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldDefaultValue: "T");
        }
    }
}
