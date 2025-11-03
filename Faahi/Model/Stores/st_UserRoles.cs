using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Stores
{
    [Index(nameof(role_name))]
    [Index(nameof(company_id))]
    [Index(nameof(description))]
    public class st_UserRoles
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? role_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? role_name { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? description { get; set; } = null;

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }
    }
}
