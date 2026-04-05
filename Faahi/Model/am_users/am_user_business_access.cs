using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.am_users
{
    [Index(nameof(user_id),nameof(business_id),nameof(store_id),Name = "IX_am_user_business_access_user_business_store")]
    [Index(nameof(business_id),nameof(store_id),Name = "X_am_user_business_access_business_store")]
    public class am_user_business_access
    {
        [Key]
        [Column(TypeName = "uniqueidentifier ")]
        public Guid access_id { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(user_id))]
        public am_users am_Users { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid user_id { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(user_role_id))]
        public am_user_roles am_user_roles { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid? user_role_id { get; set; }


        [ForeignKey(nameof(business_id))]
        public Faahi.Model.co_business.co_business c_business { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid? business_id { get; set; } = null;


        [ForeignKey(nameof(store_id))]
        public st_stores st_Stores { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; } = null;

        [Column(TypeName = "varchar(30)")]
        public string access_level { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime created_at { get; set; }

        [Column(TypeName ="char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string status { get; set; }
    }
}
