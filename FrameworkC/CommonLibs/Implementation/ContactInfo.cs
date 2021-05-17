using FrameworkC.CommonLibs.Utils;

namespace FrameworkC.CommonLibs.Implementation
{
    public class ContactInfo
    {
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string CompanyName { get => companyName; set => companyName = value; }
        public string VatId { get => vatId; set => vatId = value; }

        public Address Address { get => address; set => address = value; }


        private string firstName;
        private string lastName;
        private string email;
        private string phoneNumber;
        private string companyName;
        private Address address;
        private string vatId;


        public ContactInfo() {
            var person = new Bogus.Person();

            this.firstName = person.FirstName;
            this.lastName = person.LastName;
            this.email = person.Email;
            this.companyName = person.Company.Name;
            this.phoneNumber = person.Phone;
            this.Address = new Address(person);
        }
    }
}
