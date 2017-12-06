﻿namespace Automata.Simulator.Form
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.ManageGroupBox = new System.Windows.Forms.GroupBox();
            this.NewButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.graphViewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            this.loadAutomataDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveAutomataDialog = new System.Windows.Forms.SaveFileDialog();
            this.DrawPanel = new System.Windows.Forms.Panel();
            this.EditGroupBox = new System.Windows.Forms.GroupBox();
            this.NewStateButton = new System.Windows.Forms.Button();
            this.DeleteStateButton = new System.Windows.Forms.Button();
            this.NewTransitionButton = new System.Windows.Forms.Button();
            this.DeleteTransitionButton = new System.Windows.Forms.Button();
            this.SimulationGroupBox = new System.Windows.Forms.GroupBox();
            this.StartNewSimulationButton = new System.Windows.Forms.Button();
            this.SimulationStepButton = new System.Windows.Forms.Button();
            this.StopSimulationButton = new System.Windows.Forms.Button();
            this.SimulationStepMethodComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SimulationSpeedTrackBar = new System.Windows.Forms.TrackBar();
            this.ManageGroupBox.SuspendLayout();
            this.EditGroupBox.SuspendLayout();
            this.SimulationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SimulationSpeedTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ManageGroupBox
            // 
            this.ManageGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ManageGroupBox.Controls.Add(this.NewButton);
            this.ManageGroupBox.Controls.Add(this.SaveButton);
            this.ManageGroupBox.Controls.Add(this.LoadButton);
            this.ManageGroupBox.Location = new System.Drawing.Point(1058, 12);
            this.ManageGroupBox.Name = "ManageGroupBox";
            this.ManageGroupBox.Size = new System.Drawing.Size(194, 110);
            this.ManageGroupBox.TabIndex = 0;
            this.ManageGroupBox.TabStop = false;
            this.ManageGroupBox.Text = "Kezelés";
            // 
            // NewButton
            // 
            this.NewButton.Location = new System.Drawing.Point(6, 19);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(182, 23);
            this.NewButton.TabIndex = 2;
            this.NewButton.Text = "Új automata létrehozása";
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(6, 77);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(182, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Mentés";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(6, 48);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(182, 23);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Betöltés";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // graphViewer
            // 
            this.graphViewer.ArrowheadLength = 10D;
            this.graphViewer.AsyncLayout = false;
            this.graphViewer.AutoScroll = true;
            this.graphViewer.BackwardEnabled = false;
            this.graphViewer.BuildHitTree = true;
            this.graphViewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.UseSettingsOfTheGraph;
            this.graphViewer.EdgeInsertButtonVisible = false;
            this.graphViewer.FileName = "";
            this.graphViewer.ForwardEnabled = false;
            this.graphViewer.Graph = null;
            this.graphViewer.InsertingEdge = false;
            this.graphViewer.LayoutAlgorithmSettingsButtonVisible = true;
            this.graphViewer.LayoutEditingEnabled = true;
            this.graphViewer.Location = new System.Drawing.Point(12, 12);
            this.graphViewer.LooseOffsetForRouting = 0.25D;
            this.graphViewer.MouseHitDistance = 0.05D;
            this.graphViewer.Name = "graphViewer";
            this.graphViewer.NavigationVisible = true;
            this.graphViewer.NeedToCalculateLayout = true;
            this.graphViewer.OffsetForRelaxingInRouting = 0.6D;
            this.graphViewer.PaddingForEdgeRouting = 8D;
            this.graphViewer.PanButtonPressed = false;
            this.graphViewer.SaveAsImageEnabled = false;
            this.graphViewer.SaveAsMsaglEnabled = false;
            this.graphViewer.SaveButtonVisible = false;
            this.graphViewer.SaveGraphButtonVisible = false;
            this.graphViewer.SaveInVectorFormatEnabled = false;
            this.graphViewer.Size = new System.Drawing.Size(1034, 657);
            this.graphViewer.TabIndex = 1;
            this.graphViewer.TightOffsetForRouting = 0.125D;
            this.graphViewer.ToolBarIsVisible = false;
            this.graphViewer.Transform = ((Microsoft.Msagl.Core.Geometry.Curves.PlaneTransformation)(resources.GetObject("graphViewer.Transform")));
            this.graphViewer.UndoRedoButtonsVisible = false;
            this.graphViewer.WindowZoomButtonPressed = false;
            this.graphViewer.ZoomF = 1D;
            this.graphViewer.ZoomWindowThreshold = 9999999999D;
            // 
            // loadAutomataDialog
            // 
            this.loadAutomataDialog.Filter = "Gráfok|*.gml";
            // 
            // saveAutomataDialog
            // 
            this.saveAutomataDialog.DefaultExt = "gml";
            this.saveAutomataDialog.Filter = "Gráfok|*.gml";
            // 
            // DrawPanel
            // 
            this.DrawPanel.Location = new System.Drawing.Point(12, 12);
            this.DrawPanel.Name = "DrawPanel";
            this.DrawPanel.Size = new System.Drawing.Size(1040, 657);
            this.DrawPanel.TabIndex = 2;
            // 
            // EditGroupBox
            // 
            this.EditGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EditGroupBox.Controls.Add(this.DeleteTransitionButton);
            this.EditGroupBox.Controls.Add(this.NewStateButton);
            this.EditGroupBox.Controls.Add(this.DeleteStateButton);
            this.EditGroupBox.Controls.Add(this.NewTransitionButton);
            this.EditGroupBox.Location = new System.Drawing.Point(1058, 128);
            this.EditGroupBox.Name = "EditGroupBox";
            this.EditGroupBox.Size = new System.Drawing.Size(194, 139);
            this.EditGroupBox.TabIndex = 3;
            this.EditGroupBox.TabStop = false;
            this.EditGroupBox.Text = "Szerkesztés";
            // 
            // NewStateButton
            // 
            this.NewStateButton.Location = new System.Drawing.Point(6, 19);
            this.NewStateButton.Name = "NewStateButton";
            this.NewStateButton.Size = new System.Drawing.Size(182, 23);
            this.NewStateButton.TabIndex = 2;
            this.NewStateButton.Text = "Új állapot hozzáadása";
            this.NewStateButton.UseVisualStyleBackColor = true;
            this.NewStateButton.Click += new System.EventHandler(this.NewStateButton_Click);
            // 
            // DeleteStateButton
            // 
            this.DeleteStateButton.Location = new System.Drawing.Point(6, 77);
            this.DeleteStateButton.Name = "DeleteStateButton";
            this.DeleteStateButton.Size = new System.Drawing.Size(182, 23);
            this.DeleteStateButton.TabIndex = 1;
            this.DeleteStateButton.Text = "Állapot törlése";
            this.DeleteStateButton.UseVisualStyleBackColor = true;
            this.DeleteStateButton.Click += new System.EventHandler(this.DeleteStateButton_Click);
            // 
            // NewTransitionButton
            // 
            this.NewTransitionButton.Location = new System.Drawing.Point(6, 48);
            this.NewTransitionButton.Name = "NewTransitionButton";
            this.NewTransitionButton.Size = new System.Drawing.Size(182, 23);
            this.NewTransitionButton.TabIndex = 0;
            this.NewTransitionButton.Text = "Új átmenet hozzáadása";
            this.NewTransitionButton.UseVisualStyleBackColor = true;
            this.NewTransitionButton.Click += new System.EventHandler(this.NewTransitionButton_Click);
            // 
            // DeleteTransitionButton
            // 
            this.DeleteTransitionButton.Location = new System.Drawing.Point(6, 106);
            this.DeleteTransitionButton.Name = "DeleteTransitionButton";
            this.DeleteTransitionButton.Size = new System.Drawing.Size(182, 23);
            this.DeleteTransitionButton.TabIndex = 3;
            this.DeleteTransitionButton.Text = "Átmenet törlése";
            this.DeleteTransitionButton.UseVisualStyleBackColor = true;
            this.DeleteTransitionButton.Click += new System.EventHandler(this.DeleteTransitionButton_Click);
            // 
            // SimulationGroupBox
            // 
            this.SimulationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SimulationGroupBox.Controls.Add(this.SimulationSpeedTrackBar);
            this.SimulationGroupBox.Controls.Add(this.label1);
            this.SimulationGroupBox.Controls.Add(this.SimulationStepMethodComboBox);
            this.SimulationGroupBox.Controls.Add(this.StartNewSimulationButton);
            this.SimulationGroupBox.Controls.Add(this.SimulationStepButton);
            this.SimulationGroupBox.Controls.Add(this.StopSimulationButton);
            this.SimulationGroupBox.Location = new System.Drawing.Point(1058, 273);
            this.SimulationGroupBox.Name = "SimulationGroupBox";
            this.SimulationGroupBox.Size = new System.Drawing.Size(194, 216);
            this.SimulationGroupBox.TabIndex = 4;
            this.SimulationGroupBox.TabStop = false;
            this.SimulationGroupBox.Text = "Szimuláció";
            // 
            // StartNewSimulationButton
            // 
            this.StartNewSimulationButton.Location = new System.Drawing.Point(6, 19);
            this.StartNewSimulationButton.Name = "StartNewSimulationButton";
            this.StartNewSimulationButton.Size = new System.Drawing.Size(182, 23);
            this.StartNewSimulationButton.TabIndex = 2;
            this.StartNewSimulationButton.Text = "Szimláció indítása";
            this.StartNewSimulationButton.UseVisualStyleBackColor = true;
            this.StartNewSimulationButton.Click += new System.EventHandler(this.StartNewSimulation_Click);
            // 
            // SimulationStepButton
            // 
            this.SimulationStepButton.Location = new System.Drawing.Point(6, 187);
            this.SimulationStepButton.Name = "SimulationStepButton";
            this.SimulationStepButton.Size = new System.Drawing.Size(182, 23);
            this.SimulationStepButton.TabIndex = 1;
            this.SimulationStepButton.Text = "Léptetés";
            this.SimulationStepButton.UseVisualStyleBackColor = true;
            // 
            // StopSimulationButton
            // 
            this.StopSimulationButton.Location = new System.Drawing.Point(6, 48);
            this.StopSimulationButton.Name = "StopSimulationButton";
            this.StopSimulationButton.Size = new System.Drawing.Size(182, 23);
            this.StopSimulationButton.TabIndex = 0;
            this.StopSimulationButton.Text = "Szimuláció leállítása";
            this.StopSimulationButton.UseVisualStyleBackColor = true;
            // 
            // SimulationStepMethodComboBox
            // 
            this.SimulationStepMethodComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SimulationStepMethodComboBox.FormattingEnabled = true;
            this.SimulationStepMethodComboBox.Location = new System.Drawing.Point(6, 102);
            this.SimulationStepMethodComboBox.Name = "SimulationStepMethodComboBox";
            this.SimulationStepMethodComboBox.Size = new System.Drawing.Size(182, 21);
            this.SimulationStepMethodComboBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Szimuláció léptetésének módja:";
            // 
            // SimulationSpeedTrackBar
            // 
            this.SimulationSpeedTrackBar.LargeChange = 10;
            this.SimulationSpeedTrackBar.Location = new System.Drawing.Point(6, 136);
            this.SimulationSpeedTrackBar.Maximum = 60;
            this.SimulationSpeedTrackBar.Minimum = 1;
            this.SimulationSpeedTrackBar.Name = "SimulationSpeedTrackBar";
            this.SimulationSpeedTrackBar.Size = new System.Drawing.Size(182, 45);
            this.SimulationSpeedTrackBar.TabIndex = 5;
            this.SimulationSpeedTrackBar.TickFrequency = 5;
            this.SimulationSpeedTrackBar.Value = 1;
            this.SimulationSpeedTrackBar.Scroll += new System.EventHandler(this.SimulationSpeedTrackBar_Scroll);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.SimulationGroupBox);
            this.Controls.Add(this.EditGroupBox);
            this.Controls.Add(this.DrawPanel);
            this.Controls.Add(this.graphViewer);
            this.Controls.Add(this.ManageGroupBox);
            this.Name = "MainWindow";
            this.Text = "Automata Szimulátor";
            this.ManageGroupBox.ResumeLayout(false);
            this.EditGroupBox.ResumeLayout(false);
            this.SimulationGroupBox.ResumeLayout(false);
            this.SimulationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SimulationSpeedTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ManageGroupBox;
        private Microsoft.Msagl.GraphViewerGdi.GViewer graphViewer;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.OpenFileDialog loadAutomataDialog;
        private System.Windows.Forms.SaveFileDialog saveAutomataDialog;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.Panel DrawPanel;
        private System.Windows.Forms.GroupBox EditGroupBox;
        private System.Windows.Forms.Button NewStateButton;
        private System.Windows.Forms.Button DeleteStateButton;
        private System.Windows.Forms.Button NewTransitionButton;
        private System.Windows.Forms.Button DeleteTransitionButton;
        private System.Windows.Forms.GroupBox SimulationGroupBox;
        private System.Windows.Forms.Button StartNewSimulationButton;
        private System.Windows.Forms.Button SimulationStepButton;
        private System.Windows.Forms.Button StopSimulationButton;
        private System.Windows.Forms.ComboBox SimulationStepMethodComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar SimulationSpeedTrackBar;
    }
}
