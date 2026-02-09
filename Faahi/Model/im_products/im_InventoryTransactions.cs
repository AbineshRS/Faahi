using Faahi.Model.am_vcos;
using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(transaction_id),Name = "IX_transaction_id",IsUnique =true)]
    [Index(nameof(store_id),Name = "IX_store_id")]
    [Index(nameof(listing_id),Name = "IX_listing_id")]
    public class im_InventoryTransactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName ="uniqueidentifier")]
        public Guid? transaction_id { get; set; }


        [ForeignKey(nameof(listing_id))]
        public im_purchase_listing? Listing { get; set; }


        [Column(TypeName ="uniqueidentifier")]
        public Guid? listing_id { get;  set; }


        [ForeignKey(nameof(store_id))]
        public st_stores? stores { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }



        [ForeignKey(nameof(variant_id))]
        public im_ProductVariants? im_ProductVariants { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get;  set; }


        [ForeignKey(nameof(batch_id))]
        public im_ItemBatches ? im_ItemBatches { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? batch_id { get;  set; }



        [Column(TypeName = "datetime")]
        public DateTime? trans_date { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? trans_type { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? trans_reason { get; set; } = null;


        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? quantity_change { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? unit_cost { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? total_cost { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? source_doc_type { get; set; } = null;

        [Column(TypeName = "nvarchar(255)")]
        public string? remarks { get; set; } = null;


        [Column(TypeName = "datetime")]
        public DateTime? created_date_time { get; set; } = null;
    }
}
