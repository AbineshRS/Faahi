using Faahi.Model.am_users;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.am_users
{
    public class am_users_dto
    {
        public string? fullName { get; set; } = string.Empty;

        public string? email { get; set; } = string.Empty;

        public string? phoneNumber { get; set; } = null;

        public string? address_type { get; set; }

        public string? contact_name { get; set; }

        public string? contact_phone { get; set; }

        public string? address_line1 { get; set; }

        public string? address_line2 { get; set; }

        public string? city { get; set; } = null;

        public string? state_region { get; set; } = null;

        public string? postal_code { get; set; } = null;

        public string? country_code { get; set; } = null;
        

    }
}
