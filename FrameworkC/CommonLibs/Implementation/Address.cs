using Bogus;

namespace FrameworkC.CommonLibs.Implementation
{
    public class Address
    {

        public string Country { get => country; set => country = value; }
        public string City { get => city; set => city = value; }
        public string ZipCode { get => zipCode; set => zipCode = value; }
        public string State { get => state; set => state = value; }
        public string Street { get => street; set => street = value; }

        private string country;
        private string city;
        private string zipCode;
        private string state;
        private string street;


        public Address(Person person) {
            this.country = "United States";
            this.city = person.Address.City;
            this.zipCode = person.Address.ZipCode;
            this.state = person.Address.State;
            this.street = person.Address.Street;
        }
    }
}
