using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(store_variant_inventory_id),Name = "store_variant_inventory_id")]
    [Index(nameof(variant_id),Name = "variant_id")]
    [Index(nameof(store_id),Name = "store_id")]
    public class im_StoreVariantInventory
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_variant_inventory_id { get; set; }

        [ForeignKey("variant_id")]
        [Display(Name = "im_ProductVariants")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [ForeignKey("store_id")]
        [Display(Name = "st_stores")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? on_hand_quantity { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? committed_quantity { get; set; }


        [Column(TypeName = "nvarchar(24)")]
        public string? bin_number { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? batch_number { get; set; }=null;
    }
}
