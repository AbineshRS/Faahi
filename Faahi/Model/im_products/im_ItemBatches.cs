using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(item_batch_id), Name = "IX_item_batch_id")]
    [Index(nameof(variant_id), Name = "IX_variant_id")]
    [Index(nameof(company_id), Name = "IX_CompanyId")]
    [Index(nameof(store_id), Name = "IX_store_id")]
    [Index(nameof(batch_on_hold), Name = "IX_batch_on_hold")]
    [Index(nameof(on_hand_quantity), Name = "IX_on_hand_quantity")]
    [Index(nameof(is_active), Name = "IX_is_active")]
    [Index(nameof(expiry_date), Name = "IX_expiry_date")]

    public class im_ItemBatches
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? item_batch_id { get; set; }

        [Column(TypeName = "int")]
        public int? batch_id { get; set; }

        [ForeignKey("detail_id")]
        [Display(Name = "im_purchase_listing_details")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? detail_id { get; set; } = null;

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; } = null;

        [ForeignKey("store_id")]
        [Display(Name = "st_stores")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; } = null;

        [ForeignKey("variant_id")]
        [Display(Name = "im_ProductVariants")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }=null;

        [ForeignKey("store_variant_inventory_id")]
        [Display(Name = "im_StoreVariantInventory")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_variant_inventory_id { get; set; }=null;

        [Column(TypeName = "nvarchar(50)")]
        public string? batch_number { get; set; }

        [Column(TypeName = "date")]
        public DateTime? expiry_date { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal? received_quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "On-hand quantity cannot be negative.")]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal? on_hand_quantity { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public Decimal? reserved_quantity { get; set; }

        //Costing
        [Range(0, double.MaxValue, ErrorMessage = "Unit cost cannot be negative.")]
        [Column(TypeName ="decimal(18,2)")]
        public Decimal? unit_cost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal? total_cost { get; set; }

        //    -- Selling control
        [Column(TypeName = "decimal(18,2)")]
        public Decimal? batch_promo_price { get; set; } = null;

        [Column(TypeName ="date")]
        public DateOnly? promo_from_date { get; set; } = null;

        [Column(TypeName = "date")]
        public DateOnly? promo_to_date { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? batch_on_hold { get; set; }

        [Column(TypeName = "date")]
        public DateOnly? is_expired { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? is_active { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? received_date { get; set; } = null;

        [Column(TypeName = "nvarchar(100)")]
        public string? reference_doc { get; set; } = null;

        [Column(TypeName = "nvarchar(100)")]
        public string? notes { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? sku { get; set; } = null;

        [Column(TypeName = "nvarchar(100)")]
        public string? barcode { get; set; } = null;

        [Column(TypeName = "nvarchar(100)")]
        public string? product_description { get; set; } = null;


    }
}
