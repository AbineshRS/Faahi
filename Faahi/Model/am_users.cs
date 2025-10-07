using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model
{
    public class am_users
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? userId { get; set; }

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

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string isGoogleSignUp { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        public string? googleId { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string emailVerified { get; set; } = string.Empty;

        //[Column(TypeName = "varchar(32)")]
        //public string? siteId { get; set; } = null;

        //[Column(TypeName = "varchar(200)")]
        //public string? company_id { get; set; } = null;

        //[Column(TypeName = "varchar(20)")]
        //public string? userRole { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? edit_date_time { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? edit_user_id { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string status { get; set; } = string.Empty;

        [Column(TypeName = "varchar(20)")]
        public string? phoneNumber { get; set; } = null;

        [Column(TypeName = "varchar(200)")]
        public string? address1 { get; set; } = null;

        [Column(TypeName = "varchar(200)")]
        public string? address2 { get; set; } = null;
    }

}
