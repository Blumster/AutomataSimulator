using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata.Interface
{
    public interface IAmbiguityResolver
    {
        void Resolve(ISimulation simulation);
    }
}
