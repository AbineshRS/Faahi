using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Faahi.Model.table_key
{
    public class am_table_next_key
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid next_key_id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string name { get; set; } = string.Empty;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? business_id { get; set; }

        [Column(TypeName = "int")]
        public Int32 next_key { get; set; }

        [Column(TypeName = "varchar(16)")]
        public string? site_code { get; set; } = null;
    }
}
