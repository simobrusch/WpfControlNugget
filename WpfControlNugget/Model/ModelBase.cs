using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuplicateCheckerLib;

namespace WpfControlNugget.Model
{
    public abstract class ModelBase<TM> : IEntity
    {
        public abstract int Id { get; set; }
    }
}
