using System.Collections.Generic;

namespace Automata.Interface
{
    public interface IAlphabet
    {
        bool ContainsSymbol(object symbol);
        string ConstructSymbolText(bool list = true);
        string ConstructSymbolText(IEnumerable<object> symbols, bool list = true);
    }
}
