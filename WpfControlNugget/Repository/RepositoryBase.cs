using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WpfControlNugget.Repository
{
    public abstract class RepositoryBase<M> : IRepositoryBase<M>
    {
        protected RepositoryBase(string connectionString)
        {
            // TODO: Load ConnectionString
            this.ConnectionString = "<ConnString>";
        }
        protected string ConnectionString { get; }
        public abstract M GetSingle<P>(P pkValue);

        public abstract void Add(M entity);

        public abstract void Delete(M entity);

        public abstract void Update(M entity);
        public abstract List<M> GetAll(string whereCondition, Dictionary<string, object> parameterValues);

        public abstract List<M> GetAll();

        public IQueryable<M> Query(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new NotImplementedException();
        }
        public long Count(string whereCondition, Dictionary<string, object> parameterValues)
        {
            using (var conn = new MySqlConnection(this.ConnectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = $"select count(*) from {this.TableName}";
                    return (long)cmd.ExecuteScalar();
                }
            }
        }
        public long Count()
        {
            using (var conn = new MySqlConnection(this.ConnectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = $"select count(*) from {this.TableName}";
                    return (long)cmd.ExecuteScalar();
                }
            }
        }
        public abstract string TableName { get; }
    }
}
