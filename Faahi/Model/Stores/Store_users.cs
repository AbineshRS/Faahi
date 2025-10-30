using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Stores
{
    public class Store_users
    {
        public Guid? user_id { get; set; }

       
        public Guid? company_id { get; set; }

        public string? Full_name { get; set; }

        public string? email { get; set; }

        public string? phone { get; set; } = null;

        public string? password { get; set; } = null;

        public string? account_type { get; set; } = null;

        public DateTime? registration_date { get; set; } = null;

        
        public string? status { get; set; } = null;

      
        public Guid? store_access_id { get; set; }

        


     
        public Guid? store_id { get; set; }

     
        public Guid? role_id { get; set; }

        public DateTime? created_at { get; set; } = null;

       


    }
}
