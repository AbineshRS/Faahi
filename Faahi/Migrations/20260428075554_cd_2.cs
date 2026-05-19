using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class cd_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "customer_due_id",
                table: "ar_Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "due_days",
                table: "ar_Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "due_description",
                table: "ar_Customers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ar_Customers_customer_due_id",
                table: "ar_Customers",
                column: "customer_due_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ar_Customers_ar_customer_due_customer_due_id",
                table: "ar_Customers",
                column: "customer_due_id",
                principalTable: "ar_customer_due",
                principalColumn: "customer_due_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ar_Customers_ar_customer_due_customer_due_id",
                table: "ar_Customers");

            migrationBuilder.DropIndex(
                name: "IX_ar_Customers_customer_due_id",
                table: "ar_Customers");

            migrationBuilder.DropColumn(
                name: "customer_due_id",
                table: "ar_Customers");

            migrationBuilder.DropColumn(
                name: "due_days",
                table: "ar_Customers");

            migrationBuilder.DropColumn(
                name: "due_description",
                table: "ar_Customers");
        }
    }
}
