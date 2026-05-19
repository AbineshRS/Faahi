using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.am_vcos
{
    public class payment_terms
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid payment_term_id { get; set; }

        [ForeignKey(nameof(business_id))]
        [JsonIgnore]

        public co_business.co_business? co_Business { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }

        [Column(TypeName ="int")]
        public int due_days { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string? due_description { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? created_by_user_id { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string? created_by { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime created_at { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime updated_at { get; set; }

        [Column(TypeName ="char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string status { get; set; }
    }
}
