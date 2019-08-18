using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LinqToDB;
using MySql.Data.MySqlClient;
using WpfControlNugget.Model;

namespace WpfControlNugget.Repository
{
    public abstract class RepositoryBase<TM> : IRepositoryBase<TM> where TM : ModelBase<TM>, new()
    {
        /// <summary>
        /// Name des Datenbankproviders z.B. MySQL, MSSQl, PostgreSQL, etc.
        /// </summary>
        protected string ProviderName = "System.Data.SqlClient";

        protected RepositoryBase()
        {

        }

        public TM GetSingle<TP>(TP pkValue)
        {
            var pkValueRow = new TM();
            using (var dataCtx = new InventarisierungsloesungEntitiesNew())
            {
                try
                {
                    pkValueRow = (from e in dataCtx.Set<TM>() where e.Id.Equals(pkValue) select e).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
                return pkValueRow;
            }
        }

        public void Add(TM entity)
        {
            using (var dataCtx = new InventarisierungsloesungEntitiesNew())
            {
                try
                {
                    dataCtx.Set<TM>().Add(entity);
                    dataCtx.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        public abstract void Delete(TM entity);

        public abstract void Update(TM entity);
        

        public IQueryable<TM> GetAll(Expression<Func<TM, bool>> whereCondition)
        {
            IQueryable<TM> entities = Enumerable.Empty<TM>().AsQueryable();
            var dataCtx = new InventarisierungsloesungEntitiesNew();
            
                try
                {
                    entities = dataCtx.Set<TM>().Where(whereCondition);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            
            return entities;
        }

        public IQueryable<TM> GetAll()
        {
            IQueryable<TM> entities = Enumerable.Empty<TM>().AsQueryable();
            var dataCtx = new InventarisierungsloesungEntitiesNew();
            
                try
                {
                    entities = dataCtx.Set<TM>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            
            return entities;
        }

        public IQueryable<TM> Query(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new NotSupportedException();
        }

        public long Count(Expression<Func<TM, bool>> whereCondition)
        {
            IQueryable<TM> entities = Enumerable.Empty<TM>().AsQueryable();
            using (var dataCtx = new InventarisierungsloesungEntitiesNew())
            {
                try
                {
                    entities = dataCtx.Set<TM>().Where(whereCondition);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
                return entities.Count();
            }
        }

        public long Count()
        {
            IQueryable<TM> entities = Enumerable.Empty<TM>().AsQueryable();
            using (var dataCtx = new InventarisierungsloesungEntitiesNew())
            {
                try
                {
                    entities = dataCtx.Set<TM>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
                return entities.Count();
            }
        }
    }
}
