using System.Xml;

namespace Automata.Interface
{
    public interface IStateTransition
    {
        IAutomata Automata { get; set; }
        IState SourceState { get; }
        IState TargetState { get; }
        string Label { get; }
        object[] Symbols { get; }

        bool HandlesSymbol(object symbol);

        void WriteToXmlWriter(XmlWriter writer);
        void ReadFromXmlReader(XmlReader reader);
    }
}
