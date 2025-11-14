using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.View_Model.store
{
    public class st_StoresAddres_view
    {

        public Guid store_address_id { get; set; }

        public Guid? store_id { get; set; }

        public string? address_type { get; set; }

        public string? line1 { get; set; }

        public string? line2 { get; set; } = null;

        public string? city { get; set; } = null;

        public string? region { get; set; } = null;

        public string? postal_code { get; set; } = null;

        public string? country { get; set; } = null;

        public DateTime? valid_from { get; set; }

        public DateTime? valid_to { get; set; } = null;

        public DateTime? created_at { get; set; }


        public string? is_current { get; set; } = null;

        public ICollection<st_store_currencies_view> st_store_currencies { get; set; }
        //public ICollection<st_StoreCategories_view>? st_StoreCategories { get; set; }
    }
}
