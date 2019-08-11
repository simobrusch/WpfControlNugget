using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfControlNugget.Validators
{
    public class AddressNumberValidationRule : ValidationRule
    {
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// Regex for Address Number Validation.
        /// Beginning with CU following a 5 digit number.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^CU[0-9]{5}$");
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
