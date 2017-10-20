using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Nikoui.NotIt.Settings;

namespace Nikoui.NotIt.Forms
{
    /// <summary>
    /// Fen�tre de param�trage de l'application.
    /// </summary>
    public partial class NotItSettings : Form
    {
        #region Construction / Initialisation
        /// <summary>
        /// Constructeur par d�faut.
        /// </summary>
        public NotItSettings()
        {
            InitializeComponent();
            GetCurrentSettings();            
        }

        /// <summary>
        /// R�cup�re la configuration courante de l'application.
        /// </summary>
        private void GetCurrentSettings()
        {
            notItsFileTextBox.Text = SettingManager.Instance.Settings.NotItsFile;
            listBarTaskBarCheckBox.Checked = SettingManager.Instance.Settings.ShowListBarInTaskBar;
            showListBarCheckBox.Checked = SettingManager.Instance.Settings.ShowListBar;
        }
        #endregion // Construction / Initialisation

        #region Fermeture de la fen�tre
        /// <summary>
        /// Fermeture de la fen�tre, sans prise en compte des modifications.
        /// </summary>
        /// <param name="sender">Boutton cancelButton.</param>
        /// <param name="e">Clique.</param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Prise en compte des modifications et fermeture de la fen�tre.
        /// </summary>
        /// <param name="sender">Boutton okButton.</param>
        /// <param name="e">Clique.</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            ValidateSettings();
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Prend en compte les changements effectu�s � la configuration 
        /// et les sauvegarde.
        /// </summary>
        private void ValidateSettings()
        {
            SettingManager.Instance.Settings.NotItsFile = notItsFileTextBox.Text;
            SettingManager.Instance.Settings.ShowListBarInTaskBar = listBarTaskBarCheckBox.Checked;
            SettingManager.Instance.Settings.ShowListBar = showListBarCheckBox.Checked;
            SettingManager.Instance.Save();
        }
        #endregion // Fermeture de la fen�tre

        #region R�ponse aux entr�es utilisateur
        /// <summary>
        /// Ouverture d'une fen�tre "parcourir" pour le choix du fichier de stockage des NotIts.
        /// </summary>
        /// <param name="sender">Bouton browseButton.</param>
        /// <param name="e">Clique.</param>
        private void browseButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = notItsFileTextBox.Text;
            DialogResult res = saveFileDialog.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                // Modification du nom de fichier.
                notItsFileTextBox.Text = saveFileDialog.FileName;
            }
        }
        #endregion // R�ponse aux entr�es utilisateur
    }
}