using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LinqToDB;
using WpfControlNugget.Model;

namespace WpfControlNugget.Repository
{
    public class CustomerRepository : RepositoryBase<CustomerModel>
    {
        public CustomerRepository() : base()
        {

        }

        public override void Delete(CustomerModel entity)
        {
            using (var dataCtx = new InventarisierungsloesungEntitiesNew())
            {
                try
                {
                    var pkValue = dataCtx.customers.Find(entity.Id);
                    dataCtx.customers.Remove(pkValue);
                    dataCtx.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        public override void Update(CustomerModel entity)
        {
            using (var dataCtx = new InventarisierungsloesungEntitiesNew())
            {
                try
                {
                    var pkValue = dataCtx.customers.Find(entity.Id);
                    dataCtx.customers.Attach(pkValue);
                    dataCtx.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }
    }
}
