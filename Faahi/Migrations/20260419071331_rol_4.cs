using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class rol_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "created_user_id",
                table: "so_SalesHeaders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "om_FulfillmentOrders",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "created_user_id",
                table: "om_FulfillmentOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "om_CustomerOrders",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "created_user_id",
                table: "om_CustomerOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "im_InventoryReservations",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "created_user_id",
                table: "im_InventoryReservations",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_user_id",
                table: "so_SalesHeaders");

            migrationBuilder.DropColumn(
                name: "created_user_id",
                table: "om_FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "created_user_id",
                table: "om_CustomerOrders");

            migrationBuilder.DropColumn(
                name: "created_user_id",
                table: "im_InventoryReservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "om_FulfillmentOrders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "om_CustomerOrders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "im_InventoryReservations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);
        }
    }
}
