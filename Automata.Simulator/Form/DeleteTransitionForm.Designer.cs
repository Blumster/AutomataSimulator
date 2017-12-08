namespace Automata.Simulator.Form
{
    partial class DeleteTransitionForm
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
            this.DeleteTransitionButton = new System.Windows.Forms.Button();
            this.CancelDeletionButton = new System.Windows.Forms.Button();
            this.SourceStateIdComboBox = new System.Windows.Forms.ComboBox();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.DrawPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TransitionComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // SourceStateIdLabel
            // 
            this.SourceStateIdLabel.AutoSize = true;
            this.SourceStateIdLabel.Location = new System.Drawing.Point(389, 12);
            this.SourceStateIdLabel.Name = "SourceStateIdLabel";
            this.SourceStateIdLabel.Size = new System.Drawing.Size(73, 13);
            this.SourceStateIdLabel.TabIndex = 1;
            this.SourceStateIdLabel.Text = "Forrás állapot:";
            // 
            // DeleteTransitionButton
            // 
            this.DeleteTransitionButton.Location = new System.Drawing.Point(477, 450);
            this.DeleteTransitionButton.Name = "DeleteTransitionButton";
            this.DeleteTransitionButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteTransitionButton.TabIndex = 6;
            this.DeleteTransitionButton.Text = "Törlés";
            this.DeleteTransitionButton.UseVisualStyleBackColor = true;
            this.DeleteTransitionButton.Click += new System.EventHandler(this.DeleteTransitionButton_Click);
            // 
            // CancelDeletionButton
            // 
            this.CancelDeletionButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelDeletionButton.Location = new System.Drawing.Point(395, 450);
            this.CancelDeletionButton.Name = "CancelDeletionButton";
            this.CancelDeletionButton.Size = new System.Drawing.Size(75, 23);
            this.CancelDeletionButton.TabIndex = 5;
            this.CancelDeletionButton.Text = "Mégse";
            this.CancelDeletionButton.UseVisualStyleBackColor = true;
            this.CancelDeletionButton.Click += new System.EventHandler(this.CancelDeletionButton_Click);
            // 
            // SourceStateIdComboBox
            // 
            this.SourceStateIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SourceStateIdComboBox.FormattingEnabled = true;
            this.SourceStateIdComboBox.Location = new System.Drawing.Point(392, 28);
            this.SourceStateIdComboBox.Name = "SourceStateIdComboBox";
            this.SourceStateIdComboBox.Size = new System.Drawing.Size(160, 21);
            this.SourceStateIdComboBox.TabIndex = 12;
            this.SourceStateIdComboBox.SelectedIndexChanged += new System.EventHandler(this.SourceStateIdComboBox_SelectedIndexChanged);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(389, 92);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(163, 62);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Törlendő átmenet:";
            // 
            // TransitionComboBox
            // 
            this.TransitionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TransitionComboBox.FormattingEnabled = true;
            this.TransitionComboBox.Location = new System.Drawing.Point(392, 68);
            this.TransitionComboBox.Name = "TransitionComboBox";
            this.TransitionComboBox.Size = new System.Drawing.Size(160, 21);
            this.TransitionComboBox.TabIndex = 21;
            this.TransitionComboBox.SelectedIndexChanged += new System.EventHandler(this.TransitionComboBox_SelectedIndexChanged);
            // 
            // DeleteTransitionForm
            // 
            this.AcceptButton = this.DeleteTransitionButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelDeletionButton;
            this.ClientSize = new System.Drawing.Size(564, 485);
            this.ControlBox = false;
            this.Controls.Add(this.TransitionComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DrawPanel);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.SourceStateIdComboBox);
            this.Controls.Add(this.DeleteTransitionButton);
            this.Controls.Add(this.CancelDeletionButton);
            this.Controls.Add(this.SourceStateIdLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DeleteTransitionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Átmenet törlése";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label SourceStateIdLabel;
        private System.Windows.Forms.Button DeleteTransitionButton;
        private System.Windows.Forms.Button CancelDeletionButton;
        private System.Windows.Forms.ComboBox SourceStateIdComboBox;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Panel DrawPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TransitionComboBox;
    }
}