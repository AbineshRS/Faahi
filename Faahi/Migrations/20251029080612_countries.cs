using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class countries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "avl_countries",
                columns: table => new
                {
                    avl_countries_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: true),
                    country_code = table.Column<string>(type: "varchar(16)", nullable: true),
                    flag = table.Column<string>(type: "varchar(150)", nullable: true),
                    dialling_code = table.Column<string>(type: "varchar(16)", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(16)", nullable: true),
                    currency_name = table.Column<string>(type: "varchar(100)", nullable: true),
                    serv_available = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avl_countries", x => x.avl_countries_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "avl_countries");
        }
    }
}
