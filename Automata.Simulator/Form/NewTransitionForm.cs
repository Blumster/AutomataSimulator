using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

using WinForm = System.Windows.Forms.Form;

using Microsoft.Msagl.GraphViewerGdi;

using MsaglColor = Microsoft.Msagl.Drawing.Color;

namespace Automata.Simulator.Form
{
    using Drawing;
    using Interface;

    public partial class NewTransitionForm : WinForm
    {
        public IAutomata Automata { get; }

        public NewTransitionForm(IAutomata automata)
        {
            Automata = automata ?? throw new ArgumentNullException(nameof(automata), "The automata can not be null!");

            InitializeComponent();

            foreach (var state in Automata.States)
            {
                SourceStateIdComboBox.Items.Add(state.Id);
                TargetStateIdComboBox.Items.Add(state.Id);
            }

            if (Automata.States.Count > 0)
            {
                SourceStateIdComboBox.SelectedIndex = 0;
                TargetStateIdComboBox.SelectedIndex = 0;
            }

            AvailableInputSymbolsLabel.Text = Automata.Alphabet.ConstructSymbolText();
        }

        private void SourceStateIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void TargetStateIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void InputSymbolsFieldTextBox_TextChanged(object sender, EventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void CreateTransitionButton_Click(object sender, EventArgs e)
        {
            if (SourceStateIdComboBox.Items.Count == 0 || TargetStateIdComboBox.Items.Count == 0)
                return;

            var sourceState = Automata.GetState(SourceStateIdComboBox.SelectedItem as string);
            var targetState = Automata.GetState(TargetStateIdComboBox.SelectedItem as string);

            if (sourceState == null || targetState == null)
                return;

            var symbols = new HashSet<char>();
            symbols.UnionWith(InputSymbolsFieldTextBox.Text.Replace(" ", "").Where(c => Automata.Alphabet.ContainsSymbol(c)));

            if (symbols.Count == 0)
            {
                ErrorLabel.ForeColor = Color.Red;
                ErrorLabel.Text = "Legalább egy helyes bemeneti szimbólumot meg kell adni!";
                return;
            }

            Automata.CreateTransition(sourceState.Id, targetState.Id, symbols.Select(s => s as object).ToArray());

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelCreationButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #region Methods
        private void DrawPreviewAutomata()
        {
            if (WindowState == FormWindowState.Minimized)
                return;

            var graph = new AutomataGraph(Automata);

            try
            {
                if (SourceStateIdComboBox.SelectedIndex == -1 || TargetStateIdComboBox.SelectedIndex == -1)
                    return;

                var sourceDrawingState = graph.FindNode(SourceStateIdComboBox.SelectedItem as string) as State;
                var targetDrawingState = graph.FindNode(TargetStateIdComboBox.SelectedItem as string) as State;

                var transition = ConstructTransition(sourceDrawingState.LogicState.Id, targetDrawingState.LogicState.Id);

                var edge = new Edge(sourceDrawingState, targetDrawingState, transition);

                graph.SetupEdge(edge);

                edge.Attr.Color = edge.Label.FontColor = MsaglColor.Blue;

                graph.AddPrecalculatedEdge(edge);

                ErrorLabel.Text = "";
            }
            catch
            {
                ErrorLabel.Text = "A megadott szimbólum nem elérhető az ábécében!";
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

        private IStateTransition ConstructTransition(string sourceId, string targetId)
        {
            var symbols = new HashSet<char>();
            symbols.UnionWith(InputSymbolsFieldTextBox.Text.Replace(" ", ""));

            return Automata.InstantiateTransition(sourceId, targetId, symbols.Where(a => Automata.Alphabet.ContainsSymbol(a)).Select(s => s as object).ToArray());
        }
        #endregion
    }
}
