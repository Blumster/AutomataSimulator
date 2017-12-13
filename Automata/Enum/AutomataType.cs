namespace Automata.Enum
{
    /// <summary>
    /// Defines and enumeration for the type of the automata, based on it's determinism.
    /// </summary>
    public enum AutomataType : byte
    {
        Deterministic          = 0,
        PartiallyDeterminsitic = 1,
        Nondeterministic       = 2
    }
}
