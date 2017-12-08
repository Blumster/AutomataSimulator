namespace Automata.Simulator.Form
{
    partial class NewStateForm
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
            this.StateIdTextBox = new System.Windows.Forms.TextBox();
            this.StateIdLabel = new System.Windows.Forms.Label();
            this.CreateStateButton = new System.Windows.Forms.Button();
            this.CancelCreationButton = new System.Windows.Forms.Button();
            this.IsStartStateCheckBox = new System.Windows.Forms.CheckBox();
            this.IsAcceptStateCheckBox = new System.Windows.Forms.CheckBox();
            this.StartStateWarningLabel = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.DrawPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // StateIdTextBox
            // 
            this.StateIdTextBox.Location = new System.Drawing.Point(392, 28);
            this.StateIdTextBox.MaxLength = 3;
            this.StateIdTextBox.Name = "StateIdTextBox";
            this.StateIdTextBox.Size = new System.Drawing.Size(160, 20);
            this.StateIdTextBox.TabIndex = 0;
            this.StateIdTextBox.TextChanged += new System.EventHandler(this.StateIdTextBox_TextChanged);
            // 
            // StateIdLabel
            // 
            this.StateIdLabel.AutoSize = true;
            this.StateIdLabel.Location = new System.Drawing.Point(389, 12);
            this.StateIdLabel.Name = "StateIdLabel";
            this.StateIdLabel.Size = new System.Drawing.Size(81, 13);
            this.StateIdLabel.TabIndex = 1;
            this.StateIdLabel.Text = "Új állapot neve:";
            // 
            // CreateStateButton
            // 
            this.CreateStateButton.Location = new System.Drawing.Point(477, 450);
            this.CreateStateButton.Name = "CreateStateButton";
            this.CreateStateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateStateButton.TabIndex = 6;
            this.CreateStateButton.Text = "Létrehozás";
            this.CreateStateButton.UseVisualStyleBackColor = true;
            this.CreateStateButton.Click += new System.EventHandler(this.CreateStateButton_Click);
            // 
            // CancelCreationButton
            // 
            this.CancelCreationButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelCreationButton.Location = new System.Drawing.Point(392, 450);
            this.CancelCreationButton.Name = "CancelCreationButton";
            this.CancelCreationButton.Size = new System.Drawing.Size(75, 23);
            this.CancelCreationButton.TabIndex = 5;
            this.CancelCreationButton.Text = "Mégse";
            this.CancelCreationButton.UseVisualStyleBackColor = true;
            this.CancelCreationButton.Click += new System.EventHandler(this.CancelCreationButton_Click);
            // 
            // IsStartStateCheckBox
            // 
            this.IsStartStateCheckBox.AutoSize = true;
            this.IsStartStateCheckBox.Location = new System.Drawing.Point(392, 54);
            this.IsStartStateCheckBox.Name = "IsStartStateCheckBox";
            this.IsStartStateCheckBox.Size = new System.Drawing.Size(87, 17);
            this.IsStartStateCheckBox.TabIndex = 7;
            this.IsStartStateCheckBox.Text = "Kezdőállapot";
            this.IsStartStateCheckBox.UseVisualStyleBackColor = true;
            this.IsStartStateCheckBox.CheckedChanged += new System.EventHandler(this.IsStartStateCheckBox_CheckedChanged);
            // 
            // IsAcceptStateCheckBox
            // 
            this.IsAcceptStateCheckBox.AutoSize = true;
            this.IsAcceptStateCheckBox.Location = new System.Drawing.Point(392, 77);
            this.IsAcceptStateCheckBox.Name = "IsAcceptStateCheckBox";
            this.IsAcceptStateCheckBox.Size = new System.Drawing.Size(76, 17);
            this.IsAcceptStateCheckBox.TabIndex = 8;
            this.IsAcceptStateCheckBox.Text = "Végállapot";
            this.IsAcceptStateCheckBox.UseVisualStyleBackColor = true;
            this.IsAcceptStateCheckBox.CheckedChanged += new System.EventHandler(this.IsAcceptStateCheckBox_CheckedChanged);
            // 
            // StartStateWarningLabel
            // 
            this.StartStateWarningLabel.Location = new System.Drawing.Point(392, 97);
            this.StartStateWarningLabel.Name = "StartStateWarningLabel";
            this.StartStateWarningLabel.Size = new System.Drawing.Size(160, 87);
            this.StartStateWarningLabel.TabIndex = 9;
            this.StartStateWarningLabel.Text = "Mivel már létezik kezdőállapot az automatában, ha az új csúcs is kezdőállapotként" +
    " adódik hozzá, akkor a régi állapot már nem lesz kezdőállapot!";
            this.StartStateWarningLabel.Visible = false;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(392, 196);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(160, 88);
            this.ErrorLabel.TabIndex = 10;
            this.ErrorLabel.Visible = false;
            // 
            // DrawPanel
            // 
            this.DrawPanel.Location = new System.Drawing.Point(12, 12);
            this.DrawPanel.Name = "DrawPanel";
            this.DrawPanel.Size = new System.Drawing.Size(374, 461);
            this.DrawPanel.TabIndex = 20;
            this.DrawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPanel_Paint);
            // 
            // NewStateForm
            // 
            this.AcceptButton = this.CreateStateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelCreationButton;
            this.ClientSize = new System.Drawing.Size(564, 485);
            this.ControlBox = false;
            this.Controls.Add(this.StateIdLabel);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.DrawPanel);
            this.Controls.Add(this.StateIdTextBox);
            this.Controls.Add(this.IsStartStateCheckBox);
            this.Controls.Add(this.StartStateWarningLabel);
            this.Controls.Add(this.CreateStateButton);
            this.Controls.Add(this.CancelCreationButton);
            this.Controls.Add(this.IsAcceptStateCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewStateForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Állapot hozzáadása";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox StateIdTextBox;
        private System.Windows.Forms.Label StateIdLabel;
        private System.Windows.Forms.Button CreateStateButton;
        private System.Windows.Forms.Button CancelCreationButton;
        private System.Windows.Forms.CheckBox IsStartStateCheckBox;
        private System.Windows.Forms.CheckBox IsAcceptStateCheckBox;
        private System.Windows.Forms.Label StartStateWarningLabel;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Panel DrawPanel;
    }
}