using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Admin
{
    public class sa_roles
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid sa_role_id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? sa_role_name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? sa_description { get; set; } = null;

        [Column(TypeName ="char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string? sa_status { get; set; }

    }
}
