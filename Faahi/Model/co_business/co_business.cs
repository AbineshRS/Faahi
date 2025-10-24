using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.co_business
{
    public class co_business
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [Column(TypeName ="varchar(100)")]
        public string? business_name { get; set; }

        [Column(TypeName ="varchar(50)")]
        public string? tin_number {  get; set; }=null;

        [Column(TypeName ="varchar(50)")]
        public string? name {  get; set; }=null;

        [Column(TypeName ="varchar(100)")]
        public string? password { get; set; } =null;

        [Column(TypeName ="varchar(50)")]
        public string? reg_no { get; set; }= null;

        [Column(TypeName ="varchar(20)")]
        public string? country { get; set; }=null ;

        [Column(TypeName = "varchar(max)")]
        public string? address { get; set; } = null;

        [Column(TypeName = "varchar(100)")]
        public string? logo_fileName { get; set; } = null;

        [Column(TypeName ="varchar(20)")]
        public string? phoneNumber { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? plan_type { get; set; } = null;

        [Column(TypeName = "int")]
        public Int32? sites_allowed { get; set; } = null;

        [Column(TypeName = "int")]
        public Int32? createdSites { get; set; } = null;


        [Column(TypeName = "int")]
        public Int32? sites_users_allowed { get; set; } = null;

        [Column(TypeName = "int")]
        public Int32? createdSites_users { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? edit_date_time { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? edit_user_id { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? email { get; set; } = null;


        public ICollection<co_address>? co_addresses { get; set; } = null;
    }
}
