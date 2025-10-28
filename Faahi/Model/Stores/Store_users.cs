using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Stores
{
    public class Store_users
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? user_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Full_name { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? email { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? phone { get; set; } = null;

        [Column(TypeName = "varchar(max)")]
        public string? password { get; set; } = null;

        [Column(TypeName = "varchar(30)")]
        public string? account_type { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? registration_date { get; set; } = null;

        [DefaultValue("pending")]
        [Column(TypeName = "varchar(20)")]
        public string? status { get; set; } = null;

        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid store_access_id { get; set; }

        


        [ForeignKey("store_id")]
        [Display(Name = "st_stores")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }

        [ForeignKey("role_id")]
        [Display(Name = "st_UserRoles")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? role_id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

       


    }
}
