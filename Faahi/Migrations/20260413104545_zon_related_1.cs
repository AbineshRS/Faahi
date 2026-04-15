using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class zon_related_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mk_blacklisted_numbers",
                columns: table => new
                {
                    blacklist_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<string>(type: "nchar(1)", maxLength: 1, nullable: false, defaultValue: "T")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mk_blacklisted_numbers", x => x.blacklist_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mk_blacklisted_numbers");
        }
    }
}
