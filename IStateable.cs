using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossSensors
{
    internal interface IStateable<T>
    {
        void SetState(T state);
    }
}
