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
    public abstract class RepositoryBaseMySql<TM> : IRepositoryBase<TM> where TM : ModelBase, new()
    {
        /// <summary>
        /// Name des Datenbankproviders z.B. MySQL, MSSQl, PostgreSQL, etc.
        /// </summary>
        protected string ProviderName { get; }
        /// <summary>
        /// String für Database Verbindung im folgenden Format:
        /// "Server=localhost;Database=;Uid=root;Pwd=;"
        /// </summary>
        protected string ConnectionString { get; }
        protected RepositoryBaseMySql(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.ProviderName = "MySql";
        }

        public TM GetSingle<TP>(TP pkValue)
        {
            var pkValueRow = new TM();
            using (var dataCtx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    pkValueRow = (from e in dataCtx.GetTable<TM>() where e.Id.Equals(pkValue) select e).FirstOrDefault();
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
            using (var dataCtx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    dataCtx.Insert(entity);
                    dataCtx.BeginTransaction();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        public void Delete(TM entity)
        {
            using (var dataCtx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    var pkValueRow = (from e in dataCtx.GetTable<TM>() where e.Id.Equals(entity.Id) select e).FirstOrDefault();
                    if (pkValueRow != null)
                    {
                        dataCtx.Delete(pkValueRow);
                    }
                    dataCtx.BeginTransaction();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        public void Update(TM entity)
        {
            using (var dataCtx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    var pkValueRow = (from e in dataCtx.GetTable<TM>() where e.Id.Equals(entity.Id) select e).FirstOrDefault();
                    dataCtx.Update(entity);
                    dataCtx.BeginTransaction();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        public IQueryable<TM> GetAll(Expression<Func<TM, bool>> whereCondition)
        {
            IQueryable<TM> entities = Enumerable.Empty<TM>().AsQueryable();
            using (var dataCtx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    entities = dataCtx.GetTable<TM>().Where(whereCondition);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
            return entities;
        }

        public IQueryable<TM> GetAll()
        {
            IQueryable<TM> entities = Enumerable.Empty<TM>().AsQueryable();
            using (var dataCtx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    entities = dataCtx.GetTable<TM>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
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
            using (var dataCtx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    entities = dataCtx.GetTable<TM>().Where(whereCondition);
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
            using (var dataCtx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    entities = dataCtx.GetTable<TM>();
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
