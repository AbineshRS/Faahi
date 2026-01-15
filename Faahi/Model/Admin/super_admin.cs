using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Admin
{
    [Index(nameof(super_admin_id), Name = "super_admin_id", IsUnique = true)]
    [Index(nameof(email), Name ="email")]
    [Index(nameof(phone), Name = "phone")]
    [Index(nameof(status), Name = "status")]
    public class super_admin
    {
        [Key]
        [Column(TypeName ="uniqueidentifier")]
        public Guid? super_admin_id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? name { get; set; }= null;

        [Column(TypeName = "nvarchar(100)")]
        public string? email { get; set; }= null;

        [Column(TypeName = "nvarchar(200)")]
        public string? password { get; set; }= null;

        [Column(TypeName = "nvarchar(20)")]
        public string? phone { get; set; }= null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; }= null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; }= null;

        [Column(TypeName ="nvarchar(10)")]
        public string? user_type { get; set; }= null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; }= null;
    }
}
