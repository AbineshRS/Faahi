using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.pos_tables
{
    [Index(nameof(drawer_session_id),IsUnique =true,Name = "IX_drawer_session_id")]
    [Index(nameof(business_id), Name = "IX_business_id")]
    [Index(nameof(store_id), Name = "IX_store_id")]
    [Index(nameof(terminal_id), Name = "IX_terminal_id")]
    [Index(nameof(opened_at), Name = "IX_opened_at")]
    public class pos_DrawerSessions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid drawer_session_id { get; set; }

        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        public Faahi.Model.co_business.co_business? co_Business { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }

        [ForeignKey(nameof(store_id))]
        [JsonIgnore]
        public st_stores? st_Stores { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? terminal_id { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string opened_by { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? closed_by { get; set; }=null;

        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0.0)]
        public Decimal opening_float { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? expected_closing {  get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? actual_closing {  get; set; } = null;
            
        [Column(TypeName = "decimal(18,4)")]
        public Decimal? difference_amount {  get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? created_by { get; set; } = null;

        [Column(TypeName = "nvarchar(250)")]
        public string? notes { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime opened_at { get; } = DateTime.Now;
            
        [Column(TypeName = "datetime")]
        public DateTime? closed_at { get; } = null;

        [Column(TypeName = "datetime")]
        public DateTime created_at { get; }

        [Column(TypeName ="char(1)")] // O = Open, C = Closed
        [DefaultValue("O")]
        public string status {  get; set; } ="O";


    }
}
