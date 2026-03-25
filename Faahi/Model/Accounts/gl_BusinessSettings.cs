using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Accounts
{
    public class gl_BusinessSettings
    {
        [Key]
        [Column("business_id")]
        public Guid BusinessId { get; set; }

        [Column("ar_control_account_id")]
        public Guid? ARControlAccountId { get; set; }

        [Column("ap_control_account_id")]
        public Guid? APControlAccountId { get; set; }

        [Column("inventory_account_id")]
        public Guid? InventoryAccountId { get; set; }

        [Column("cogs_account_id")]
        public Guid? CogsAccountId { get; set; }

        [Column("sales_account_id")]
        public Guid? SalesAccountId { get; set; }

        [Column("tax_payable_account_id")]
        public Guid? TaxPayableAccountId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property to BusinessDetails
        [ForeignKey("BusinessId")]
        public virtual Faahi.Model.co_business.co_business co_business { get; set; }
    }
}
