using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_site
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? site_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [ForeignKey("company_address_id")]
        [Display(Name = "co_address")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_address_id { get; set; }

        [ForeignKey("avl_countries_id")]
        [Display(Name = "co_avl_countries")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? avl_countries_id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? site_name { get; set; } = null;

        [Column(TypeName = "varchar(100)")]
        public string? tin_number { get; set; }= null;

        [Column(TypeName = "varchar(100)")]
        public string? edit_user_id { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; } = string.Empty;

        public ICollection<im_item_site> im_item_site { get; set; }
    }
}
