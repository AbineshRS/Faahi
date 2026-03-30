using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class userssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_am_roles_am_users_am_usersuserId",
                table: "am_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_gl_AccountMapping_gl_Accounts_GlAccountId",
                table: "gl_AccountMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_gl_ledger_co_business_BusinessId",
                table: "gl_ledger");

            migrationBuilder.DropForeignKey(
                name: "FK_gl_ledger_gl_Accounts_GlAccountId",
                table: "gl_ledger");

            migrationBuilder.DropForeignKey(
                name: "FK_gl_ledger_gl_JournalHeaders_JournalId",
                table: "gl_ledger");

            migrationBuilder.DropForeignKey(
                name: "FK_gl_ledger_gl_JournalLines_JournalLineId",
                table: "gl_ledger");

            migrationBuilder.DropIndex(
                name: "IX_am_roles_am_usersuserId",
                table: "am_roles");

            migrationBuilder.DropColumn(
                name: "am_usersuserId",
                table: "am_roles");

            migrationBuilder.AlterColumn<Guid>(
                name: "LedgerId",
                table: "gl_ledger",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AlterColumn<string>(
                name: "IsClosed",
                table: "gl_FiscalPeriods",
                type: "Char(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldDefaultValue: "F");

            migrationBuilder.AlterColumn<Guid>(
                name: "PeriodId",
                table: "gl_FiscalPeriods",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AlterColumn<decimal>(
                name: "exchange_rate",
                table: "co_avl_countries",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "firstName",
                table: "am_users",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AddColumn<Guid>(
                name: "userId",
                table: "am_roles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_am_roles_userId",
                table: "am_roles",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_am_roles_am_users_userId",
                table: "am_roles",
                column: "userId",
                principalTable: "am_users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gl_AccountMapping_gl_Accounts_GlAccountId",
                table: "gl_AccountMapping",
                column: "GlAccountId",
                principalTable: "gl_Accounts",
                principalColumn: "GlAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_gl_ledger_gl_Accounts_GlAccountId",
                table: "gl_ledger",
                column: "GlAccountId",
                principalTable: "gl_Accounts",
                principalColumn: "GlAccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gl_ledger_gl_JournalHeaders_JournalId",
                table: "gl_ledger",
                column: "JournalId",
                principalTable: "gl_JournalHeaders",
                principalColumn: "JournalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gl_ledger_gl_JournalLines_JournalLineId",
                table: "gl_ledger",
                column: "JournalLineId",
                principalTable: "gl_JournalLines",
                principalColumn: "JournalLineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_im_purchase_return_details_line_im_ProductVariants_sub_variant_id",
                table: "im_purchase_return_details_line",
                column: "sub_variant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_purchase_return_details_line_im_Products_product_id",
                table: "im_purchase_return_details_line",
                column: "product_id",
                principalTable: "im_Products",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_purchase_return_details_line_im_StoreVariantInventory_store_variant_inventory_id",
                table: "im_purchase_return_details_line",
                column: "store_variant_inventory_id",
                principalTable: "im_StoreVariantInventory",
                principalColumn: "store_variant_inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_purchase_return_details_line_im_purchase_return_header_return_id",
                table: "im_purchase_return_details_line",
                column: "return_id",
                principalTable: "im_purchase_return_header",
                principalColumn: "return_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_purchase_return_header_ap_Vendors_vendor_id",
                table: "im_purchase_return_header",
                column: "vendor_id",
                principalTable: "ap_Vendors",
                principalColumn: "vendor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_im_purchase_return_header_st_stores_site_id",
                table: "im_purchase_return_header",
                column: "site_id",
                principalTable: "st_stores",
                principalColumn: "store_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pos_DrawerCountDetails_co_business_business_id",
                table: "pos_DrawerCountDetails",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pos_DrawerCountDetails_pos_DrawerSessions_drawer_session_id",
                table: "pos_DrawerCountDetails",
                column: "drawer_session_id",
                principalTable: "pos_DrawerSessions",
                principalColumn: "drawer_session_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pos_DrawerCountDetails_so_Payment_Types_payment_method_id",
                table: "pos_DrawerCountDetails",
                column: "payment_method_id",
                principalTable: "so_Payment_Types",
                principalColumn: "payment_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pos_DrawerSessions_co_business_business_id",
                table: "pos_DrawerSessions",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pos_DrawerSessions_st_stores_store_id",
                table: "pos_DrawerSessions",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pos_ReturnsalePayments_co_business_business_id",
                table: "pos_ReturnsalePayments",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pos_ReturnsalePayments_so_Payment_Types_payment_method_id",
                table: "pos_ReturnsalePayments",
                column: "payment_method_id",
                principalTable: "so_Payment_Types",
                principalColumn: "payment_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pos_ReturnsalePayments_so_SalesHeaders_sale_id",
                table: "pos_ReturnsalePayments",
                column: "sale_id",
                principalTable: "so_SalesHeaders",
                principalColumn: "sales_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pos_ReturnsalePayments_st_stores_store_id",
                table: "pos_ReturnsalePayments",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_pos_SalePayments_st_stores_store_id",
                table: "pos_SalePayments",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_Payment_Types_co_business_business_id",
                table: "so_Payment_Types",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesHeaders_ar_Customers_customer_id",
                table: "so_SalesHeaders",
                column: "customer_id",
                principalTable: "ar_Customers",
                principalColumn: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesHeaders_co_business_business_id",
                table: "so_SalesHeaders",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesHeaders_so_Payment_Types_payment_term_id",
                table: "so_SalesHeaders",
                column: "payment_term_id",
                principalTable: "so_Payment_Types",
                principalColumn: "payment_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesHeaders_st_stores_store_id",
                table: "so_SalesHeaders",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesLines_co_business_business_id",
                table: "so_SalesLines",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesLines_im_ProductVariants_variant_id",
                table: "so_SalesLines",
                column: "variant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesLines_im_Products_product_id",
                table: "so_SalesLines",
                column: "product_id",
                principalTable: "im_Products",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesLines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "so_SalesLines",
                column: "store_variant_inventory_id",
                principalTable: "im_StoreVariantInventory",
                principalColumn: "store_variant_inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesLines_im_itemBatches_batch_id",
                table: "so_SalesLines",
                column: "batch_id",
                principalTable: "im_itemBatches",
                principalColumn: "item_batch_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesLines_so_SalesHeaders_sales_id",
                table: "so_SalesLines",
                column: "sales_id",
                principalTable: "so_SalesHeaders",
                principalColumn: "sales_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesLines_st_stores_store_id",
                table: "so_SalesLines",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnHeaders_ar_Customers_customer_id",
                table: "so_SalesReturnHeaders",
                column: "customer_id",
                principalTable: "ar_Customers",
                principalColumn: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnHeaders_co_business_business_id",
                table: "so_SalesReturnHeaders",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnHeaders_so_Payment_Types_payment_term_id",
                table: "so_SalesReturnHeaders",
                column: "payment_term_id",
                principalTable: "so_Payment_Types",
                principalColumn: "payment_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnHeaders_so_SalesHeaders_sales_id",
                table: "so_SalesReturnHeaders",
                column: "sales_id",
                principalTable: "so_SalesHeaders",
                principalColumn: "sales_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnHeaders_st_stores_store_id",
                table: "so_SalesReturnHeaders",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnLines_co_business_business_id",
                table: "so_SalesReturnLines",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnLines_im_ProductVariants_variant_id",
                table: "so_SalesReturnLines",
                column: "variant_id",
                principalTable: "im_ProductVariants",
                principalColumn: "variant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnLines_im_Products_product_id",
                table: "so_SalesReturnLines",
                column: "product_id",
                principalTable: "im_Products",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnLines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "so_SalesReturnLines",
                column: "store_variant_inventory_id",
                principalTable: "im_StoreVariantInventory",
                principalColumn: "store_variant_inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnLines_im_itemBatches_batch_id",
                table: "so_SalesReturnLines",
                column: "batch_id",
                principalTable: "im_itemBatches",
                principalColumn: "item_batch_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnLines_so_SalesReturnHeaders_sales_return_id",
                table: "so_SalesReturnLines",
                column: "sales_return_id",
                principalTable: "so_SalesReturnHeaders",
                principalColumn: "sales_return_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnLines_so_SalesReturnHeaders_so_SalesReturnHeaderssales_return_id",
                table: "so_SalesReturnLines",
                column: "so_SalesReturnHeaderssales_return_id",
                principalTable: "so_SalesReturnHeaders",
                principalColumn: "sales_return_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesReturnLines_st_stores_store_id",
                table: "so_SalesReturnLines",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_am_roles_am_users_userId",
                table: "am_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_gl_AccountMapping_gl_Accounts_GlAccountId",
                table: "gl_AccountMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_gl_ledger_gl_Accounts_GlAccountId",
                table: "gl_ledger");

            migrationBuilder.DropForeignKey(
                name: "FK_gl_ledger_gl_JournalHeaders_JournalId",
                table: "gl_ledger");

            migrationBuilder.DropForeignKey(
                name: "FK_gl_ledger_gl_JournalLines_JournalLineId",
                table: "gl_ledger");

            migrationBuilder.DropForeignKey(
                name: "FK_im_purchase_return_details_line_im_ProductVariants_sub_variant_id",
                table: "im_purchase_return_details_line");

            migrationBuilder.DropForeignKey(
                name: "FK_im_purchase_return_details_line_im_Products_product_id",
                table: "im_purchase_return_details_line");

            migrationBuilder.DropForeignKey(
                name: "FK_im_purchase_return_details_line_im_StoreVariantInventory_store_variant_inventory_id",
                table: "im_purchase_return_details_line");

            migrationBuilder.DropForeignKey(
                name: "FK_im_purchase_return_details_line_im_purchase_return_header_return_id",
                table: "im_purchase_return_details_line");

            migrationBuilder.DropForeignKey(
                name: "FK_im_purchase_return_header_ap_Vendors_vendor_id",
                table: "im_purchase_return_header");

            migrationBuilder.DropForeignKey(
                name: "FK_im_purchase_return_header_st_stores_site_id",
                table: "im_purchase_return_header");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_DrawerCountDetails_co_business_business_id",
                table: "pos_DrawerCountDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_DrawerCountDetails_pos_DrawerSessions_drawer_session_id",
                table: "pos_DrawerCountDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_DrawerCountDetails_so_Payment_Types_payment_method_id",
                table: "pos_DrawerCountDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_DrawerSessions_co_business_business_id",
                table: "pos_DrawerSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_DrawerSessions_st_stores_store_id",
                table: "pos_DrawerSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_ReturnsalePayments_co_business_business_id",
                table: "pos_ReturnsalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_ReturnsalePayments_so_Payment_Types_payment_method_id",
                table: "pos_ReturnsalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_ReturnsalePayments_so_SalesHeaders_sale_id",
                table: "pos_ReturnsalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_ReturnsalePayments_st_stores_store_id",
                table: "pos_ReturnsalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_SalePayments_co_business_business_id",
                table: "pos_SalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_SalePayments_so_Payment_Types_payment_method_id",
                table: "pos_SalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_SalePayments_so_SalesHeaders_sale_id",
                table: "pos_SalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_pos_SalePayments_st_stores_store_id",
                table: "pos_SalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_so_Payment_Types_co_business_business_id",
                table: "so_Payment_Types");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesHeaders_ar_Customers_customer_id",
                table: "so_SalesHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesHeaders_co_business_business_id",
                table: "so_SalesHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesHeaders_so_Payment_Types_payment_term_id",
                table: "so_SalesHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesHeaders_st_stores_store_id",
                table: "so_SalesHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesLines_co_business_business_id",
                table: "so_SalesLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesLines_im_ProductVariants_variant_id",
                table: "so_SalesLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesLines_im_Products_product_id",
                table: "so_SalesLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesLines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "so_SalesLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesLines_im_itemBatches_batch_id",
                table: "so_SalesLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesLines_so_SalesHeaders_sales_id",
                table: "so_SalesLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesLines_st_stores_store_id",
                table: "so_SalesLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnHeaders_ar_Customers_customer_id",
                table: "so_SalesReturnHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnHeaders_co_business_business_id",
                table: "so_SalesReturnHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnHeaders_so_Payment_Types_payment_term_id",
                table: "so_SalesReturnHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnHeaders_so_SalesHeaders_sales_id",
                table: "so_SalesReturnHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnHeaders_st_stores_store_id",
                table: "so_SalesReturnHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnLines_co_business_business_id",
                table: "so_SalesReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnLines_im_ProductVariants_variant_id",
                table: "so_SalesReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnLines_im_Products_product_id",
                table: "so_SalesReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnLines_im_StoreVariantInventory_store_variant_inventory_id",
                table: "so_SalesReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnLines_im_itemBatches_batch_id",
                table: "so_SalesReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnLines_so_SalesReturnHeaders_sales_return_id",
                table: "so_SalesReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnLines_so_SalesReturnHeaders_so_SalesReturnHeaderssales_return_id",
                table: "so_SalesReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesReturnLines_st_stores_store_id",
                table: "so_SalesReturnLines");

            migrationBuilder.DropIndex(
                name: "IX_am_roles_userId",
                table: "am_roles");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "am_roles");

            migrationBuilder.AlterColumn<Guid>(
                name: "LedgerId",
                table: "gl_ledger",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "IsClosed",
                table: "gl_FiscalPeriods",
                type: "char(1)",
                nullable: false,
                defaultValue: "F",
                oldClrType: typeof(string),
                oldType: "Char(1)");

            migrationBuilder.AlterColumn<Guid>(
                name: "PeriodId",
                table: "gl_FiscalPeriods",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "exchange_rate",
                table: "co_avl_countries",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "firstName",
                table: "am_users",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "am_usersuserId",
                table: "am_roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_am_roles_am_usersuserId",
                table: "am_roles",
                column: "am_usersuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_am_roles_am_users_am_usersuserId",
                table: "am_roles",
                column: "am_usersuserId",
                principalTable: "am_users",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_gl_AccountMapping_gl_Accounts_GlAccountId",
                table: "gl_AccountMapping",
                column: "GlAccountId",
                principalTable: "gl_Accounts",
                principalColumn: "GlAccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_gl_ledger_co_business_BusinessId",
                table: "gl_ledger",
                column: "BusinessId",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_gl_ledger_gl_Accounts_GlAccountId",
                table: "gl_ledger",
                column: "GlAccountId",
                principalTable: "gl_Accounts",
                principalColumn: "GlAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_gl_ledger_gl_JournalHeaders_JournalId",
                table: "gl_ledger",
                column: "JournalId",
                principalTable: "gl_JournalHeaders",
                principalColumn: "JournalId");

            migrationBuilder.AddForeignKey(
                name: "FK_gl_ledger_gl_JournalLines_JournalLineId",
                table: "gl_ledger",
                column: "JournalLineId",
                principalTable: "gl_JournalLines",
                principalColumn: "JournalLineId");
        }
    }
}
