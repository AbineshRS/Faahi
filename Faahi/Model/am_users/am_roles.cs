using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.am_users
{
    public class am_roles
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid role_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string role_code { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string role_name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string role_group { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string? description { get; set; } = null;

        [Column(TypeName ="char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string? is_system_role { get; set; }

        [Column(TypeName ="char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string? status { get; set; }

        public ICollection<am_user_roles>? am_user_roles { get; set; } = null;
    }
}
