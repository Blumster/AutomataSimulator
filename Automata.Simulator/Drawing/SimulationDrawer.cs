using System;
using System.Drawing;

namespace Automata.Simulator.Drawing
{
    /// <summary>
    /// A helper class that draws the simulation input symbol bar on the given graphic surface.
    /// </summary>
    public class SimulationDrawer
    {
        #region Constants
        public const int SimulationBarHeight = 50;
        public const int InputSymbolTopOffset = 5;
        public const int InputDisplayHeight = 40;
        public const int InputDisplayWidth = 40;
        public const int InputSymbolHistoryCount = 5;
        #endregion

        #region Properties
        /// <summary>
        /// The graph representation of the automata.
        /// </summary>
        public AutomataGraph Graph { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new simulation drawer for the given graph representation.
        /// </summary>
        /// <param name="graph">The graph representation.</param>
        public SimulationDrawer(AutomataGraph graph)
        {
            Graph = graph ?? throw new ArgumentNullException(nameof(graph), "The graph representation can not be null!");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draws the simulation bar to the graphic surface.
        /// </summary>
        /// <param name="graphics">The graphic surface.</param>
        /// <param name="left">The left parameter to draw from.</param>
        /// <param name="top">The top parameter to draw from.</param>
        /// <param name="width">The width of the drawable surface.</param>
        public void DrawSimulationData(Graphics graphics, int left, int top, int width)
        {
            if (Graph.Simulation == null)
                throw new Exception("The simulation data can't be drawn while there is no simulation in progress!");

            var barTop = top + InputSymbolTopOffset;
            var barLeft = left + (width % InputDisplayWidth) / 2;

            var inputCount = width / InputDisplayWidth;
            var currentInputBox = inputCount / 2 - 1;
            var indexDiff = currentInputBox - Graph.Simulation.CurrentInputIndex;

            var symbols = new string[inputCount];

            for (var i = 0; i < Graph.Simulation.Input.Length && i + indexDiff < symbols.Length; ++i)
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
        #endregion
    }
}
