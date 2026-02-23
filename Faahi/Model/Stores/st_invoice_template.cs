using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Stores
{
    public class st_invoice_template
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? invoices_temp_id { get; set; }

        [Column(TypeName ="nvarchar(30)")]
        public string? invoices_temp_name { get; set; } = null;

        [Column(TypeName ="nvarchar(50)")]
        public string? invoices_temp_description { get;set; }

        [Column(TypeName ="nvarchar(20)")]
        public string? type_name { get; set; }
    }
}
