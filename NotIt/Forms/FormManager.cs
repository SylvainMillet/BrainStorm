using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Smilly.BrainStorm.Settings;

namespace Smilly.BrainStorm.Forms
{
    /// <summary>
    /// Gestion des Forms sp�cifiques de l'applications :
    /// - ListBar
    /// - ToolBar
    /// Permet l'affichage/le masquage de ces Forms.
    /// </summary>
    public sealed class FormManager
    {
		#region Variables locales

        /// Instance unique du FormManager.

		private static volatile FormManager instance = null;


        /// Objet de synchronisation utilis� pour assurer l'unicit� du singleton.

		private static object syncRoot = new Object();


        /// Valeur indiquant si la ListBar est visible.

        private bool listBarVisible;


        /// ListBar de l'application.

        private BrainStormListBar listBar;


        /// Valeur indiquant si la ToolBar est visible.

        private bool toolBarVisible;


        /// ToolBar de l'application.

        private BrainStormToolBar toolBar;


        /// Sauvegarde temporaire de la propri�t� TopMost de la ListBar.

        private bool listBarTopMost;


        /// Sauvegarde temporaire de la propri�t� TopMost de la ToolBar.

        private bool toolBarTopMost;


        /// R�ponse aux �v�nements de fermeture de la ListBar et de la ToolBar.

        private FormClosedEventHandler formClosedEventHandler;


        /// R�f�rence vers la fen�tre principale de l'application.

        private BrainStormApplication brainStormApplication;
		#endregion // Variables locales

		#region Construction / Initialisation

        /// Constructeur priv� du singleton.

		private FormManager()
		{
            brainStormApplication = null;
            formClosedEventHandler = new FormClosedEventHandler(formClosed);
            listBarVisible = false;
            toolBarVisible = false;
            listBarTopMost = false;
		}
		#endregion // Construction / Initialisation

		#region Acc�s � l'instance

        /// Obtient l'instance unique du FormManager.

		public static FormManager Instance
		{
			get
			{
    			if (instance == null)
    			{
    				lock (syncRoot)
    				{
        				if (instance == null)
        				{
                            // Premier appel au singleton.
            				instance = new FormManager();
        				}
    				}
    			}
    			return instance;
			}
		}
		#endregion // Acc�s � l'instance

        #region ListBar

        #region Ev�nement ListBarVisibleChanged

        /// D�l�gu� de r�ponse � l'�v�nement ListBarVisibleChanged.

        /// <param name="source">ListBar.</param>
        /// <param name="e">Modification de la visibilit� de la ListBar.</param>
        public delegate void ListBarVisibleChangedEventHandler(object source, ListBarVisibleChangedArgs e);


        /// Ev�nement de changement de visibilit� de la ListBar.
        /// D�clench� lorsque la ListBar est affich�e/masqu�e.

        public event ListBarVisibleChangedEventHandler ListBarVisibleChanged;


        /// Arguments de l'�v�nement ListBarVisibleChanged.
        /// Fournit la visibilit� courante de la ListBar.

        public class ListBarVisibleChangedArgs
        {
    
            /// Valeur indiquant si la ListBar est visible.
    
            bool visible;

    
            /// Obtient une valeur indiquant si la ListBar est visible.
    
            public bool Visible
            {
                get
                {
                    return (visible);
                }
            }

    
            /// Construction par d�faut.
    
            /// <param name="visible">Valeur indiquant si la ListBar est actuellement visible.</param>
            public ListBarVisibleChangedArgs(bool visible)
            {
                this.visible = visible;
            }
        }
        #endregion // Ev�nement ListBarVisibleChanged


        /// Obtient ou d�finis une valeur indiquant si la ListBar est visible.

        public bool ListBarVisible
        {
            get
            {
                return(listBarVisible);
            }
            set
            {
                if (listBarVisible != value)
                {
                    listBarVisible = value;
                    FireListBarVisibleChanged();
                    if (listBarVisible == true)
                    {
                        ShowListBar();
                    }
                    else
                    {
                        HideListBar();
                    }                    
                }
            }
        }


        /// D�clenche l'�v�nement ListBarVisibleChanged.

        private void FireListBarVisibleChanged()
        {
            ListBarVisibleChangedEventHandler listBarVisibleChanged = ListBarVisibleChanged;
            if (listBarVisibleChanged != null)
            {
                listBarVisibleChanged(this, new ListBarVisibleChangedArgs(listBarVisible));
            }
        }


        /// Affiche la ListBar.

        private void ShowListBar()
        {
            if (listBar == null)
            {
                Settings.Settings settings = SettingManager.Instance.Settings;
                listBar = new BrainStormListBar();
                listBar.ShowInTaskbar = settings.ShowListBarInTaskBar;
                listBar.Location = settings.ListBarLocation;
                listBar.Size = settings.ListBarSize;
                listBar.FormClosed += formClosedEventHandler;
                listBar.Show();
            }
            else
            {
                if (listBar.TopMost == false)
                {
                    listBar.TopMost = true;
                    listBar.TopMost = false;
                }
            }
        }


        /// Masque la ListBar.

        private void HideListBar()
        {
            if (listBar != null)
            {
                listBar.FormClosed -= formClosedEventHandler;
                SaveConfiguration();
                listBar.Close();
                listBar = null;
            }
        }


        /// D�finis une valeur indiquant si la ListBar doit apparaitre dans la barre des t�ches.

        public bool ListBarShowInTaskBar
        {
            set
            {
                if (listBar != null)
                {
                    listBar.ShowInTaskbar = value;
                }
            }
        }
        #endregion // ListBar

        #region ToolBar

        #region Ev�nement ToolBarVisibleChanged

        /// D�l�gu� de r�ponse � l'�v�nement ToolBarVisibleChanged.

        /// <param name="source">ListBar.</param>
        /// <param name="e">Modification de la visibilit� de la ListBar.</param>
        public delegate void ToolBarVisibleChangedEventHandler(object source, ToolBarVisibleChangedArgs e);


        /// Ev�nement de changement de visibilit� de la ToolBar.
        /// D�clench� lorsque la ToolBar est affich�e/masqu�e.

        public event ToolBarVisibleChangedEventHandler ToolBarVisibleChanged;


        /// Arguments de l'�v�nement ToolBarVisibleChanged.
        /// Fournit la visibilit� courante de la ToolBar.

        public class ToolBarVisibleChangedArgs
        {
    
            /// Valeur indiquant si la ToolBar est visible.
    
            bool visible;

    
            /// Obtient une valeur indiquant si la ToolBar est visible.
    
            public bool Visible
            {
                get
                {
                    return (visible);
                }
            }

    
            /// Constructeur par d�faut.
    
            /// <param name="visible">Valeur indiquant si la ToolBar est actuellement visible.</param>
            public ToolBarVisibleChangedArgs(bool visible)
            {
                this.visible = visible;
            }
        }
        #endregion // Ev�nement ToolBarVisibleChanged


        /// Obtient ou d�finis une valeur indiquant si la ToolBar est visible.

        public bool ToolBarVisible
        {
            get
            {
                return (toolBarVisible);
            }
            set
            {
                if (toolBarVisible != value)
                {
                    toolBarVisible = value;
                    FireToolBarVisibleChanged();
                    if (toolBarVisible == true)
                    {
                        ShowToolBar();
                    }
                    else
                    {
                        HideToolBar();
                    }
                }
            }
        }


        /// D�clenche l'�v�nement ToolBarVisibleChanged.

        private void FireToolBarVisibleChanged()
        {
            ToolBarVisibleChangedEventHandler toolBarVisibleChanged = ToolBarVisibleChanged;
            if (toolBarVisibleChanged != null)
            {
                toolBarVisibleChanged(this, new ToolBarVisibleChangedArgs(toolBarVisible));
            }
        }


        /// Affiche la ToolBar.

        private void ShowToolBar()
        {
            if (toolBar == null)
            {
                Settings.Settings settings = SettingManager.Instance.Settings;
                toolBar = new BrainStormToolBar();
                toolBar.BrainStormApplication = brainStormApplication;
                toolBar.Location = settings.ToolBarLocation;
                toolBar.Size = settings.ToolBarSize;
                toolBar.FormClosed += formClosedEventHandler;
                toolBar.Show();
            }
            else
            {
                if (toolBar.TopMost == false)
                {
                    toolBar.TopMost = true;
                    toolBar.TopMost = false;
                }
            }
        }


        /// Masque la ToolBar.

        private void HideToolBar()
        {
            if (toolBar != null)
            {
                toolBar.FormClosed -= formClosedEventHandler;
                SaveConfiguration();
                toolBar.Close();
                toolBar = null;
            }
        }


        /// D�finis une valeur indiquant si la ToolBar doit �tre affich�e dans la barre des t�ches.

        public bool ToolBarShowInTaskBar
        {
            set
            {
                toolBar.ShowInTaskbar = value;
            }
        }
        #endregion // ToolBar

        #region Activation / D�sactivation de l'affichage TopMost

        /// Autorise ou bloque l'affichage des Forms en TopMost.

        /// <param name="enable"><c>true</c> pour autoriser l'affichage en TopMost, <c>false</c> pour le d�sactiver.</param>
        public void EnableTopMostForms(bool enable)
        {
            if (enable)
            {
                // On autorise l'affiche en TopMost des fen�tres.
                if (listBar != null)
                {
                    listBar.TopMost = listBarTopMost;
                }
                if (toolBar != null)
                {
                    toolBar.TopMost = toolBarTopMost;
                }
            }
            else
            {
                // On d�sactive l'affichage TopMost des fen�tres concern�es.
                if (listBar != null)
                {
                    listBarTopMost = listBar.TopMost;
                    listBar.TopMost = false;
                }
                if (toolBar != null)
                {
                    toolBarTopMost = toolBar.TopMost;
                    toolBar.TopMost = false;
                }
            }
        }
        #endregion // Activation / D�sactivation de l'affichage TopMost

        #region Gestion de la configuration

        /// Sauvegarde la configuration courante :
        /// - Position et taille de la ListBar
        /// - Position et taille de la ToolBar

        public void SaveConfiguration()
        {
            Settings.Settings settings = SettingManager.Instance.Settings;
            if (listBar != null)
            {
                settings.ListBarSize = listBar.Size;
                settings.ListBarLocation = listBar.Location;
            }
            if (toolBar != null)
            {
                settings.ToolBarSize = toolBar.Size;
                settings.ToolBarLocation = toolBar.Location;
            }
            SettingManager.Instance.Save();
        }
        #endregion // Gestion de la configuration

        #region Fermeture de l'application

        /// Fermeture de l'application : on ferme proprement les fen�tres g�r�es par le FormManager.

        public void Close()
        {
            if (listBar != null)
            {
                listBar.FormClosed -= formClosedEventHandler;
            }
            if (toolBar != null)
            {
                toolBar.FormClosed -= formClosedEventHandler;
            }
            HideToolBar();
            HideListBar();
        }


        /// R�ponse aux �v�nements de fermetures des fen�tres :
        /// - ListBar
        /// - ToolBar
        /// Lors la fermeture d'une fen�tre, la propri�t� Visible est modifi�e � <c>false</c>.

        /// <param name="sender">ListBar ou ToolBar.</param>
        /// <param name="e">Fermeture de la fen�tre.</param>
        void formClosed(object sender, FormClosedEventArgs e)
        {
            if (sender == listBar)
            {
                ListBarVisible = false;
            }
            else if (sender == toolBar)
            {
                ToolBarVisible = false;
            }
        }
        #endregion // Fermeture de l'application

        #region Propri�t�s

        /// D�finis la r�f�rence vers la fen�tre principale de l'application.

        public BrainStormApplication BrainStormApplication
        {
            set
            {
                brainStormApplication = value;
                if (toolBar != null)
                {
                    toolBar.BrainStormApplication = brainStormApplication;
                }
            }
        }
        #endregion // Propri�t�s
    }
}
