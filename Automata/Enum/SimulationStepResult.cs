namespace Automata.Enum
{
    /// <summary>
    /// Defines an enumeration for the result of a simulation step.
    /// </summary>
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
