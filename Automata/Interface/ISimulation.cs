using System;
using System.Collections.Generic;

namespace Automata.Interface
{
    using Enum;

    public interface ISimpleSimulation
    {
        #region Properties
        IAutomata Automata { get; }
        SimulationStepMethod StepMethod { get; }
        bool IsPaused { get; set; }
        int StepDelaySeconds { get; }
        IState CurrentState { get; }
        object[] Input { get; }
        object NextInputSymbol { get; }
        int RemainingInputLength { get; }
        #endregion

        #region Events
        event Action OnStep;
        #endregion

        #region Methods
        SimulationStepResult CanStep();
        SimulationStepResult Step();
        SimulationStepResult SpecificStep(IStateTransition transition);
        void UpdateStepMethod(SimulationStepMethod type);
        IEnumerable<IStateTransition> GetApplicableTransitions();
        #endregion
    }
}
