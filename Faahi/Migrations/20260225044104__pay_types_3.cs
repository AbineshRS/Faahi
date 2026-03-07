using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class _pay_types_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pos_SalePayments_co_business_business_id",
                table: "pos_SalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_SalePayments_so_Payment_Types_payment_method_id",
                table: "pos_SalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_SalePayments_so_SalesHeaders_sale_id",
                table: "pos_SalePayments");

            migrationBuilder.AlterColumn<Guid>(
                name: "sale_id",
                table: "pos_SalePayments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "payment_method_id",
                table: "pos_SalePayments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "business_id",
                table: "pos_SalePayments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_pos_SalePayments_co_business_business_id",
                table: "pos_SalePayments",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pos_SalePayments_so_Payment_Types_payment_method_id",
                table: "pos_SalePayments",
                column: "payment_method_id",
                principalTable: "so_Payment_Types",
                principalColumn: "payment_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pos_SalePayments_so_SalesHeaders_sale_id",
                table: "pos_SalePayments",
                column: "sale_id",
                principalTable: "so_SalesHeaders",
                principalColumn: "sales_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pos_SalePayments_co_business_business_id",
                table: "pos_SalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_SalePayments_so_Payment_Types_payment_method_id",
                table: "pos_SalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_SalePayments_so_SalesHeaders_sale_id",
                table: "pos_SalePayments");

            migrationBuilder.AlterColumn<Guid>(
                name: "sale_id",
                table: "pos_SalePayments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "payment_method_id",
                table: "pos_SalePayments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "business_id",
                table: "pos_SalePayments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_pos_SalePayments_co_business_business_id",
                table: "pos_SalePayments",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pos_SalePayments_so_Payment_Types_payment_method_id",
                table: "pos_SalePayments",
                column: "payment_method_id",
                principalTable: "so_Payment_Types",
                principalColumn: "payment_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pos_SalePayments_so_SalesHeaders_sale_id",
                table: "pos_SalePayments",
                column: "sale_id",
                principalTable: "so_SalesHeaders",
                principalColumn: "sales_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
