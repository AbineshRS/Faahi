using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class st_users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "st_sellers");

            migrationBuilder.CreateTable(
                name: "st_Users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Full_name = table.Column<string>(type: "varchar(255)", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", nullable: true),
                    phone = table.Column<string>(type: "varchar(30)", nullable: true),
                    password = table.Column<string>(type: "varchar(max)", nullable: true),
                    account_type = table.Column<string>(type: "varchar(30)", nullable: true),
                    registration_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_Users", x => x.user_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "st_Users");

            migrationBuilder.CreateTable(
                name: "st_sellers",
                columns: table => new
                {
                    seller_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Full_name = table.Column<string>(type: "varchar(255)", nullable: true),
                    account_type = table.Column<string>(type: "varchar(30)", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", nullable: true),
                    password = table.Column<string>(type: "varchar(max)", nullable: true),
                    phone = table.Column<string>(type: "varchar(30)", nullable: true),
                    registration_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_sellers", x => x.seller_id);
                });
        }
    }
}
