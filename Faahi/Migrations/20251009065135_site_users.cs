using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class site_users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "im_site_users",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    site_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    site_user_code = table.Column<string>(type: "varchar(30)", nullable: true),
                    userName = table.Column<string>(type: "varchar(32)", nullable: true),
                    password = table.Column<string>(type: "varchar(200)", nullable: true),
                    firstName = table.Column<string>(type: "varchar(50)", nullable: false),
                    lastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    fullName = table.Column<string>(type: "varchar(200)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    userRole = table.Column<string>(type: "varchar(20)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    phoneNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    address = table.Column<string>(type: "varchar(200)", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_site_users", x => x.userId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_site_users");
        }
    }
}
