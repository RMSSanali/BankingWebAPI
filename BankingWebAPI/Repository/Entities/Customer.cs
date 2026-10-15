namespace BankingWebAPI.Repository.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Gender { get; set; } = null!;
        public string Givenname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Streetaddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Zipcode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string Telephonecountrycode { get; set; } = null!;
        public string Telephonenumber { get; set; } = null!;
        public string Emailaddress { get; set; } = null!;

        public Customer()
        {

        }

        public Customer(int customerId, string gender, string givenname, string surname, string streetaddress, string city, string zipcode, string country, string countryCode, DateTime birthday, string telephonecountrycode, string telephonenumber, string emailaddress)
        {
            CustomerId = customerId;
            Gender = gender;
            Givenname = givenname;
            Surname = surname;
            Streetaddress = streetaddress;
            City = city;
            Zipcode = zipcode;
            Country = country;
            CountryCode = countryCode;
            Birthday = birthday;
            Telephonecountrycode = telephonecountrycode;
            Telephonenumber = telephonenumber;
            Emailaddress = emailaddress;
        }     
                 
    }
}
