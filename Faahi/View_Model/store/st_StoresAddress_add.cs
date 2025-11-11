namespace Faahi.View_Model.store
{
    public class st_StoresAddress_add
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
    }
}
