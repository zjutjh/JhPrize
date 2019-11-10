using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JhPrize.Core
{
    public interface IUpdatable<T>
    {
        void Update(T other);
    }
}
