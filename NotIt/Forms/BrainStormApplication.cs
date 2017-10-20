using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Smilly.BrainStorm.Settings;
using Smilly.BrainStorm.Properties;

namespace Smilly.BrainStorm.Forms
{
    /// Fen�tre d'administration des BrainStorms.
    /// Il s'agit de la fen�tre principale de l'application, sa fermeture entraine la fermeture 
    /// de toutes les BrainStorms.
    public partial class BrainStormApplication : Form
    {
        #region Variables locales
        /// R�f�rence vers le controler de BrainStorms.
        private BrainStormControler controler;

        /// Timer d'affichage de la fen�tre au d�marrage.
        private System.Timers.Timer splashTimer;

        private int type = 1;
        #endregion // Variables locales

        #region Construction / Initialisation
        /// Constructeur par d�faut de la fen�tre.
        public BrainStormApplication()
        {
            InitializeSplashEffect();
            InitializeComponent();
            InitializeControler();
            InitializeListBar();
            InitializeToolBar();
        }

        /// Obtention d'une r�f�rence vers le controler de BrainStorm.
        private void InitializeControler()
        {
            controler = BrainStormControler.Instance;
        }

        /// Initialise la ListBar.
        private void InitializeListBar()
        {
            FormManager.Instance.ListBarVisibleChanged += new FormManager.ListBarVisibleChangedEventHandler(Instance_ListBarVisibleChanged);
            if (SettingManager.Instance.Settings.ShowListBar)
            {
                listbarToolStripMenuItem.Checked = true;                
                FormManager.Instance.ListBarVisible = true;
            }
        }

        /// Initialise la ToolBar.
        private void InitializeToolBar()
        {
            FormManager.Instance.ToolBarVisibleChanged += new FormManager.ToolBarVisibleChangedEventHandler(Instance_ToolBarVisibleChanged);
            if (SettingManager.Instance.Settings.ShowToolBar)
            {
                toolBarToolStripMenuItem.Checked = true;
                FormManager.Instance.ToolBarVisible = true;
            }
        } 

        /// Initialisation de l'effet splash screen (l'application est affich�e au d�marrage, 
        /// puis masqu�e ensuite).
        private void InitializeSplashEffect()
        {
            splashTimer = new System.Timers.Timer(2000);
            splashTimer.Elapsed += new System.Timers.ElapsedEventHandler(splashTimer_Elapsed);
            splashTimer.Start();
        }
        #endregion // Construction / Initialisation

        #region Nouvelle BrainStorm
        /// Demande de cr�ation d'une nouvelle BrainStorm.
        /// <param name="sender">Bouton newBrainStorm.</param>
        /// <param name="e">Ev�nement de clique.</param>
        private void newButton_Click(object sender, EventArgs e)
        {
            NewBrainStorm(type);
        }

        /// Cr�ation d'une nouvelle BrainStorm.
        private static void NewBrainStorm(int type)
        {
            BrainStormControler.Instance.New(type);
        }

        /// Demande de cr�ation d'une nouvelle BrainStorm.
        /// <param name="sender">Menu de l'ic�ne de notification.</param>
        /// <param name="e">Ev�nement de clique.</param>
        private void newBrainStormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewBrainStorm(type);
        }
        #endregion // Nouvelle BrainStorm

        #region Fermeture de l'application
        /// Requ�te de fermeture de la fen�tre.
        /// On demande confirmation car la fermeture de cette fen�tre 
        /// entraine l'arr�t de l'application.
        /// <param name="sender">Fen�tre d'administration.</param>
        /// <param name="e">Demande de fermeture de la fen�tre.</param>
        private void FrmBrainStormManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                EnableTopMostForms(false);
                DialogResult res;
                res = MessageBox.Show(Resources.ReallyWantToQuit, Resources.ApplicationClosing, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res != DialogResult.Yes)
                {
                    // Fermeture annul�e.
                    e.Cancel = true;
                    EnableTopMostForms(true);
                }
            }
        }

        /// Demande de fermeture de l'application.
        /// <param name="sender">Entr�e du menu.</param>
        /// <param name="e">Ev�nement de clique.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// La fen�tre � �t� ferm�e, on lib�re le controler de BrainStorm.
        /// La lib�ration du controler entraine la sauvegarde des BrainStorms courantes.
        /// <param name="sender">Fen�tre d'administration.</param>
        /// <param name="e">Fermeture confirm�e de la fen�tre.</param>
        private void BrainStormAdministration_FormClosed(object sender, FormClosedEventArgs e)
        {
            //FormManager.Instance.SaveConfiguration();
            FormManager.Instance.Close();
            splashTimer.Enabled = false;           
            BrainStormControler.Instance.Dispose();
        }
        #endregion // Fermeture de l'application

        #region Action sur la collection de BrainStorms
        /// Demande d'affichage au premier plan des BrainStorms.
        /// <param name="sender">Entr�e de menu.</param>
        /// <param name="e">Ev�nement de clique.</param>
        private void showAllBrainStormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.ShowAll();
        }

        /// Demande de masquage des BrainStorms.
        /// <param name="sender">Entr�e de menu.</param>
        /// <param name="e">Ev�nement de clique.</param>
        private void hideAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.HideAll();
        }
        #endregion // Action sur la collection de BrainStorms

        #region Masquage de la fen�tre apr�s l'initialisation
        /// D�clenchement du timer, on cache la fen�tre de l'application.
        /// <param name="sender">Timer splashTimer.</param>
        /// <param name="e">D�clencement du timer.</param>
        void splashTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            HideSplash();
        }

        /// D�l�gu� utiliser pour appeler la m�thode de masquage depuis le thread courant.
        delegate void HideSplashCallback();

        /// Masque la fen�tre d'application.
        private void HideSplash()
        {
            if (this.InvokeRequired)
            {
                // On appel la m�thode depuis le thread courant.
                HideSplashCallback callBack = new HideSplashCallback(HideSplash);
                try
                {
                    this.Invoke(callBack, new object[] { });
                }
                catch (ObjectDisposedException)
                {
                }
            }
            else
            {
                Hide();
            }
        }
        #endregion // Masquage de la fen�tre apr�s l'initialisation

        #region Configuration de l'application
        /// Acc�s au param�trage de l'application.
        /// <param name="sender">Entr�e de menu optionsToolStripMenuItem.</param>
        /// <param name="e">Clique.</param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditOptions();
        }

        /// Affiche la fen�tre d'�dition des options et applique les modifications si n�cessaire.
        public void EditOptions()
        {
            optionsToolStripMenuItem.Enabled = false;
            BrainStormSettings brainStormSettings = new BrainStormSettings();
            EnableTopMostForms(false);
            DialogResult res = brainStormSettings.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                // Prise en compte des modifications
                controler.BrainStormsFile = SettingManager.Instance.Settings.BrainStormsFile;
                FormManager.Instance.ListBarShowInTaskBar = SettingManager.Instance.Settings.ShowListBarInTaskBar;
            }
            optionsToolStripMenuItem.Enabled = true;
            EnableTopMostForms(true);
        }
        #endregion // Configuration de l'application

        #region Gestion des fen�tres TopMost
        /// Active ou d�sactive l'affichage TopMost des fen�tres de l'application.
        /// Les fen�tres concern�es sont les BrainStorms et la ListBar.
        /// D�sactiver l'affichage TopMost permet l'affichage de MessageBox ou de Dialog par dessus ces fen�tres.
        /// <param name="enable"><c>true</c> pour activer les fen�tre TopMost, 
        /// <c>false</c> pour les d�sactiver.</param>
        private void EnableTopMostForms(bool enable)
        {
            FormManager.Instance.EnableTopMostForms(enable);
            if (enable)
            {
                // On autorise l'affiche en TopMost des fen�tres.
                controler.EnableTopMostBrainStorms();               
            }
            else
            {
                controler.DisableTopMostBrainStorms();
            }
        }
        #endregion // Gestion des fen�tres TopMost

        #region Gestion de la ListBar
        /// Demande d'affichage/masquage de la ListBar.
        /// <param name="sender">Entr�e du menu.</param>
        /// <param name="e">Clique.</param>
        private void listbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.Instance.ListBarVisible = listbarToolStripMenuItem.Checked;
        }

        /// Affichage de la ListBar par double clique sur l'ic�ne de notification.
        /// <param name="sender">Ic�ne de notification.</param>
        /// <param name="e">Double clique.</param>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FormManager.Instance.ListBarVisible = true;
        }

        /// R�ponse � l'�v�nement ListBarVisibleChanged.
        /// <param name="source">FormManager.</param>
        /// <param name="e">Changement de visibilit� de la ListBar.</param>
        void Instance_ListBarVisibleChanged(object source, FormManager.ListBarVisibleChangedArgs e)
        {
            listbarToolStripMenuItem.Checked = e.Visible;
            SettingManager.Instance.Settings.ShowListBar = e.Visible;
            SettingManager.Instance.Save();
        }
        #endregion // Gestion de la ListBar

        #region Gestion de la ToolBar
        /// Demande d'affichage/masquage de la ToolBar.
        /// <param name="sender">Entr�e du menu.</param>
        /// <param name="e">Clique.</param>
        private void toolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.Instance.ToolBarVisible = toolBarToolStripMenuItem.Checked;
        }

        /// R�ponse � l'�v�nement ToolBarVisibleChanged.
        /// <param name="source">FormManager.</param>
        /// <param name="e">Changement de visibilit� de la ToolBar.</param>
        void Instance_ToolBarVisibleChanged(object source, FormManager.ToolBarVisibleChangedArgs e)
        {
            toolBarToolStripMenuItem.Checked = e.Visible;
            SettingManager.Instance.Settings.ShowToolBar = e.Visible;
            SettingManager.Instance.Save();
        }
        #endregion // Gestion de la ToolBar
    }
}