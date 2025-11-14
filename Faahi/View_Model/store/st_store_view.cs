using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.View_Model.store
{
    public class st_store_view
    {

        public Guid? store_id { get; set; }


        public Guid? company_id { get; set; }


        public string? store_name { get; set; }

        public string? store_location { get; set; } = null;


        public string? store_type { get; set; } = null;


        public DateTime? created_at { get; set; } = null;


        public string? status { get; set; } = string.Empty;

        public TimeOnly? default_close_time { get; set; }

        public string? phone1 { get; set; } = null;

        public string? phone2 { get; set; } = null;

        public string? email { get; set; } = null;

        public string? tax_identification_number { get; set; } = null;

        public string? default_invoice_init { get; set; } = null;

        
        public string? default_quote_init { get; set; } = null;

      
        public string? default_invoice_template { get; set; } = null;

    
        public string? default_receipt_template { get; set; } = null;

     
        public DateOnly? last_transaction_date { get; set; } = null;
        
        public string? default_currency { get; set; } = null;


    
        public Decimal? service_charge { get; set; } = null;

      
        public string? tax_inclusive_price { get; set; } = null;

  
        public string? tax_activity_no { get; set; } = null;

       
        public string? tax_payer_name { get; set; } = null;

      
        public string? low_stock_alert_email { get; set; } = null;

       
        public Decimal? plastic_bag_tax_amount { get; set; } = null;

   
        public string? message_on_receipt { get; set; } = null;

     
        public string? message_on_invoice { get; set; } = null;

        public ICollection<st_StoresAddres_view>? st_StoresAddres { get; set; }

        public ICollection<st_StoreCategories_view>? st_StoreCategories { get; set; }

    }
}
