using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlNugget.ViewModel.DuplicateChecker
{
    public class DuplicateChecker
    {
        public IEnumerable<IEntity> FindDuplicates(IEnumerable<IEntity> list)
        {
            var hashSet = new HashSet<IEntity>();
            var ret = new List<IEntity>();
            foreach (var item in list)
            {
                if (!hashSet.Add(item))
                {
                    ret.Add(item);
                }
            }

            return ret;
        }
    }
}
