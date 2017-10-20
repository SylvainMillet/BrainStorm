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
    /// ToolBar de l'application.
    /// Fournis l'accès aux fonctionnalités principales de l'application.
    /// </summary>
    public partial class NotItToolBar : Form
    {
        #region Variables locales
        /// <summary>
        /// Référence vers la fenêtre principale de l'application.
        /// </summary>
        NotItApplication notItApplication;
        #endregion // Variables locales

        #region Construction / Initialisation
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public NotItToolBar()
        {
            notItApplication = null;
            InitializeComponent();
            FormManager.Instance.ListBarVisibleChanged += new FormManager.ListBarVisibleChangedEventHandler(Instance_ListBarVisibleChanged);
            showListToolStripButton.Checked = FormManager.Instance.ListBarVisible;
            if (SettingManager.Instance.Settings.VerticalToolBar)
            {
                SetVerticalLayout();
            }
        }
        #endregion // Construction / Initialisation

        #region Actions de la ToolBar

        #region Affichage / Masquage de la ListBar
        /// <summary>
        /// La ListBar a été affichée/masquée.
        /// </summary>
        /// <param name="source">FormManager.</param>
        /// <param name="e">Changement de visibilité de la ListBar.</param>
        void Instance_ListBarVisibleChanged(object source, FormManager.ListBarVisibleChangedArgs e)
        {
            showListToolStripButton.Checked = e.Visible;
        }

        /// <summary>
        /// Affichage/Masquage de la ListBar.
        /// </summary>
        /// <param name="sender">Bouton ShowList.</param>
        /// <param name="e">Clique.</param>
        private void showListToolStripButton_Click(object sender, EventArgs e)
        {
            FormManager.Instance.ListBarVisible = showListToolStripButton.Checked;
        }
        #endregion // Affichage / Masquage de la ListBar

        /// <summary>
        /// Nouvelle NotIt.
        /// </summary>
        /// <param name="sender">Bouton New.</param>
        /// <param name="e">Clique.</param>
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            NotItControler.Instance.New();
            Focus();
        }

        /// <summary>
        /// Affiche toutes les NotIts au premier plan.
        /// </summary>
        /// <param name="sender">Bouton ShowAll.</param>
        /// <param name="e">Clique.</param>
        private void showAllToolStripButton_Click(object sender, EventArgs e)
        {
            NotItControler.Instance.ShowAll();
            Focus();
        }

        /// <summary>
        /// Masque toutes les NotIts.
        /// </summary>
        /// <param name="sender">Bouton HideAll.</param>
        /// <param name="e">Clique.</param>
        private void hideAllToolStripButton_Click(object sender, EventArgs e)
        {
            NotItControler.Instance.HideAll();
        }

        /// <summary>
        /// Accède à la configuration de l'application.
        /// </summary>
        /// <param name="sender">Bouton Option.</param>
        /// <param name="e">Clique.</param>
        private void optionToolStripButton_Click(object sender, EventArgs e)
        {
            // Todo : Accéder à la fenêtre de configuration sans passer par la fenêtre principale (via le SettingManager)
            if (notItApplication != null)
            {
                notItApplication.EditOptions();
            }
        }
        #endregion // Actions de la ToolBar

        #region Placement et disposition de la ToolBar
        /// <summary>
        /// Placement de la ToolBar sur la bordure supérieure de l'écran.
        /// La ToolBar est centrée horizontalement.
        /// </summary>
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verticalToolStripMenuItem.Enabled == false)
            {
                // La barre est en disposition verticale, on la rement en horizontale
                SetHorizontalLayout();
            }
            Top = 0;
            Left = (Screen.GetWorkingArea(this).Width - Width) / 2;
        }

        /// <summary>
        /// Placement de la ToolBar sur la bordure inférieure de l'écran.
        /// La ToolBar est centrée horizontalement.
        /// </summary>
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void bottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verticalToolStripMenuItem.Enabled == false)
            {
                // La barre est en disposition verticale, on la rement en horizontale
                SetHorizontalLayout();
            }
            Top = Screen.GetWorkingArea(this).Height - Height;
            Left = (Screen.GetWorkingArea(this).Width - Width) / 2;
        }

        /// <summary>
        /// Placement de la ToolBar sur la bordure gauche de l'écran.
        /// La ToolBar est centrée verticalement.
        /// </summary>
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (horizontalToolStripMenuItem.Enabled == false)
            {
                // La barre est en disposition horizontale, on la rement en verticale
                SetVerticalLayout();
            }
            Left = 0;
            Top = (Screen.GetWorkingArea(this).Height - Height) / 2;
        }

        /// <summary>
        /// Placement de la ToolBar sur la bordure droite de l'écran.
        /// La ToolBar est centrée verticalement.
        /// </summary>
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (horizontalToolStripMenuItem.Enabled == false)
            {
                // La barre est en disposition horizontale, on la rement en verticale
                SetVerticalLayout();
            }
            Left = Screen.GetWorkingArea(this).Width - Width;
            Top = (Screen.GetWorkingArea(this).Height - Height) / 2;
        }

        /// <summary>
        /// Demande de disposition verticale de la ToolBar.
        /// </summary>
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVerticalLayout();
        }

        /// <summary>
        /// Basculement en disposition verticale de la ToolBar.
        /// </summary>
        private void SetVerticalLayout()
        {
            verticalToolStripMenuItem.Enabled = false;
            horizontalToolStripMenuItem.Enabled = true;
            int height = ClientSize.Width + (Height - ClientSize.Height);
            int width = ClientSize.Height + (Width - ClientSize.Width);
            Height = height;
            Width = width;
            toolStripContainer.LeftToolStripPanel.Controls.Add(toolStripContainer.TopToolStripPanel.Controls[0]);
            SaveConfiguration();
        }

        /// <summary>
        /// Demande de disposition horizontale de la ToolBar.
        /// </summary>
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetHorizontalLayout();
        }

        /// <summary>
        /// Basculement en disposition horizontale de la ToolBar.
        /// </summary>
        private void SetHorizontalLayout()
        {
            horizontalToolStripMenuItem.Enabled = true;
            verticalToolStripMenuItem.Enabled = true;
            int height = ClientSize.Width + (Height - ClientSize.Height);
            int width = ClientSize.Height + (Width - ClientSize.Width);
            Height = height;
            Width = width;
            toolStripContainer.TopToolStripPanel.Controls.Add(toolStripContainer.LeftToolStripPanel.Controls[0]);
            SaveConfiguration();
        }
        #endregion // Placement et disposition de la ToolBar

        #region Gestion de la configuration
        /// <summary>
        /// Sauvegarde la configuration courante de la ToolBar :
        /// - disposition (verticale ou horizontale)
        /// </summary>
        private void SaveConfiguration()
        {
            SettingManager.Instance.Settings.VerticalToolBar = horizontalToolStripMenuItem.Enabled;
            SettingManager.Instance.Save();
        }
        #endregion // Gestion de la configuration

        #region Propriétés
        /// <summary>
        /// Définis la référence vers la fenêtre principale de l'application.
        /// </summary>
        public NotItApplication NotItApplication
        {
            set
            {
                notItApplication = value;
            }
        }
        #endregion // Propriétés
    }
}