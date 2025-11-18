using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(user_id))]
    [Index(nameof(variant_id))]
    [Index(nameof(store_id))]
    public class im_SellerInventory
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid seller_inventory_id { get; set; }

        [ForeignKey("store_id")]
        [Display(Name = "st_stores")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }

        [ForeignKey("user_id")]
        [Display(Name = "st_Users")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? user_id { get; set; }

        [ForeignKey("variant_id")]
        [Display(Name = "im_ProductVariants")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }

        [ForeignKey("uom_id")]
        [Display(Name = "im_UnitsOfMeasure")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? uom_id { get; set; } = null;

        [Column(TypeName ="decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal stock_quantity { get; set; }


        [Column(TypeName ="decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal? reorder_level { get; set; }

        [Column(TypeName ="int")]
        [DefaultValue(0)]
        public Int16? sales_count { get; set; }= null;

        [Column(TypeName = "varchar(25)")]
        public string? Rack_no { get; set; } = null;


        [Column(TypeName = "varchar(25)")]
        public string? bin_number { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal? Consignment_quantity { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal? committed_quantity { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? updated_at { get; set; }


        [Column(TypeName ="char(1)")]
        [StringLength(1)]
        [DefaultValue("F")]
        public string? on_hold { get; set; }=string.Empty;

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        [DefaultValue("F")]
        public string? allow_Inter_Location_Transfer { get; set; } = string.Empty;

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        [DefaultValue("F")]
        public string? sales_on_hold { get; set; } = string.Empty;

        

    }
}
