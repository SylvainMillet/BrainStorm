using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Nikoui.NotIt.Settings;
using Nikoui.NotIt.Properties;

namespace Nikoui.NotIt.Forms
{
    /// <summary>
    /// Fen�tre d'administration des NotIts.
    /// </summary>
    /// <remarks>
    /// Il s'agit de la fen�tre principale de l'application, sa fermeture entraine la fermeture 
    /// de toutes les NotIts.
    /// </remarks>
    public partial class NotItApplication : Form
    {
        #region Variables locales
        /// <summary>
        /// R�f�rence vers le controler de NotIts.
        /// </summary>
        private NotItControler controler;

        /// <summary>
        /// Timer d'affichage de la fen�tre au d�marrage.
        /// </summary>
        private System.Timers.Timer splashTimer;
        #endregion // Variables locales

        #region Construction / Initialisation
        /// <summary>
        /// Constructeur par d�faut de la fen�tre.
        /// </summary>
        public NotItApplication()
        {
            InitializeSplashEffect();
            InitializeComponent();
            InitializeControler();
            InitializeListBar();
            InitializeToolBar();
        }

        /// <summary>
        /// Obtention d'une r�f�rence vers le controler de NotIt.
        /// </summary>
        private void InitializeControler()
        {
            controler = NotItControler.Instance;
        }

        /// <summary>
        /// Initialise la ListBar.
        /// </summary>
        private void InitializeListBar()
        {
            FormManager.Instance.ListBarVisibleChanged += new FormManager.ListBarVisibleChangedEventHandler(Instance_ListBarVisibleChanged);
            if (SettingManager.Instance.Settings.ShowListBar)
            {
                listbarToolStripMenuItem.Checked = true;                
                FormManager.Instance.ListBarVisible = true;
            }
        }

        /// <summary>
        /// Initialise la ToolBar.
        /// </summary>
        private void InitializeToolBar()
        {
            FormManager.Instance.ToolBarVisibleChanged += new FormManager.ToolBarVisibleChangedEventHandler(Instance_ToolBarVisibleChanged);
            if (SettingManager.Instance.Settings.ShowToolBar)
            {
                toolBarToolStripMenuItem.Checked = true;
                FormManager.Instance.ToolBarVisible = true;
            }
        } 

        /// <summary>
        /// Initialisation de l'effet splash screen (l'application est affich�e au d�marrage, 
        /// puis masqu�e ensuite).
        /// </summary>
        private void InitializeSplashEffect()
        {
            splashTimer = new System.Timers.Timer(2000);
            splashTimer.Elapsed += new System.Timers.ElapsedEventHandler(splashTimer_Elapsed);
            splashTimer.Start();
        }
        #endregion // Construction / Initialisation

        #region Nouvelle NotIt
        /// <summary>
        /// Demande de cr�ation d'une nouvelle NotIt.
        /// </summary>
        /// <param name="sender">Bouton newNotIt.</param>
        /// <param name="e">Ev�nement de clique.</param>
        private void newButton_Click(object sender, EventArgs e)
        {
            NewNotIt();
        }

        /// <summary>
        /// Cr�ation d'une nouvelle NotIt.
        /// </summary>
        private static void NewNotIt()
        {
            NotItControler.Instance.New();
        }

        /// <summary>
        /// Demande de cr�ation d'une nouvelle NotIt.
        /// </summary>
        /// <param name="sender">Menu de l'ic�ne de notification.</param>
        /// <param name="e">Ev�nement de clique.</param>
        private void newNotItToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewNotIt();
        }
        #endregion // Nouvelle NotIt

        #region Fermeture de l'application
        /// <summary>
        /// Requ�te de fermeture de la fen�tre.
        /// On demande confirmation car la fermeture de cette fen�tre 
        /// entraine l'arr�t de l'application.
        /// </summary>
        /// <param name="sender">Fen�tre d'administration.</param>
        /// <param name="e">Demande de fermeture de la fen�tre.</param>
        private void FrmNotItManager_FormClosing(object sender, FormClosingEventArgs e)
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

        /// <summary>
        /// Demande de fermeture de l'application.
        /// </summary>
        /// <param name="sender">Entr�e du menu.</param>
        /// <param name="e">Ev�nement de clique.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// La fen�tre � �t� ferm�e, on lib�re le controler de NotIt.
        /// </summary>
        /// <remarks>
        /// La lib�ration du controler entraine la sauvegarde des NotIts courantes.
        /// </remarks>
        /// <param name="sender">Fen�tre d'administration.</param>
        /// <param name="e">Fermeture confirm�e de la fen�tre.</param>
        private void NotItAdministration_FormClosed(object sender, FormClosedEventArgs e)
        {
            //FormManager.Instance.SaveConfiguration();
            FormManager.Instance.Close();
            splashTimer.Enabled = false;           
            NotItControler.Instance.Dispose();
        }
        #endregion // Fermeture de l'application

        #region Action sur la collection de NotIts
        /// <summary>
        /// Demande d'affichage au premier plan des NotIts.
        /// </summary>
        /// <param name="sender">Entr�e de menu.</param>
        /// <param name="e">Ev�nement de clique.</param>
        private void showAllNotItsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.ShowAll();
        }

        /// <summary>
        /// Demande de masquage des NotIts.
        /// </summary>
        /// <param name="sender">Entr�e de menu.</param>
        /// <param name="e">Ev�nement de clique.</param>
        private void hideAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.HideAll();
        }
        #endregion // Action sur la collection de NotIts

        #region Masquage de la fen�tre apr�s l'initialisation
        /// <summary>
        /// D�clenchement du timer, on cache la fen�tre de l'application.
        /// </summary>
        /// <param name="sender">Timer splashTimer.</param>
        /// <param name="e">D�clencement du timer.</param>
        void splashTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            HideSplash();
        }

        /// <summary>
        /// D�l�gu� utiliser pour appeler la m�thode de masquage depuis le thread courant.
        /// </summary>
        delegate void HideSplashCallback();

        /// <summary>
        /// Masque la fen�tre d'application.
        /// </summary>
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
        /// <summary>
        /// Acc�s au param�trage de l'application.
        /// </summary>
        /// <param name="sender">Entr�e de menu optionsToolStripMenuItem.</param>
        /// <param name="e">Clique.</param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditOptions();
        }

        /// <summary>
        /// Affiche la fen�tre d'�dition des options et applique les modifications si n�cessaire.
        /// </summary>
        public void EditOptions()
        {
            optionsToolStripMenuItem.Enabled = false;
            NotItSettings notItSettings = new NotItSettings();
            EnableTopMostForms(false);
            DialogResult res = notItSettings.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                // Prise en compte des modifications
                controler.NotItsFile = SettingManager.Instance.Settings.NotItsFile;
                FormManager.Instance.ListBarShowInTaskBar = SettingManager.Instance.Settings.ShowListBarInTaskBar;
            }
            optionsToolStripMenuItem.Enabled = true;
            EnableTopMostForms(true);
        }
        #endregion // Configuration de l'application

        #region Gestion des fen�tres TopMost
        /// <summary>
        /// Active ou d�sactive l'affichage TopMost des fen�tres de l'application.
        /// Les fen�tres concern�es sont les NotIts et la ListBar.
        /// </summary>
        /// <remarks>
        /// D�sactiver l'affichage TopMost permet l'affichage de MessageBox ou de Dialog par dessus ces fen�tres.
        /// </remarks>
        /// <param name="enable"><c>true</c> pour activer les fen�tre TopMost, 
        /// <c>false</c> pour les d�sactiver.</param>
        private void EnableTopMostForms(bool enable)
        {
            FormManager.Instance.EnableTopMostForms(enable);
            if (enable)
            {
                // On autorise l'affiche en TopMost des fen�tres.
                controler.EnableTopMostNotIts();               
            }
            else
            {
                controler.DisableTopMostNotIts();
            }
        }
        #endregion // Gestion des fen�tres TopMost

        #region Gestion de la ListBar
        /// <summary>
        /// Demande d'affichage/masquage de la ListBar.
        /// </summary>
        /// <param name="sender">Entr�e du menu.</param>
        /// <param name="e">Clique.</param>
        private void listbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.Instance.ListBarVisible = listbarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Affichage de la ListBar par double clique sur l'ic�ne de notification.
        /// </summary>
        /// <param name="sender">Ic�ne de notification.</param>
        /// <param name="e">Double clique.</param>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FormManager.Instance.ListBarVisible = true;
        }

        /// <summary>
        /// R�ponse � l'�v�nement ListBarVisibleChanged.
        /// </summary>
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
        /// <summary>
        /// Demande d'affichage/masquage de la ToolBar.
        /// </summary>
        /// <param name="sender">Entr�e du menu.</param>
        /// <param name="e">Clique.</param>
        private void toolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.Instance.ToolBarVisible = toolBarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// R�ponse � l'�v�nement ToolBarVisibleChanged.
        /// </summary>
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