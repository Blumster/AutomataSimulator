namespace Automata.Enum
{
    public enum SimulationStepResult
    {
        Success = 0,
        NoTransition = 1,
        Ambiguous = 2,
        NoMoreInputSymbols = 3,
        Invalid = 10
    }
}
