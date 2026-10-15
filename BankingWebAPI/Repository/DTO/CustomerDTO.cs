namespace BankingWebAPI.Repository.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; } // Add this
        public string Givenname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Emailaddress { get; set; } = null!;
        public string? Streetaddress { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
