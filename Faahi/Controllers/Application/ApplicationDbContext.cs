using Faahi.Model;
using Faahi.Model.am_vcos;
using Faahi.Model.co_business;
using Faahi.Model.Email_verify;
using Faahi.Model.im_products;
using Faahi.Model.Shared_tables;
using Faahi.Model.table_key;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Controllers.Application
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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


    }

}
