using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfControlNugget.Validators
{
    public class StringRangeValidationRule : ValidationRule
    {
        public int MinimumLength { get; set; } = 3;
        public int MaximumLength { get; set; } = 255;

        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value,
            CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            string inputString = (value ?? string.Empty).ToString();
            if (inputString.Length < this.MinimumLength ||
                (this.MaximumLength > 0 &&
                 inputString.Length > this.MaximumLength))
            {
                result = new ValidationResult(false, this.ErrorMessage);
            }
            return result;
        }
    }
}
