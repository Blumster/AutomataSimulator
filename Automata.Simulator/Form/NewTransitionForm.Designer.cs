namespace Automata.Simulator.Form
{
    partial class NewTransitionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SourceStateIdLabel = new System.Windows.Forms.Label();
            this.CreateTransitionButton = new System.Windows.Forms.Button();
            this.CancelCreationButton = new System.Windows.Forms.Button();
            this.TargetStateIdLabel = new System.Windows.Forms.Label();
            this.SourceStateIdComboBox = new System.Windows.Forms.ComboBox();
            this.TargetStateIdComboBox = new System.Windows.Forms.ComboBox();
            this.InputSymbolsDescriptionLabel = new System.Windows.Forms.Label();
            this.AvailableInputSymbolsLabel = new System.Windows.Forms.Label();
            this.InputSymbolsFieldTextBox = new System.Windows.Forms.TextBox();
            this.InputSymbolsFieldLabel = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.DrawPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // SourceStateIdLabel
            // 
            this.SourceStateIdLabel.AutoSize = true;
            this.SourceStateIdLabel.Location = new System.Drawing.Point(392, 12);
            this.SourceStateIdLabel.Name = "SourceStateIdLabel";
            this.SourceStateIdLabel.Size = new System.Drawing.Size(73, 13);
            this.SourceStateIdLabel.TabIndex = 1;
            this.SourceStateIdLabel.Text = "Forrás állapot:";
            // 
            // CreateTransitionButton
            // 
            this.CreateTransitionButton.Location = new System.Drawing.Point(477, 450);
            this.CreateTransitionButton.Name = "CreateTransitionButton";
            this.CreateTransitionButton.Size = new System.Drawing.Size(75, 23);
            this.CreateTransitionButton.TabIndex = 6;
            this.CreateTransitionButton.Text = "Létrehozás";
            this.CreateTransitionButton.UseVisualStyleBackColor = true;
            this.CreateTransitionButton.Click += new System.EventHandler(this.CreateTransitionButton_Click);
            // 
            // CancelCreationButton
            // 
            this.CancelCreationButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelCreationButton.Location = new System.Drawing.Point(395, 450);
            this.CancelCreationButton.Name = "CancelCreationButton";
            this.CancelCreationButton.Size = new System.Drawing.Size(75, 23);
            this.CancelCreationButton.TabIndex = 5;
            this.CancelCreationButton.Text = "Mégse";
            this.CancelCreationButton.UseVisualStyleBackColor = true;
            this.CancelCreationButton.Click += new System.EventHandler(this.CancelCreationButton_Click);
            // 
            // TargetStateIdLabel
            // 
            this.TargetStateIdLabel.AutoSize = true;
            this.TargetStateIdLabel.Location = new System.Drawing.Point(392, 52);
            this.TargetStateIdLabel.Name = "TargetStateIdLabel";
            this.TargetStateIdLabel.Size = new System.Drawing.Size(59, 13);
            this.TargetStateIdLabel.TabIndex = 11;
            this.TargetStateIdLabel.Text = "Cél állapot:";
            // 
            // SourceStateIdComboBox
            // 
            this.SourceStateIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SourceStateIdComboBox.FormattingEnabled = true;
            this.SourceStateIdComboBox.Location = new System.Drawing.Point(395, 28);
            this.SourceStateIdComboBox.Name = "SourceStateIdComboBox";
            this.SourceStateIdComboBox.Size = new System.Drawing.Size(157, 21);
            this.SourceStateIdComboBox.TabIndex = 12;
            this.SourceStateIdComboBox.SelectedIndexChanged += new System.EventHandler(this.SourceStateIdComboBox_SelectedIndexChanged);
            // 
            // TargetStateIdComboBox
            // 
            this.TargetStateIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TargetStateIdComboBox.FormattingEnabled = true;
            this.TargetStateIdComboBox.Location = new System.Drawing.Point(395, 68);
            this.TargetStateIdComboBox.Name = "TargetStateIdComboBox";
            this.TargetStateIdComboBox.Size = new System.Drawing.Size(157, 21);
            this.TargetStateIdComboBox.TabIndex = 13;
            this.TargetStateIdComboBox.SelectedIndexChanged += new System.EventHandler(this.TargetStateIdComboBox_SelectedIndexChanged);
            // 
            // InputSymbolsDescriptionLabel
            // 
            this.InputSymbolsDescriptionLabel.AutoSize = true;
            this.InputSymbolsDescriptionLabel.Location = new System.Drawing.Point(392, 131);
            this.InputSymbolsDescriptionLabel.Name = "InputSymbolsDescriptionLabel";
            this.InputSymbolsDescriptionLabel.Size = new System.Drawing.Size(138, 13);
            this.InputSymbolsDescriptionLabel.TabIndex = 14;
            this.InputSymbolsDescriptionLabel.Text = "Elérhető input szimbólumok:";
            // 
            // AvailableInputSymbolsLabel
            // 
            this.AvailableInputSymbolsLabel.Location = new System.Drawing.Point(392, 144);
            this.AvailableInputSymbolsLabel.Name = "AvailableInputSymbolsLabel";
            this.AvailableInputSymbolsLabel.Size = new System.Drawing.Size(160, 54);
            this.AvailableInputSymbolsLabel.TabIndex = 15;
            // 
            // InputSymbolsFieldTextBox
            // 
            this.InputSymbolsFieldTextBox.Location = new System.Drawing.Point(395, 108);
            this.InputSymbolsFieldTextBox.Name = "InputSymbolsFieldTextBox";
            this.InputSymbolsFieldTextBox.Size = new System.Drawing.Size(157, 20);
            this.InputSymbolsFieldTextBox.TabIndex = 16;
            this.InputSymbolsFieldTextBox.TextChanged += new System.EventHandler(this.InputSymbolsFieldTextBox_TextChanged);
            // 
            // InputSymbolsFieldLabel
            // 
            this.InputSymbolsFieldLabel.AutoSize = true;
            this.InputSymbolsFieldLabel.Location = new System.Drawing.Point(392, 92);
            this.InputSymbolsFieldLabel.Name = "InputSymbolsFieldLabel";
            this.InputSymbolsFieldLabel.Size = new System.Drawing.Size(97, 13);
            this.InputSymbolsFieldLabel.TabIndex = 17;
            this.InputSymbolsFieldLabel.Text = "Input szimbólumok:";
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(392, 216);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(160, 62);
            this.ErrorLabel.TabIndex = 18;
            // 
            // DrawPanel
            // 
            this.DrawPanel.Location = new System.Drawing.Point(12, 12);
            this.DrawPanel.Name = "DrawPanel";
            this.DrawPanel.Size = new System.Drawing.Size(374, 461);
            this.DrawPanel.TabIndex = 19;
            this.DrawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPanel_Paint);
            // 
            // NewTransitionForm
            // 
            this.AcceptButton = this.CreateTransitionButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelCreationButton;
            this.ClientSize = new System.Drawing.Size(564, 485);
            this.ControlBox = false;
            this.Controls.Add(this.DrawPanel);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.InputSymbolsFieldLabel);
            this.Controls.Add(this.InputSymbolsFieldTextBox);
            this.Controls.Add(this.AvailableInputSymbolsLabel);
            this.Controls.Add(this.InputSymbolsDescriptionLabel);
            this.Controls.Add(this.TargetStateIdComboBox);
            this.Controls.Add(this.SourceStateIdComboBox);
            this.Controls.Add(this.TargetStateIdLabel);
            this.Controls.Add(this.CreateTransitionButton);
            this.Controls.Add(this.CancelCreationButton);
            this.Controls.Add(this.SourceStateIdLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewTransitionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Átmenet hozzáadása";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label SourceStateIdLabel;
        private System.Windows.Forms.Button CreateTransitionButton;
        private System.Windows.Forms.Button CancelCreationButton;
        private System.Windows.Forms.Label TargetStateIdLabel;
        private System.Windows.Forms.ComboBox SourceStateIdComboBox;
        private System.Windows.Forms.ComboBox TargetStateIdComboBox;
        private System.Windows.Forms.Label InputSymbolsDescriptionLabel;
        private System.Windows.Forms.Label AvailableInputSymbolsLabel;
        private System.Windows.Forms.TextBox InputSymbolsFieldTextBox;
        private System.Windows.Forms.Label InputSymbolsFieldLabel;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Panel DrawPanel;
    }
}