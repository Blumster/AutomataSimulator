<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<Automata Type="Automata.Finite.FiniteAutomata, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
    <Alphabet Type="Automata.Alphabet.CharacterAlphabet, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Symbols="ab" />
    <States>
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="S" IsStartState="True" IsAcceptState="False" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="B1" IsStartState="False" IsAcceptState="False" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="A1" IsStartState="False" IsAcceptState="False" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="A2" IsStartState="False" IsAcceptState="False" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="A3" IsStartState="False" IsAcceptState="False" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="A4" IsStartState="False" IsAcceptState="False" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="B2" IsStartState="False" IsAcceptState="True" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="H" IsStartState="False" IsAcceptState="False" />
    </States>
    <Transitions>
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="S" TargetStateId="B1" Symbols="b" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="B1" TargetStateId="A1" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="A1" TargetStateId="A2" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="A2" TargetStateId="A3" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="A3" TargetStateId="A4" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="A1" TargetStateId="B2" Symbols="b" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="A2" TargetStateId="B2" Symbols="b" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="A4" TargetStateId="B2" Symbols="b" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="S" TargetStateId="H" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="B1" TargetStateId="H" Symbols="b" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="A3" TargetStateId="H" Symbols="b" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="A4" TargetStateId="H" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="H" TargetStateId="H" Symbols="ab" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="B2" TargetStateId="H" Symbols="ab" />
    </Transitions>
</Automata>