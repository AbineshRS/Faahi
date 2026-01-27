using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.table_key
{
    public class super_abi
    {
        [Key]
        [Column(TypeName = "varchar(255)")]
        public string? description { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public Int32? next_key { get; set; }

        
    }
}
