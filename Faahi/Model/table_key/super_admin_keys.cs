using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.table_key
{
    public class super_admin_keys
    {
        [Key]
        [Column(TypeName = "varchar(255)")]
        public string name { get; set; } = string.Empty;


        [Column(TypeName = "int")]
        public Int32 next_key { get; set; }

        [Column(TypeName = "varchar(16)")]
        public string? site_code { get; set; } = null;
    }
}
