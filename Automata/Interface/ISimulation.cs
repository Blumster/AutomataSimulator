using System;
using System.Collections.Generic;

namespace Automata.Interface
{
    using Enum;

    /// <summary>
    /// Defines an intarface for a simulation implementation.
    /// </summary>
    public interface ISimulation
    {
        #region Properties
        /// <summary>
        /// The automata instance being simulated.
        /// </summary>
        IAutomata Automata { get; }

        /// <summary>
        /// The ambiguity resolver instance.
        /// </summary>
        IAmbiguityResolver Resolver { get; set; }

        /// <summary>
        /// The current active state of the simulation.
        /// </summary>
        IState CurrentState { get; }

        /// <summary>
        /// The array that contains the input symbols.
        /// </summary>
        object[] Input { get; }

        /// <summary>
        /// The index of the current symbol being handled.
        /// </summary>
        int CurrentInputIndex { get; }

        /// <summary>
        /// The current symbol that is being handled.
        /// </summary>
        object CurrentInputSymbol { get; }

        /// <summary>
        /// Determines, if the simulation is finished.
        /// </summary>
        bool IsFinished { get; }

        /// <summary>
        /// Determines, if the input is accepted.
        /// </summary>
        bool IsInputAccepted { get; }

        /// <summary>
        /// Determines, if the current state is an accept state.
        /// </summary>
        bool IsInAcceptState { get; }

        /// <summary>
        /// Determinies, if the current state and current input symbol creates an ambiguous state.
        /// </summary>
        bool IsInAmbiguousState { get; }
        #endregion

        #region Events
        /// <summary>
        /// Defines an event handler to be called after the simulation has made a step.
        /// </summary>
        event Action OnStep;
        #endregion

        #region Methods
        /// <summary>
        /// Determines the step result, without taking the step, for the current input symbol.
        /// </summary>
        /// <returns>The step result for the current input symbol.</returns>
        SimulationStepResult CanStep();

        /// <summary>
        /// Does a step in the simulation.
        /// </summary>
        /// <returns>The result of the step.</returns>
        SimulationStepResult Step();

        /// <summary>
        /// Does a specified step in the simulation for ambiguous state.
        /// </summary>
        /// <param name="transition">The specific transition to handle.</param>
        /// <returns>The result of the step.</returns>
        SimulationStepResult SpecificStep(IStateTransition transition);

        /// <summary>
        /// Automatically runs the simulation until it's finished or becomes invalid.
        /// </summary>
        /// <returns>The last step's result.</returns>
        SimulationStepResult DoAllSteps();

        /// <summary>
        /// Returns the list of applicable transitions from the current state.
        /// </summary>
        /// <returns>The list of applicable transitions.</returns>
        IEnumerable<IStateTransition> GetApplicableTransitions();
        #endregion
    }
}
