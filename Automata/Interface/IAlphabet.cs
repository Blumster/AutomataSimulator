using System.Collections.Generic;
using System.Xml;

namespace Automata.Interface
{
    public interface IAlphabet
    {
        bool ContainsSymbol(object symbol);
        string ConstructSymbolText(bool list = true);
        string ConstructSymbolText(IEnumerable<object> symbols, bool list = true);

        void WriteToXmlWriter(XmlWriter writer);
        void ReadFromXmlReader(XmlReader reader);
    }
}
