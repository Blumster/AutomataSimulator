namespace Automata.Interface
{
    public interface IAlphabet
    {
        bool ContainsSymbol(object symbol);
        string ConstructLabel(object[] symbols);
    }
}
