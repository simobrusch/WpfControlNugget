using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfControlNugget.Validators
{
    public class IntRangeValidationRule : ValidationRule
    {
        public int MinimumLength { get; set; }
        public int MaximumLength { get; set; }
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value,
            CultureInfo cultureInfo)
        {
            int parameter = 0;

            try
            {
                if (((string)value).Length > 0)
                {
                    parameter = Int32.Parse((String)value);
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or "
                                                   + e.Message);
            }

            if ((parameter < this.MinimumLength) || (parameter > this.MaximumLength))
            {
                return new ValidationResult(false,
                    "Input must be a number between "
                    + this.MinimumLength + " - " + this.MaximumLength + ".");
            }
            return new ValidationResult(true, null);
        }
    }
}
