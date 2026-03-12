using Faahi.Model;
using Faahi.Model.Admin;
using Faahi.Model.am_vcos;
using Faahi.Model.co_business;
using Faahi.Model.countries;
using Faahi.Model.Email_verify;
using Faahi.Model.im_products;
using Faahi.Model.pos_tables;
using Faahi.Model.sales;
using Faahi.Model.Shared_tables;
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


        //TEMPTABLES
        public DbSet<temp_im_variant> temp_im_variants { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configurations can be added here if needed
            modelBuilder.Entity<im_ItemBatches>().ToTable(tb =>
            {
                tb.HasCheckConstraint("CK_im_itemBatches_on_hand_quantity", "[on_hand_quantity] >= 0");
                tb.HasCheckConstraint("CK_im_itemBatches_expiry_date", "[expiry_date] > GETDATE()");
                tb.HasCheckConstraint("CK_im_itemBatches_unit_cost", "[unit_cost] >= 0");
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
                entity.Property(e => e.doc_type).HasColumnType("varchar(10)").HasDefaultValue("SALE");
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
        }

    }

}
