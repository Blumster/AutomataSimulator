using System;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

using WinForm = System.Windows.Forms.Form;

using Microsoft.Msagl.GraphViewerGdi;

namespace Automata.Simulator.Form
{
    using Drawing;
    using Enum;
    using IO;

    public partial class MainWindow : WinForm
    {
        #region Constants
        private const string Manual = "Manuális";
        private const string Timed = "Időzített";
        private const string Instant = "Azonnali";
        #endregion

        #region Fields
        private AutomataGraph _graph;
        #endregion

        #region Properties
        AutomataGraph Graph
        {
            get
            {
                return _graph;
            }
            set
            {
                _graph = value;

                SetupUI();
            }
        }
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            SimulationStepMethodComboBox.Items.Add(Manual);
            SimulationStepMethodComboBox.Items.Add(Timed);
            SimulationStepMethodComboBox.Items.Add(Instant);

            SimulationStepMethodComboBox.SelectedIndex = 0;

            SetupUI();
        }
        #endregion

        #region Control event handlers
        #region MainWindow
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            DrawGraph();
        }
        #endregion

        #region Manage Controls
        private void NewButton_Click(object sender, EventArgs e)
        {
            if (Graph != null && !Graph.IsSaved)
            {
                var mBoxResult = MessageBox.Show("A jelenlegi automata nincs elmentve. Szertné menteni?", "Mentés", MessageBoxButtons.YesNoCancel);
                if (mBoxResult == DialogResult.Yes)
                {
                    SaveAutomata();

                    if (!Graph.IsSaved)
                        return;
                }
                else if (mBoxResult == DialogResult.No)
                {
                    Graph = null;
                    DrawGraph();
                }
                else
                    return;
            }

            using (var form = new CreateAutomataForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Graph = new AutomataGraph(form.CreateAutomata(), true);

                    DrawGraph();
                }
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
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

                DrawGraph();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveAutomata();
        }
        #endregion

        #region Edit Controls
        private void NewStateButton_Click(object sender, EventArgs e)
        {
            using (var newStateForm = new NewStateForm(Graph.Automata))
            {
                var result = newStateForm.ShowDialog();
                if (result == DialogResult.Cancel)
                    return;

                DrawGraph();
            }

            SetupUI();
        }

        private void NewTransitionButton_Click(object sender, EventArgs e)
        {
            using (var newTransitionForm = new NewTransitionForm(Graph.Automata))
            {
                var result = newTransitionForm.ShowDialog();
                if (result == DialogResult.Cancel)
                    return;

                DrawGraph();
            }

            SetupUI();
        }

        private void DeleteStateButton_Click(object sender, EventArgs e)
        {
            using (var deleteStateForm = new DeleteStateForm(Graph.Automata))
            {
                var result = deleteStateForm.ShowDialog();
                if (result == DialogResult.Cancel)
                    return;

                DrawGraph();
            }

            SetupUI();
        }

        private void DeleteTransitionButton_Click(object sender, EventArgs e)
        {
            using (var deleteTransitionForm = new DeleteTransitionForm(Graph.Automata))
            {
                var result = deleteTransitionForm.ShowDialog();
                if (result == DialogResult.Cancel)
                    return;

                DrawGraph();
            }

            SetupUI();
        }
        #endregion

        #region Simulation Controls
        private void StartNewSimulation_Click(object sender, EventArgs e)
        {
            using (var simulationSettingsForm = new SimulationSettingsForm())
            {
                if (simulationSettingsForm.ShowDialog() == DialogResult.Cancel)
                    return;

                Graph.StartSimulation(GetStepType(), simulationSettingsForm.GetInputArray(), SimulationSpeedTrackBar.Value);
            }

            //Text = "\u275A\u275A " + Text; // pause icon
            Text = "\u25B6 " + Text;

            SetupUI();
        }

        private void StopSimulationButton_Click(object sender, EventArgs e)
        {
            StopSimulation();
        }

        private void SimulationStepButton_Click(object sender, EventArgs e)
        {
            var result = Graph.StepSimulation();
            switch (result)
            {
                case SimulationStepResult.NoMoreInputSymbols:
                    StopSimulation();
                    break;

                case SimulationStepResult.Ambiguous:
                    // TODO
                    break;
            }
        }

        private void SimulationStepMethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Graph != null && Graph.Simulation != null)
                Graph.Simulation.UpdateStepMethod(GetStepType());

            SetupUI();
        }

        private void SimulationSpeedTrackBar_Scroll(object sender, EventArgs e)
        {
            SimulationSpeedLabel.Text = SimulationSpeedTrackBar.Value.ToString();
        }
        #endregion
        #endregion

        #region Methods
        private void SetupUI()
        {
            if (Graph == null)
            {
                SaveButton.Enabled = false;

                EditGroupBox.Enabled = false;

                SimulationGroupBox.Enabled = false;
            }
            else if (Graph.Simulation == null)
            {
                SaveButton.Enabled = true;

                EditGroupBox.Enabled = true;
                NewStateButton.Enabled = true;
                NewTransitionButton.Enabled = Graph.NodeCount > 0;
                DeleteStateButton.Enabled = Graph.NodeCount > 0;
                DeleteTransitionButton.Enabled = Graph.EdgeCount > 0;

                SimulationGroupBox.Enabled = true;
                StartNewSimulationButton.Enabled = true;
                StopSimulationButton.Enabled = false;
                SimulationStepMethodComboBox.Enabled = true;
                SimulationStepButton.Enabled = false;
                SimulationSpeedTrackBar.Enabled = true;
            }
            else
            {
                ManageGroupBox.Enabled = false;
                EditGroupBox.Enabled = false;

                SimulationGroupBox.Enabled = true;
                StartNewSimulationButton.Enabled = false;
                StopSimulationButton.Enabled = true;
                SimulationStepMethodComboBox.Enabled = false;
                SimulationStepButton.Enabled = true;
                SimulationSpeedTrackBar.Enabled = true;
            }

            var isManual = Manual.Equals(SimulationStepMethodComboBox.SelectedItem);

            SimulationStepButton.Visible = isManual;

            var isTimed = Timed.Equals(SimulationStepMethodComboBox.SelectedItem);

            SimulationSpeedTrackBar.Visible = isTimed;
            SimulationSpeedLabel.Visible = isTimed;
            SimulationSpeedDescriptionLabel.Visible = isTimed;
        }

        private void SaveAutomata()
        {
            if (saveAutomataDialog.ShowDialog() == DialogResult.OK)
                Graph.IsSaved = AutomataSaver.Save(saveAutomataDialog.FileName, Graph.Automata);
        }

        private void StopSimulation()
        {
            if (Graph == null)
                return;

            Graph.StopSimulation();

            Text = Text.Substring(Text.IndexOf(' ')).Trim();

            SetupUI();
        }

        public void DrawGraph()
        {
            if (WindowState == FormWindowState.Minimized)
                return;

            if (Graph == null)
            {
                DrawPanel.Invalidate();
                return;
            }

            var renderer = new GraphRenderer(Graph);
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

        private SimulationStepMethod GetStepType()
        {
            if (Instant.Equals(SimulationStepMethodComboBox.SelectedItem))
                return SimulationStepMethod.Instant;

            if (Timed.Equals(SimulationStepMethodComboBox.SelectedItem))
                return SimulationStepMethod.Timed;

            return SimulationStepMethod.Manual;
        }
        #endregion
    }
}
