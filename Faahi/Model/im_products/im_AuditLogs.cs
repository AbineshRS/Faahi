using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.im_products
{
    public class im_AuditLogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid audit_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? record_id { get; set; }

        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        public co_business.co_business co_business  =null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid? business_id { get; set; } = null;

        [Column(TypeName ="nvarchar(50)")]
        public string action_type { get; set; }
        //INSERT / UPDATE / DELETE

        [Column(TypeName ="nvarchar(100)")]
        public string field_name { get; set; }

        [Column(TypeName ="nvarchar(max)")]
        public string old_value { get; set; }

        [Column(TypeName ="nvarchar(max)")]
        public string new_value { get; set; }

        [Column(TypeName ="nvarchar(max)")]
        public string? changedby_name { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? changed_by_user_id { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime? changed_at { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string remarks { get; set; }
    }
}
