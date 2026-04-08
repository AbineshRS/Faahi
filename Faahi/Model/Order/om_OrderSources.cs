using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Order
{
    [Index(nameof(source_id),nameof(source_code),Name = "UX_om_OrderSources_business_source_code")]
    [Index(nameof(business_id),nameof(store_id),nameof(status),nameof(source_name),Name = "IX_om_OrderSources_business_store_active")]
    public class om_OrderSources
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid source_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? business_id { get; set; }
        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        public Faahi.Model.co_business.co_business? co_business { get; set; }


        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }
        [ForeignKey(nameof(store_id))]
        [JsonIgnore]
        public st_stores? st_Stores { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string source_code { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string source_name { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? description { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime created_at { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; }

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string status { get; set; }
    }
}
