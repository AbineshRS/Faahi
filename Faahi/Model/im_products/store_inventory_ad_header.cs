using Faahi.Model.st_sellers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.im_products
{
    [Table("im_store_inventory_ad_header")]
    public class store_inventory_ad_header
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid store_inventory_ad_id { get; set; }


        [ForeignKey(nameof(adjustment_id))]
        [JsonIgnore]
        public inventory_adjustment_header inventory_adjustment_header { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid? adjustment_id { get; set; }=null;

        [Column(TypeName = "nvarchar(50)")]
        public string inventory_code { get; set; }


        [ForeignKey(nameof(store_id))]
        [JsonIgnore]
        public st_stores st_Stores { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid store_id { get; set; }

        [Column(TypeName = "int")]
        public int total_items { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal total_negative_value { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal total_positive_value { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_item_adjusted { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_cost { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime created_at { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string status { get; set; }
        //PENDING,APPROVED,POSTED,REJECTED


      
        public ICollection<store_inventory_ad_details>? store_inventory_ad_details { get; set; }=null;


    }
}
