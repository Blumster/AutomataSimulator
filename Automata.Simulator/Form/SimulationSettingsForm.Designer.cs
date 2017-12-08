﻿namespace Automata.Simulator.Form
{
    partial class SimulationSettingsForm
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
            this.CancelSimulationButton = new System.Windows.Forms.Button();
            this.StartSimulationButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SimulationInputTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CancelSimulationButton
            // 
            this.CancelSimulationButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelSimulationButton.Location = new System.Drawing.Point(116, 226);
            this.CancelSimulationButton.Name = "CancelSimulationButton";
            this.CancelSimulationButton.Size = new System.Drawing.Size(75, 23);
            this.CancelSimulationButton.TabIndex = 0;
            this.CancelSimulationButton.Text = "Mégse";
            this.CancelSimulationButton.UseVisualStyleBackColor = true;
            this.CancelSimulationButton.Click += new System.EventHandler(this.CancelSimulationButton_Click);
            // 
            // StartSimulationButton
            // 
            this.StartSimulationButton.Location = new System.Drawing.Point(197, 226);
            this.StartSimulationButton.Name = "StartSimulationButton";
            this.StartSimulationButton.Size = new System.Drawing.Size(75, 23);
            this.StartSimulationButton.TabIndex = 1;
            this.StartSimulationButton.Text = "Indítás";
            this.StartSimulationButton.UseVisualStyleBackColor = true;
            this.StartSimulationButton.Click += new System.EventHandler(this.StartSimulationButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Input:";
            // 
            // SimulationInputTextBox
            // 
            this.SimulationInputTextBox.Location = new System.Drawing.Point(13, 26);
            this.SimulationInputTextBox.Name = "SimulationInputTextBox";
            this.SimulationInputTextBox.Size = new System.Drawing.Size(259, 20);
            this.SimulationInputTextBox.TabIndex = 3;
            // 
            // SimulationSettingsForm
            // 
            this.AcceptButton = this.StartSimulationButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelSimulationButton;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.SimulationInputTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StartSimulationButton);
            this.Controls.Add(this.CancelSimulationButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SimulationSettingsForm";
            this.ShowIcon = false;
            this.Text = "Szimuláció indítása";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelSimulationButton;
        private System.Windows.Forms.Button StartSimulationButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SimulationInputTextBox;
    }
}