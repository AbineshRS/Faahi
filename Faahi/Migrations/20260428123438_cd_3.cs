using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class cd_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ar_Customers_ar_customer_due_customer_due_id",
                table: "ar_Customers");

            migrationBuilder.DropIndex(
                name: "IX_ar_Customers_customer_due_id",
                table: "ar_Customers");

            migrationBuilder.DropIndex(
                name: "IX_ar_customer_due",
                table: "ar_customer_due");

            migrationBuilder.DropIndex(
                name: "IX_created_at",
                table: "ar_customer_due");

            migrationBuilder.DropColumn(
                name: "customer_due_id",
                table: "ar_Customers");

            migrationBuilder.RenameColumn(
                name: "customer_due_id",
                table: "ar_customer_due",
                newName: "payment_term_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "payment_term_id",
                table: "ar_Customers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "ar_customer_due",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "ar_customer_due",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldDefaultValue: "T");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ar_customer_due",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateIndex(
                name: "IX_ar_Customers_payment_term_id",
                table: "ar_Customers",
                column: "payment_term_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ar_Customers_ar_customer_due_payment_term_id",
                table: "ar_Customers",
                column: "payment_term_id",
                principalTable: "ar_customer_due",
                principalColumn: "payment_term_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ar_Customers_ar_customer_due_payment_term_id",
                table: "ar_Customers");

            migrationBuilder.DropIndex(
                name: "IX_ar_Customers_payment_term_id",
                table: "ar_Customers");

            migrationBuilder.RenameColumn(
                name: "payment_term_id",
                table: "ar_customer_due",
                newName: "customer_due_id");

            migrationBuilder.AlterColumn<string>(
                name: "payment_term_id",
                table: "ar_Customers",
                type: "varchar(30)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "customer_due_id",
                table: "ar_Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "ar_customer_due",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "ar_customer_due",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "T",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ar_customer_due",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.CreateIndex(
                name: "IX_ar_Customers_customer_due_id",
                table: "ar_Customers",
                column: "customer_due_id");

            migrationBuilder.CreateIndex(
                name: "IX_ar_customer_due",
                table: "ar_customer_due",
                columns: new[] { "customer_due_id", "business_id" });

            migrationBuilder.CreateIndex(
                name: "IX_created_at",
                table: "ar_customer_due",
                column: "created_at");

            migrationBuilder.AddForeignKey(
                name: "FK_ar_Customers_ar_customer_due_customer_due_id",
                table: "ar_Customers",
                column: "customer_due_id",
                principalTable: "ar_customer_due",
                principalColumn: "customer_due_id");
        }
    }
}
