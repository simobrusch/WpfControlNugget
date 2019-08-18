using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using WpfControlNugget.Model;

namespace WpfControlNugget.Repository
{
    public class LocationRepository : RepositoryBase<location>
    {
        public LocationRepository() : base()
        {

        }

        public override void Delete(location entity)
        {
            using (var dataCtx = new InventarisierungsloesungEntities())
            {
                try
                {
                    var pkValue = dataCtx.locations.Find(entity.location_id);
                    dataCtx.locations.Remove(pkValue);
                    dataCtx.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }
        public override void Update(location entity)
        {
            using (var dataCtx = new InventarisierungsloesungEntities())
            {
                try
                {
                    var pkValue = dataCtx.locations.Find(entity.location_id);
                    dataCtx.locations.Attach(pkValue);
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
