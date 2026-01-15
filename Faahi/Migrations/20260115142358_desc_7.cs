using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class desc_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ar_Customerscustomer_id",
                table: "fin_PartyBankAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_fin_PartyBankAccounts_ar_Customerscustomer_id",
                table: "fin_PartyBankAccounts",
                column: "ar_Customerscustomer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_fin_PartyBankAccounts_ar_Customers_ar_Customerscustomer_id",
                table: "fin_PartyBankAccounts",
                column: "ar_Customerscustomer_id",
                principalTable: "ar_Customers",
                principalColumn: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fin_PartyBankAccounts_ar_Customers_ar_Customerscustomer_id",
                table: "fin_PartyBankAccounts");

            migrationBuilder.DropIndex(
                name: "IX_fin_PartyBankAccounts_ar_Customerscustomer_id",
                table: "fin_PartyBankAccounts");

            migrationBuilder.DropColumn(
                name: "ar_Customerscustomer_id",
                table: "fin_PartyBankAccounts");
        }
    }
}
