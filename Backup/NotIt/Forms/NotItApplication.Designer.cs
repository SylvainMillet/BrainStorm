namespace Nikoui.NotIt.Forms
{
    /// <summary>
    /// Fenêtre d'administration des NotIts.
    /// </summary>
    /// <remarks>
    /// Il s'agit de la fenêtre principale de l'application, sa fermeture entraine la fermeture 
    /// de toutes les NotIts.
    /// </remarks>
    partial class NotItApplication
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotItApplication));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.newNotItToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.showAllNotItsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.notItLabel = new System.Windows.Forms.Label();
            this.notifyContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyContextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Not It !";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // notifyContextMenuStrip
            // 
            this.notifyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listbarToolStripMenuItem,
            this.toolBarToolStripMenuItem,
            this.toolStripMenuItem1,
            this.newNotItToolStripMenuItem,
            this.toolStripMenuItem3,
            this.showAllNotItsToolStripMenuItem,
            this.hideAllToolStripMenuItem,
            this.toolStripMenuItem2,
            this.optionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.notifyContextMenuStrip.Name = "notifyContextMenuStrip";
            this.notifyContextMenuStrip.Size = new System.Drawing.Size(153, 198);
            // 
            // listbarToolStripMenuItem
            // 
            this.listbarToolStripMenuItem.CheckOnClick = true;
            this.listbarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("listbarToolStripMenuItem.Image")));
            this.listbarToolStripMenuItem.Name = "listbarToolStripMenuItem";
            this.listbarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.listbarToolStripMenuItem.Text = "Listbar";
            this.listbarToolStripMenuItem.Click += new System.EventHandler(this.listbarToolStripMenuItem_Click);
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.toolBarToolStripMenuItem.Text = "ToolBar";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.toolBarToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // newNotItToolStripMenuItem
            // 
            this.newNotItToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newNotItToolStripMenuItem.Image")));
            this.newNotItToolStripMenuItem.Name = "newNotItToolStripMenuItem";
            this.newNotItToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newNotItToolStripMenuItem.Text = "New";
            this.newNotItToolStripMenuItem.Click += new System.EventHandler(this.newNotItToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // showAllNotItsToolStripMenuItem
            // 
            this.showAllNotItsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showAllNotItsToolStripMenuItem.Image")));
            this.showAllNotItsToolStripMenuItem.Name = "showAllNotItsToolStripMenuItem";
            this.showAllNotItsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showAllNotItsToolStripMenuItem.Text = "Show all";
            this.showAllNotItsToolStripMenuItem.Click += new System.EventHandler(this.showAllNotItsToolStripMenuItem_Click);
            // 
            // hideAllToolStripMenuItem
            // 
            this.hideAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("hideAllToolStripMenuItem.Image")));
            this.hideAllToolStripMenuItem.Name = "hideAllToolStripMenuItem";
            this.hideAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hideAllToolStripMenuItem.Text = "Hide all";
            this.hideAllToolStripMenuItem.Click += new System.EventHandler(this.hideAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("optionsToolStripMenuItem.Image")));
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox.BackgroundImage")));
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox.Location = new System.Drawing.Point(3, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(64, 64);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // notItLabel
            // 
            this.notItLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.notItLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notItLabel.Location = new System.Drawing.Point(3, 79);
            this.notItLabel.Name = "notItLabel";
            this.notItLabel.Size = new System.Drawing.Size(64, 13);
            this.notItLabel.TabIndex = 2;
            this.notItLabel.Text = "NotIt !";
            this.notItLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NotItApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(70, 101);
            this.ControlBox = false;
            this.Controls.Add(this.notItLabel);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NotItApplication";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NotItAdministration_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNotItManager_FormClosing);
            this.notifyContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newNotItToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem showAllNotItsToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label notItLabel;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listbarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem hideAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
    }
}

