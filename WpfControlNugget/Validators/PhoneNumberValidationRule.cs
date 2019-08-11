using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfControlNugget.Model;

namespace WpfControlNugget.Validators
{
    public class PhoneNumberValidationRule : ValidationRule
    {
        public string ErrorMessage { get; set; }
        public CustomerModel Customer;

        public PhoneNumberValidationRule()
        {

        }
        public PhoneNumberValidationRule(CustomerModel customer)
        {
            this.Customer = customer;
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(Customer.CustomerCountry.PhoneNumberRegex);
            Match match = regex.Match(value.ToString());

            if (match == Match.Empty)
            {
                return new ValidationResult(false, ErrorMessage);
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
