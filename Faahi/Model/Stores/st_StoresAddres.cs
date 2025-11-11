using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Stores
{
    [Index(nameof(store_id))]
    [Index(nameof(address_type))]
    [Index(nameof(is_current))]
    [Index(nameof(valid_from))]
    public class st_StoresAddres
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid store_address_id { get; set; }

        [ForeignKey("store_id")]
        [Display(Name = "st_stores")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? address_type { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? line1 { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? line2 { get; set; }=null;

        [Column(TypeName = "nvarchar(100)")]
        public string? city { get; set; }=null;

        [Column(TypeName = "nvarchar(100)")]
        public string? region { get; set; }=null;

        [Column(TypeName = "nvarchar(30)")]
        public string? postal_code { get; set; }=null;

        [Column(TypeName = "nvarchar(100)")]
        public string? country { get; set; }=null;

        [Column(TypeName ="datetime")]  
        public DateTime? valid_from { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? valid_to { get; set; }=null;

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; }

        [Column(TypeName ="char(1)")]
        [DefaultValue("T")]
        [StringLength(1)]
        public string? is_current { get; set; }=null;


    }
}
