<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<Automata Type="Automata.Finite.FiniteAutomata, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
    <Alphabet Type="Automata.Alphabet.CharacterAlphabet, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Symbols="ab" />
    <States>
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="q0" IsStartState="True" IsAcceptState="True" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="q1" IsStartState="False" IsAcceptState="False" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="q2" IsStartState="False" IsAcceptState="False" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="q3" IsStartState="False" IsAcceptState="False" />
    </States>
    <Transitions>
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="q0" TargetStateId="q1" Symbols="b" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="q0" TargetStateId="q2" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="q1" TargetStateId="q0" Symbols="b" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="q1" TargetStateId="q3" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="q2" TargetStateId="q0" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="q2" TargetStateId="q3" Symbols="b" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="q3" TargetStateId="q1" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="q3" TargetStateId="q2" Symbols="b" />
    </Transitions>
</Automata>