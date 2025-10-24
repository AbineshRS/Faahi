using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.st_sellers
{
    public class st_sellers
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? seller_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string? Full_name { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string? email { get; set; }

        [Column(TypeName ="varchar(30)")]
        public string? phone { get; set; } = null;

        [Column(TypeName ="varchar(max)")]
        public string? password { get; set; }=null;

        [Column(TypeName = "varchar(30)")]
        public string? account_type { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? registration_date { get; set; }=null ;

        [DefaultValue("pending")]
        [Column(TypeName ="varchar(20)")]
        public string? status { get; set; } = null;
    }
}
