using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.co_business
{
    public class co_address
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_address_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name ="co_business")]
        [Column(TypeName ="uniqueidentifier")]
        public Guid? company_id { get; set; }

        [Column(TypeName = "varchar(60)")]
        public string? street_1 { get; set; } = null;

        [Column(TypeName = "varchar(60)")]
        public string? street_2 { get; set; } = null;

        [Column(TypeName = "varchar(60)")]
        public string? city { get; set; } = null;

        [Column(TypeName = "varchar(60)")]
        public string? state { get; set; } = null;

        [Column(TypeName = "varchar(60)")]
        public string? postal_code { get; set; } = null;

        [Column(TypeName = "varchar(60)")]
        public string? country { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? telephone_1 { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? telephone_2 { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? email { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName ="char(1)")]
        public string status { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime? edit_date_time { get; set; }=null;

        [Column(TypeName ="varchar(16)")]
        public string? edit_user_id { get; set; } = null;

        [Column(TypeName ="varchar(50)")]
        public string? contact_person { get; set; } = null;

    }
}
