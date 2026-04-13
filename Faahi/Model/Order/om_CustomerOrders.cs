using Faahi.Model.am_users;
using Faahi.Model.am_vcos;
using Faahi.Model.Shared_tables;
using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Order
{
    [Index(nameof(customer_order_id),IsUnique =true,Name = "IX_om_CustomerOrders_customer_order_id")]
    [Index(nameof(business_id),nameof(store_id),nameof(order_no),Name = "UX_om_CustomerOrders_business_store_order_no")]
    public class om_CustomerOrders
    {
        [Key]
        [Column(TypeName ="uniqueidentifier")]
        public Guid customer_order_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }
        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        public Faahi.Model.co_business.co_business? co_business { get; set; }


        [Column(TypeName = "uniqueidentifier")]
        public Guid store_id { get; set; }
        [ForeignKey(nameof(store_id))]
        [JsonIgnore]
        public st_stores? st_Stores { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? source_id { get; set; } = null;
        [ForeignKey(nameof(source_id))]
        [JsonIgnore]
        public om_OrderSources? om_OrderSources { get; set; } = null;


        [Column(TypeName = "uniqueidentifier")]
        public Guid? customer_id { get; set; } = null;
        [ForeignKey(nameof(customer_id))]
        [JsonIgnore]
        public ar_Customers? ar_Customers { get; set; }=null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? party_id { get; set; } = null;
        [ForeignKey(nameof(party_id))]
        [JsonIgnore]
        public st_Parties? st_Parties { get; set; }=null;

        [Column(TypeName ="bigint")]
        public int? order_no { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? order_reference_no { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime order_date { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [DefaultValue("COD")]
        public string expected_payment_method { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [DefaultValue("UNPAID")]
        public string payment_status { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [DefaultValue("NEW")]
        public string order_status { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [DefaultValue("PENDING")]
        public string fulfillment_status { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [DefaultValue("PENDING")]
        public string delivery_status { get; set; }

        [Column(TypeName ="navarchar(15)")]
        public string currency_code { get; set; }

        [Column(TypeName ="decimal(18,4)")]
        public Decimal exchange_rate { get; set; }

        [Column(TypeName ="decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal sub_total { get; set; }

        [Column(TypeName ="decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal discount_amount { get; set; }

        [Column(TypeName ="decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal tax_amount { get; set; }

        [Column(TypeName ="decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal delivery_charge { get; set; }

        [Column(TypeName ="decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal other_charges { get; set; }

        [Column(TypeName ="decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal grand_total { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? delivery_contact_name { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? delivery_contact_no { get; set; } = null;

        [Column(TypeName = "nvarchar(255)")]
        public string? delivery_address1 { get; set; } = null;

        [Column(TypeName = "nvarchar(255)")]
        public string? delivery_address2 { get; set; } = null;

        [Column(TypeName = "nvarchar(100)")]
        public string? delivery_area { get; set; } = null;

        [Column(TypeName = "nvarchar(100)")]
        public string? delivery_city { get; set; } = null;

        [Column(TypeName = "nvarchar(20)")]
        public string? delivery_postal_code { get; set; }=null;

        [Column(TypeName ="decimal(18,4)")]
        public Decimal? delivery_latitude { get; set; }=null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? delivery_longitude { get; set; }=null;

        [Column(TypeName ="nvarchar(500)")]
        public string? notes { get; set; }=null;

        [Column(TypeName ="nvarchar(500)")]
        public string? internal_notes { get; set; }=null;

        [Column(TypeName = "datetime")]
        public DateTime? confirmed_at { get; set; }=null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? confirmed_by { get; set; }=null;

        [Column(TypeName = "datetime")]
        public DateTime? cancelled_at { get; set; }=null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? cancelled_by { get; set; }=null;

        [Column(TypeName ="nvarchar(255)")]
        public string? cancellation_reason { get; set; }=null;

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; }=null;

        [Column(TypeName ="uniqueidentifier")]
        public Guid? created_by { get; set; }=null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? updated_by { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? zone_name { get; set; }

        public ICollection<om_CustomerOrderLines>? om_CustomerOrderLines { get; set; } = null;

    }
}
