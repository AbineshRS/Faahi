using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.OneMall
{
    [Table("mk_customer_profiles", Schema = "dbo")]
    [Index(nameof(user_id), IsUnique = true, Name = "UQ_mk_customer_profiles_user_id")]
    public class mk_customer_profiles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid customer_profile_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid user_id { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? customer_code { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_of_birth { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? preferred_language { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string? notes { get; set; }

        [Column(TypeName = "char(1)")]
        public string status { get; set; } = "A";

        [Column(TypeName = "datetime")]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; }

        [ForeignKey(nameof(user_id))]
        [JsonIgnore]
        public Faahi.Model.am_users? am_users { get; set; }
    }
}