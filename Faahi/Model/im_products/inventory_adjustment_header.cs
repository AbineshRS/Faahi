using Faahi.Model.st_sellers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.im_products
{
    [Table("im_inventory_adjustment_header")]
    public class inventory_adjustment_header
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid adjustment_id { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string adjustment_code { get; set; }

        [ForeignKey(nameof(store_id))]
        [JsonIgnore]
        public st_stores st_stores { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid store_id { get; set; }

        [Column(TypeName ="nvarchar(30)")]
        public string adjustment_type { get; set; }

        [Column(TypeName ="int")]
        public int total_items { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public Decimal total_negative_value { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public Decimal total_positive_value { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public Decimal total_adjustment_value { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? created_by { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? approved_by { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? transaction_date { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? approved_date { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime created_at { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? updated_at { get; set; }

        //[Column(TypeName ="char(1)")]
        //[StringLength(1)]
        //[DefaultValue("F")]
        //public string is_freeze { get; set; }

        [Column(TypeName ="char(1)")]
        [StringLength(1)]
        [DefaultValue("F")]
        public string is_posted { get; set; }

        [Column(TypeName ="nvarchar(30)")]
        public string status { get; set; }
        //PENDING,APPROVED,POSTED,REJECTED

        public ICollection<inventory_adjustment_lines>? inventory_adjustment_lines { get; set; } = null;
    }
}
