using System.Collections.Generic;
using System.Xml;

namespace Automata.Interface
{
    public interface IState
    {
        string Id { get; }
        IAutomata Automata { get; }
        bool IsStartState { get; set; }
        bool IsAcceptState { get; set; }

        IEnumerable<IStateTransition> SelfTransitions { get; }
        IEnumerable<IStateTransition> InTransitions { get; }
        IEnumerable<IStateTransition> OutTransitions { get; }

        void WriteToXmlWriter(XmlWriter writer);
        void ReadFromXmlReader(XmlReader reader);
    }
}
