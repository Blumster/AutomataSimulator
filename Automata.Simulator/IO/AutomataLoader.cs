using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Automata.Simulator.IO
{
    using Alphabet;
    using Drawing;
    using Finite;

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

            try
            {
                return new AutomataGraph(automata, true);
            }
            catch
            {
                return null;
            }
        }

        public static void a(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = XmlReader.Create(fileStream))
                {
                }
            }
        }
    }
}
