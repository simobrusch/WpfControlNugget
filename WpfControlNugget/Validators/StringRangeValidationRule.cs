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
        private int _minimumLength = 3;
        private int _maximumLength = 255;

        public int MinimumLength
        {
            get { return _minimumLength; }
            set { _minimumLength = value; }
        }

        public int MaximumLength
        {
            get { return _maximumLength; }
            set { _maximumLength = value; }
        }

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
