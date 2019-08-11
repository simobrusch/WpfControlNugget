using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlNugget.Model
{
    public class CountryModel
    {
        public string Name { get; set; }

        public CountryModel(string name)
        {
            this.Name = name;
        }
        
        public override string ToString()
        {
            return Name;
        }
    }
}
