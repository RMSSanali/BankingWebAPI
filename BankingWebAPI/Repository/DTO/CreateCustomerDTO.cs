namespace BankingWebAPI.Repository.DTO
{
    public class CreateCustomerDTO
    {
        public string Givenname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Emailaddress { get; set; } = null!;
        public string Streetaddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
