using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Automata.Simulator.IO
{
    using Alphabet;
    using Finite;
    using Interface;
    using Pushdown;
    using State;
    using Transition;

    public static class AutomataSaver
    {
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
                    writer.WriteStartElement(automata.GetType().Name);
                    writer.WriteAttributeString("Type", automata.GetType().AssemblyQualifiedName);

                    writer.WriteStartElement("Alphabet");
                    writer.WriteAttributeString("Type", automata.Alphabet.GetType().AssemblyQualifiedName);
                    
                    automata.Alphabet.WriteToXmlWriter(writer);

                    writer.WriteEndElement();

                    writer.WriteStartElement("States");

                    foreach (var state in automata.States)
                    {
                        writer.WriteStartElement("State");
                        writer.WriteAttributeString("Type", state.GetType().AssemblyQualifiedName);
                        writer.WriteAttributeString("IsStartState", state.IsStartState.ToString(CultureInfo.InvariantCulture));
                        writer.WriteAttributeString("IsAcceptState", state.IsAcceptState.ToString(CultureInfo.InvariantCulture));

                        state.WriteToXmlWriter(writer);

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();

                    writer.WriteStartElement("Transitions");

                    // TODO
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
            }

            return true;
        }
    }
}
