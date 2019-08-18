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
    public class LocationRepository : RepositoryBase<LocationModel>
    {
        public LocationRepository() : base()
        {

        }

        public override void Delete(LocationModel entity)
        {
            using (var dataCtx = new InventarisierungsloesungEntitiesNew())
            {
                try
                {
                    var pkValue = dataCtx.locations.Find(entity.Id);
                    dataCtx.locations.Remove(pkValue);
                    dataCtx.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }
        public override void Update(LocationModel entity)
        {
            using (var dataCtx = new InventarisierungsloesungEntitiesNew())
            {
                try
                {
                    var pkValue = dataCtx.locations.Find(entity.Id);
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
