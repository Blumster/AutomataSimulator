using System;
using System.Collections.Generic;

namespace Automata.Interface
{
    using Enum;

    public interface ISimulation
    {
        #region Properties
        IAutomata Automata { get; }
        IAmbiguityResolver Resolver { get; set; }
        SimulationStepMethod StepMethod { get; }
        bool IsPaused { get; set; }
        int StepDelaySeconds { get; }
        IState CurrentState { get; }
        object[] Input { get; }
        object CurrentInputSymbol { get; }
        int CurrentInputIndex { get; }
        int RemainingInputLength { get; }
        bool IsFinished { get; }
        bool IsInAcceptState { get; }
        bool IsInAmbiguousState { get; }
        #endregion

        #region Events
        event Action OnStep;
        #endregion

        #region Methods
        SimulationStepResult CanStep();
        SimulationStepResult Step();
        SimulationStepResult SpecificStep(IStateTransition transition);
        SimulationStepResult DoAllSteps();
        IEnumerable<IStateTransition> GetApplicableTransitions();
        #endregion
    }
}
