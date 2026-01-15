using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class sper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "super_admin",
                columns: table => new
                {
                    super_admin_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_super_admin", x => x.super_admin_id);
                });

            migrationBuilder.CreateIndex(
                name: "email",
                table: "super_admin",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "phone",
                table: "super_admin",
                column: "phone");

            migrationBuilder.CreateIndex(
                name: "status",
                table: "super_admin",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "super_admin_id",
                table: "super_admin",
                column: "super_admin_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "super_admin");
        }
    }
}
