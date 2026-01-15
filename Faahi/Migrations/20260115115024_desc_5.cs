using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class desc_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fin_PartyBankAccounts",
                columns: table => new
                {
                    party_account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    party_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    bank_name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    account_holder_name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    account_number = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    routing_number = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    swift_code = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    iban = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    currency = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_default = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    ap_Vendorsvendor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fin_PartyBankAccounts", x => x.party_account_id);
                    table.ForeignKey(
                        name: "FK_fin_PartyBankAccounts_ap_Vendors_ap_Vendorsvendor_id",
                        column: x => x.ap_Vendorsvendor_id,
                        principalTable: "ap_Vendors",
                        principalColumn: "vendor_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_fin_PartyBankAccounts_ap_Vendorsvendor_id",
                table: "fin_PartyBankAccounts",
                column: "ap_Vendorsvendor_id");

            migrationBuilder.CreateIndex(
                name: "party_account_id",
                table: "fin_PartyBankAccounts",
                column: "party_account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "party_id",
                table: "fin_PartyBankAccounts",
                column: "party_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fin_PartyBankAccounts");
        }
    }
}
