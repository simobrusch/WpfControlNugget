﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlNugget.Model;

namespace WpfControlNugget.Repository
{
    public class LocationRepository : RepositoryBase<Location>
    {
        public override string TableName => "Location";
        public LocationRepository(string connectionString) : base(connectionString)
        {
        }
        public override void Add(Location entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Location entity)
        {
            throw new NotImplementedException();
        }

        public override List<Location> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new NotImplementedException();
        }

        public override List<Location> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Location GetSingle<P>(P pkValue)
        {
            throw new NotImplementedException();
        }

        public override void Update(Location entity)
        {
            throw new NotImplementedException();
        }
    }
}
