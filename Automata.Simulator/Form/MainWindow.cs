using System;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

using WinForm = System.Windows.Forms.Form;

using Microsoft.Msagl.GraphViewerGdi;

namespace Automata.Simulator.Form
{
    using Drawing;
    using IO;

    public partial class MainWindow : WinForm
    {
        private const string Manual = "Manuális";
        private const string Timed = "Időzített";
        private const string Instant = "Azonnali";

        private AutomataGraph _graph;

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
        object Simulation { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            SimulationStepMethodComboBox.Items.Add(Manual);
            SimulationStepMethodComboBox.Items.Add(Timed);
            SimulationStepMethodComboBox.Items.Add(Instant);

            SimulationStepMethodComboBox.SelectedIndex = 0;

            SetupUI();
        }

        #region Button handlers
        #region Manage Buttons
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
                    Graph = new AutomataGraph(form.CreateAutomata());

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

        #region Edit Buttons
        private void NewStateButton_Click(object sender, EventArgs e)
        {
            SetupUI();
        }

        private void NewTransitionButton_Click(object sender, EventArgs e)
        {
            SetupUI();
        }

        private void DeleteStateButton_Click(object sender, EventArgs e)
        {
            SetupUI();
        }

        private void DeleteTransitionButton_Click(object sender, EventArgs e)
        {
            SetupUI();
        }
        #endregion

        #region Simulation Buttons
        private void StartNewSimulation_Click(object sender, EventArgs e)
        {
            SetupUI();
        }

        private void SimulationSpeedTrackBar_Scroll(object sender, EventArgs e)
        {

        }
        #endregion
        #endregion

        #region Methods
        private void SetupUI()
        {
            if (Graph == null)
            {
                ManageGroupBox.Enabled = true;
                SaveButton.Enabled = false;

                EditGroupBox.Enabled = false;

                SimulationGroupBox.Enabled = false;
            }
            else if (Simulation == null)
            {
                EditGroupBox.Enabled = true;
                NewStateButton.Enabled = true;
                NewTransitionButton.Enabled = true;
                DeleteStateButton.Enabled = Graph.NodeCount > 0;
                DeleteTransitionButton.Enabled = Graph.EdgeCount > 0;

                SimulationGroupBox.Enabled = true;
                StartNewSimulationButton.Enabled = true;
                StopSimulationButton.Enabled = false;
                SimulationStepButton.Enabled = false;
                SimulationStepMethodComboBox.Enabled = true;
            }
            else
            {
                ManageGroupBox.Enabled = false;

                EditGroupBox.Enabled = false;

                SimulationGroupBox.Enabled = true;
                StartNewSimulationButton.Enabled = false;
                StopSimulationButton.Enabled = true;
                SimulationStepButton.Enabled = Manual.Equals(SimulationStepMethodComboBox.SelectedItem);
                SimulationStepMethodComboBox.Enabled = false;
            }

            // TODO: step button, track bar visibility based on selected method!
        }

        private void SaveAutomata()
        {
            if (saveAutomataDialog.ShowDialog() == DialogResult.OK)
                Graph.IsSaved = AutomataSaver.Save(saveAutomataDialog.FileName, Graph);
        }

        public void DrawGraph()
        {
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
        #endregion
    }
}
