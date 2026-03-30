using Faahi.Model.co_business;
using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.am_users
{
    [Index(nameof(user_id),Name = "IX_am_user_roles_user_id")]
    [Index(nameof(business_id),nameof(store_id),Name = "IX_am_user_roles_business_store")]
    public class am_user_roles
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid user_role_id { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(user_id))]
        public am_users am_Users { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid user_id { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(role_id))]
        public am_roles am_roles { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid? role_id { get; set; }


        [ForeignKey(nameof(business_id))]
        public Faahi.Model.co_business.co_business c_business { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid? business_id { get; set; }=null;


        [ForeignKey(nameof(store_user_id))]
        public st_Users st_Users { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_user_id { get; set; }=null;


        [ForeignKey(nameof(store_id))]
        public st_stores st_Stores { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; } = null;


        [Column(TypeName ="datetime")]
        public DateTime created_at { get; set; }

        public ICollection<am_user_business_access>? am_user_business_access { get; set; } = null;

        public ICollection<mk_customer_profiles>? mk_customer_profiles { get; set; } = null;
    }
}
