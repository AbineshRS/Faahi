using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.View_Model.store
{
    public class im_ProductCategories_view
    {

        public Guid? category_id { get; set; }

        public string? category_name { get; set; } = null;

        public Guid? parent_id { get; set; } = null;

        public string? image_url { get; set; } = null;

        public string? edit_user_id { get; set; } = null;

        public DateTime? edit_date_time { get; set; } = null;

       
        public string? is_active { get; set; } = null;

        public string? Level { get; set; } = null;
    }
}
