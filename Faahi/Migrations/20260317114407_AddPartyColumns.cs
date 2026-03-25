using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class AddPartyColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address1",
                table: "st_Parties",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address2",
                table: "st_Parties",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "st_Parties",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contact_person",
                table: "st_Parties",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country_id",
                table: "st_Parties",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_no",
                table: "st_Parties",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "is_active",
                table: "st_Parties",
                type: "char(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mobile",
                table: "st_Parties",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "st_Parties",
                type: "varchar(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "party_category",
                table: "st_Parties",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "party_code",
                table: "st_Parties",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "party_kind",
                table: "st_Parties",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "party_name",
                table: "st_Parties",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reg_no",
                table: "st_Parties",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tax_no",
                table: "st_Parties",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "party_id",
                table: "ar_Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "idx_party_code",
                table: "st_Parties",
                column: "party_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_party_code",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "address1",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "address2",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "city",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "contact_person",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "country_id",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "id_no",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "mobile",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "party_category",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "party_code",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "party_kind",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "party_name",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "reg_no",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "tax_no",
                table: "st_Parties");

            migrationBuilder.DropColumn(
                name: "party_id",
                table: "ar_Customers");
        }
    }
}
