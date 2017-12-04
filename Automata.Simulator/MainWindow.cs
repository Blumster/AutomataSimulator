using System;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

using Microsoft.Msagl.GraphViewerGdi;

namespace Automata.Simulator
{
    using Drawing;
    using IO;

    public partial class MainWindow : Form
    {
        AutomataGraph Graph { get; set; }
        GraphRenderer Renderer { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            //graphViewer.OutsideAreaBrush = Brushes.White;
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            if (Graph.NodeMap["K"] != null)
            {
                Graph.Automata.CreateTransition("K", "A", "a,b,c");

                DrawGraph();
            }

            Graph.Automata.GetOrCreateState("K");
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (true || loadAutomataDialog.ShowDialog() == DialogResult.OK)
            {
                Graph = AutomataLoader.Load(loadAutomataDialog.FileName);
                if (Graph == null)
                {
                    MessageBox.Show("Hiba történt az automata beolvasása közben!");
                    return;
                }

                Graph.OnRedraw += DrawGraph;
                Renderer = new GraphRenderer(Graph);

                DrawGraph();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveAutomataDialog.ShowDialog() == DialogResult.OK)
            {
                AutomataSaver.Save(saveAutomataDialog.FileName, Graph);
            }
        }

        public void DrawGraph()
        {
            Renderer.CalculateLayout();

            using (var graphics = drawPanel.CreateGraphics())
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                Renderer.Render(graphics, 0, 0, drawPanel.Width, drawPanel.Height);
            }
        }
    }
}
