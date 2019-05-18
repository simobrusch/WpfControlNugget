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
        private int Min = 1;
        private int Max = 3;

        public int MinimumLength
        {
            get { return Min; }
            set { Min = value; }
        }

        public int MaximumLength
        {
            get { return Max; }
            set { Max = value; }
        }

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

            if ((parameter < this.Min) || (parameter > this.Max))
            {
                return new ValidationResult(false,
                    "Severity must be a number between "
                    + this.Min + " - " + this.Max + ".");
            }
            return new ValidationResult(true, null);
        }
    }
}
