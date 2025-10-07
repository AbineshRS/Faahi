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
                    vendor_code = table.Column<string>(type: "varchar(30)", nullable: true),
                    payment_term_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    preferred_payment_method = table.Column<string>(type: "varchar(20)", nullable: true),
                    withholding_tax_rate = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    ap_control_account = table.Column<string>(type: "varchar(40)", nullable: true),
                    note = table.Column<string>(type: "varchar(40)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
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
                    customer_code = table.Column<string>(type: "varchar(30)", nullable: true),
                    payment_term_id = table.Column<string>(type: "varchar(30)", nullable: true),
                    credit_limit = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    default_billing_address_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    default_shipping_address_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    loyalty_points = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    loyalty_level = table.Column<string>(type: "varchar(40)", nullable: true),
                    note = table.Column<string>(type: "varchar(50)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    credit_hold = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    tax_exempt = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ar_Customers", x => x.customer_id);
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
                    business_name = table.Column<string>(type: "varchar(100)", nullable: true),
                    tin_number = table.Column<string>(type: "varchar(50)", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", nullable: true),
                    password = table.Column<string>(type: "varchar(100)", nullable: true),
                    reg_no = table.Column<string>(type: "varchar(50)", nullable: true),
                    country = table.Column<string>(type: "varchar(20)", nullable: true),
                    address = table.Column<string>(type: "varchar(max)", nullable: true),
                    logo_fileName = table.Column<string>(type: "varchar(100)", nullable: true),
                    phoneNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    plan_type = table.Column<string>(type: "varchar(20)", nullable: true),
                    sites_allowed = table.Column<int>(type: "int", nullable: true),
                    createdSites = table.Column<int>(type: "int", nullable: true),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    edit_user_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    email = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_co_business", x => x.company_id);
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
                name: "im_ProductCategories",
                columns: table => new
                {
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    category_name = table.Column<string>(type: "varchar(30)", nullable: true),
                    parent_id = table.Column<string>(type: "varchar(30)", nullable: true),
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
                name: "im_ProductImages",
                columns: table => new
                {
                    image_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    image_url = table.Column<string>(type: "varchar(200)", nullable: true),
                    is_primary = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: true),
                    uploaded_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_ProductImages", x => x.image_id);
                });

            migrationBuilder.CreateTable(
                name: "im_Products",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    item_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    item_subclass_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    title = table.Column<string>(type: "varchar(200)", nullable: true),
                    description = table.Column<string>(type: "varchar(max)", nullable: true),
                    brand = table.Column<string>(type: "varchar(100)", nullable: true),
                    tax_class = table.Column<string>(type: "varchar(20)", nullable: true),
                    thumbnail_url = table.Column<string>(type: "varchar(200)", nullable: true),
                    HS_CODE = table.Column<string>(type: "varchar(10)", nullable: true),
                    vendor_Code = table.Column<string>(type: "varchar(30)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    dutyP = table.Column<decimal>(type: "decimal(16,4)", nullable: true),
                    katta = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    featured_item = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    ignore_direct = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    consign_item = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    free_item = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    iqnore_decimal_qty = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    restrict_HS = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    stock = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
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
                    item_class_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    item_subclass_id = table.Column<string>(type: "varchar(20)", nullable: true),
                    description = table.Column<string>(type: "varchar(200)", nullable: true),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_products_tag", x => x.tag_id);
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
                name: "im_ProductVariants",
                columns: table => new
                {
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uom_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sku = table.Column<string>(type: "varchar(50)", nullable: true),
                    barcode = table.Column<string>(type: "varchar(50)", nullable: true),
                    color = table.Column<string>(type: "varchar(50)", nullable: true),
                    size = table.Column<string>(type: "varchar(50)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    stock_quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    weight_kg = table.Column<decimal>(type: "decimal(16,4)", nullable: true),
                    length_cm = table.Column<decimal>(type: "decimal(16,4)", nullable: true),
                    width_cm = table.Column<decimal>(type: "decimal(16,4)", nullable: true),
                    height_cm = table.Column<decimal>(type: "decimal(16,4)", nullable: true),
                    chargeable_weight_kg = table.Column<decimal>(type: "decimal(16,4)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    low_stock_alert = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    allow_below_Zero = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
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
                    generateBarcode = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    im_ProductVariantsvariant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_product_subvariant", x => x.sub_variant_id);
                    table.ForeignKey(
                        name: "FK_im_product_subvariant_im_ProductVariants_im_ProductVariantsvariant_id",
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
                    st_PartyRolesparty_role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_PartyAddresses", x => x.address_id);
                    table.ForeignKey(
                        name: "FK_st_PartyAddresses_st_PartyRoles_st_PartyRolesparty_role_id",
                        column: x => x.st_PartyRolesparty_role_id,
                        principalTable: "st_PartyRoles",
                        principalColumn: "party_role_id");
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

            migrationBuilder.CreateIndex(
                name: "IX_co_address_co_businesscompany_id",
                table: "co_address",
                column: "co_businesscompany_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_item_site_im_sitesite_id",
                table: "im_item_site",
                column: "im_sitesite_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_item_subcategory_im_item_Categoryitem_class_id",
                table: "im_item_subcategory",
                column: "im_item_Categoryitem_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_PriceTiers_im_product_subvariantsub_variant_id",
                table: "im_PriceTiers",
                column: "im_product_subvariantsub_variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_product_subvariant_im_ProductVariantsvariant_id",
                table: "im_product_subvariant",
                column: "im_ProductVariantsvariant_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductVariantPrices_im_PriceTiersprice_tier_id",
                table: "im_ProductVariantPrices",
                column: "im_PriceTiersprice_tier_id");

            migrationBuilder.CreateIndex(
                name: "IX_im_ProductVariants_im_Productsproduct_id",
                table: "im_ProductVariants",
                column: "im_Productsproduct_id");

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
                name: "ap_Vendors");

            migrationBuilder.DropTable(
                name: "ar_Customers");

            migrationBuilder.DropTable(
                name: "co_address");

            migrationBuilder.DropTable(
                name: "co_avl_countries");

            migrationBuilder.DropTable(
                name: "im_item_site");

            migrationBuilder.DropTable(
                name: "im_item_subcategory");

            migrationBuilder.DropTable(
                name: "im_ProductCategories");

            migrationBuilder.DropTable(
                name: "im_ProductImages");

            migrationBuilder.DropTable(
                name: "im_products_tag");

            migrationBuilder.DropTable(
                name: "im_ProductVariantPrices");

            migrationBuilder.DropTable(
                name: "im_UnitsOfMeasures");

            migrationBuilder.DropTable(
                name: "st_PartyContacts");

            migrationBuilder.DropTable(
                name: "co_business");

            migrationBuilder.DropTable(
                name: "im_site");

            migrationBuilder.DropTable(
                name: "im_item_Category");

            migrationBuilder.DropTable(
                name: "im_PriceTiers");

            migrationBuilder.DropTable(
                name: "st_PartyAddresses");

            migrationBuilder.DropTable(
                name: "im_product_subvariant");

            migrationBuilder.DropTable(
                name: "st_PartyRoles");

            migrationBuilder.DropTable(
                name: "im_ProductVariants");

            migrationBuilder.DropTable(
                name: "st_Parties");

            migrationBuilder.DropTable(
                name: "im_Products");
        }
    }
}
