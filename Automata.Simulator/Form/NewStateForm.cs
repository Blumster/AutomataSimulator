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

    public partial class NewStateForm : WinForm
    {
        public IAutomata Automata { get; }

        #region Constructors
        public NewStateForm(IAutomata automata)
        {
            Automata = automata ?? throw new ArgumentNullException(nameof(automata), "The automata can not be null!");

            InitializeComponent();

            if (Automata.GetStartState() != null)
                StartStateWarningLabel.Visible = true;
        }
        #endregion

        #region Control event handlers
        private void StateIdTextBox_TextChanged(object sender, EventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void IsStartStateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void IsAcceptStateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void CreateStateButton_Click(object sender, EventArgs e)
        {
            var stateId = StateIdTextBox.Text;

            if (string.IsNullOrWhiteSpace(stateId))
            {
                ErrorLabel.Text = "Az új állapot neve nem lehet üres!";
                ErrorLabel.Visible = true;
                return;
            }

            foreach (var state in Automata.States)
            {
                if (state.Id.Equals(stateId))
                {
                    ErrorLabel.Text = "Már létezik ilyen nevű állapot!";
                    ErrorLabel.Visible = true;
                    return;
                }
            }

            if (IsStartStateCheckBox.Checked)
            {
                var startState = Automata.GetStartState();
                if (startState != null)
                {
                    startState.IsStartState = false;

                    Automata.UpdateState(startState);
                }
            }

            Automata.CreateState(stateId, IsStartStateCheckBox.Checked, IsAcceptStateCheckBox.Checked);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelCreationButton_Click(object sender, EventArgs e)
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

            if (Automata.GetState(StateIdTextBox.Text) != null)
                return;

            if (StateIdTextBox.Text.Length > 0)
            {
                var state = new State(ConstructState());

                state.Attr.Color = MsaglColor.Blue;
                state.Label.FontColor = MsaglColor.Blue;

                var startState = Automata.GetStartState();

                if (state.IsStartState && startState != null)
                {
                    var oldStartState = graph.FindNode(startState.Id) as State;

                    oldStartState.OverrideIsStartState = false;
                }

                graph.SetupState(state);
                graph.AddNode(state);
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

        private IState ConstructState()
        {
            return Automata.InstantiateState(StateIdTextBox.Text, IsStartStateCheckBox.Checked, IsAcceptStateCheckBox.Checked);
        }


        #endregion
    }
}
