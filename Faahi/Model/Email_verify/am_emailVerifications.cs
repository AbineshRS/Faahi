using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Email_verify
{
    public class am_emailVerifications
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? Email_id { get; set; }

        [Column(TypeName ="varchar(50)")]
        public string? email { get; set; }=null;

        [Column(TypeName ="varchar(30)")]
        public string? verificationType { get; set;} =null;

        [Column(TypeName = "datetime")]
        public DateTime? tokenExpiryTime { get; set; } = null;

        [Column(TypeName = "varchar(max)")]
        public string? token { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? isExpired { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? verified { get; set; } = string.Empty;

        [Column(TypeName = "varchar(30)")]
        public string? userType { get; set; } = null;
    }
}
