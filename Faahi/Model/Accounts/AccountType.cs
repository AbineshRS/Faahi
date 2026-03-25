using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Accounts
{
    [Index(nameof(AccountId), IsUnique = true)]
    [Index(nameof(AccountParentId))]
    public class AccountType
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? AccountId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? AccountName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string AccountNumber { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? AccountParentId { get; set; } = null;

        [ForeignKey(nameof(AccountParentId))]
        public AccountType? Parent { get; set; } = null;
    }
}
