using System;
using System.Collections.Generic;
using System.Linq;

namespace Automata.Simulation
{
    using Enum;
    using Interface;

    public class SimpleSimulation : ISimulation
    {
        #region Fields
        private int _index = 0;
        #endregion

        #region Properties
        public IAutomata Automata { get; }
        public IAmbiguityResolver Resolver { get; set; }
        public IState CurrentState { get; private set; }
        public object[] Input { get; }

        public object CurrentInputSymbol
        {
            get
            {
                return _index < Input.Length ? Input[_index] : null;
            }
        }

        public int CurrentInputIndex
        {
            get
            {
                return _index;
            }
        }

        public bool IsFinished
        {
            get
            {
                return CanStep() != SimulationStepResult.Success && CanStep() != SimulationStepResult.Ambiguous;
            }
        }

        /// <summary>
        /// Determines, if the input is accepted.
        /// </summary>
        public bool IsInputAccepted
        {
            get
            {
                return IsInAcceptState && Input.Length == CurrentInputIndex;
            }
        }

        public bool IsInAcceptState
        {
            get
            {
                return CurrentState.IsAcceptState;
            }
        }

        public bool IsInAmbiguousState
        {
            get
            {
                return GetApplicableTransitions().Count() > 1;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Defines an event handler to be called after the simulation has made a step.
        /// </summary>
        public event Action OnStep;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new simulation based on the given parameters.
        /// </summary>
        /// <param name="automata">The automata to be simulated.</param>
        /// <param name="input">The input symbols.</param>
        public SimpleSimulation(IAutomata automata, object[] input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "The input symbols array can not be null!");

            Automata = automata ?? throw new ArgumentNullException(nameof(automata), "The automata can not be null!");
            CurrentState = Automata.GetStartState() ?? throw new ArgumentException(nameof(automata), "The automata must have a start state!");

            Input = new object[input.Length];

            Array.Copy(input, Input, input.Length);
        }
        #endregion

        #region Interface
        /// <summary>
        /// Determines the step result, without taking the step, for the current input symbol.
        /// </summary>
        /// <returns>The step result for the current input symbol.</returns>
        public SimulationStepResult CanStep()
        {
            if (Input.Length - _index == 0)
                return SimulationStepResult.NoMoreInputSymbols;

            var applicableTransitions = GetApplicableTransitions();

            if (applicableTransitions.Count() == 0)
                return SimulationStepResult.NoTransition;

            if (applicableTransitions.Count() > 1)
            {
                if (Resolver == null)
                    return SimulationStepResult.AmbiguousNoResolver;

                return SimulationStepResult.Ambiguous;
            }

            return SimulationStepResult.Success;
        }

        /// <summary>
        /// Does a step in the simulation.
        /// </summary>
        /// <returns>The result of the step.</returns>
        public SimulationStepResult Step()
        {
            var canStep = CanStep();
            if (canStep == SimulationStepResult.Success)
            {
                var applicableTransitions = GetApplicableTransitions();

                StepInternal(applicableTransitions.First());
            }

            if (IsFinished)
                return SimulationStepResult.Finished;

            return canStep;
        }

        /// <summary>
        /// Does a specified step in the simulation for ambiguous state.
        /// </summary>
        /// <param name="transition">The specific transition to handle.</param>
        /// <returns>The result of the step.</returns>
        public SimulationStepResult SpecificStep(IStateTransition transition)
        {
            if (transition == null)
                throw new ArgumentNullException(nameof(transition), "The transition can not be null!");

            var canStep = CanStep();
            if (canStep != SimulationStepResult.Ambiguous && canStep != SimulationStepResult.AmbiguousNoResolver)
                return SimulationStepResult.Invalid;
            
            var applicableTransitions = GetApplicableTransitions();
            if (!applicableTransitions.Contains(transition))
                return SimulationStepResult.Invalid;

            StepInternal(transition);

            return SimulationStepResult.Success;
        }

        /// <summary>
        /// Automatically runs the simulation until it's finished or becomes invalid.
        /// </summary>
        /// <returns>The last step's result.</returns>
        public SimulationStepResult DoAllSteps()
        {
            var i = 0;

            while (++i <= 10000)
            {
                var result = Step();

                switch (result)
                {
                    case SimulationStepResult.Success:
                        continue;

                    case SimulationStepResult.Ambiguous:
                        result = SpecificStep(Resolver.Resolve(this));
                        if (result == SimulationStepResult.Success)
                            continue;

                        break;
                }

                return result;
            }

            return SimulationStepResult.Timeout;
        }

        /// <summary>
        /// Returns the list of applicable transitions from the current state.
        /// </summary>
        /// <returns>The list of applicable transitions.</returns>
        public IEnumerable<IStateTransition> GetApplicableTransitions()
        {
            var applicableTransitions = new List<IStateTransition>();

            foreach (var transition in CurrentState.OutTransitions)
                if (transition.HandlesSymbol(CurrentInputSymbol))
                    applicableTransitions.Add(transition);

            return applicableTransitions;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles the internal process of stepping.
        /// </summary>
        /// <param name="transition">The selected transition to handle.</param>
        private void StepInternal(IStateTransition transition)
        {
            if (transition == null)
                throw new ArgumentNullException(nameof(transition), "The transition can not be null!");

            if (transition.SourceState != CurrentState)
                throw new ArgumentException("The transition's source isn't the current state!", nameof(transition));

            if (!transition.HandlesSymbol(CurrentInputSymbol))
                throw new ArgumentException("The transition can't handle the current input symbol!", nameof(transition));

            CurrentState = transition.TargetState;

            ++_index;

            OnStep?.Invoke();
        }
        #endregion
    }
}
