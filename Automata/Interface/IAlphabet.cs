using System.Collections.Generic;

namespace Automata.Interface
{
    /// <summary>
    /// Defines an interface for an alphabet implementation.
    /// </summary>
    public interface IAlphabet : IXmlObject
    {
        /// <summary>
        /// Returns the iterateable list of symbols contained in this alphabet.
        /// </summary>
        /// <returns>The symbols in this alphabet.</returns>
        IEnumerable<object> GetSymbols();

        /// <summary>
        /// Checks, if the current alphabet contains a given symbol.
        /// </summary>
        /// <param name="symbol">The symbol to be checked.</param>
        /// <returns>True, if the alphabet contains the symbol.</returns>
        bool ContainsSymbol(object symbol);

        /// <summary>
        /// Calculates and returns the textual representation of this alphabet.
        /// </summary>
        /// <param name="list">If true, the output will be comma separated.</param>
        /// <returns>The textual representation of this alphabet.</returns>
        string ConstructSymbolText(bool asList = true);

        /// <summary>
        /// Calculates and returns the textual representation of the given symbols.
        /// </summary>
        /// <param name="symbols">The list of symbols.</param>
        /// <param name="list">If true, the output will be comma separated.</param>
        /// <returns>The textual representation of the given symbols.</returns>
        string ConstructSymbolText(IEnumerable<object> symbols, bool list = true);
    }
}
