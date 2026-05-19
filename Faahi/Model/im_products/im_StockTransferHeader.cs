using Faahi.Model.st_sellers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.im_products
{
    public class im_StockTransferHeader
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid transfer_id { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string transfer_code { get; set; }

        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        [ValidateNever]
        public co_business.co_business co_Business { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }

        [ForeignKey(nameof(from_store_id))]
        [JsonIgnore]
        [ValidateNever]
        public st_stores FromStore { get; set; } = null!;
        [Column(TypeName = "uniqueidentifier")]
        public Guid from_store_id { get; set; }

        [ForeignKey(nameof(to_store_id))]
        [JsonIgnore]
        [ValidateNever]
        public st_stores ToStore { get; set; } = null!;
        [Column(TypeName = "uniqueidentifier")]
        public Guid to_store_id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_quantity { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_amount { get; set; } = 0m;

        [Column(TypeName ="datetime")]
        public DateTime created_at { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? created_by_user_id { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string? created_by { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? approved_at { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? approved_by { get; set; } = null;

        [Column(TypeName ="nvarchar(20)")]
        public string status { get; set; }
        //Draft / Approved / Completed / Cancelled


        public ICollection<im_StockTransferLines>? im_StockTransferLines { get; set; } = null;
    }
}
