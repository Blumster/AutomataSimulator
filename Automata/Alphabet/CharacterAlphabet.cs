using System;
using System.Collections.Generic;
using System.Text;

namespace Automata.Alphabet
{
    using Interface;

    public class CharacterAlphabet : IAlphabet
    {
        public HashSet<char> Symbols { get; } = new HashSet<char>();

        public CharacterAlphabet(IEnumerable<char> symbols)
        {
            if (symbols == null)
                throw new ArgumentNullException(nameof(symbols), "The alphabet's input symbols can't be null!");

            Symbols.UnionWith(symbols);
        }

        public bool ContainsSymbol(object symbol)
        {
            if (symbol is char temp)
                return Symbols.Contains(temp);
            
            try
            {
                return Symbols.Contains(Convert.ToChar(symbol));
            }
            catch
            {
                return false;
            }
        }

        public string ConstructLabel(object[] symbols)
        {
            var sb = new StringBuilder();

            foreach (var symbol in symbols)
            {
                if (!ContainsSymbol(symbol))
                    throw new ArgumentOutOfRangeException(nameof(symbols), "The symbol array can not contain symbols that are not in the alphabet!");

                if (sb.Length == 0)
                    sb.Append(symbol);
                else
                    sb.Append($", {symbol}");
            }

            return sb.ToString();
        }
    }
}
