namespace Nikoui.NotIt.Forms
{
    /// <summary>
    /// Représentation d'une NotIt (vue).
    /// </summary>
    partial class NotItView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotItView));
            this.titleLabel = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.detailsLabel = new System.Windows.Forms.Label();
            this.detailsTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.deletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoEllipsis = true;
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(26, 2);
            this.titleLabel.MaximumSize = new System.Drawing.Size(90, 20);
            this.titleLabel.MinimumSize = new System.Drawing.Size(40, 13);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(41, 13);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "label1";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.titleLabel.Click += new System.EventHandler(this.titleLabel_Click);
            // 
            // titleTextBox
            // 
            this.titleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleTextBox.Location = new System.Drawing.Point(1, 2);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(90, 18);
            this.titleTextBox.TabIndex = 1;
            this.titleTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.titleTextBox.Visible = false;
            this.titleTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.titleTextBox_KeyDown);
            // 
            // detailsLabel
            // 
            this.detailsLabel.AutoEllipsis = true;
            this.detailsLabel.AutoSize = true;
            this.detailsLabel.BackColor = System.Drawing.Color.Transparent;
            this.detailsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailsLabel.ForeColor = System.Drawing.Color.White;
            this.detailsLabel.Location = new System.Drawing.Point(1, 22);
            this.detailsLabel.MaximumSize = new System.Drawing.Size(90, 100);
            this.detailsLabel.MinimumSize = new System.Drawing.Size(35, 13);
            this.detailsLabel.Name = "detailsLabel";
            this.detailsLabel.Size = new System.Drawing.Size(35, 13);
            this.detailsLabel.TabIndex = 2;
            this.detailsLabel.Text = "label1";
            this.detailsLabel.Click += new System.EventHandler(this.detailsLabel_Click);
            // 
            // detailsTextBox
            // 
            this.detailsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailsTextBox.Location = new System.Drawing.Point(1, 22);
            this.detailsTextBox.Multiline = true;
            this.detailsTextBox.Name = "detailsTextBox";
            this.detailsTextBox.Size = new System.Drawing.Size(90, 100);
            this.detailsTextBox.TabIndex = 3;
            this.detailsTextBox.Visible = false;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pinToolStripMenuItem,
            this.toolStripMenuItem1,
            this.newToolStripMenuItem,
            this.toolStripMenuItem2,
            this.deletedToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(117, 82);
            // 
            // pinToolStripMenuItem
            // 
            this.pinToolStripMenuItem.Name = "pinToolStripMenuItem";
            this.pinToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.pinToolStripMenuItem.Text = "Pin";
            this.pinToolStripMenuItem.Click += new System.EventHandler(this.pinToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(113, 6);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(113, 6);
            // 
            // deletedToolStripMenuItem
            // 
            this.deletedToolStripMenuItem.Name = "deletedToolStripMenuItem";
            this.deletedToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.deletedToolStripMenuItem.Text = "Delete";
            this.deletedToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // NotItView
            //             
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Nikoui.NotIt.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(93, 128);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.ControlBox = false;
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.detailsTextBox);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.detailsLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NotItView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.MouseEnter += new System.EventHandler(this.NotItView_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.NotItView_MouseLeave);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label detailsLabel;
        private System.Windows.Forms.TextBox detailsTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem pinToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deletedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolTip toolTip;
    }
}