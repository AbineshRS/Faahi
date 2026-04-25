using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Shared_tables
{
    public class sys_Images
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid image_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid source_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string? source_type { get; set; }

        [Column(TypeName ="nvarchar(max)")]
        public string? image_url { get;set; }

        [Column(TypeName ="datetime")]
        public DateTime created_at { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime updated_at { get; set; }

        [Column(TypeName ="char(1)")]
        [DefaultValue("T")]
        public string status { get; set; }

    }
}
