using System;

namespace Automata.Finite
{
    using System.Collections.Generic;
    using Interface;

    public class NondeterministicFiniteAutomata : FiniteAutomata
    {
        public NondeterministicFiniteAutomata(IAlphabet alphabet)
            : base(alphabet)
        {

        }

        public NondeterministicFiniteAutomata(IAlphabet alphabet, IEnumerable<IState> states, IState startState, IEnumerable<IStateTransition> transitions, IEnumerable<IState> finalStates)
            : base(alphabet, states, startState, transitions, finalStates)
        {
        }

        public override bool CanAddTransition(IStateTransition transition)
        {
            return true;
        }
    }
}
