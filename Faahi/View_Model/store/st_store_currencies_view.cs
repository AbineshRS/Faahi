using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.View_Model.store
{
    public class st_store_currencies_view
    {
        
        public Guid? store_currency_id { get; set; }

      
        public Guid? store_id { get; set; }

       
        public string? currency_code { get; set; } 

        public string? is_default { get; set; }

    }
}
