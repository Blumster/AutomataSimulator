using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Automata.Simulator.IO
{
    using Alphabet;
    using Drawing;
    using Finite;
    using Interface;

    public static class AutomataLoader
    {
        public static AutomataGraph Load(string path)
        {
            var automata = new FiniteAutomata(new CharacterAlphabet("abc"));

            var aState = automata.GetOrCreateState("A", true);
            var bState = automata.GetOrCreateState("B");
            var cState = automata.GetOrCreateState("C");
            var dState = automata.GetOrCreateState("D", isAcceptState: true);

            automata.CreateTransition("A", "B", 'a');
            automata.CreateTransition("A", "A", 'b', 'c');
            automata.CreateTransition("B", "B", 'a');
            automata.CreateTransition("B", "C", 'b');
            automata.CreateTransition("B", "A", 'c');
            automata.CreateTransition("C", "B", 'a');
            automata.CreateTransition("C", "A", 'b');
            automata.CreateTransition("C", "D", 'c');
            automata.CreateTransition("D", "D", 'a', 'b', 'c');

            return new AutomataGraph(automata, true);
        }

        public static AutomataGraph a(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var settings = new XmlReaderSettings
                {
                    ConformanceLevel = ConformanceLevel.Document
                };

                IAutomata automata = null;

                var stateLookup = new Dictionary<string, IState>();

                using (var reader = XmlReader.Create(fileStream))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "Automata":
                                    automata = Activator.CreateInstance(Type.GetType(reader.GetAttribute("Type"))) as IAutomata;
                                    automata.ReadFromXmlReader(reader);
                                    break;

                                case "Alphabet":
                                    automata.Alphabet = Activator.CreateInstance(Type.GetType(reader.GetAttribute("Type"))) as IAlphabet;
                                    automata.Alphabet.ReadFromXmlReader(reader);
                                    break;

                                case "State":
                                    var state = Activator.CreateInstance(Type.GetType(reader.GetAttribute("Type")), reader.GetAttribute("Id")) as IState;

                                    state.Automata = automata;
                                    state.IsStartState = reader.GetAttribute("IsStartState") == "True";
                                    state.IsAcceptState = reader.GetAttribute("IsAcceptState") == "True";

                                    if (stateLookup.ContainsKey(state.Id))
                                        throw new Exception("Multiple states has the same state id!");

                                    stateLookup[state.Id] = state;

                                    state.ReadFromXmlReader(reader);

                                    automata.States.Add(state);
                                    break;

                                case "Transition":
                                    var symbols = reader.GetAttribute("Symbols").Select(c => c as object).ToArray();
                                    var transition = Activator.CreateInstance(Type.GetType(reader.GetAttribute("Type")), stateLookup[reader.GetAttribute("SourceStateId")], stateLookup[reader.GetAttribute("TargetStateId")], symbols) as IStateTransition;

                                    transition.Automata = automata;
                                    transition.ReadFromXmlReader(reader);

                                    automata.Transitions.Add(transition);
                                    break;
                            }
                        }
                    }
                }

                return new AutomataGraph(automata, true);
            }
        }
    }
}
