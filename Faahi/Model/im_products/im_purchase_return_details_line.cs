using Faahi.Service.im_products;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(return_detail_id), Name = "return_detail_id")]
    [Index(nameof(return_id), Name = "return_id")]
    [Index(nameof(product_id), Name = "product_id")]
    public class im_purchase_return_details_line
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid return_detail_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? return_id { get; set; }
        [ForeignKey(nameof(return_id))]
        public im_purchase_return_header return_detail_header { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? product_id { get; set; }
        [ForeignKey(nameof(product_id))]
        public Faahi.Model.im_products.im_Products im_Products { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? sub_variant_id { get; set; }
        [ForeignKey(nameof(sub_variant_id))]
        public im_ProductVariants im_ProductVariants { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_variant_inventory_id { get; set; }
        [ForeignKey(nameof(store_variant_inventory_id))]
        public im_StoreVariantInventory im_StoreVariantInventory { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? uom_name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? return_qty { get; set; } 

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? unit_price { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? line_total { get; set; }


        [Column(TypeName = "decimal(18,4)")]
        public Decimal? orginal_unit_price { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? orginal_line_total { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? other_expenses { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? orginal_quantity { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? batch_no { get; set; }

        [Column(TypeName = "date")]
        public DateOnly? expiry_date { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? return_reason { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? product_brand { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? product_title { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? barcode { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? sku { get; set; }
    }
}
