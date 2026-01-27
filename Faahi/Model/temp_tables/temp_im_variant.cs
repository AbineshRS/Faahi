using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.temp_tables
{
    [Index(nameof(temp_variant_id),Name = "temp_variant_id")]
    [Index(nameof(store_id),Name ="store_id")]
    [Index(nameof(company_id),Name = "company_id")]
    public class temp_im_variant
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? temp_variant_id { get; set; }

        [ForeignKey("detail_id")]
        [Display(Name = "im_purchase_listing_details")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? detail_id { get; set; } = null;

        [ForeignKey("store_id")]
        [Display(Name = "st_stores")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [ForeignKey("product_id")]
        [Display(Name = "im_Products")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? product_id { get; set; }

        [ForeignKey("variant_id")]
        [Display(Name = "im_ProductVariants")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? cost_price { get; set; } = null;

        [Column(TypeName = "decimal(18,2)")]
        public Decimal? quantity { get; set; }

    }
}
