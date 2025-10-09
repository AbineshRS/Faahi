using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_site_users
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? userId { get; set; }

        [ForeignKey("site_id")]
        [Display(Name = "im_site")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? site_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? site_user_code { get; set; }

        [Column(TypeName = "varchar(32)")]
        public string? userName { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? password { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string firstName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string? lastName { get; set; } = null;

        [Column(TypeName = "varchar(200)")]
        public string fullName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        public string email { get; set; } = string.Empty;

        [Column(TypeName = "varchar(20)")]
        public string? userRole { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? edit_date_time { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? edit_user_id { get; set; } = null;


        [Column(TypeName = "varchar(20)")]
        public string? phoneNumber { get; set; } = null;

        [Column(TypeName = "varchar(200)")]
        public string? address { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string status { get; set; } = string.Empty;
    }
}
