using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class st_pa_ad_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ap_Vendorsvendor_id",
                table: "st_PartyAddresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "vendor_id",
                table: "st_PartyAddresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_st_PartyAddresses_ap_Vendorsvendor_id",
                table: "st_PartyAddresses",
                column: "ap_Vendorsvendor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_st_PartyAddresses_ap_Vendors_ap_Vendorsvendor_id",
                table: "st_PartyAddresses",
                column: "ap_Vendorsvendor_id",
                principalTable: "ap_Vendors",
                principalColumn: "vendor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_st_PartyAddresses_ap_Vendors_ap_Vendorsvendor_id",
                table: "st_PartyAddresses");

            migrationBuilder.DropIndex(
                name: "IX_st_PartyAddresses_ap_Vendorsvendor_id",
                table: "st_PartyAddresses");

            migrationBuilder.DropColumn(
                name: "ap_Vendorsvendor_id",
                table: "st_PartyAddresses");

            migrationBuilder.DropColumn(
                name: "vendor_id",
                table: "st_PartyAddresses");
        }
    }
}
