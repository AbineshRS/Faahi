using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "am_emailVerifications",
                columns: table => new
                {
                    Email_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", nullable: true),
                    verificationType = table.Column<string>(type: "varchar(30)", nullable: true),
                    tokenExpiryTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    token = table.Column<string>(type: "varchar(max)", nullable: true),
                    isExpired = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    verified = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    userType = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_am_emailVerifications", x => x.Email_id);
                });

            migrationBuilder.CreateTable(
                name: "am_table_next_key",
                columns: table => new
                {
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    next_key = table.Column<int>(type: "int", nullable: false),
                    site_code = table.Column<string>(type: "varchar(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_am_table_next_key", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "am_users",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userName = table.Column<string>(type: "varchar(32)", nullable: true),
                    password = table.Column<string>(type: "varchar(200)", nullable: true),
                    firstName = table.Column<string>(type: "varchar(50)", nullable: false),
                    lastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    fullName = table.Column<string>(type: "varchar(200)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    isGoogleSignUp = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    googleId = table.Column<string>(type: "varchar(100)", nullable: true),
                    emailVerified = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    phoneNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    address1 = table.Column<string>(type: "varchar(200)", nullable: true),
                    address2 = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_am_users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "ap_Vendors",
                columns: table => new
                {
                    vendor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    vendor_code = table.Column<string>(type: "varchar(30)", nullable: true),
                    payment_term_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    preferred_payment_method = table.Column<string>(type: "varchar(20)", nullable: true),
                    withholding_tax_rate = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    ap_control_account = table.Column<string>(type: "varchar(40)", nullable: true),
                    note = table.Column<string>(type: "varchar(40)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    contact_name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    contact_phone1 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    contact_phone2 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    contact_website = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    contact_email = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    tex_identification_number = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ap_Vendors", x => x.vendor_id);
                });

            migrationBuilder.CreateTable(
                name: "ar_Customers",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price_tier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    customer_code = table.Column<string>(type: "varchar(30)", nullable: true),
                    payment_term_id = table.Column<string>(type: "varchar(30)", nullable: true),
                    credit_limit = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    default_billing_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    default_shipping_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    loyalty_points = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    loyalty_level = table.Column<string>(type: "varchar(40)", nullable: true),
                    note = table.Column<string>(type: "varchar(50)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    credit_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    tax_exempt = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    contact_name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    contact_phone1 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    contact_phone2 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    contact_email = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    tex_identification_number = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ar_Customers", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "avl_countries",
                columns: table => new
                {
                    avl_countries_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: true),
                    country_code = table.Column<string>(type: "varchar(16)", nullable: true),
                    flag = table.Column<string>(type: "varchar(150)", nullable: true),
                    dialling_code = table.Column<string>(type: "varchar(16)", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(16)", nullable: true),
                    currency_name = table.Column<string>(type: "varchar(100)", nullable: true),
                    serv_available = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avl_countries", x => x.avl_countries_id);
                });

            migrationBuilder.CreateTable(
                name: "co_avl_countries",
                columns: table => new
                {
                    avl_countries_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: true),
                    country_code = table.Column<string>(type: "varchar(16)", nullable: true),
                    flag = table.Column<string>(type: "varchar(150)", nullable: true),
                    dialling_code = table.Column<string>(type: "varchar(16)", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(16)", nullable: true),
                    currency_name = table.Column<string>(type: "varchar(100)", nullable: true),
                    serv_available = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_co_avl_countries", x => x.avl_countries_id);
                });

            migrationBuilder.CreateTable(
                name: "co_business",
                columns: table => new
                {
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_code = table.Column<string>(type: "varchar(200)", nullable: true),
                    business_name = table.Column<string>(type: "varchar(100)", nullable: true),
                    tin_number = table.Column<string>(type: "varchar(50)", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", nullable: true),
                    password = table.Column<string>(type: "varchar(100)", nullable: true),
                    reg_no = table.Column<string>(type: "varchar(50)", nullable: true),
                    country = table.Column<string>(type: "varchar(20)", nullable: true),
                    address = table.Column<string>(type: "varchar(max)", nullable: true),
                    logo_fileName = table.Column<string>(type: "varchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    plan_type = table.Column<string>(type: "varchar(20)", nullable: true),
                    sites_allowed = table.Column<int>(type: "int", nullable: true),
                    createdSites = table.Column<int>(type: "int", nullable: true),
                    sites_users_allowed = table.Column<int>(type: "int", nullable: true),
                    createdSites_users = table.Column<int>(type: "int", nullable: true),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    email = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_co_business", x => x.company_id);
                });

            migrationBuilder.CreateTable(
                name: "fx_Currencies",
                columns: table => new
                {
                    currency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    country_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    country_code = table.Column<string>(type: "char(3)", nullable: false),
                    currency_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    currency_symbol = table.Column<string>(type: "nvarchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fx_Currencies", x => x.currency_id);
                });

            migrationBuilder.CreateTable(
                name: "im_InventoryLedger",
                columns: table => new
                {
                    ledger_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    transaction_type = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    unit_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    currency = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    source_doc_type = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    source_doc_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    source_doc_line = table.Column<int>(type: "int", nullable: true),
                    reference_note = table.Column<string>(type: "varchar(400)", nullable: true),
                    cost_method = table.Column<string>(type: "varchar(50)", nullable: true),
                    transaction_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_InventoryLedger", x => x.ledger_id);
                });

            migrationBuilder.CreateTable(
                name: "im_item_Category",
                columns: table => new
                {
                    item_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    item_class = table.Column<string>(type: "varchar(20)", nullable: true),
                    description = table.Column<string>(type: "varchar(200)", nullable: true),
                    categoryType = table.Column<string>(type: "varchar(20)", nullable: true),
                    item_count = table.Column<int>(type: "int", nullable: true),
                    Sales_count = table.Column<int>(type: "int", nullable: true),
                    code = table.Column<string>(type: "varchar(10)", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    categoryImage = table.Column<string>(type: "varchar(max)", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    publish = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_item_Category", x => x.item_class_id);
                });

            migrationBuilder.CreateTable(
                name: "im_Lots",
                columns: table => new
                {
                    lot_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    lot_code = table.Column<string>(type: "varchar(80)", nullable: true),
                    mfg_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    note = table.Column<string>(type: "varchar(200)", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    committed_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    consign_qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    promo_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    is_on_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_Lots", x => x.lot_id);
                });

            migrationBuilder.CreateTable(
                name: "im_product_subvariant",
                columns: table => new
                {
                    sub_variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variantType = table.Column<string>(type: "varchar(50)", nullable: true),
                    variantValue = table.Column<string>(type: "varchar(50)", nullable: true),
                    list_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    standard_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    last_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    avg_cost = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    ws_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    profit_p = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    minimum_selling = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    item_barcode = table.Column<string>(type: "varchar(50)", nullable: true),
                    modal_number = table.Column<string>(type: "varchar(50)", nullable: true),
                    shipping_weight = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    shipping_length = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    shipping_width = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    shipping_height = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_volume = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    total_weight = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    deduct_qnty = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(50)", nullable: true),
                    unit_breakdown = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    fixed_price = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    generateBarcode = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_product_subvariant", x => x.sub_variant_id);
                });

            migrationBuilder.CreateTable(
                name: "im_ProductAttributes",
                columns: table => new
                {
                    attribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_ProductAttributes", x => x.attribute_id);
                });

            migrationBuilder.CreateTable(
                name: "im_ProductCategories",
                columns: table => new
                {
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_name = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    image_url = table.Column<string>(type: "varchar(200)", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(30)", nullable: true),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    Level = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_ProductCategories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "im_Products",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sub_category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sub_sub_category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    title = table.Column<string>(type: "varchar(200)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brand = table.Column<string>(type: "varchar(100)", nullable: true),
                    tax_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    thumbnail_url = table.Column<string>(type: "varchar(200)", nullable: true),
                    vendor_Code = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    kitchen_type = table.Column<string>(type: "varchar(32)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    dutyP = table.Column<decimal>(type: "decimal(16,4)", nullable: true),
                    fixed_price = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    track_expiry = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    allow_below_zero = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    is_multi_unit = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    low_stock_alert = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    published = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    featured_item = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    ignore_direct = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    has_free_item = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    restrict_deciaml_qty = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    restrict_HS = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    stock_flag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_Products", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "im_products_tag",
                columns: table => new
                {
                    tag_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    item_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    item_subclass_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    description = table.Column<string>(type: "varchar(200)", nullable: true),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_products_tag", x => x.tag_id);
                });

            migrationBuilder.CreateTable(
                name: "im_purchase_listing",
                columns: table => new
                {
                    listing_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    site_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    vendor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    payment_mode = table.Column<string>(type: "varchar(20)", nullable: true),
                    purchase_type = table.Column<string>(type: "varchar(20)", nullable: true),
                    supplier_invoice_no = table.Column<string>(type: "varchar(50)", nullable: true),
                    supplier_invoice_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(10)", nullable: true),
                    exchange_rate = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    sub_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    freight_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    other_expenses = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    received_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    notes = table.Column<string>(type: "varchar(400)", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true),
                    doc_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_purchase_listing", x => x.listing_id);
                });

            migrationBuilder.CreateTable(
                name: "im_SellerInventory",
                columns: table => new
                {
                    seller_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    stock_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    reorder_level = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    sales_count = table.Column<int>(type: "int", nullable: true),
                    Rack_no = table.Column<string>(type: "varchar(25)", nullable: true),
                    bin_number = table.Column<string>(type: "varchar(25)", nullable: true),
                    Consignment_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    committed_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    on_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    allow_Inter_Location_Transfer = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    sales_on_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_SellerInventory", x => x.seller_inventory_id);
                });

            migrationBuilder.CreateTable(
                name: "im_site",
                columns: table => new
                {
                    site_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    company_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    avl_countries_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    site_name = table.Column<string>(type: "varchar(100)", nullable: true),
                    tin_number = table.Column<string>(type: "varchar(100)", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(100)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_site", x => x.site_id);
                });

            migrationBuilder.CreateTable(
                name: "im_site_users",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    site_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    site_user_code = table.Column<string>(type: "varchar(30)", nullable: true),
                    userName = table.Column<string>(type: "varchar(32)", nullable: true),
                    password = table.Column<string>(type: "varchar(200)", nullable: true),
                    firstName = table.Column<string>(type: "varchar(50)", nullable: false),
                    lastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    fullName = table.Column<string>(type: "varchar(200)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    userRole = table.Column<string>(type: "varchar(20)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    phoneNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    address = table.Column<string>(type: "varchar(200)", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_site_users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "im_UnitsOfMeasures",
                columns: table => new
                {
                    uom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: true),
                    abbreviation = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_UnitsOfMeasures", x => x.uom_id);
                });

            migrationBuilder.CreateTable(
                name: "st_Parties",
                columns: table => new
                {
                    party_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    vsco_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    party_type = table.Column<string>(type: "varchar(20)", nullable: false),
                    display_name = table.Column<string>(type: "varchar(200)", nullable: true),
                    legal_name = table.Column<string>(type: "varchar(200)", nullable: true),
                    payable_name = table.Column<string>(type: "varchar(200)", nullable: true),
                    tax_id = table.Column<string>(type: "varchar(50)", nullable: true),
                    email = table.Column<string>(type: "varchar(50)", nullable: true),
                    phone = table.Column<string>(type: "varchar(50)", nullable: true),
                    default_currency = table.Column<string>(type: "varchar(20)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_Parties", x => x.party_id);
                });

            migrationBuilder.CreateTable(
                name: "st_StoreCategoryTemplates",
                columns: table => new
                {
                    store_category_template_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_type = table.Column<string>(type: "varchar(50)", nullable: true),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_StoreCategoryTemplates", x => x.store_category_template_id);
                });

            migrationBuilder.CreateTable(
                name: "st_stores",
                columns: table => new
                {
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    timezone_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_name = table.Column<string>(type: "varchar(255)", nullable: true),
                    store_location = table.Column<string>(type: "varchar(max)", nullable: true),
                    store_type = table.Column<string>(type: "varchar(100)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    default_close_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    phone1 = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    phone2 = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    tax_identification_number = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    default_invoice_init = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    default_quote_init = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    default_invoice_template = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    default_receipt_template = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    last_transaction_date = table.Column<DateOnly>(type: "date", nullable: true),
                    default_currency = table.Column<string>(type: "char(3)", maxLength: 3, nullable: true),
                    service_charge = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    tax_inclusive_price = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    tax_activity_no = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    tax_payer_name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    low_stock_alert_email = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    plastic_bag_tax_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    message_on_receipt = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    message_on_invoice = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_stores", x => x.store_id);
                });

            migrationBuilder.CreateTable(
                name: "st_UserRoles",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_name = table.Column<string>(type: "varchar(50)", nullable: true),
                    description = table.Column<string>(type: "varchar(200)", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_UserRoles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "st_Users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Full_name = table.Column<string>(type: "varchar(255)", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", nullable: true),
                    phone = table.Column<string>(type: "varchar(30)", nullable: true),
                    password = table.Column<string>(type: "varchar(max)", nullable: true),
                    account_type = table.Column<string>(type: "varchar(30)", nullable: true),
                    registration_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "st_UserStoreAccess",
                columns: table => new
                {
                    store_access_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_UserStoreAccess", x => x.store_access_id);
                });

            migrationBuilder.CreateTable(
                name: "co_address",
                columns: table => new
                {
                    company_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    street_1 = table.Column<string>(type: "varchar(60)", nullable: true),
                    street_2 = table.Column<string>(type: "varchar(60)", nullable: true),
                    city = table.Column<string>(type: "varchar(60)", nullable: true),
                    state = table.Column<string>(type: "varchar(60)", nullable: true),
                    postal_code = table.Column<string>(type: "varchar(60)", nullable: true),
                    country = table.Column<string>(type: "varchar(60)", nullable: true),
                    telephone_1 = table.Column<string>(type: "varchar(20)", nullable: true),
                    telephone_2 = table.Column<string>(type: "varchar(20)", nullable: true),
                    email = table.Column<string>(type: "varchar(50)", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(16)", nullable: true),
                    contact_person = table.Column<string>(type: "varchar(50)", nullable: true),
                    co_businesscompany_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_co_address", x => x.company_address_id);
                    table.ForeignKey(
                        name: "FK_co_address_co_business_co_businesscompany_id",
                        column: x => x.co_businesscompany_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateTable(
                name: "fx_Timezones",
                columns: table => new
                {
                    timezone_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    currency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    timezone = table.Column<string>(type: "varchar(100)", nullable: false),
                    timezone_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    fx_Currenciescurrency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fx_Timezones", x => x.timezone_id);
                    table.ForeignKey(
                        name: "FK_fx_Timezones_fx_Currencies_fx_Currenciescurrency_id",
                        column: x => x.fx_Currenciescurrency_id,
                        principalTable: "fx_Currencies",
                        principalColumn: "currency_id");
                });

            migrationBuilder.CreateTable(
                name: "im_item_subcategory",
                columns: table => new
                {
                    item_subclass_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    item_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    description = table.Column<string>(type: "varchar(200)", nullable: true),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    im_item_Categoryitem_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_item_subcategory", x => x.item_subclass_id);
                    table.ForeignKey(
                        name: "FK_im_item_subcategory_im_item_Category_im_item_Categoryitem_class_id",
                        column: x => x.im_item_Categoryitem_class_id,
                        principalTable: "im_item_Category",
                        principalColumn: "item_class_id");
                });

            migrationBuilder.CreateTable(
                name: "im_PriceTiers",
                columns: table => new
                {
                    price_tier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sub_variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", nullable: true),
                    description = table.Column<string>(type: "varchar(max)", nullable: true),
                    im_product_subvariantsub_variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_PriceTiers", x => x.price_tier_id);
                    table.ForeignKey(
                        name: "FK_im_PriceTiers_im_product_subvariant_im_product_subvariantsub_variant_id",
                        column: x => x.im_product_subvariantsub_variant_id,
                        principalTable: "im_product_subvariant",
                        principalColumn: "sub_variant_id");
                });

            migrationBuilder.CreateTable(
                name: "im_AttributeValues",
                columns: table => new
                {
                    value_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    attribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    value = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    color_name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: true),
                    im_ProductAttributesattribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_AttributeValues", x => x.value_id);
                    table.ForeignKey(
                        name: "FK_im_AttributeValues_im_ProductAttributes_im_ProductAttributesattribute_id",
                        column: x => x.im_ProductAttributesattribute_id,
                        principalTable: "im_ProductAttributes",
                        principalColumn: "attribute_id");
                });

            migrationBuilder.CreateTable(
                name: "im_ProductVariants",
                columns: table => new
                {
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sku = table.Column<string>(type: "varchar(50)", nullable: true),
                    barcode = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    barcovendor_part_number = table.Column<string>(type: "varchar(50)", nullable: true),
                    base_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_default = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    im_Productsproduct_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_ProductVariants", x => x.variant_id);
                    table.ForeignKey(
                        name: "FK_im_ProductVariants_im_Products_im_Productsproduct_id",
                        column: x => x.im_Productsproduct_id,
                        principalTable: "im_Products",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "im_purchase_listing_details",
                columns: table => new
                {
                    detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    listing_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sub_variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    unit_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    discount_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    freight_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    other_expenses = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    line_total = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    notes = table.Column<string>(type: "varchar(400)", nullable: true),
                    expiry_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    im_purchase_listinglisting_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_purchase_listing_details", x => x.detail_id);
                    table.ForeignKey(
                        name: "FK_im_purchase_listing_details_im_purchase_listing_im_purchase_listinglisting_id",
                        column: x => x.im_purchase_listinglisting_id,
                        principalTable: "im_purchase_listing",
                        principalColumn: "listing_id");
                });

            migrationBuilder.CreateTable(
                name: "im_item_site",
                columns: table => new
                {
                    item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    site_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    bin_number = table.Column<string>(type: "varchar(20)", nullable: true),
                    primary_vendor_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    on_hand_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    committed_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    purchase_order_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    sales_order_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    c_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(100)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    on_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    im_sitesite_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_item_site", x => x.item_id);
                    table.ForeignKey(
                        name: "FK_im_item_site_im_site_im_sitesite_id",
                        column: x => x.im_sitesite_id,
                        principalTable: "im_site",
                        principalColumn: "site_id");
                });

            migrationBuilder.CreateTable(
                name: "st_PartyRoles",
                columns: table => new
                {
                    party_role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    party_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    role = table.Column<string>(type: "varchar(30)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    st_Partiesparty_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_PartyRoles", x => x.party_role_id);
                    table.ForeignKey(
                        name: "FK_st_PartyRoles_st_Parties_st_Partiesparty_id",
                        column: x => x.st_Partiesparty_id,
                        principalTable: "st_Parties",
                        principalColumn: "party_id");
                });

            migrationBuilder.CreateTable(
                name: "st_StoreCategories",
                columns: table => new
                {
                    store_category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    is_selected = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_StoreCategories", x => x.store_category_id);
                    table.ForeignKey(
                        name: "FK_st_StoreCategories_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateTable(
                name: "st_StoresAddres",
                columns: table => new
                {
                    store_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    address_type = table.Column<string>(type: "varchar(20)", nullable: true),
                    line1 = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    line2 = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    region = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    postal_code = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    valid_from = table.Column<DateTime>(type: "datetime", nullable: true),
                    valid_to = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_current = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    st_storesstore_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_StoresAddres", x => x.store_address_id);
                    table.ForeignKey(
                        name: "FK_st_StoresAddres_st_stores_st_storesstore_id",
                        column: x => x.st_storesstore_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateTable(
                name: "im_ProductVariantPrices",
                columns: table => new
                {
                    variant_price_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sub_variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    price_tier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    currency = table.Column<string>(type: "varchar(20)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    im_PriceTiersprice_tier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_ProductVariantPrices", x => x.variant_price_id);
                    table.ForeignKey(
                        name: "FK_im_ProductVariantPrices_im_PriceTiers_im_PriceTiersprice_tier_id",
                        column: x => x.im_PriceTiersprice_tier_id,
                        principalTable: "im_PriceTiers",
                        principalColumn: "price_tier_id");
                });

            migrationBuilder.CreateTable(
                name: "im_ProductImages",
                columns: table => new
                {
                    image_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    image_url = table.Column<string>(type: "varchar(max)", nullable: true),
                    is_primary = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: true),
                    uploaded_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    im_ProductVariantsvariant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_ProductImages", x => x.image_id);
                    table.ForeignKey(
                        name: "FK_im_ProductImages_im_ProductVariants_im_ProductVariantsvariant_id",
                        column: x => x.im_ProductVariantsvariant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                });

            migrationBuilder.CreateTable(
                name: "im_StoreVariantInventory",
                columns: table => new
                {
                    store_variant_inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    on_hand_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    committed_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    bin_number = table.Column<string>(type: "nvarchar(24)", nullable: true),
                    im_ProductVariantsvariant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_StoreVariantInventory", x => x.store_variant_inventory_id);
                    table.ForeignKey(
                        name: "FK_im_StoreVariantInventory_im_ProductVariants_im_ProductVariantsvariant_id",
                        column: x => x.im_ProductVariantsvariant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                });

            migrationBuilder.CreateTable(
                name: "im_VariantAttributes",
                columns: table => new
                {
                    varient_attribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    value_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    im_ProductVariantsvariant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_VariantAttributes", x => x.varient_attribute_id);
                    table.ForeignKey(
                        name: "FK_im_VariantAttributes_im_ProductVariants_im_ProductVariantsvariant_id",
                        column: x => x.im_ProductVariantsvariant_id,
                        principalTable: "im_ProductVariants",
                        principalColumn: "variant_id");
                });

            migrationBuilder.CreateTable(
                name: "st_PartyAddresses",
                columns: table => new
                {
                    address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    party_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    address_type = table.Column<string>(type: "varchar(30)", nullable: true),
                    line1 = table.Column<string>(type: "varchar(200)", nullable: true),
                    line2 = table.Column<string>(type: "varchar(200)", nullable: true),
                    region = table.Column<string>(type: "varchar(100)", nullable: true),
                    postal_code = table.Column<string>(type: "varchar(30)", nullable: true),
                    country = table.Column<string>(type: "varchar(100)", nullable: true),
                    latitude = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    longitude = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_default = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    vendor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ap_Vendorsvendor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ar_Customerscustomer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    st_PartyRolesparty_role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_PartyAddresses", x => x.address_id);
                    table.ForeignKey(
                        name: "FK_st_PartyAddresses_ap_Vendors_ap_Vendorsvendor_id",
                        column: x => x.ap_Vendorsvendor_id,
                        principalTable: "ap_Vendors",
                        principalColumn: "vendor_id");
                    table.ForeignKey(
                        name: "FK_st_PartyAddresses_ar_Customers_ar_Customerscustomer_id",
                        column: x => x.ar_Customerscustomer_id,
                        principalTable: "ar_Customers",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK_st_PartyAddresses_st_PartyRoles_st_PartyRolesparty_role_id",
                        column: x => x.st_PartyRolesparty_role_id,
                        principalTable: "st_PartyRoles",
                        principalColumn: "party_role_id");
                });

            migrationBuilder.CreateTable(
                name: "st_store_currencies",
                columns: table => new
                {
                    store_currency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    currency_code = table.Column<string>(type: "char(3)", maxLength: 3, nullable: true),
                    is_default = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    st_StoresAddresstore_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_store_currencies", x => x.store_currency_id);
                    table.ForeignKey(
                        name: "FK_st_store_currencies_st_StoresAddres_st_StoresAddresstore_address_id",
                        column: x => x.st_StoresAddresstore_address_id,
                        principalTable: "st_StoresAddres",
                        principalColumn: "store_address_id");
                });

            migrationBuilder.CreateTable(
                name: "st_PartyContacts",
                columns: table => new
                {
                    contact_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    party_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    first_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(100)", nullable: true),
                    email = table.Column<string>(type: "varchar(50)", nullable: true),
                    phone = table.Column<string>(type: "varchar(50)", nullable: true),
                    title = table.Column<string>(type: "varchar(100)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_primary = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    st_PartyAddressesaddress_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_PartyContacts", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_st_PartyContacts_st_PartyAddresses_st_PartyAddressesaddress_id",
                        column: x => x.st_PartyAddressesaddress_id,
                        principalTable: "st_PartyAddresses",
                        principalColumn: "address_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_co_address_co_businesscompany_id",
                table: "co_address",
                column: "co_businesscompany_id");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_business_name",
                table: "co_business",
                column: "business_name");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_country",
                table: "co_business",
                column: "country");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_created_at",
                table: "co_business",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_createdSites",
                table: "co_business",
                column: "createdSites");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_createdSites_users",
                table: "co_business",
                column: "createdSites_users");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_edit_date_time",
                table: "co_business",
                column: "edit_date_time");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_edit_user_id",
                table: "co_business",
                column: "edit_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_email",
                table: "co_business",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_phoneNumber",
                table: "co_business",
                column: "phoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_plan_type",
                table: "co_business",
                column: "plan_type");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_reg_no",
                table: "co_business",
                column: "reg_no");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_sites_allowed",
                table: "co_business",
                column: "sites_allowed");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_sites_users_allowed",
                table: "co_business",
                column: "sites_users_allowed");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_tin_number",
                table: "co_business",
                column: "tin_number");

            migrationBuilder.CreateIndex(
                name: "IX_fx_Currencies_currency_id",
                table: "fx_Currencies",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_fx_Currencies_currency_name",
                table: "fx_Currencies",
                column: "currency_name");

            migrationBuilder.CreateIndex(
                name: "IX_fx_Timezones_fx_Currenciescurrency_id",
                table: "fx_Timezones",
                column: "fx_Currenciescurrency_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_AttributeValues_im_ProductAttributesattribute_id",
                table: "im_AttributeValues",
                column: "im_ProductAttributesattribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryLedger_source_doc_id",
                table: "im_InventoryLedger",
                column: "source_doc_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryLedger_source_doc_type",
                table: "im_InventoryLedger",
                column: "source_doc_type");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryLedger_transaction_date",
                table: "im_InventoryLedger",
                column: "transaction_date");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryLedger_user_id",
                table: "im_InventoryLedger",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_InventoryLedger_variant_id",
                table: "im_InventoryLedger",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_item_site_im_sitesite_id",
                table: "im_item_site",
                column: "im_sitesite_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_item_subcategory_im_item_Categoryitem_class_id",
                table: "im_item_subcategory",
                column: "im_item_Categoryitem_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_Lots_expiry_date",
                table: "im_Lots",
                column: "expiry_date");

            migrationBuilder.CreateIndex(
                name: "IX_im_Lots_is_on_hold",
                table: "im_Lots",
                column: "is_on_hold");

            migrationBuilder.CreateIndex(
                name: "IX_im_Lots_lot_code",
                table: "im_Lots",
                column: "lot_code");

            migrationBuilder.CreateIndex(
                name: "IX_im_Lots_variant_id",
                table: "im_Lots",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_PriceTiers_im_product_subvariantsub_variant_id",
                table: "im_PriceTiers",
                column: "im_product_subvariantsub_variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductAttributes_company_id",
                table: "im_ProductAttributes",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductAttributes_name",
                table: "im_ProductAttributes",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductCategories_category_name",
                table: "im_ProductCategories",
                column: "category_name");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductCategories_is_active",
                table: "im_ProductCategories",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductCategories_Level",
                table: "im_ProductCategories",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductCategories_parent_id",
                table: "im_ProductCategories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductImages_im_ProductVariantsvariant_id",
                table: "im_ProductImages",
                column: "im_ProductVariantsvariant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_Products_company_id",
                table: "im_Products",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_Products_title",
                table: "im_Products",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductVariantPrices_im_PriceTiersprice_tier_id",
                table: "im_ProductVariantPrices",
                column: "im_PriceTiersprice_tier_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductVariants_im_Productsproduct_id",
                table: "im_ProductVariants",
                column: "im_Productsproduct_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_purchase_listing_details_im_purchase_listinglisting_id",
                table: "im_purchase_listing_details",
                column: "im_purchase_listinglisting_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_SellerInventory_store_id",
                table: "im_SellerInventory",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_SellerInventory_user_id",
                table: "im_SellerInventory",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_SellerInventory_variant_id",
                table: "im_SellerInventory",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StoreVariantInventory_im_ProductVariantsvariant_id",
                table: "im_StoreVariantInventory",
                column: "im_ProductVariantsvariant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StoreVariantInventory_store_id",
                table: "im_StoreVariantInventory",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_StoreVariantInventory_variant_id",
                table: "im_StoreVariantInventory",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_VariantAttributes_im_ProductVariantsvariant_id",
                table: "im_VariantAttributes",
                column: "im_ProductVariantsvariant_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_PartyAddresses_ap_Vendorsvendor_id",
                table: "st_PartyAddresses",
                column: "ap_Vendorsvendor_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_PartyAddresses_ar_Customerscustomer_id",
                table: "st_PartyAddresses",
                column: "ar_Customerscustomer_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_PartyAddresses_st_PartyRolesparty_role_id",
                table: "st_PartyAddresses",
                column: "st_PartyRolesparty_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_PartyContacts_st_PartyAddressesaddress_id",
                table: "st_PartyContacts",
                column: "st_PartyAddressesaddress_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_PartyRoles_st_Partiesparty_id",
                table: "st_PartyRoles",
                column: "st_Partiesparty_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_store_currencies_currency_code",
                table: "st_store_currencies",
                column: "currency_code");

            migrationBuilder.CreateIndex(
                name: "IX_st_store_currencies_st_StoresAddresstore_address_id",
                table: "st_store_currencies",
                column: "st_StoresAddresstore_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_store_currencies_store_currency_id",
                table: "st_store_currencies",
                column: "store_currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_store_currencies_store_id",
                table: "st_store_currencies",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoreCategories_category_id",
                table: "st_StoreCategories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoreCategories_is_selected",
                table: "st_StoreCategories",
                column: "is_selected");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoreCategories_store_id",
                table: "st_StoreCategories",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoreCategoryTemplates_category_id",
                table: "st_StoreCategoryTemplates",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoreCategoryTemplates_store_type",
                table: "st_StoreCategoryTemplates",
                column: "store_type");

            migrationBuilder.CreateIndex(
                name: "IX_st_stores_company_id",
                table: "st_stores",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_stores_created_at",
                table: "st_stores",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_st_stores_status",
                table: "st_stores",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_st_stores_store_name",
                table: "st_stores",
                column: "store_name");

            migrationBuilder.CreateIndex(
                name: "IX_st_stores_store_type",
                table: "st_stores",
                column: "store_type");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoresAddres_address_type",
                table: "st_StoresAddres",
                column: "address_type");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoresAddres_is_current",
                table: "st_StoresAddres",
                column: "is_current");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoresAddres_st_storesstore_id",
                table: "st_StoresAddres",
                column: "st_storesstore_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoresAddres_store_id",
                table: "st_StoresAddres",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoresAddres_valid_from",
                table: "st_StoresAddres",
                column: "valid_from");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserRoles_company_id",
                table: "st_UserRoles",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserRoles_description",
                table: "st_UserRoles",
                column: "description");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserRoles_role_name",
                table: "st_UserRoles",
                column: "role_name");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_account_type",
                table: "st_Users",
                column: "account_type");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_company_id",
                table: "st_Users",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_email",
                table: "st_Users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_Full_name",
                table: "st_Users",
                column: "Full_name");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_phone",
                table: "st_Users",
                column: "phone");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_registration_date",
                table: "st_Users",
                column: "registration_date");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_status",
                table: "st_Users",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserStoreAccess_created_at",
                table: "st_UserStoreAccess",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserStoreAccess_role_id",
                table: "st_UserStoreAccess",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserStoreAccess_status",
                table: "st_UserStoreAccess",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserStoreAccess_store_id",
                table: "st_UserStoreAccess",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserStoreAccess_user_id",
                table: "st_UserStoreAccess",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "am_emailVerifications");

            migrationBuilder.DropTable(
                name: "am_table_next_key");

            migrationBuilder.DropTable(
                name: "am_users");

            migrationBuilder.DropTable(
                name: "avl_countries");

            migrationBuilder.DropTable(
                name: "co_address");

            migrationBuilder.DropTable(
                name: "co_avl_countries");

            migrationBuilder.DropTable(
                name: "fx_Timezones");

            migrationBuilder.DropTable(
                name: "im_AttributeValues");

            migrationBuilder.DropTable(
                name: "im_InventoryLedger");

            migrationBuilder.DropTable(
                name: "im_item_site");

            migrationBuilder.DropTable(
                name: "im_item_subcategory");

            migrationBuilder.DropTable(
                name: "im_Lots");

            migrationBuilder.DropTable(
                name: "im_ProductCategories");

            migrationBuilder.DropTable(
                name: "im_ProductImages");

            migrationBuilder.DropTable(
                name: "im_products_tag");

            migrationBuilder.DropTable(
                name: "im_ProductVariantPrices");

            migrationBuilder.DropTable(
                name: "im_purchase_listing_details");

            migrationBuilder.DropTable(
                name: "im_SellerInventory");

            migrationBuilder.DropTable(
                name: "im_site_users");

            migrationBuilder.DropTable(
                name: "im_StoreVariantInventory");

            migrationBuilder.DropTable(
                name: "im_UnitsOfMeasures");

            migrationBuilder.DropTable(
                name: "im_VariantAttributes");

            migrationBuilder.DropTable(
                name: "st_PartyContacts");

            migrationBuilder.DropTable(
                name: "st_store_currencies");

            migrationBuilder.DropTable(
                name: "st_StoreCategories");

            migrationBuilder.DropTable(
                name: "st_StoreCategoryTemplates");

            migrationBuilder.DropTable(
                name: "st_UserRoles");

            migrationBuilder.DropTable(
                name: "st_Users");

            migrationBuilder.DropTable(
                name: "st_UserStoreAccess");

            migrationBuilder.DropTable(
                name: "co_business");

            migrationBuilder.DropTable(
                name: "fx_Currencies");

            migrationBuilder.DropTable(
                name: "im_ProductAttributes");

            migrationBuilder.DropTable(
                name: "im_site");

            migrationBuilder.DropTable(
                name: "im_item_Category");

            migrationBuilder.DropTable(
                name: "im_PriceTiers");

            migrationBuilder.DropTable(
                name: "im_purchase_listing");

            migrationBuilder.DropTable(
                name: "im_ProductVariants");

            migrationBuilder.DropTable(
                name: "st_PartyAddresses");

            migrationBuilder.DropTable(
                name: "st_StoresAddres");

            migrationBuilder.DropTable(
                name: "im_product_subvariant");

            migrationBuilder.DropTable(
                name: "im_Products");

            migrationBuilder.DropTable(
                name: "ap_Vendors");

            migrationBuilder.DropTable(
                name: "ar_Customers");

            migrationBuilder.DropTable(
                name: "st_PartyRoles");

            migrationBuilder.DropTable(
                name: "st_stores");

            migrationBuilder.DropTable(
                name: "st_Parties");
        }
    }
}
