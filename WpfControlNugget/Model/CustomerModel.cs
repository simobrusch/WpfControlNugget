using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WpfControlNugget.Model
{

    public class CustomerModel : ModelBase<CustomerModel>
    {
        public override int Id { get; set; }

        public long customer_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string customernumber { get; set; }
        public long kundenkonto_fk { get; set; }
        public string tel { get; set; }
        public string eMail { get; set; }
        public string url { get; set; }
        public string password { get; set; }

        public CountryModel CustomerCountry { get; set; }



        public CustomerModel()
        {
            this.CustomerCountry = new CountryModel("Switzerland");
        }

        public bool Equals(CustomerModel secondCustomerModel)
        {
            if (Object.ReferenceEquals(null, secondCustomerModel)) return false;
            if (Object.ReferenceEquals(this, secondCustomerModel)) return true;

            return String.Equals(firstname, secondCustomerModel.firstname) && String.Equals(lastname, secondCustomerModel.lastname) && String.Equals(customernumber, secondCustomerModel.customernumber);
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
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, firstname) ? firstname.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, lastname) ? lastname.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, customernumber) ? customernumber.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
