using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Stores
{
    public class st_UserStoreAccess
    {
        [Key]
        [Column(TypeName= "uniqueidentifier")]
        public Guid store_access_id { get; set; }

        [ForeignKey("user_id")]
        [Display(Name = "st_Users")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? user_id { get; set; }


        [ForeignKey("store_id")]
        [Display(Name = "st_stores")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }

        [ForeignKey("role_id")]
        [Display(Name = "st_UserRoles")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? role_id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; }=null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; } = null;

    }
}
