using Faahi.Model.sales;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.pos_tables
{
    [Index(nameof(drawer_session_id),IsUnique =true,Name = "IX_drawer_session_id")]
    [Index(nameof(payment_method_id), Name = "IX_payment_method_id")]

    public class pos_DrawerCountDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid drawer_count_id { get; set; }

        [ForeignKey(nameof(drawer_session_id))]
        [JsonIgnore]
        public pos_DrawerSessions pos_DrawerSessions { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid drawer_session_id { get; set; }

        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        public Faahi.Model.co_business.co_business? co_Business { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }

        [ForeignKey(nameof(payment_method_id))]
        public so_payment_type? payment_type { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid payment_method_id { get; set; }


        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0.0)]
        public Decimal expected_amount { get; set; } = 0m;


        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0.0)]
        public Decimal counted_amount { get; set; } = 0m;


        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0.0)]
        public Decimal difference_amount { get; set; } = 0m;

        [Column(TypeName = "nvarchar(50)")]
        public string? created_by { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = DateTime.Now;


    }
}
