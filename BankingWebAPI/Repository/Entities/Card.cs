using BankingWebAPI.Repository.Entities;

namespace BankingWebAPI.Repository.Entities
{
    public class Card

    {
        public int CardId { get; set; }
        public int DispositionId { get; set; }
        public string Type { get; set; } = null!;
        public DateTime Issued { get; set; }
        public string CCType { get; set; } = null!;
        public string CCNumber { get; set; } = null!;
        public string CVV2 { get; set; } = null!;   // NEW → exists in DB
        public int ExpM { get; set; }
        public int ExpY { get; set; }

        // Relationships
        public Disposition Disposition { get; set; } = null!;        
    }
}
    
