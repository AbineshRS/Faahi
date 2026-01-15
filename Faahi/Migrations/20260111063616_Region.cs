using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class Region : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sa_country_regions",
                columns: table => new
                {
                    country_region_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    avl_countries_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    region_name = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sa_country_regions", x => x.country_region_id);
                });

            migrationBuilder.CreateTable(
                name: "sa_regions",
                columns: table => new
                {
                    region_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    country_region_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    city = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    sa_country_regionscountry_region_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sa_regions", x => x.region_id);
                    table.ForeignKey(
                        name: "FK_sa_regions_sa_country_regions_sa_country_regionscountry_region_id",
                        column: x => x.sa_country_regionscountry_region_id,
                        principalTable: "sa_country_regions",
                        principalColumn: "country_region_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_sa_regions_sa_country_regionscountry_region_id",
                table: "sa_regions",
                column: "sa_country_regionscountry_region_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sa_regions");

            migrationBuilder.DropTable(
                name: "sa_country_regions");
        }
    }
}
