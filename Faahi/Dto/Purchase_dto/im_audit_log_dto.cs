using Faahi.Model.co_business;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.Purchase_dto
{
    public class im_audit_log_dto
    {
        public Guid? audit_id { get; set; }

        public Guid? record_id { get; set; }

        public string? action_type { get; set; }

        public string? field_name { get; set; }

        public string? old_value { get; set; }

        public string? new_value { get; set; }

        public string? changedby_name { get; set; }

        public DateTime? changed_at { get; set; }

    }
}
