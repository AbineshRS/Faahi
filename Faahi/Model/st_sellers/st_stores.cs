﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.st_sellers
{
    public class st_stores
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }


        [Column(TypeName = "varchar(255)")]
        public string? store_name { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? store_location { get; set; } = null;

        [DefaultValue("online")]
        [Column(TypeName = "varchar(100)")]
        public string? store_type { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; } = null;

       
        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; } = string.Empty;

    }
}
