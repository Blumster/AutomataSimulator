using System;
using System.Collections.Generic;

namespace Automata.Alphabet
{
    using Interface;

    public class SymbolAlphabet : IAlphabet
    {
        public HashSet<char> Symbols { get; } = new HashSet<char>();

        public SymbolAlphabet(IEnumerable<char> symbols)
        {
            if (symbols == null)
                throw new ArgumentNullException(nameof(symbols), "The alphabet's input symbols can't be null!");

            Symbols.UnionWith(symbols);
        }
        public bool IsCharacterInside(char symbol)
        {
            return Symbols.Contains(symbol);
        }

        public static SymbolAlphabet Create(IEnumerable<char> symbols)
        {
            return new SymbolAlphabet(symbols);
        }
    }
}
