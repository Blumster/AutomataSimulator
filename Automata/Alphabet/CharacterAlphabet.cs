using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Automata.Alphabet
{
    using Interface;

    /// <summary>
    /// An alphabet implementation based on regular characters.
    /// </summary>
    public class CharacterAlphabet : IAlphabet
    {
        /// <summary>
        /// 
        /// </summary>
        public HashSet<char> Symbols { get; } = new HashSet<char>();

        /// <summary>
        /// Creates a new alphabet without any symbol.
        /// </summary>
        public CharacterAlphabet()
        {
        }

        /// <summary>
        /// Creates a new alphabet with the set of the given symbols.
        /// </summary>
        /// <param name="symbols"></param>
        public CharacterAlphabet(IEnumerable<char> symbols)
        {
            if (symbols == null)
                throw new ArgumentNullException(nameof(symbols), "The alphabet's input symbols can't be null!");

            Symbols.UnionWith(symbols);
        }

        /// <summary>
        /// Returns the iterateable list of symbols contained in this alphabet.
        /// </summary>
        /// <returns>The symbols in this alphabet.</returns>
        public IEnumerable<object> GetSymbols()
        {
            return Symbols.Select(c => c as object);
        }

        /// <summary>
        /// Checks, if the current alphabet contains a given symbol.
        /// </summary>
        /// <param name="symbol">The symbol to be checked.</param>
        /// <returns>True, if the alphabet contains the symbol.</returns>
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

        /// <summary>
        /// Calculates and returns the textual representation of this alphabet.
        /// </summary>
        /// <param name="list">If true, the output will be comma separated.</param>
        /// <returns>The textual representation of this alphabet.</returns>
        public string ConstructSymbolText(bool list = true)
        {
            return ConstructSymbolText(Symbols.Select(s => s as object), list);
        }

        /// <summary>
        /// Calculates and returns the textual representation of the given symbols.
        /// </summary>
        /// <param name="symbols">The list of symbols.</param>
        /// <param name="list">If true, the output will be comma separated.</param>
        /// <returns>The textual representation of the given symbols.</returns>
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

        /// <summary>
        /// This method is called to attach extra data to the XML element of this entity.
        /// </summary>
        /// <param name="writer">The current XML writer instance.</param>
        public void WriteToXmlWriter(XmlWriter writer)
        {
            writer.WriteAttributeString("Symbols", ConstructSymbolText(false).Replace(" ", ""));
        }

        /// <summary>
        /// This method is called to read extra data from the XML element of this entity.
        /// </summary>
        /// <param name="reader">The current XML reader instance.</param>
        public void ReadFromXmlReader(XmlReader reader)
        {
            Symbols.UnionWith(reader.GetAttribute("Symbols"));
        }
    }
}
