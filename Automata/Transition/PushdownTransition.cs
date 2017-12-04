using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata.Transition
{
    using State;

    public class PushdownTransition : SimpleTransition
    {
        public PushdownTransition(State source, State target)
            : base(source, target)
        {

        }
    }
}
