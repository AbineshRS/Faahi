using Faahi.Model.st_sellers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.View_Model.store
{
    public class st_StoreCategories_view
    {
      
        public Guid? store_category_id { get; set; }

 
        public Guid? store_id { get; set; }


     
        public Guid? category_id { get; set; }

     
        public string? is_selected { get; set; } = null;

     public ICollection<im_ProductCategories_view>? im_ProductCategories_view { get; set; }
    }
}
