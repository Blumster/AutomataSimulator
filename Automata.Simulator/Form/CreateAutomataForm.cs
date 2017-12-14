using System;
using System.Windows.Forms;

using WinForm = System.Windows.Forms.Form;

namespace Automata.Simulator.Form
{
    using Alphabet;
    using Finite;
    using Interface;

    /// <summary>
    /// Defines a form to create a new automata.
    /// </summary>
    public partial class CreateAutomataForm : WinForm
    {
        #region Consts
        private const string FiniteAutomata = "Véges Automata";
        private const string PushdownAutomata = "Veremautomata";
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new create automata form.
        /// </summary>
        public CreateAutomataForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new automata.
        /// </summary>
        /// <returns></returns>
        public IAutomata CreateAutomata()
        {
            return new FiniteAutomata(new CharacterAlphabet(AlphabetTextBox.Text.Replace(" ", "")));
        }
        #endregion

        #region Control event handlers
        /// <summary>
        /// Handles the create button's click event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the cancel button's click event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
    }
}
