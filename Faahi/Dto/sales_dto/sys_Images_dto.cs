using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.sales_dto
{
    public class sys_Images_dto
    {

        public Guid? image_id { get; set; }


        public Guid? source_id { get; set; }


        public Guid? business_id { get; set; }


        public string? source_type { get; set; }


        public string? image_url { get; set; }


        public DateTime? created_at { get; set; }


        public DateTime? updated_at { get; set; }


        public string? status { get; set; }

        public IFormFile? file { get; set; }

    }
}
