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

        /// <summary>
        /// Regex for Phone number Validation
        /// Not working for Germany and Liechtenstein.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regexCh = new Regex(@"^(\+41|0041)([\(0\)]?[0-9()]{2})([0-9]{7})(-?[0-9]{2})?|(^[0-9()]{3})\/?([0-9]{7})(-?[0-9]{2})?");
            Match matchCh = regexCh.Match(value.ToString());

            Regex regexLi = new Regex(@"^(\+423|00423)?\s?[0-9]{3}\s?[0-9]{2}\s?[0-9]{2}$");
            Match matchLi = regexCh.Match(value.ToString());

            Regex regexDe = new Regex(@"[0-9]*\/*(\+49)*[ ]*(\([0-9]+\))*([ ]*(-|–)*[ ]*[0-9]+)*");
            Match matchDe = regexCh.Match(value.ToString());

            if (matchCh == Match.Empty)
            {
                return new ValidationResult(false, ErrorMessage);
            }
            else if (matchLi == Match.Empty)
            {
                return new ValidationResult(false, ErrorMessage);
            }
            else if (matchDe == Match.Empty)
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

