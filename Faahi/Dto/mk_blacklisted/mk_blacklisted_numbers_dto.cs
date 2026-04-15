using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.mk_blacklisted
{
    public class mk_blacklisted_numbers_dto
    {

        public Guid? blacklist_id { get; set; }

        public Guid? business_id { get; set; }

        public string? phone_number { get; set; }

        [DefaultValue("T")]
        public string is_active { get; set; }
    }
}
