using System.ComponentModel.DataAnnotations.Schema;

namespace BankingWebAPI.Repository.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; } = null!;
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }

        // EF property name = AccountTypeId (FK),
        // DB column name = AccountTypesId (map with [Column])
        [Column("AccountTypesId")]
        public int? AccountTypeId { get; set; }
        public AccountType? AccountType { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
