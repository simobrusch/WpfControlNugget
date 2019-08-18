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
    public class CustomerRepository : RepositoryBase<customer>
    {
        public CustomerRepository() : base()
        {

        }

        public override void Delete(customer entity)
        {
            var dataCtx = new InventarisierungsloesungEntities();

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

        public override void Update(customer entity)
        {
            var dataCtx = new InventarisierungsloesungEntities();

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
