using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuplicateCheckerLib;

namespace WpfControlNugget.Model
{
    public abstract class ModelBase<M> : IEntity
    {
        public int Id { get; set; }
    }
}
