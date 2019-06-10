using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlNugget.Model;

namespace WpfControlNugget.Repository
{
    class LogModelRepository : RepositoryBase<LogModel>
    {
        public LogModelRepository(string connectionString) : base(connectionString)
        {
        }

        public override LogModel GetSingle<P>(P pkValue)
        {
            throw new NotImplementedException();
        }

        public override void Add(LogModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(LogModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(LogModel entity)
        {
            throw new NotImplementedException();
        }

        public override List<LogModel> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new NotImplementedException();
        }

        public override List<LogModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public override string TableName { get; }
    }
}
