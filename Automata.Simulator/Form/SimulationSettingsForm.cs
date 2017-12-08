using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using WinForm = System.Windows.Forms.Form;

namespace Automata.Simulator.Form
{
    public partial class SimulationSettingsForm : WinForm
    {
        public SimulationSettingsForm()
        {
            InitializeComponent();
        }

        public object[] GetInputArray()
        {
            return SimulationInputTextBox.Text.Select(c => c as object).ToArray();
        }

        private void StartSimulationButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelSimulationButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
