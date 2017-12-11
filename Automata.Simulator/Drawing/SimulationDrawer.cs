using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata.Simulator.Drawing
{
    public class SimulationDrawer
    {
        public const int InputDisplayHeight = 20;
        public const int InputDisplayWidth = 20;
        public const int InputSymbolHistoryCount = 5;

        public AutomataGraph Graph { get; }

        public SimulationDrawer(AutomataGraph graph)
        {
            Graph = graph;
        }

        public void DrawSimulationData(Graphics graphics, int left, int top, int width, int height)
        {
            if (Graph.Simulation == null)
                throw new Exception("The simulation data can't be drawn while there is no simulation in progress!");

            /*
             20x20as kockák, benne a karakter.
             a középen lévő kocka dupla falú, hogy ez az aktív kocka
             középen legyen alul
             ha inputcount * inputwidth > width akkor csúszni fog az input szalag, de az aktív szimbólum jelölö középen marad
             */
             /*
             Ha marad idő, egy viszaszámláló kört, hogy mikor fog újra lépni, ha időzített.
             Kb mint youtubeon amikor új videót akar auto játszani egy kör visszaszámol, olyat valamelyik sarokba
             */
        }
    }
}
