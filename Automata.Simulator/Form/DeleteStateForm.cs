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

    /// <summary>
    /// Defines a form for deleting states from an automata.
    /// </summary>
    public partial class DeleteStateForm : WinForm
    {
        #region Properties
        /// <summary>
        /// The automata that is operated upon.
        /// </summary>
        public IAutomata Automata { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new delete state form for the given automata.
        /// </summary>
        /// <param name="automata">The automata.</param>
        public DeleteStateForm(IAutomata automata)
        {
            Automata = automata ?? throw new ArgumentNullException(nameof(automata), "The automata can not be null!");

            InitializeComponent();

            DeleteStateComboBox.Items.Clear();

            foreach (var state in Automata.States)
                DeleteStateComboBox.Items.Add(state.Id);

            if (DeleteStateComboBox.Items.Count > 0)
                DeleteStateComboBox.SelectedIndex = 0;
        }
        #endregion

        #region Control event handlers
        /// <summary>
        /// Handles the draw panel's paint event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawPreviewAutomata();
        }

        /// <summary>
        /// Handles the state combobox's change event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void DeleteStateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawPreviewAutomata();
        }

        /// <summary>
        /// Handles the delete state button's click event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void DeleteStateButton_Click(object sender, EventArgs e)
        {
            if (DeleteStateComboBox.Items.Count == 0)
                return;

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

        /// <summary>
        /// Handles the cancel button's click event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void CancelDeletionButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draws the modified automata based on the form fields' value.
        /// </summary>
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
