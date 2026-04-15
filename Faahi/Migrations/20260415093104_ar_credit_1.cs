using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class ar_credit_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "delevery_end_time",
                table: "om_CustomerOrders",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "delevery_start_time",
                table: "om_CustomerOrders",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "delevery_end_time",
                table: "om_CustomerOrders");

            migrationBuilder.DropColumn(
                name: "delevery_start_time",
                table: "om_CustomerOrders");
        }
    }
}
