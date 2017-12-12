using System;
using System.Windows.Forms;

using WinForm = System.Windows.Forms.Form;

namespace Automata.Simulator.Form
{
    using AmbiguityResolver;
    using Drawing;
    using Interface;
    using Enum;
    using IO;
    using Resolver;

    public partial class MainWindow : WinForm
    {
        #region Constants
        private const string Manual  = "Manuális";
        private const string Timed   = "Időzített";
        private const string Instant = "Azonnali";

        private const string NoResolver     = "Nincs";
        private const string RandomResolver = "Véletlenszerű";
        private const string ManualResolver = "Manuális";
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

            AmbiguityResolverComboBox.Items.Add(NoResolver);
            AmbiguityResolverComboBox.Items.Add(RandomResolver);
            AmbiguityResolverComboBox.Items.Add(ManualResolver);

            AmbiguityResolverComboBox.SelectedIndex = 0;

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
            if (loadAutomataDialog.ShowDialog() == DialogResult.OK)
            {
                Graph = null;

                try
                {
                    Graph = AutomataLoader.a(loadAutomataDialog.FileName);
                }
                catch
                {
                }
                
                if (Graph == null)
                {
                    MessageBox.Show("Hiba történt az automata beolvasása közben!");
                    return;
                }

                Graph.OnRedraw += DrawGraph;

                //Graph.StartSimulation(SimulationStepMethod.Manual, new object[] { 'a', 'a', 'a' }, 10);

                //Text = "\u25B6 " + Text;

                SetupUI();

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

                IAmbiguityResolver resolver = null;

                if (ManualResolver.Equals(AmbiguityResolverComboBox.SelectedItem))
                    resolver = new ManualResolver();
                else if (RandomResolver.Equals(AmbiguityResolverComboBox.SelectedItem))
                    resolver = new RandomResolver();

                Graph.StartSimulation(GetStepType(), simulationSettingsForm.GetInputArray(), SimulationSpeedTrackBar.Value, resolver);
            }

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
            if (result == SimulationStepResult.Success)
                return;

            SimulationStepButton.Enabled = false;
        }

        private void SimulationStepMethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                ManageGroupBox.Enabled = true;
                SaveButton.Enabled = false;

                EditGroupBox.Enabled = false;

                SimulationGroupBox.Enabled = false;
            }
            else if (Graph.Simulation == null)
            {
                ManageGroupBox.Enabled = true;
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
                AmbiguityResolverComboBox.Enabled = true;
            }
            else
            {
                ManageGroupBox.Enabled = false;
                EditGroupBox.Enabled = false;

                SimulationGroupBox.Enabled = true;
                StartNewSimulationButton.Enabled = false;
                StopSimulationButton.Enabled = true;
                SimulationStepMethodComboBox.Enabled = false;
                SimulationSpeedTrackBar.Enabled = false;
                SimulationStepButton.Enabled = true;
                AmbiguityResolverComboBox.Enabled = false;
            }

            var isManual = Manual.Equals(SimulationStepMethodComboBox.SelectedItem);

            SimulationStepButton.Visible = isManual;

            var isTimed = Timed.Equals(SimulationStepMethodComboBox.SelectedItem);

            SimulationSpeedTrackBar.Visible = isTimed;
            SimulationSpeedLabel.Visible = isTimed;
            SimulationSpeedDescriptionLabel.Visible = isTimed;

            PropertiesGroupBox.Visible = Graph != null;
            if (PropertiesGroupBox.Visible)
            {
                switch (Graph.Automata.Type)
                {
                    case AutomataType.Deterministic:
                        TypeLabel.Text = "Determinisztikus";
                        break;

                    case AutomataType.PartiallyDeterminsitic:
                        TypeLabel.Text = "Parciálisan determinisztikus";
                        break;

                    case AutomataType.Nondeterministic:
                        TypeLabel.Text = "Nemdeterminisztikus";
                        break;
                }

                StateCountLabel.Text = Graph.Automata.States.Count.ToString();
                TransitionCountLabel.Text = Graph.Automata.Transitions.Count.ToString();
                AlphabetLabel.Text = Graph.Automata.Alphabet.ConstructSymbolText();
            }
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

            using (var graphics = DrawPanel.CreateGraphics())
                Graph.DrawAutomata(graphics, 0, 0, DrawPanel.Width, DrawPanel.Height);
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
