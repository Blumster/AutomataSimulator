using System;
using System.Windows.Forms;

using WinForm = System.Windows.Forms.Form;

namespace Automata.Simulator.Form
{
    using Alphabet;
    using Finite;
    using Interface;
    using Pushdown;

    public partial class CreateAutomataForm : WinForm
    {
        private const string FiniteAutomata = "Véges Automata";
        private const string PushdownAutomata = "Veremautomata";

        public CreateAutomataForm()
        {
            InitializeComponent();

            TypeComboBox.Items.Add(FiniteAutomata);
            //TypeComboBox.Items.Add(PushdownAutomata);

            TypeComboBox.SelectedIndex = 0;
        }

        public IAutomata CreateAutomata()
        {
            if (FiniteAutomata.Equals(TypeComboBox.SelectedItem))
                return new FiniteAutomata(new CharacterAlphabet(AlphabetTextBox.Text.Replace(" ", "")));

            if (PushdownAutomata.Equals(TypeComboBox.SelectedItem))
                return new PushdownAutomata(new CharacterAlphabet(AlphabetTextBox.Text.Replace(" ", "")));

            return null;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
