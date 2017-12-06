using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata.Pushdown
{
    using Automata.Interface;
    using Finite;

    public class PushdownAutomata : FiniteAutomata
    {
        public PushdownAutomata(IAlphabet alphabet)
            : base(alphabet)
        {
        }
    }
}
