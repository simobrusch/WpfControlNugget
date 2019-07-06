using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlNugget.Model;

namespace WpfControlNugget.Repository
{
    public class CustomerRepository : RepositoryBase<CustomerModel>
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {

        }
    }
}
