namespace Automata.Simulator.Form
{
    partial class TransitionResolverForm
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
            this.SelectTransitionButton = new System.Windows.Forms.Button();
            this.DrawPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TransitionComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // SelectTransitionButton
            // 
            this.SelectTransitionButton.Location = new System.Drawing.Point(477, 450);
            this.SelectTransitionButton.Name = "SelectTransitionButton";
            this.SelectTransitionButton.Size = new System.Drawing.Size(75, 23);
            this.SelectTransitionButton.TabIndex = 6;
            this.SelectTransitionButton.Text = "Kiválasztás";
            this.SelectTransitionButton.UseVisualStyleBackColor = true;
            this.SelectTransitionButton.Click += new System.EventHandler(this.SelectTransitionButton_Click);
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
            this.label1.Location = new System.Drawing.Point(392, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Kiválasztott átmenet:";
            // 
            // TransitionComboBox
            // 
            this.TransitionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TransitionComboBox.FormattingEnabled = true;
            this.TransitionComboBox.Location = new System.Drawing.Point(395, 28);
            this.TransitionComboBox.Name = "TransitionComboBox";
            this.TransitionComboBox.Size = new System.Drawing.Size(160, 21);
            this.TransitionComboBox.TabIndex = 21;
            this.TransitionComboBox.SelectedIndexChanged += new System.EventHandler(this.TransitionComboBox_SelectedIndexChanged);
            // 
            // TransitionResolverForm
            // 
            this.AcceptButton = this.SelectTransitionButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 485);
            this.ControlBox = false;
            this.Controls.Add(this.TransitionComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DrawPanel);
            this.Controls.Add(this.SelectTransitionButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TransitionResolverForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Átmenet kiválasztása";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SelectTransitionButton;
        private System.Windows.Forms.Panel DrawPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TransitionComboBox;
    }
}