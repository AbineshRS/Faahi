
namespace Faahi.View_Model.store
{
    public class st_store_add
    {
        public Guid? store_id { get; set; }


        public Guid? company_id { get; set; }


        public string? store_name { get; set; }

        public string? store_location { get; set; } = null;

        public string? store_type { get; set; } = null;

        public DateTime? created_at { get; set; } = null;


        public string? status { get; set; } = string.Empty;


    }
}
