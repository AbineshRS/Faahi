using Faahi.Controllers.site_settings;
using Faahi.Dto.om_Orders;
using Faahi.Dto.sales_dto;
using Faahi.Model;
using Faahi.Model.Accounts;
using Faahi.Model.Admin;
using Faahi.Model.am_users;
using Faahi.Model.am_vcos;
using Faahi.Model.co_business;
using Faahi.Model.countries;
using Faahi.Model.Email_verify;
using Faahi.Model.Finance;
using Faahi.Model.im_products;
using Faahi.Model.Order;
using Faahi.Model.pos_tables;
using Faahi.Model.sales;
using Faahi.Model.Shared_tables;
using Faahi.Model.site_settings;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;
using Faahi.Model.table_key;
using Faahi.Model.tax_class_table;
using Faahi.Model.temp_tables;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Controllers.Application
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;

        }


        public DbSet<am_users> am_users { get; set; }

        public DbSet<am_roles> am_roles { get; set; }

        public DbSet<am_user_roles> am_user_roles { get; set; }

        public DbSet<am_user_business_access> am_user_business_access { get; set; }

        public DbSet<mk_customer_profiles> mk_customer_profiles { get; set; }

        public DbSet<mk_customer_addresses> mk_customer_addresses { get; set; }

        public DbSet<am_table_next_key> am_table_next_key { get; set; }

        public DbSet<super_admin_keys> super_admin_keys { get; set; }

        public DbSet<co_business> co_business { get; set; }

        public DbSet<co_address> co_address { get; set; }

        public DbSet<am_emailVerifications> am_emailVerifications { get; set; }

        public DbSet<im_ProductCategories> im_ProductCategories { get; set; }

        public DbSet<im_Products> im_Products { get; set; }

        public DbSet<im_ProductVariants> im_ProductVariants { get; set; }

        public DbSet<im_PriceTiers> im_PriceTiers { get; set; }

        public DbSet<im_ProductVariantPrices> im_ProductVariantPrices { get; set; }

        public DbSet<im_ProductImages> im_ProductImages { get; set; }

        public DbSet<st_Parties> st_Parties { get; set; }

        public DbSet<st_PartyRoles> st_PartyRoles { get; set; }

        public DbSet<st_PartyAddresses> st_PartyAddresses { get; set; }

        public DbSet<st_PartyContacts> st_PartyContacts { get; set; }

        public DbSet<im_item_Category> im_item_Category { get; set; }

        public DbSet<im_item_subcategory> im_item_subcategory { get; set; }

        public DbSet<im_products_tag> im_products_tag { get; set; }

        public DbSet<im_UnitsOfMeasure> im_UnitsOfMeasures { get; set; }

        public DbSet<co_avl_countries> co_avl_countries { get; set; }

        public DbSet<im_site> im_site { get; set; }

        public DbSet<im_item_site> im_item_site { get; set; }

        public DbSet<im_product_subvariant> im_product_subvariant { get; set; }

        public DbSet<ar_Customers> ar_Customers { get; set; }

        public DbSet<ap_Vendors> ap_Vendors { get; set; }

        public DbSet<im_purchase_listing> im_purchase_listing { get; set; }

        public DbSet<im_purchase_listing_details> im_purchase_listing_details { get; set; }

        public DbSet<im_site_users> im_site_users { get; set; }

        public DbSet<st_Users> st_Users { get; set; }

        public DbSet<st_stores> st_stores { get; set; }

        public DbSet<st_UserRoles> st_UserRoles { get; set; }

        public DbSet<st_UserStoreAccess> st_UserStoreAccess { get; set; }

        public DbSet<avl_countries> avl_countries { get; set; }

        public DbSet<st_StoreCategories> st_StoreCategories { get; set; }

        public DbSet<st_StoreCategoryTemplates> st_StoreCategoryTemplates { get; set; }

        public DbSet<st_StoresAddres> st_StoresAddres { get; set; }

        public DbSet<fx_Currencies> fx_Currencies { get; set; }

        public DbSet<fx_timezones> fx_Timezones { get; set; }

        public DbSet<st_store_currencies> st_store_currencies { get; set; }

        public DbSet<im_InventoryLedger> im_InventoryLedger { get; set; }

        public DbSet<im_SellerInventory> im_SellerInventory { get; set; }

        public DbSet<im_Lots> im_Lots { get; set; }

        public DbSet<im_ProductAttributes> im_ProductAttributes { get; set; }

        public DbSet<im_AttributeValues> im_AttributeValues { get; set; }

        public DbSet<im_VariantAttributes> im_VariantAttributes { get; set; }

        public DbSet<im_StoreVariantInventory> im_StoreVariantInventory { get; set; }

        public DbSet<super_admin> super_admin { get; set; }

        public DbSet<sa_country_regions> sa_country_regions { get; set; }

        public DbSet<sa_regions> sa_regions { get; set; }

        public DbSet<fin_PartyBankAccounts> fin_PartyBankAccounts { get; set; }


        public DbSet<im_bin_location> im_bin_locations { get; set; }

        public DbSet<im_ItemBatches> im_itemBatches { get; set; }

        public DbSet<super_abi> super_abi { get; set; }

        public DbSet<tx_TaxClasses> tx_TaxClasses { get; set; }

        public DbSet<im_InventoryTransactions> im_InventoryTransactions { get; set; }

        public DbSet<so_payment_type> so_Payment_Types { get; set; }

        public DbSet<so_SalesHeaders> so_SalesHeaders { get; set; }

        public DbSet<so_SalesLines> so_SalesLines { get; set; }

        public DbSet<st_invoice_template> st_Invoice_Templates { get; set; }

        public DbSet<pos_SalePayments> pos_SalePayments { get; set; }

        public DbSet<pos_DrawerSessions> pos_DrawerSessions { get; set; }

        public DbSet<pos_DrawerCountDetails> pos_DrawerCountDetails { get; set; }

        public DbSet<so_SalesReturnHeaders> so_SalesReturnHeaders { get; set; }

        public DbSet<so_SalesReturnLines> so_SalesReturnLines { get; set; }

        public DbSet<pos_ReturnsalePayments> pos_ReturnsalePayments { get; set; }

        public DbSet<im_GoodsReceiptHeaders> im_GoodsReceiptHeaders { get; set; }

        public DbSet<im_GoodsReceiptLines> im_GoodsReceiptLines { get; set; }

        public DbSet<im_GoodsReceiptLineBatches> im_GoodsReceiptLineBatches {  get; set; }

        public DbSet<im_purchase_return_header> im_purchase_return_header { get; set; }

        public DbSet<im_purchase_return_details_line> im_purchase_return_details_line { get; set; }

        public DbSet<sa_roles> sa_roles { get; set; }

        public DbSet<om_OrderSources> om_OrderSources { get; set; }

        public DbSet<om_CustomerOrders> om_CustomerOrders { get; set; }

        public DbSet<om_CustomerOrderLines> om_CustomerOrderLines { get; set; }

        public DbSet<om_OrderStatusHistory> om_OrderStatusHistories { get; set; }

        public DbSet<im_InventoryReservations> im_InventoryReservations { get; set; }

        public DbSet<mk_business_zones> mk_business_zones { get; set; }

        public DbSet<mk_blacklisted_numbers> mk_blacklisted_numbers { get; set; }

        public DbSet<om_FulfillmentOrders> om_FulfillmentOrders { get; set; }

        public  DbSet<om_FulfillmentLines> om_FulfillmentLines { get;set; }

        public DbSet<sys_Images> sys_Images { get; set; }


        //TEMPTABLES
        public DbSet<temp_im_variant> temp_im_variants { get; set; }

        public DbSet<temp_im_purchase_listing_details> temp_Im_Purchase_Listing_Details { get; set; }

        // view tables
        public DbSet<om_CustomerOrders_dto> CustomerOrdersDto { get; set; }

        public DbSet<so_sales_header_customer> so_sales_header_customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<st_stores>(entity =>
            {
                entity.Property(e => e.sales_mode).HasColumnType("nvarchar(50)").HasDefaultValue("ROUNDOFF");
            });
            // Additional configurations can be added here if needed
            modelBuilder.Entity<im_ItemBatches>().ToTable(tb =>
            {
                tb.HasCheckConstraint("CK_im_itemBatches_on_hand_quantity", "[on_hand_quantity] >= 0");
                tb.HasCheckConstraint("CK_im_itemBatches_expiry_date", "[expiry_date] > GETDATE()");
                tb.HasCheckConstraint("CK_im_itemBatches_unit_cost", "[unit_cost] >= 0");

            });
            modelBuilder.Entity<gl_Accounts>(entity =>
            {
                entity.Property(e => e.GlAccountId).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(e => e.IsPostable).HasColumnType("char(1)").HasDefaultValue("T");
                entity.Property(e => e.IsActive).HasColumnType("char(1)").HasDefaultValue("T");
            });

            modelBuilder.Entity<gl_FiscalPeriods>(entity =>
            {
                entity.Property(e => e.PeriodId).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(e => e.IsClosed).HasColumnType("char(1)").HasDefaultValue("F");
            });
            modelBuilder.Entity<gl_AccountMapping>()
            .HasOne(x => x.GlAccount).WithMany().HasForeignKey(x => x.GlAccountId).OnDelete(DeleteBehavior.Restrict);   // or NoAction

            // Configure gl_Ledger to prevent cascade delete conflicts
            modelBuilder.Entity<gl_Ledger>(entity =>
            {
                entity.Property(e => e.LedgerId).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                
                // Explicitly tell EF Core NOT to create foreign key for CompanyId
                entity.HasOne<co_business>()
                    .WithMany()
                    .HasForeignKey(e => e.BusinessId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired(false); // Make relationship optional to avoid constraint issues
                
                // Configure foreign keys with NoAction to prevent cascade conflicts
                entity.HasOne(x => x.JournalHeader)
                    .WithMany()
                    .HasForeignKey(x => x.JournalId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(x => x.JournalLine)
                    .WithMany()
                    .HasForeignKey(x => x.JournalLineId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(x => x.GlAccount)
                    .WithMany()
                    .HasForeignKey(x => x.GlAccountId)
                    .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<so_payment_type>(entity =>
            {
                entity.Property(a => a.payment_type_id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(sp => sp.is_avilable).HasDefaultValue("T");
            });

            modelBuilder.Entity<so_SalesHeaders>(entity =>
            {
                entity.Property(e => e.sub_total).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.service_charge_percent).HasColumnType("decimal(6,2)").HasDefaultValue("0");
                entity.Property(e => e.discount_total).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.service_charge).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.tax_total).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.grand_total).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.total_plastic_bag).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.total_taxable_value).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.total_zero_value).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.total_exempted_value).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.total_charge_customer).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.total_plastic_bag_tax).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.sub_total_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.discount_total_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.tax_total_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.grand_total_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.total_taxable_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.total_zero_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.total_exempted_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                //entity.Property(e => e.total_charge_customer_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");  
                entity.Property(e => e.service_charge_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.total_plastic_bag_tax_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.total_charge_bank_marchant).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.transaction_cost).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.amount_paid_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.change_given_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.change_given_doc).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.balance_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.status).HasColumnType("varchar(20)").HasDefaultValue("OPEN");
                entity.Property(e => e.sales_mode).HasColumnType("varchar(25)").HasDefaultValue("GENERAL");
                entity.Property(e => e.doc_type).HasColumnType("varchar(30)").HasDefaultValue("SALE");
                entity.Property(e => e.sales_on_hold).HasColumnType("char(1)").HasDefaultValue("F");
                entity.Property(e => e.is_mutiple_payment).HasColumnType("char(1)").HasDefaultValue("F");


                entity.ToTable(tb =>
                {
                    tb.HasCheckConstraint(
                        "CK_so_SalesHeaders_Totals_NonNegative",
                        "sub_total >= 0 AND discount_total >= 0 AND tax_total >= 0 AND grand_total >= 0 AND sub_total_base >= 0 AND discount_total_base >= 0 AND tax_total_base >= 0 AND grand_total_base >= 0"
                    );
                });
            });

            modelBuilder.Entity<so_SalesLines>(entity =>
            {
                entity.Property(e => e.sales_line_id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(a => a.quantity).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.line_discount_amount).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.unit_price).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.discount_amount).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.discount_percent).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.tax_amount).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.detected_qty).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.original_price_base).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.returned_quantity).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.original_quantity).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.fx_rate_to_base).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.unit_price_base).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.discount_amount_base).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.tax_amount_base).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.unit_discount_amount_base).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.line_total_base).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.line_total).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(a => a.return_qty).HasColumnType("decimal(18,4)").HasDefaultValueSql("0");
                entity.Property(e => e.track_expiry).HasColumnType("char(1)").HasDefaultValue("F");
                entity.Property(e => e.stock_item).HasColumnType("char(1)").HasDefaultValue("F");

                entity.ToTable(tb =>
                {
                    tb.HasCheckConstraint("CK_so_SalesLines_Amounts",
                        "unit_price >= 0 AND discount_amount >= 0 AND tax_amount >= 0 AND discount_percent >= 0 AND fx_rate_to_base > 0");
                });
            });

            modelBuilder.Entity<pos_SalePayments>(entity =>
            {
                entity.Property(a => a.sale_payment_id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(a => a.line_no).HasColumnType("int").HasDefaultValue(1);
                entity.Property(a => a.change_given).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.is_voided).HasColumnType("char(1)").HasDefaultValue("F");
            });

            modelBuilder.Entity<pos_DrawerSessions>(entity =>
            {
                entity.Property(a => a.drawer_session_id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(a => a.opening_float).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.opened_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.status).HasColumnType("char(1)").HasDefaultValue("O");

            });

            modelBuilder.Entity<pos_DrawerCountDetails>(entity =>
            {
                entity.Property(a => a.drawer_count_id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(a => a.expected_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.counted_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.difference_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");


            });

            modelBuilder.Entity<so_SalesReturnHeaders>(entity =>
            {
                entity.Property(e => e.sub_total).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.discount_total).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.tax_total).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.grand_total).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.sub_total_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.discount_total_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.tax_total_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.grand_total_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(e => e.discount_total_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.HasOne(e => e.st_Stores).WithMany().HasForeignKey(e => e.store_id).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<so_SalesReturnLines>(entity =>
            {
                entity.Property(a => a.return_qty).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.unit_price).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.discount_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.discount_percent).HasColumnType("decimal(6,2)").HasDefaultValue("0");
                entity.Property(a => a.tax_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.line_total).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.unit_price_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.discount_amount_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.tax_amount_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.line_total_base).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.HasOne(a => a.so_SalesReturnHeaders).WithMany().HasForeignKey(a => a.sales_return_id).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.st_Stores).WithMany().HasForeignKey(a => a.store_id).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<pos_ReturnsalePayments>(entity =>
            {
                entity.Property(a => a.pos_retuen_sales_id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(a => a.line_no).HasColumnType("int").HasDefaultValue(1);
                entity.Property(a => a.change_given).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.is_voided).HasColumnType("char(1)").HasDefaultValue("F");
            });

            modelBuilder.Entity<im_GoodsReceiptHeaders>(entity =>
            {
                entity.Property(a => a.goods_receipt_id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(a => a.subtotal).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.discount_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.tax_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.total_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.updated_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.posted_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.receipt_date).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.is_posted).HasColumnType("char(1)").HasDefaultValue("F");
                entity.Property(a => a.is_cancelled).HasColumnType("char(1)").HasDefaultValue("F");
            });

            modelBuilder.Entity<im_GoodsReceiptLines>(entity =>
            {
                entity.Property(a => a.goods_receipt_id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(a => a.ordered_qty).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.discount_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.tax_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.received_qty).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.free_qty).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.rejected_qty).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.accepted_qty).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.unit_cost).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.discount_percent).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.line_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.net_amount).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.net_unit_cost).HasColumnType("decimal(18,6)").HasDefaultValue("0");
                entity.Property(a => a.tax_percent).HasColumnType("decimal(9,4)").HasDefaultValue("0");
                entity.Property(a => a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.updated_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            });

            modelBuilder.Entity<im_GoodsReceiptLineBatches>(entity =>
            {
                entity.Property(a => a.goods_receipt_line_id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(a => a.received_qty).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.free_qty).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.rejected_qty).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.accepted_qty).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.unit_cost).HasColumnType("decimal(18,4)").HasDefaultValue("0");
                entity.Property(a => a.net_unit_cost).HasColumnType("decimal(18,6)").HasDefaultValue("0");
                entity.Property(a => a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.updated_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<om_OrderSources>(entity =>
            {
                entity.Property(a => a.source_id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                entity.Property(a=>a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a=>a.updated_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.status).HasColumnType("char(1)").HasDefaultValue("T");
            });

            modelBuilder.Entity<om_CustomerOrders>(entity =>
            {
                entity.Property(a => a.order_date).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.expected_payment_method).HasColumnType("nvarchar(30)").HasDefaultValue("COD");
                entity.Property(a => a.payment_status).HasColumnType("nvarchar(30)").HasDefaultValue("UNPAID");
                entity.Property(a => a.order_status).HasColumnType("nvarchar(30)").HasDefaultValue("NEW");
                entity.Property(a => a.fulfillment_status).HasColumnType("nvarchar(30)").HasDefaultValue("PENDING");
                entity.Property(a => a.delivery_status).HasColumnType("nvarchar(30)").HasDefaultValue("PENDING");
                entity.Property(a => a.urget_delivery).HasColumnType("char(1)").HasDefaultValue("F");
                entity.Property(a => a.currency_code).HasColumnType("nvarchar(15)");
                entity.Property(a => a.exchange_rate).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.sub_total).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.discount_amount).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.tax_amount).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.delivery_charge).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.other_charges).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.grand_total).HasColumnType("decimal(18,4)").HasDefaultValue(0m);

                entity.HasIndex(e=> new { e.business_id, e.store_id, e.order_date}).HasDatabaseName("IX_om_CustomerOrders_BusinessId_StoreId_OrderDate").IsDescending(false,false,true);
                entity.HasIndex(e => new { e.business_id, e.store_id, e.order_status,e.delivery_status,e.fulfillment_status }).HasDatabaseName("IX_om_CustomerOrders_order_status");
            });

            modelBuilder.Entity<om_CustomerOrderLines>(entity =>
            {
                entity.Property(a => a.ordered_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.reserved_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.picked_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.dispatched_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.delivered_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.returned_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.cancelled_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.unit_price).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.discount_amount).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.tax_amount).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.line_total).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.line_status).HasColumnType("nvarchar(30)").HasDefaultValue("OPEN");
                entity.Property(a => a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => new { e.customer_order_id, e.line_no }).HasDatabaseName("UX_om_CustomerOrderLines_order_line_no");
                entity.HasIndex(e => new { e.variant_id, e.line_status,e.customer_order_id,e.ordered_qty,e.reserved_qty,e.delivered_qty }).HasDatabaseName("IX_om_CustomerOrderLines_variant");

            });

            modelBuilder.Entity<om_OrderStatusHistory>(entity =>
            {
                entity.HasIndex(e=> new { e.customer_order_id, e.changed_at }).HasDatabaseName("IX_om_OrderStatusHistory_order_changed_at").IsDescending(false, true);
            });

            modelBuilder.Entity<im_InventoryReservations>(entity =>
            {
                entity.Property(a => a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.reserved_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.released_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.consumed_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.reservation_status).HasColumnType("nvarchar(20)").HasDefaultValue("ACTIVE");

                entity.HasIndex(a => new { a.customer_order_id, a.reservation_status }).HasDatabaseName("IX_im_InventoryReservations_order");
                entity.HasIndex(a => new { a.store_id, a.variant_id, a.reservation_status }).HasDatabaseName("IX_im_InventoryReservations_store_variant_status");
            });

            modelBuilder.Entity<mk_blacklisted_numbers>(entity =>
            {
                entity.Property(a => a.is_active).HasColumnType("char(1)").HasDefaultValue("T");
            });

            modelBuilder.Entity<om_FulfillmentOrders>(entity =>
            {
                entity.Property(a => a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.total_ordered_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.total_reserved_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.total_delivered_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.total_returned_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.total_rejected_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);

                entity.HasIndex(a => new { a.fulfillment_id, a.business_id, a.store_id, a.customer_order_id }).HasDatabaseName("IX_om_FulfillmentOrders");
                entity.HasIndex(a => new { a.fulfillment_no }).HasDatabaseName("IX_fulfillment_no");
                entity.HasIndex(a => new { a.picked_by,a.pick_started_at,a.pick_completed_at,a.packed_at,a.ready_at }).HasDatabaseName("IX_Orders");

                entity.HasOne(e => e.om_CustomerOrders)
        .WithMany()
        .HasForeignKey(e => e.customer_order_id)
        .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<om_FulfillmentLines>(entity =>
            {
                entity.Property(a => a.ordered_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.reserved_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.picked_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.packed_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.delivered_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.returned_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);
                entity.Property(a => a.rejected_qty).HasColumnType("decimal(18,4)").HasDefaultValue(0m);

                entity.HasIndex(a => new { a.fulfillment_line_id, a.fulfillment_id, a.product_id, a.store_variant_inventory_id, a.batch_id }).HasDatabaseName("IX_om_FulfillmentLines");
                entity.HasIndex(a => new { a.rejected_qty, a.line_status }).HasDatabaseName("IX_line_statuss");

                entity.HasOne(e => e.om_FulfillmentOrders)
        .WithMany()
        .HasForeignKey(e => e.fulfillment_id)
        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<sys_Images>(entity =>
            {
                entity.Property(a => a.status).HasColumnType("char(1)").HasDefaultValue("T");
                entity.Property(a => a.created_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.updated_at).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

                entity.HasIndex(a => new { a.image_id, a.source_id }).HasDatabaseName("IX_sys_Images");
                entity.HasIndex(a => new { a.source_id }).HasDatabaseName("IX_image_url");
            });

            // view tables

            modelBuilder.Entity<om_CustomerOrders_dto>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null); 
            });

            modelBuilder.Entity<so_sales_header_customer>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null); 
            });
        }

        // edited by rijobin 

        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<gl_Accounts> gl_Accounts { get; set; }
        public DbSet<gl_AccountMapping> gl_AccountMapping { get; set; }
        public DbSet<gl_Ledger> gl_ledger { get; set; }
        public DbSet<gl_AccountCurrentBalances> gl_AccountCurrentBalances {get; set; }
        public DbSet<gl_FiscalPeriods> gl_FiscalPeriods { get; set; }
        public DbSet<gl_BusinessSettings> gl_BusinessSettings { get; set; }
        public DbSet<gl_JournalHeaders> gl_JournalHeaders { get; set; }
        public DbSet<gl_JournalLines> gl_JournalLines { get; set; }
        public DbSet<gl_JournalAttachments> gl_JournalAttachments { get; set; }
        public DbSet<ap_Payments> ap_Payments { get; set; }
        public DbSet<ap_PaymentAllocations> ap_PaymentAllocations { get; set; }
        public DbSet<ap_PaymentLines> ap_PaymentLines { get; set; }
        public DbSet<ap_PaymentAttachments> ap_PaymentAttachments { get; set; }
        public DbSet<fin_BankDeposits> fin_BankDeposits { get; set; }
        public DbSet<fin_BankDepositLines> fin_BankDepositLines { get; set; }
        public DbSet<fin_BankDepositAttachments> fin_BankDepositAttachments { get; set; }
        public DbSet<ap_Expenses> ap_Expenses { get; set; }
        public DbSet<ap_ExpenseLines> ap_ExpenseLines { get; set; }
        public DbSet<ap_ExpensesAttachments> ap_ExpensesAttachments { get; set; }
        public DbSet<ap_Cheques> ap_Cheques { get; set; }
        public DbSet<ap_ChequeLines> ap_ChequeLines { get; set; }
        public DbSet<ap_ChequesAttachments> ap_ChequesAttachments { get; set; }
    }

}
