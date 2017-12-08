namespace Automata.Simulator.Form
{
    partial class DeleteStateForm
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
            this.DeleteStateButton = new System.Windows.Forms.Button();
            this.CancelDeletionButton = new System.Windows.Forms.Button();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.DrawPanel = new System.Windows.Forms.Panel();
            this.DeleteStateLabel = new System.Windows.Forms.Label();
            this.DeleteStateComboBox = new System.Windows.Forms.ComboBox();
            this.StateDeletionWarningLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DeleteStateButton
            // 
            this.DeleteStateButton.Location = new System.Drawing.Point(477, 450);
            this.DeleteStateButton.Name = "DeleteStateButton";
            this.DeleteStateButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteStateButton.TabIndex = 6;
            this.DeleteStateButton.Text = "Törlés";
            this.DeleteStateButton.UseVisualStyleBackColor = true;
            this.DeleteStateButton.Click += new System.EventHandler(this.DeleteStateButton_Click);
            // 
            // CancelDeletionButton
            // 
            this.CancelDeletionButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelDeletionButton.Location = new System.Drawing.Point(392, 450);
            this.CancelDeletionButton.Name = "CancelDeletionButton";
            this.CancelDeletionButton.Size = new System.Drawing.Size(75, 23);
            this.CancelDeletionButton.TabIndex = 5;
            this.CancelDeletionButton.Text = "Mégse";
            this.CancelDeletionButton.UseVisualStyleBackColor = true;
            this.CancelDeletionButton.Click += new System.EventHandler(this.CancelDeletionButton_Click);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(392, 158);
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
            // DeleteStateLabel
            // 
            this.DeleteStateLabel.AutoSize = true;
            this.DeleteStateLabel.Location = new System.Drawing.Point(389, 9);
            this.DeleteStateLabel.Name = "DeleteStateLabel";
            this.DeleteStateLabel.Size = new System.Drawing.Size(86, 13);
            this.DeleteStateLabel.TabIndex = 21;
            this.DeleteStateLabel.Text = "Törlendő állapot:";
            // 
            // DeleteStateComboBox
            // 
            this.DeleteStateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeleteStateComboBox.FormattingEnabled = true;
            this.DeleteStateComboBox.Location = new System.Drawing.Point(392, 25);
            this.DeleteStateComboBox.Name = "DeleteStateComboBox";
            this.DeleteStateComboBox.Size = new System.Drawing.Size(160, 21);
            this.DeleteStateComboBox.TabIndex = 22;
            this.DeleteStateComboBox.SelectedIndexChanged += new System.EventHandler(this.DeleteStateComboBox_SelectedIndexChanged);
            // 
            // StateDeletionWarningLabel
            // 
            this.StateDeletionWarningLabel.Location = new System.Drawing.Point(392, 59);
            this.StateDeletionWarningLabel.Name = "StateDeletionWarningLabel";
            this.StateDeletionWarningLabel.Size = new System.Drawing.Size(160, 90);
            this.StateDeletionWarningLabel.TabIndex = 23;
            this.StateDeletionWarningLabel.Text = "Megjegyzés: Az összes átmenet, ami értinti ezt az állapotot, törölve lesz.";
            // 
            // DeleteStateForm
            // 
            this.AcceptButton = this.DeleteStateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelDeletionButton;
            this.ClientSize = new System.Drawing.Size(564, 485);
            this.ControlBox = false;
            this.Controls.Add(this.StateDeletionWarningLabel);
            this.Controls.Add(this.DeleteStateComboBox);
            this.Controls.Add(this.DeleteStateLabel);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.DrawPanel);
            this.Controls.Add(this.DeleteStateButton);
            this.Controls.Add(this.CancelDeletionButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DeleteStateForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Állapot törlése";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button DeleteStateButton;
        private System.Windows.Forms.Button CancelDeletionButton;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Panel DrawPanel;
        private System.Windows.Forms.Label DeleteStateLabel;
        private System.Windows.Forms.ComboBox DeleteStateComboBox;
        private System.Windows.Forms.Label StateDeletionWarningLabel;
    }
}