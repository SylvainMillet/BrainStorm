namespace Smilly.BrainStorm.Forms
{
    /// <summary>
    /// Fenêtre de paramétrage de l'application.
    /// </summary>
    partial class BrainStormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrainStormSettings));
            this.brainStormsFileLabel = new System.Windows.Forms.Label();
            this.brainStormsFileTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.listBarTaskBarCheckBox = new System.Windows.Forms.CheckBox();
            this.showListBarCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // brainStormsFileLabel
            // 
            this.brainStormsFileLabel.AutoSize = true;
            this.brainStormsFileLabel.Location = new System.Drawing.Point(12, 15);
            this.brainStormsFileLabel.Name = "brainStormsFileLabel";
            this.brainStormsFileLabel.Size = new System.Drawing.Size(118, 13);
            this.brainStormsFileLabel.TabIndex = 0;
            this.brainStormsFileLabel.Text = "Fichier de sauvegarde :";
            // 
            // brainStormsFileTextBox
            // 
            this.brainStormsFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.brainStormsFileTextBox.Location = new System.Drawing.Point(15, 31);
            this.brainStormsFileTextBox.Name = "brainStormsFileTextBox";
            this.brainStormsFileTextBox.Size = new System.Drawing.Size(336, 20);
            this.brainStormsFileTextBox.TabIndex = 0;
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(357, 29);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(26, 23);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(308, 114);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(227, 114);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // listBarTaskBarCheckBox
            // 
            this.listBarTaskBarCheckBox.AutoSize = true;
            this.listBarTaskBarCheckBox.Checked = true;
            this.listBarTaskBarCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.listBarTaskBarCheckBox.Location = new System.Drawing.Point(15, 91);
            this.listBarTaskBarCheckBox.Name = "listBarTaskBarCheckBox";
            this.listBarTaskBarCheckBox.Size = new System.Drawing.Size(227, 17);
            this.listBarTaskBarCheckBox.TabIndex = 5;
            this.listBarTaskBarCheckBox.Text = "Afficher la ListBar dans la barre des tâches";
            this.listBarTaskBarCheckBox.UseVisualStyleBackColor = true;
            // 
            // showListBarCheckBox
            // 
            this.showListBarCheckBox.AutoSize = true;
            this.showListBarCheckBox.Checked = true;
            this.showListBarCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showListBarCheckBox.Location = new System.Drawing.Point(15, 68);
            this.showListBarCheckBox.Name = "showListBarCheckBox";
            this.showListBarCheckBox.Size = new System.Drawing.Size(176, 17);
            this.showListBarCheckBox.TabIndex = 6;
            this.showListBarCheckBox.Text = "Afficher la ListBar au démarrage";
            this.showListBarCheckBox.UseVisualStyleBackColor = true;
            // 
            // BrainStormSettings
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(395, 149);
            this.Controls.Add(this.showListBarCheckBox);
            this.Controls.Add(this.listBarTaskBarCheckBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.brainStormsFileTextBox);
            this.Controls.Add(this.brainStormsFileLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BrainStormSettings";
            this.Text = "BrainStorm Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label brainStormsFileLabel;
        private System.Windows.Forms.TextBox brainStormsFileTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.CheckBox listBarTaskBarCheckBox;
        private System.Windows.Forms.CheckBox showListBarCheckBox;
    }
}