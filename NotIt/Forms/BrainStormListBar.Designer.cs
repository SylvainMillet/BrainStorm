namespace Smilly.BrainStorm.Forms
{
    /// <summary>
    /// ListBar, affichant la liste des BrainStorms.
    /// Permet de lister toutes les BrainStorms et d'intéragir avec elles.
    /// </summary>
    partial class BrainStormListBar
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrainStormListBar));
            this.listView = new System.Windows.Forms.ListView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.showAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.agrementsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ideasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.notsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hideAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memosToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.agrementsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ideasToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.notsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.topToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.ContextMenuStrip = this.contextMenuStrip;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.LargeImageList = this.imageList;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(136, 218);
            this.listView.SmallImageList = this.imageList;
            this.listView.TabIndex = 0;
            this.listView.TileSize = new System.Drawing.Size(100, 40);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Tile;
            this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deletedToolStripMenuItem,
            this.toolStripMenuItem2,
            this.showAllToolStripMenuItem,
            this.hideAllToolStripMenuItem,
            this.toolStripMenuItem1,
            this.alwaysOnTopToolStripMenuItem,
            this.toolStripMenuItem3});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(186, 126);
            // 
            // deletedToolStripMenuItem
            // 
            this.deletedToolStripMenuItem.Enabled = false;
            this.deletedToolStripMenuItem.Name = "deletedToolStripMenuItem";
            this.deletedToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.deletedToolStripMenuItem.Text = "Delete";
            this.deletedToolStripMenuItem.Click += new System.EventHandler(this.deletedToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(182, 6);
            // 
            // showAllToolStripMenuItem
            // 
            this.showAllToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memosToolStripMenuItem1,
            this.agrementsToolStripMenuItem1,
            this.ideasToolStripMenuItem1,
            this.notsToolStripMenuItem1});
            this.showAllToolStripMenuItem.Name = "showAllToolStripMenuItem";
            this.showAllToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.showAllToolStripMenuItem.Text = "Show all";
            this.showAllToolStripMenuItem.Click += new System.EventHandler(this.showAllToolStripMenuItem_Click);
            // 
            // memosToolStripMenuItem1
            // 
            this.memosToolStripMenuItem1.Name = "memosToolStripMenuItem1";
            this.memosToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.memosToolStripMenuItem1.Text = "Memos";
            this.memosToolStripMenuItem1.Click += new System.EventHandler(this.memosToolStripMenuItem1_Click);
            // 
            // agrementsToolStripMenuItem1
            // 
            this.agrementsToolStripMenuItem1.Name = "agrementsToolStripMenuItem1";
            this.agrementsToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.agrementsToolStripMenuItem1.Text = "Agrements";
            this.agrementsToolStripMenuItem1.Click += new System.EventHandler(this.agrementsToolStripMenuItem1_Click);
            // 
            // ideasToolStripMenuItem1
            // 
            this.ideasToolStripMenuItem1.Name = "ideasToolStripMenuItem1";
            this.ideasToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.ideasToolStripMenuItem1.Text = "Ideas";
            this.ideasToolStripMenuItem1.Click += new System.EventHandler(this.ideasToolStripMenuItem1_Click);
            // 
            // notsToolStripMenuItem1
            // 
            this.notsToolStripMenuItem1.Name = "notsToolStripMenuItem1";
            this.notsToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.notsToolStripMenuItem1.Text = "Nots";
            this.notsToolStripMenuItem1.Click += new System.EventHandler(this.notsToolStripMenuItem1_Click);
            // 
            // hideAllToolStripMenuItem
            // 
            this.hideAllToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memosToolStripMenuItem2,
            this.agrementsToolStripMenuItem2,
            this.ideasToolStripMenuItem2,
            this.notsToolStripMenuItem2});
            this.hideAllToolStripMenuItem.Name = "hideAllToolStripMenuItem";
            this.hideAllToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.hideAllToolStripMenuItem.Text = "Hide all";
            this.hideAllToolStripMenuItem.Click += new System.EventHandler(this.hideAllToolStripMenuItem_Click);
            // 
            // memosToolStripMenuItem2
            // 
            this.memosToolStripMenuItem2.Name = "memosToolStripMenuItem2";
            this.memosToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.memosToolStripMenuItem2.Text = "Memos";
            this.memosToolStripMenuItem2.Click += new System.EventHandler(this.memosToolStripMenuItem2_Click);
            // 
            // agrementsToolStripMenuItem2
            // 
            this.agrementsToolStripMenuItem2.Name = "agrementsToolStripMenuItem2";
            this.agrementsToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.agrementsToolStripMenuItem2.Text = "Agrements";
            this.agrementsToolStripMenuItem2.Click += new System.EventHandler(this.agrementsToolStripMenuItem2_Click);
            // 
            // ideasToolStripMenuItem2
            // 
            this.ideasToolStripMenuItem2.Name = "ideasToolStripMenuItem2";
            this.ideasToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.ideasToolStripMenuItem2.Text = "Ideas";
            this.ideasToolStripMenuItem2.Click += new System.EventHandler(this.ideasToolStripMenuItem2_Click);
            // 
            // notsToolStripMenuItem2
            // 
            this.notsToolStripMenuItem2.Name = "notsToolStripMenuItem2";
            this.notsToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.notsToolStripMenuItem2.Text = "Nots";
            this.notsToolStripMenuItem2.Click += new System.EventHandler(this.notsToolStripMenuItem2_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(182, 6);
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.Checked = true;
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always on forground";
            this.alwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.topToolStripMenuItem,
            this.bottomToolStripMenuItem,
            this.leftToolStripMenuItem,
            this.rightToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItem3.Text = "Send to";
            // 
            // topToolStripMenuItem
            // 
            this.topToolStripMenuItem.Name = "topToolStripMenuItem";
            this.topToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.topToolStripMenuItem.Text = "Top";
            this.topToolStripMenuItem.Click += new System.EventHandler(this.topToolStripMenuItem_Click);
            // 
            // bottomToolStripMenuItem
            // 
            this.bottomToolStripMenuItem.Name = "bottomToolStripMenuItem";
            this.bottomToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.bottomToolStripMenuItem.Text = "Bottom";
            this.bottomToolStripMenuItem.Click += new System.EventHandler(this.bottomToolStripMenuItem_Click);
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.leftToolStripMenuItem_Click);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.rightToolStripMenuItem.Text = "Right";
            this.rightToolStripMenuItem.Click += new System.EventHandler(this.rightToolStripMenuItem_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "BrainStorm.ico");
            // 
            // BrainStormListBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(136, 218);
            this.Controls.Add(this.listView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BrainStormListBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Brain";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BrainStormListBar_FormClosed);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deletedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem topToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem showAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem agrementsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ideasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem notsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem memosToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem agrementsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ideasToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem notsToolStripMenuItem2;
    }
}