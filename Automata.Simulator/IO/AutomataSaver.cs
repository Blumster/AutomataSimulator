using System.Globalization;
using System.IO;
using System.Xml;

namespace Automata.Simulator.IO
{
    using Interface;

    /// <summary>
    /// Helper class to save an automata
    /// </summary>
    public static class AutomataSaver
    {
        /// <summary>
        /// Saves the automata to the given file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="automata">The automata to be saved.</param>
        /// <returns>True, if saving the automata was successful.</returns>
        public static bool Save(string path, IAutomata automata)
        {
            using (var outputFileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "    "
                };

                using (var writer = XmlWriter.Create(outputFileStream, settings))
                {
                    writer.WriteStartDocument(true);

                    writer.WriteStartElement("Automata");
                    writer.WriteAttributeString("Type", automata.GetType().AssemblyQualifiedName);

                    automata.WriteToXmlWriter(writer);

                    writer.WriteStartElement("Alphabet");
                    writer.WriteAttributeString("Type", automata.Alphabet.GetType().AssemblyQualifiedName);
                    
                    automata.Alphabet.WriteToXmlWriter(writer);

                    writer.WriteEndElement(); // Alphabet

                    writer.WriteStartElement("States");

                    foreach (var state in automata.States)
                    {
                        writer.WriteStartElement("State");
                        writer.WriteAttributeString("Type", state.GetType().AssemblyQualifiedName);
                        writer.WriteAttributeString("Id", state.Id);
                        writer.WriteAttributeString("IsStartState", state.IsStartState.ToString(CultureInfo.InvariantCulture));
                        writer.WriteAttributeString("IsAcceptState", state.IsAcceptState.ToString(CultureInfo.InvariantCulture));

                        state.WriteToXmlWriter(writer);

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement(); // States

                    writer.WriteStartElement("Transitions");

                    foreach (var transition in automata.Transitions)
                    {
                        writer.WriteStartElement("Transition");
                        writer.WriteAttributeString("Type", transition.GetType().AssemblyQualifiedName);
                        writer.WriteAttributeString("SourceStateId", transition.SourceState.Id);
                        writer.WriteAttributeString("TargetStateId", transition.TargetState.Id);
                        writer.WriteAttributeString("Symbols", automata.Alphabet.ConstructSymbolText(transition.Symbols, false).Replace(" ", ""));

                        transition.WriteToXmlWriter(writer);

                        writer.WriteEndElement(); // Transition
                    }

                    writer.WriteEndElement(); // Transitions

                    writer.WriteEndElement(); // Automata

                    writer.WriteEndDocument();
                }
            }

            return true;
        }
    }
}
