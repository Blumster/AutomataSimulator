using System;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

using WinForm = System.Windows.Forms.Form;

using Microsoft.Msagl.GraphViewerGdi;

using MsaglColor = Microsoft.Msagl.Drawing.Color;

namespace Automata.Simulator.Form
{
    using Drawing;
    using Interface;

    public partial class DeleteStateForm : WinForm
    {
        #region Properties
        public IAutomata Automata { get; }
        #endregion

        #region Constructors
        public DeleteStateForm(IAutomata automata)
        {
            Automata = automata;

            InitializeComponent();

            DeleteStateComboBox.Items.Clear();

            foreach (var state in Automata.States)
                DeleteStateComboBox.Items.Add(state.Id);

            DeleteStateComboBox.SelectedIndex = 0;
        }
        #endregion

        #region Control event handlers
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void DeleteStateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void DeleteStateButton_Click(object sender, EventArgs e)
        {
            var selectedStateId = DeleteStateComboBox.SelectedItem as string;
            if (String.IsNullOrWhiteSpace(selectedStateId))
                return;

            var state = Automata.GetState(selectedStateId);
            if (state == null)
                return;

            Automata.RemoveState(state);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelDeletionButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        #region Methods
        private void DrawPreviewAutomata()
        {
            if (WindowState == FormWindowState.Minimized)
                return;

            var graph = new AutomataGraph(Automata);

            var selectedStateId = DeleteStateComboBox.SelectedItem as string;

            if (!String.IsNullOrWhiteSpace(selectedStateId) && Automata.GetState(selectedStateId) != null)
            {
                if (graph.FindNode(selectedStateId) is State state)
                {
                    state.Attr.Color = MsaglColor.Red;
                    state.Label.FontColor = MsaglColor.Red;

                    foreach (var edge in state.Edges)
                    {
                        edge.Attr.Color = MsaglColor.Red;
                        edge.Label.FontColor = MsaglColor.Red;
                    }
                }
            }

            var renderer = new GraphRenderer(graph);
            renderer.CalculateLayout();

            using (var graphics = DrawPanel.CreateGraphics())
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                renderer.Render(graphics, 0, 0, DrawPanel.Width, DrawPanel.Height);
            }
        }
        #endregion
    }
}
