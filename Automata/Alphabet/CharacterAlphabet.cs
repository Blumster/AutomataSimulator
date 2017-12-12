using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Automata.Alphabet
{
    using Interface;

    public class CharacterAlphabet : IAlphabet
    {
        public HashSet<char> Symbols { get; } = new HashSet<char>();

        public CharacterAlphabet()
        {

        }

        public CharacterAlphabet(IEnumerable<char> symbols)
        {
            if (symbols == null)
                throw new ArgumentNullException(nameof(symbols), "The alphabet's input symbols can't be null!");

            Symbols.UnionWith(symbols);
        }

        public IEnumerable<object> GetSymbols()
        {
            return Symbols.Select(c => c as object);
        }

        public bool ContainsSymbol(object symbol)
        {
            if (symbol == null)
                return false;

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

        public string ConstructSymbolText(bool list = true)
        {
            return ConstructSymbolText(Symbols.Select(s => s as object), list);
        }

        public string ConstructSymbolText(IEnumerable<object> symbols, bool list = true)
        {
            var sb = new StringBuilder();

            foreach (var symbol in symbols)
            {
                if (!ContainsSymbol(symbol))
                    throw new ArgumentOutOfRangeException(nameof(symbols), "The symbol array can not contain symbols that are not in the alphabet!");

                if (sb.Length == 0)
                    sb.Append(symbol);
                else if (list)
                    sb.Append($", {symbol}");
                else
                    sb.Append($" {symbol}");
            }

            return sb.ToString();
        }

        public void WriteToXmlWriter(XmlWriter writer)
        {
            writer.WriteAttributeString("Symbols", ConstructSymbolText(false).Replace(" ", ""));
        }

        public void ReadFromXmlReader(XmlReader reader)
        {
            Symbols.UnionWith(reader.GetAttribute("Symbols"));
        }
    }
}
