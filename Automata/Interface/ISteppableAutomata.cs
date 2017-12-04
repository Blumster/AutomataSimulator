namespace Automata.Interface
{
    public interface ISteppableSimulation
    {
        IAutomata Automata { get; }
        bool CanStep(char symbol);
        void Step(char symbol);
    }
}
