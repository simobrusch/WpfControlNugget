using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WpfControlNugget.Model
{
    [Table("Customer")]
    public class CustomerModel : ModelBase<CustomerModel>
    {
        [Column("customer_id"), PrimaryKey, NotNull]
        public override int Id { get; set; }
        [Column("firstname")]
        public string FirstName { get; set; }
        [Column("lastname")]
        public string LastName { get; set; }
        [Column("addressnumber")]
        public string AddressNumber { get; set; }
        [Column("kundenkonto_fk")]
        public int CustomerBankAccountId { get; set; }
        [Column("tel")]
        public string TelephoneNumber { get; set; }
        [Column("eMail")]
        public string EmailAddress { get; set; }
        [Column("url")]
        public string Url { get; set; }
        [Column("password")]
        public string Password { get; set; }
        public CountryModel CustomerCountry { get; set; }



        public CustomerModel()
        {
            this.CustomerCountry = new CountryModel("Switzerland", @"^(\+41|0041|0){1}(\(0\))?[0-9]{9}$");
        }

        public CustomerModel(int id, string firstName, string lastName, string addressNumber, int customerBankAccountId, string telephoneNumber, string emailAddress, string url, string password )
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.AddressNumber = addressNumber;
            this.CustomerBankAccountId = customerBankAccountId;
            this.TelephoneNumber = telephoneNumber;
            this.EmailAddress = emailAddress;
            this.Url = url;
            this.Password = password;
        }

        public bool Equals(CustomerModel secondCustomerModel)
        {
            if (Object.ReferenceEquals(null, secondCustomerModel)) return false;
            if (Object.ReferenceEquals(this, secondCustomerModel)) return true;

            return String.Equals(FirstName, secondCustomerModel.FirstName) && String.Equals(LastName, secondCustomerModel.LastName) && String.Equals(AddressNumber, secondCustomerModel.AddressNumber);
        }
        public override bool Equals(object value)
        {
            return Equals(value as CustomerModel);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, FirstName) ? FirstName.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, LastName) ? LastName.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, AddressNumber) ? AddressNumber.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
