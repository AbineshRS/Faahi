using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(user_id))]
    [Index(nameof(variant_id))]
    [Index(nameof(transaction_date))]
    [Index(nameof(source_doc_type))]
    [Index(nameof(source_doc_id))]
    public class im_InventoryLedger
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid ledger_id { get; set; }

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

        [Column(TypeName ="nvarchar(50)")]
        public string? transaction_type { get; set; }

        [Column(TypeName ="decimal(18,4)")]
        public Decimal? quantity { get; set; }


        [Column(TypeName ="decimal(18,4)")]
        public Decimal? unit_cost { get; set; }

        [Column(TypeName ="nvarchar(10)")]
        public string? currency { get; set; }=null;

        [Column(TypeName ="nvarchar(50)")]
        public string? source_doc_type { get; set; }=null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? source_doc_id { get; set; } = null;

        [Column(TypeName = "int")]
        public Int16? source_doc_line { get; set; } = null;

        [Column(TypeName ="varchar(400)")]
        public string? reference_note { get; set; } = null;

        [Column(TypeName ="varchar(50)")]
        public string? cost_method { get;set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? transaction_date { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get;set; }

    }
}
