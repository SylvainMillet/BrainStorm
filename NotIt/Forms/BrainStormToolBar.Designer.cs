namespace Smilly.BrainStorm.Forms
{
    /// <summary>
    /// ToolBar de l'application.
    /// Fournis l'accès aux fonctionnalités de base de l'application.
    /// </summary>
    partial class BrainStormToolBar
    {

        /// Required designer variable.

        private System.ComponentModel.IContainer components = null;


        /// Clean up any resources being used.

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


        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrainStormToolBar));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonMemo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAgrement = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonIdea = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNot = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.hideAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showListToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.optionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.topToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.hideMemoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.hideAgrementToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.hideIdeaToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.hideNotToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.showMemoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.showAgrementToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.showIdeaToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.showNotToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStripContainer2.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonMemo,
            this.toolStripButtonAgrement,
            this.toolStripButtonIdea,
            this.toolStripButtonNot,
            this.toolStripSeparator2,
            this.showAllToolStripButton,
            this.hideAllToolStripButton,
            this.toolStripSeparator1,
            this.showListToolStripButton,
            this.toolStripSeparator3,
            this.optionToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(3, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(318, 39);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonMemo
            // 
            this.toolStripButtonMemo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMemo.Image = global::Smilly.BrainStorm.Properties.Resources.Memo;
            this.toolStripButtonMemo.ImageTransparentColor = System.Drawing.Color.CornflowerBlue;
            this.toolStripButtonMemo.Name = "toolStripButtonMemo";
            this.toolStripButtonMemo.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonMemo.Text = "New Memo";
            this.toolStripButtonMemo.Click += new System.EventHandler(this.toolStripButtonMemo_Click);
            // 
            // toolStripButtonAgrement
            // 
            this.toolStripButtonAgrement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAgrement.Image = global::Smilly.BrainStorm.Properties.Resources.Agrement;
            this.toolStripButtonAgrement.ImageTransparentColor = System.Drawing.Color.PaleGreen;
            this.toolStripButtonAgrement.Name = "toolStripButtonAgrement";
            this.toolStripButtonAgrement.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonAgrement.Text = "New Agrement";
            this.toolStripButtonAgrement.Click += new System.EventHandler(this.toolStripButtonAgrement_Click);
            // 
            // toolStripButtonIdea
            // 
            this.toolStripButtonIdea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonIdea.Image = global::Smilly.BrainStorm.Properties.Resources.Idea;
            this.toolStripButtonIdea.ImageTransparentColor = System.Drawing.Color.Tomato;
            this.toolStripButtonIdea.Name = "toolStripButtonIdea";
            this.toolStripButtonIdea.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonIdea.Text = "New Idea";
            this.toolStripButtonIdea.Click += new System.EventHandler(this.toolStripButtonIdea_Click);
            // 
            // toolStripButtonNot
            // 
            this.toolStripButtonNot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNot.Image = global::Smilly.BrainStorm.Properties.Resources.Not;
            this.toolStripButtonNot.ImageTransparentColor = System.Drawing.Color.LemonChiffon;
            this.toolStripButtonNot.Name = "toolStripButtonNot";
            this.toolStripButtonNot.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonNot.Text = "New Not";
            this.toolStripButtonNot.Click += new System.EventHandler(this.toolStripButtonNot_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // showAllToolStripButton
            // 
            this.showAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showAllToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("showAllToolStripButton.Image")));
            this.showAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showAllToolStripButton.Name = "showAllToolStripButton";
            this.showAllToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.showAllToolStripButton.Text = "Show all Brains";
            this.showAllToolStripButton.Click += new System.EventHandler(this.showAllToolStripButton_Click);
            // 
            // hideAllToolStripButton
            // 
            this.hideAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hideAllToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("hideAllToolStripButton.Image")));
            this.hideAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hideAllToolStripButton.Name = "hideAllToolStripButton";
            this.hideAllToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.hideAllToolStripButton.Text = "Hide all Brains";
            this.hideAllToolStripButton.Click += new System.EventHandler(this.hideAllToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // showListToolStripButton
            // 
            this.showListToolStripButton.CheckOnClick = true;
            this.showListToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showListToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("showListToolStripButton.Image")));
            this.showListToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showListToolStripButton.Name = "showListToolStripButton";
            this.showListToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.showListToolStripButton.Text = "Show ListBar";
            this.showListToolStripButton.Click += new System.EventHandler(this.showListToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // optionToolStripButton
            // 
            this.optionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.optionToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("optionToolStripButton.Image")));
            this.optionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionToolStripButton.Name = "optionToolStripButton";
            this.optionToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.optionToolStripButton.Text = "Options";
            this.optionToolStripButton.Click += new System.EventHandler(this.optionToolStripButton_Click);
            // 
            // topToolStripMenuItem
            // 
            this.topToolStripMenuItem.Name = "topToolStripMenuItem";
            this.topToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // bottomToolStripMenuItem
            // 
            this.bottomToolStripMenuItem.Name = "bottomToolStripMenuItem";
            this.bottomToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // horizontalToolStripMenuItem
            // 
            this.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            this.horizontalToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(224, 92);
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.toolStripContainer1);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(321, 75);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(321, 114);
            this.toolStripContainer.TabIndex = 2;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.toolStripContainer2);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(321, 36);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(321, 75);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(321, 0);
            this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.Size = new System.Drawing.Size(321, 36);
            this.toolStripContainer2.TabIndex = 3;
            this.toolStripContainer2.Text = "toolStripContainer1";
            // 
            // toolStripContainer2.TopToolStripPanel
            // 
            this.toolStripContainer2.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideMemoToolStripButton,
            this.hideAgrementToolStripButton,
            this.hideIdeaToolStripButton,
            this.hideNotToolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(3, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(156, 39);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip1";
            // 
            // hideMemoToolStripButton
            // 
            this.hideMemoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hideMemoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("hideMemoToolStripButton.Image")));
            this.hideMemoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hideMemoToolStripButton.Name = "hideMemoToolStripButton";
            this.hideMemoToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.hideMemoToolStripButton.Text = "Hide Memos";
            this.hideMemoToolStripButton.Click += new System.EventHandler(this.hideMemoToolStripButton_Click);
            // 
            // hideAgrementToolStripButton
            // 
            this.hideAgrementToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hideAgrementToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("hideAgrementToolStripButton.Image")));
            this.hideAgrementToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hideAgrementToolStripButton.Name = "hideAgrementToolStripButton";
            this.hideAgrementToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.hideAgrementToolStripButton.Text = "Hide Agrement";
            this.hideAgrementToolStripButton.Click += new System.EventHandler(this.hideAgrementToolStripButton_Click);
            // 
            // hideIdeaToolStripButton
            // 
            this.hideIdeaToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hideIdeaToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("hideIdeaToolStripButton.Image")));
            this.hideIdeaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hideIdeaToolStripButton.Name = "hideIdeaToolStripButton";
            this.hideIdeaToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.hideIdeaToolStripButton.Text = "Hide Ideas";
            this.hideIdeaToolStripButton.Click += new System.EventHandler(this.hideIdeaToolStripButton_Click);
            // 
            // hideNotToolStripButton
            // 
            this.hideNotToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hideNotToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("hideNotToolStripButton.Image")));
            this.hideNotToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hideNotToolStripButton.Name = "hideNotToolStripButton";
            this.hideNotToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.hideNotToolStripButton.Text = "Hide Nots";
            this.hideNotToolStripButton.Click += new System.EventHandler(this.hideNotToolStripButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMemoToolStripButton,
            this.showAgrementToolStripButton,
            this.showIdeaToolStripButton,
            this.showNotToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(156, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // showMemoToolStripButton
            // 
            this.showMemoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showMemoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("showMemoToolStripButton.Image")));
            this.showMemoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showMemoToolStripButton.Name = "showMemoToolStripButton";
            this.showMemoToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.showMemoToolStripButton.Text = "Show Memos";
            this.showMemoToolStripButton.Click += new System.EventHandler(this.showMemoToolStripButton_Click);
            // 
            // showAgrementToolStripButton
            // 
            this.showAgrementToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showAgrementToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("showAgrementToolStripButton.Image")));
            this.showAgrementToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showAgrementToolStripButton.Name = "showAgrementToolStripButton";
            this.showAgrementToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.showAgrementToolStripButton.Text = "Show Agrements";
            this.showAgrementToolStripButton.Click += new System.EventHandler(this.showAgrementToolStripButton_Click);
            // 
            // showIdeaToolStripButton
            // 
            this.showIdeaToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showIdeaToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("showIdeaToolStripButton.Image")));
            this.showIdeaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showIdeaToolStripButton.Name = "showIdeaToolStripButton";
            this.showIdeaToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.showIdeaToolStripButton.Text = "Show Ideas";
            this.showIdeaToolStripButton.Click += new System.EventHandler(this.showIdeaToolStripButton_Click);
            // 
            // showNotToolStripButton
            // 
            this.showNotToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showNotToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("showNotToolStripButton.Image")));
            this.showNotToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showNotToolStripButton.Name = "showNotToolStripButton";
            this.showNotToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.showNotToolStripButton.Text = "Show Nots";
            this.showNotToolStripButton.Click += new System.EventHandler(this.showNotToolStripButton_Click);
            // 
            // BrainStormToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 114);
            this.Controls.Add(this.toolStripContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(337, 153);
            this.Name = "BrainStormToolBar";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "BrainStormToolBar";
            this.TopMost = true;
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStripContainer2.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer2.TopToolStripPanel.PerformLayout();
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton showListToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem topToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton showAllToolStripButton;
        private System.Windows.Forms.ToolStripButton hideAllToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton optionToolStripButton;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStripButton toolStripButtonMemo;
        private System.Windows.Forms.ToolStripButton toolStripButtonAgrement;
        private System.Windows.Forms.ToolStripButton toolStripButtonIdea;
        private System.Windows.Forms.ToolStripButton toolStripButtonNot;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton hideMemoToolStripButton;
        private System.Windows.Forms.ToolStripButton hideAgrementToolStripButton;
        private System.Windows.Forms.ToolStripButton hideIdeaToolStripButton;
        private System.Windows.Forms.ToolStripButton hideNotToolStripButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton showMemoToolStripButton;
        private System.Windows.Forms.ToolStripButton showAgrementToolStripButton;
        private System.Windows.Forms.ToolStripButton showIdeaToolStripButton;
        private System.Windows.Forms.ToolStripButton showNotToolStripButton;
    }
}