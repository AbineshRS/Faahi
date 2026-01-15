using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class sper_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "super_admin",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "super_admin",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_type",
                table: "super_admin",
                type: "nvarchar(10)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "super_admin");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "super_admin");

            migrationBuilder.DropColumn(
                name: "user_type",
                table: "super_admin");
        }
    }
}
