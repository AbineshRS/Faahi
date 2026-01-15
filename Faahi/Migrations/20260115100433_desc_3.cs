using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class desc_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_st_PartyRoles_st_Parties_st_Partiesparty_id",
                table: "st_PartyRoles");

            migrationBuilder.DropIndex(
                name: "IX_st_PartyRoles_st_Partiesparty_id",
                table: "st_PartyRoles");

            migrationBuilder.DropColumn(
                name: "st_Partiesparty_id",
                table: "st_PartyRoles");

            migrationBuilder.RenameColumn(
                name: "vsco_id",
                table: "st_Parties",
                newName: "company_id");

            migrationBuilder.AddColumn<Guid>(
                name: "st_Partiesparty_id",
                table: "ar_Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "party_id",
                table: "ap_Vendors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "st_Partiesparty_id",
                table: "ap_Vendors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ar_Customers_st_Partiesparty_id",
                table: "ar_Customers",
                column: "st_Partiesparty_id");

            migrationBuilder.CreateIndex(
                name: "IX_ap_Vendors_st_Partiesparty_id",
                table: "ap_Vendors",
                column: "st_Partiesparty_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ap_Vendors_st_Parties_st_Partiesparty_id",
                table: "ap_Vendors",
                column: "st_Partiesparty_id",
                principalTable: "st_Parties",
                principalColumn: "party_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ar_Customers_st_Parties_st_Partiesparty_id",
                table: "ar_Customers",
                column: "st_Partiesparty_id",
                principalTable: "st_Parties",
                principalColumn: "party_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ap_Vendors_st_Parties_st_Partiesparty_id",
                table: "ap_Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_ar_Customers_st_Parties_st_Partiesparty_id",
                table: "ar_Customers");

            migrationBuilder.DropIndex(
                name: "IX_ar_Customers_st_Partiesparty_id",
                table: "ar_Customers");

            migrationBuilder.DropIndex(
                name: "IX_ap_Vendors_st_Partiesparty_id",
                table: "ap_Vendors");

            migrationBuilder.DropColumn(
                name: "st_Partiesparty_id",
                table: "ar_Customers");

            migrationBuilder.DropColumn(
                name: "party_id",
                table: "ap_Vendors");

            migrationBuilder.DropColumn(
                name: "st_Partiesparty_id",
                table: "ap_Vendors");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "st_Parties",
                newName: "vsco_id");

            migrationBuilder.AddColumn<Guid>(
                name: "st_Partiesparty_id",
                table: "st_PartyRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_st_PartyRoles_st_Partiesparty_id",
                table: "st_PartyRoles",
                column: "st_Partiesparty_id");

            migrationBuilder.AddForeignKey(
                name: "FK_st_PartyRoles_st_Parties_st_Partiesparty_id",
                table: "st_PartyRoles",
                column: "st_Partiesparty_id",
                principalTable: "st_Parties",
                principalColumn: "party_id");
        }
    }
}
