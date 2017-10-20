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
    /// ToolBar de l'application.
    /// Fournis l'accès aux fonctionnalités principales de l'application.
    public partial class BrainStormToolBar : Form
    {
        #region Variables locales
        /// Référence vers la fenêtre principale de l'application.
        BrainStormApplication brainStormApplication;
        private int type;
        #endregion // Variables locales

        #region Construction / Initialisation
        /// Constructeur par défaut.
        public BrainStormToolBar()
        {
            brainStormApplication = null;
            InitializeComponent();
            FormManager.Instance.ListBarVisibleChanged += new FormManager.ListBarVisibleChangedEventHandler(Instance_ListBarVisibleChanged);
            showListToolStripButton.Checked = FormManager.Instance.ListBarVisible;
        }
        #endregion // Construction / Initialisation

        #region Actions de la ToolBar

        #region Affichage / Masquage de la ListBar
        /// La ListBar a été affichée/masquée.
        /// <param name="source">FormManager.</param>
        /// <param name="e">Changement de visibilité de la ListBar.</param>
        void Instance_ListBarVisibleChanged(object source, FormManager.ListBarVisibleChangedArgs e)
        {
            showListToolStripButton.Checked = e.Visible;
        }

        /// Affichage/Masquage de la ListBar.
        /// <param name="sender">Bouton ShowList.</param>
        /// <param name="e">Clique.</param>
        private void showListToolStripButton_Click(object sender, EventArgs e)
        {
            FormManager.Instance.ListBarVisible = showListToolStripButton.Checked;
        }
        #endregion // Affichage / Masquage de la ListBar

        /// Nouvelles BrainStorm avec envoi du type
        /// <param name="sender">Bouton New.</param>
        /// <param name="e">Clique.</param>

        private void toolStripButtonMemo_Click(object sender, EventArgs e)
        {
            type = 1;
            BrainStormControler.Instance.New(type);
            Focus();
        }

        private void toolStripButtonAgrement_Click(object sender, EventArgs e)
        {
            type = 2;
            BrainStormControler.Instance.New(type);
            Focus();
        }

        private void toolStripButtonIdea_Click(object sender, EventArgs e)
        {
            type = 3;
            BrainStormControler.Instance.New(type);
            Focus();
        }
        private void toolStripButtonNot_Click(object sender, EventArgs e)
        {
            type = 4;
            BrainStormControler.Instance.New(type);
            Focus();
        }


        /// Affiche toutes les BrainStorms au premier plan.
        /// <param name="sender">Bouton ShowAll.</param>
        /// <param name="e">Clique.</param>
        private void showAllToolStripButton_Click(object sender, EventArgs e)
        {
            BrainStormControler.Instance.ShowAll();
            Focus();
        }
        private void showMemoToolStripButton_Click(object sender, EventArgs e)
        {
            BrainStormControler.Instance.ShowMemo();
            Focus();
        }

        private void showAgrementToolStripButton_Click(object sender, EventArgs e)
        {
            BrainStormControler.Instance.ShowAgrement();
            Focus();
        }

        private void showIdeaToolStripButton_Click(object sender, EventArgs e)
        {
            BrainStormControler.Instance.ShowIdea();
            Focus();
        }

        private void showNotToolStripButton_Click(object sender, EventArgs e)
        {
            BrainStormControler.Instance.ShowNot();
            Focus();
        }


        /// Masque toutes les BrainStorms.
        /// <param name="sender">Bouton HideAll.</param>
        /// <param name="e">Clique.</param>
        private void hideAllToolStripButton_Click(object sender, EventArgs e)
        {
            BrainStormControler.Instance.HideAll();
        }

        private void hideMemoToolStripButton_Click(object sender, EventArgs e)
        {
            BrainStormControler.Instance.HideMemo();
            Focus();
        }

        private void hideAgrementToolStripButton_Click(object sender, EventArgs e)
        {
            BrainStormControler.Instance.HideAgrement();
            Focus();
        }

        private void hideIdeaToolStripButton_Click(object sender, EventArgs e)
        {
            BrainStormControler.Instance.HideIdea();
            Focus();
        }

        private void hideNotToolStripButton_Click(object sender, EventArgs e)
        {
            BrainStormControler.Instance.HideNot();
            Focus();
        }


        /// Accède à la configuration de l'application.
        /// <param name="sender">Bouton Option.</param>
        /// <param name="e">Clique.</param>
        private void optionToolStripButton_Click(object sender, EventArgs e)
        {
            // Todo : Accéder à la fenêtre de configuration sans passer par la fenêtre principale (via le SettingManager)
            if (brainStormApplication != null)
            {
                brainStormApplication.EditOptions();
            }
        }
        #endregion // Actions de la ToolBar

        #region Gestion de la configuration
        /// Sauvegarde la configuration courante de la ToolBar :
        /// - disposition (verticale ou horizontale)
        private void SaveConfiguration()
        {
            SettingManager.Instance.Settings.VerticalToolBar = horizontalToolStripMenuItem.Enabled;
            SettingManager.Instance.Save();
        }
        #endregion // Gestion de la configuration

        #region Propriétés
        /// Définis la référence vers la fenêtre principale de l'application.
        public BrainStormApplication BrainStormApplication
        {
            set
            {
                brainStormApplication = value;
            }
        }
        #endregion // Propriétés

    }
}