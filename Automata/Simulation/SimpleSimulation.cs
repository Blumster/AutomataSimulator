﻿using System;
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
        public SimulationStepMethod StepMethod { get; }

        public bool IsPaused { get; set; } = false;

        public int StepDelaySeconds { get; }

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

        public int RemainingInputLength
        {
            get
            {
                return Input.Length - _index;
            }
        }

        public bool IsFinished
        {
            get
            {
                return CanStep() != SimulationStepResult.Success && CanStep() != SimulationStepResult.Ambiguous;
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
        public event Action OnStep;
        #endregion

        public SimpleSimulation(IAutomata automata, SimulationStepMethod stepMethod, object[] input, int stepDelaySeconds = 1)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "The input symbols array can not be null!");

            Automata = automata ?? throw new ArgumentNullException(nameof(automata), "The automata can not be null!");
            CurrentState = Automata.GetStartState() ?? throw new ArgumentException(nameof(automata), "The automata must have a start state!");

            StepMethod = stepMethod;
            StepDelaySeconds = stepDelaySeconds;

            Input = new object[input.Length];

            Array.Copy(input, Input, input.Length);
        }

        #region Interface
        public SimulationStepResult CanStep()
        {
            if (RemainingInputLength == 0)
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

        public IEnumerable<IStateTransition> GetApplicableTransitions()
        {
            var applicableTransitions = new List<IStateTransition>();

            foreach (var transition in CurrentState.OutTransitions)
                if (transition.HandlesSymbol(CurrentInputSymbol))
                    applicableTransitions.Add(transition);

            return applicableTransitions;
        }

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
                        SpecificStep(Resolver.Resolve(this));
                        continue;
                }

                return result;
            }

            return SimulationStepResult.Timeout;
        }
        #endregion

        #region Methods
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
