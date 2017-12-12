<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<Automata Type="Automata.Finite.FiniteAutomata, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
    <Alphabet Type="Automata.Alphabet.CharacterAlphabet, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Symbols="abc" />
    <States>
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="A" IsStartState="True" IsAcceptState="False" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="B" IsStartState="False" IsAcceptState="False" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="C" IsStartState="False" IsAcceptState="False" />
        <State Type="Automata.State.State, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Id="D" IsStartState="False" IsAcceptState="True" />
    </States>
    <Transitions>
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="A" TargetStateId="B" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="A" TargetStateId="A" Symbols="bc" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="B" TargetStateId="B" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="B" TargetStateId="C" Symbols="b" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="B" TargetStateId="A" Symbols="c" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="C" TargetStateId="B" Symbols="a" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="C" TargetStateId="A" Symbols="b" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="C" TargetStateId="D" Symbols="c" />
        <Transition Type="Automata.Transition.SimpleTransition, Automata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" SourceStateId="D" TargetStateId="D" Symbols="abc" />
    </Transitions>
</Automata>