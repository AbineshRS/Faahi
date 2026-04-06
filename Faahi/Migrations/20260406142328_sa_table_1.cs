using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class sa_table_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "st_UserRoles",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "st_UserRoles",
                type: "char(1)",
                maxLength: 1,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "st_UserRoles");

            migrationBuilder.DropColumn(
                name: "status",
                table: "st_UserRoles");
        }
    }
}
