using System;

namespace Automata.Finite
{
    using System.Collections.Generic;
    using Interface;

    public class DeterministicFiniteAutomata : FiniteAutomata
    {
        public DeterministicFiniteAutomata(IAlphabet alphabet)
            : base(alphabet)
        {

        }

        public DeterministicFiniteAutomata(IAlphabet alphabet, IEnumerable<IState> states, IState startState, IEnumerable<IStateTransition> transitions, IEnumerable<IState> finalStates)
            : base(alphabet, states, startState, transitions, finalStates)
        {
        }

        public override bool CanAddTransition(IStateTransition transition)
        {
            throw new NotImplementedException();
        }
    }
}
