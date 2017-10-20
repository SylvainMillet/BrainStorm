using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Smilly.BrainStorm.Settings;

namespace Smilly.BrainStorm.Forms
{
    /// <summary>
    /// Fenêtre de paramétrage de l'application.
    /// </summary>
    public partial class BrainStormSettings : Form
    {
        #region Construction / Initialisation

        /// Constructeur par défaut.

        public BrainStormSettings()
        {
            InitializeComponent();
            GetCurrentSettings();            
        }


        /// Récupère la configuration courante de l'application.

        private void GetCurrentSettings()
        {
            brainStormsFileTextBox.Text = SettingManager.Instance.Settings.BrainStormsFile;
            listBarTaskBarCheckBox.Checked = SettingManager.Instance.Settings.ShowListBarInTaskBar;
            showListBarCheckBox.Checked = SettingManager.Instance.Settings.ShowListBar;
        }
        #endregion // Construction / Initialisation

        #region Fermeture de la fenêtre

        /// Fermeture de la fenêtre, sans prise en compte des modifications.

        /// <param name="sender">Boutton cancelButton.</param>
        /// <param name="e">Clique.</param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }


        /// Prise en compte des modifications et fermeture de la fenêtre.

        /// <param name="sender">Boutton okButton.</param>
        /// <param name="e">Clique.</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            ValidateSettings();
            DialogResult = DialogResult.OK;
        }


        /// Prend en compte les changements effectués à la configuration 
        /// et les sauvegarde.

        private void ValidateSettings()
        {
            SettingManager.Instance.Settings.BrainStormsFile = brainStormsFileTextBox.Text;
            SettingManager.Instance.Settings.ShowListBarInTaskBar = listBarTaskBarCheckBox.Checked;
            SettingManager.Instance.Settings.ShowListBar = showListBarCheckBox.Checked;
            SettingManager.Instance.Save();
        }
        #endregion // Fermeture de la fenêtre

        #region Réponse aux entrées utilisateur

        /// Ouverture d'une fenêtre "parcourir" pour le choix du fichier de stockage des BrainStorms.

        /// <param name="sender">Bouton browseButton.</param>
        /// <param name="e">Clique.</param>
        private void browseButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = brainStormsFileTextBox.Text;
            DialogResult res = saveFileDialog.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                // Modification du nom de fichier.
                brainStormsFileTextBox.Text = saveFileDialog.FileName;
            }
        }
        #endregion // Réponse aux entrées utilisateur
    }
}