using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class _pay_types_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesHeaders_st_Parties_customer_id",
                table: "so_SalesHeaders");

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "st_Parties");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesHeaders_ar_Customers_customer_id",
                table: "so_SalesHeaders",
                column: "customer_id",
                principalTable: "ar_Customers",
                principalColumn: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesHeaders_ar_Customers_customer_id",
                table: "so_SalesHeaders");

            migrationBuilder.AddColumn<Guid>(
                name: "customer_id",
                table: "st_Parties",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesHeaders_st_Parties_customer_id",
                table: "so_SalesHeaders",
                column: "customer_id",
                principalTable: "st_Parties",
                principalColumn: "party_id");
        }
    }
}
