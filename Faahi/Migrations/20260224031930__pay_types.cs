using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class _pay_types : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_so_payment_type_co_business_company_id",
                table: "so_payment_type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_so_payment_type",
                table: "so_payment_type");

            migrationBuilder.RenameTable(
                name: "so_payment_type",
                newName: "so_Payment_Types");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "so_Payment_Types",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "IX_so_payment_type_company_id",
                table: "so_Payment_Types",
                newName: "IX_so_Payment_Types_business_id");

            migrationBuilder.AlterColumn<string>(
                name: "PayTypeCode",
                table: "so_Payment_Types",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AddColumn<Guid>(
                name: "payment_type_id",
                table: "so_Payment_Types",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_so_Payment_Types",
                table: "so_Payment_Types",
                column: "payment_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_so_SalesHeaders_payment_term_id",
                table: "so_SalesHeaders",
                column: "payment_term_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pos_DrawerCountDetails_so_Payment_Types_payment_method_id",
                table: "pos_DrawerCountDetails",
                column: "payment_method_id",
                principalTable: "so_Payment_Types",
                principalColumn: "payment_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pos_SalePayments_so_Payment_Types_payment_method_id",
                table: "pos_SalePayments",
                column: "payment_method_id",
                principalTable: "so_Payment_Types",
                principalColumn: "payment_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_so_Payment_Types_co_business_business_id",
                table: "so_Payment_Types",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesHeaders_so_Payment_Types_payment_term_id",
                table: "so_SalesHeaders",
                column: "payment_term_id",
                principalTable: "so_Payment_Types",
                principalColumn: "payment_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pos_DrawerCountDetails_so_Payment_Types_payment_method_id",
                table: "pos_DrawerCountDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_SalePayments_so_Payment_Types_payment_method_id",
                table: "pos_SalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_so_Payment_Types_co_business_business_id",
                table: "so_Payment_Types");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesHeaders_so_Payment_Types_payment_term_id",
                table: "so_SalesHeaders");

            migrationBuilder.DropIndex(
                name: "IX_so_SalesHeaders_payment_term_id",
                table: "so_SalesHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_so_Payment_Types",
                table: "so_Payment_Types");

            migrationBuilder.DropColumn(
                name: "payment_type_id",
                table: "so_Payment_Types");

            migrationBuilder.RenameTable(
                name: "so_Payment_Types",
                newName: "so_payment_type");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "so_payment_type",
                newName: "company_id");

            migrationBuilder.RenameIndex(
                name: "IX_so_Payment_Types_business_id",
                table: "so_payment_type",
                newName: "IX_so_payment_type_company_id");

            migrationBuilder.AlterColumn<string>(
                name: "PayTypeCode",
                table: "so_payment_type",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_so_payment_type",
                table: "so_payment_type",
                column: "PayTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_so_payment_type_co_business_company_id",
                table: "so_payment_type",
                column: "company_id",
                principalTable: "co_business",
                principalColumn: "company_id");
        }
    }
}
