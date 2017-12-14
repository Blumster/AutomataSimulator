using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Automata.Simulator.IO
{
    using Drawing;
    using Interface;

    /// <summary>
    /// Helper class to load an automata.
    /// </summary>
    public static class AutomataLoader
    {
        /// <summary>
        /// Loads an automata from the given path.
        /// </summary>
        /// <param name="path">THe file path.</param>
        /// <returns>The new automata's graph representation.</returns>
        public static AutomataGraph Load(string path)
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
