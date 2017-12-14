using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using WinForm = System.Windows.Forms.Form;

namespace Automata.Simulator.Form
{
    /// <summary>
    /// Defines a form to start a simulation with.
    /// </summary>
    public partial class SimulationSettingsForm : WinForm
    {
        #region Constructors
        /// <summary>
        /// Creates a new simulation settings form.
        /// </summary>
        public SimulationSettingsForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the input symbol array.
        /// </summary>
        /// <returns>The input symbol array.</returns>
        public object[] GetInputArray()
        {
            return SimulationInputTextBox.Text.Select(c => c as object).ToArray();
        }
        #endregion

        #region Control event handlers
        /// <summary>
        /// Handles the start button's click event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void StartSimulationButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the cancel button's click event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void CancelSimulationButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
    }
}
