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
        public const int SimulationBarHeight = 50;
        public const int InputSymbolTopOffset = 5;
        public const int InputDisplayHeight = 40;
        public const int InputDisplayWidth = 40;
        public const int InputSymbolHistoryCount = 5;

        public AutomataGraph Graph { get; }

        public SimulationDrawer(AutomataGraph graph)
        {
            Graph = graph ?? throw new ArgumentNullException(nameof(graph), "The graph can not be null!");
        }

        public void DrawSimulationData(Graphics graphics, int left, int top, int width)
        {
            if (Graph.Simulation == null)
                throw new Exception("The simulation data can't be drawn while there is no simulation in progress!");

            var barTop = top + InputSymbolTopOffset;
            var barLeft = left;

            var inputCount = width / InputDisplayWidth;

            var rem = width % InputDisplayWidth;
            barLeft += rem / 2;

            var currentInputBox = inputCount / 2 - 1;

            
            var symbolCount = Graph.Simulation.Input.Length;
            var currentSymbolIndex = Graph.Simulation.CurrentInputIndex;
            var indexDiff = currentInputBox - currentSymbolIndex;

            var symbols = new string[inputCount];

            for (var i = 0; i < symbolCount && i + indexDiff < symbols.Length; ++i)
            {
                if (i + indexDiff < 0)
                    continue;

                symbols[i + indexDiff] = Graph.Simulation.Input[i].ToString();
            }

            using (var brush = new SolidBrush(Color.Black))
            {
                using (var pen = new Pen(brush, 2))
                {
                    for (var i = 0; i < inputCount; ++i)
                    {
                        var rectLeft = barLeft + i * InputDisplayWidth;

                        graphics.DrawRectangle(pen, new Rectangle(rectLeft, barTop, InputDisplayWidth, InputDisplayHeight));

                        if (i == currentInputBox)
                        {
                            using (var linePen = new Pen(brush, 3))
                            {
                                graphics.DrawLine(linePen, rectLeft + InputDisplayWidth / 4, barTop - InputSymbolTopOffset, rectLeft + InputDisplayWidth / 2, barTop);
                                graphics.DrawLine(linePen, rectLeft + 3 * InputDisplayWidth / 4, barTop - InputSymbolTopOffset, rectLeft + InputDisplayWidth / 2, barTop);
                            }

                            graphics.FillRectangle(new SolidBrush(Color.Cyan), new Rectangle(rectLeft + 1, barTop + 1, InputDisplayWidth - 2, InputDisplayHeight - 2));
                        }

                        graphics.DrawString(symbols[i], new Font(FontFamily.GenericSansSerif, 25), brush, rectLeft + 5, barTop);
                    }
                }
            }
        }
    }
}
