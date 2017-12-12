namespace Automata.Enum
{
    public enum SimulationStepResult
    {
        Success             = 0,
        NoTransition        = 1,
        Ambiguous           = 2,
        AmbiguousNoResolver = 3,
        NoMoreInputSymbols  = 4,
        Finished            = 5,
        Invalid             = 6,
        Timeout             = 7
    }
}
