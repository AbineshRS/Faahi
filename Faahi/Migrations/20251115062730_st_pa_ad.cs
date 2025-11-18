using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class st_pa_ad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ar_Customerscustomer_id",
                table: "st_PartyAddresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "customer_id",
                table: "st_PartyAddresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_st_PartyAddresses_ar_Customerscustomer_id",
                table: "st_PartyAddresses",
                column: "ar_Customerscustomer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_st_PartyAddresses_ar_Customers_ar_Customerscustomer_id",
                table: "st_PartyAddresses",
                column: "ar_Customerscustomer_id",
                principalTable: "ar_Customers",
                principalColumn: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_st_PartyAddresses_ar_Customers_ar_Customerscustomer_id",
                table: "st_PartyAddresses");

            migrationBuilder.DropIndex(
                name: "IX_st_PartyAddresses_ar_Customerscustomer_id",
                table: "st_PartyAddresses");

            migrationBuilder.DropColumn(
                name: "ar_Customerscustomer_id",
                table: "st_PartyAddresses");

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "st_PartyAddresses");
        }
    }
}
