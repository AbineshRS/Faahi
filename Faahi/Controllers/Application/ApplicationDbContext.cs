using Faahi.Model;
using Faahi.Model.Accounts;
using Faahi.Model.Admin;
using Faahi.Model.am_vcos;
using Faahi.Model.co_business;
using Faahi.Model.countries;
using Faahi.Model.Email_verify;
using Faahi.Model.Finance;
using Faahi.Model.im_products;
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

        public DbSet<im_InventoryLedger> im_InventoryLedger { get;set; }
         
        public DbSet<im_SellerInventory> im_SellerInventory { get; set; }

        public DbSet<im_Lots> im_Lots { get; set; }

        public DbSet<im_ProductAttributes> im_ProductAttributes { get; set; }

        public DbSet<im_AttributeValues> im_AttributeValues { get; set; }

        public DbSet<im_VariantAttributes> im_VariantAttributes { get; set; }

        public DbSet<im_StoreVariantInventory> im_StoreVariantInventory { get;set; }

        public DbSet<super_admin> super_admin { get; set; }

        public DbSet<sa_country_regions> sa_country_regions { get; set; }

        public DbSet<sa_regions> sa_regions { get; set; }

        public DbSet<fin_PartyBankAccounts> fin_PartyBankAccounts { get; set; }


        public DbSet<im_bin_location> im_bin_locations { get; set; }

        public DbSet<im_ItemBatches> im_itemBatches { get; set; }

        public DbSet<super_abi> super_abi { get; set; }

        public DbSet<tx_TaxClasses> tx_TaxClasses { get; set; }

        public DbSet<im_InventoryTransactions> im_InventoryTransactions { get; set; }







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


            //modelBuilder.Entity<im_InventoryTransactions>().ToTable(tb =>
            //{
            //    tb.HasCheckConstraint("CK_im_InventoryTransactions_sub_total", "[sub_total]>=0");
            //    tb.HasCheckConstraint("CK_im_InventoryTransactions_doc_totall", "[doc_total]>=0");

            //});
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
