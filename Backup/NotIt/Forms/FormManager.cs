using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Nikoui.NotIt.Settings;

namespace Nikoui.NotIt.Forms
{
    /// <summary>
    /// Gestion des Forms spécifiques de l'applications :
    /// - ListBar
    /// - ToolBar
    /// Permet l'affichage/le masquage de ces Forms.
    /// </summary>
    public sealed class FormManager
    {
		#region Variables locales
        /// <summary>
        /// Instance unique du FormManager.
        /// </summary>
		private static volatile FormManager instance = null;

        /// <summary>
        /// Objet de synchronisation utilisé pour assurer l'unicité du singleton.
        /// </summary>
		private static object syncRoot = new Object();

        /// <summary>
        /// Valeur indiquant si la ListBar est visible.
        /// </summary>
        private bool listBarVisible;

        /// <summary>
        /// ListBar de l'application.
        /// </summary>
        private NotItListBar listBar;

        /// <summary>
        /// Valeur indiquant si la ToolBar est visible.
        /// </summary>
        private bool toolBarVisible;

        /// <summary>
        /// ToolBar de l'application.
        /// </summary>
        private NotItToolBar toolBar;

        /// <summary>
        /// Sauvegarde temporaire de la propriété TopMost de la ListBar.
        /// </summary>
        private bool listBarTopMost;

        /// <summary>
        /// Sauvegarde temporaire de la propriété TopMost de la ToolBar.
        /// </summary>
        private bool toolBarTopMost;

        /// <summary>
        /// Réponse aux évènements de fermeture de la ListBar et de la ToolBar.
        /// </summary>
        private FormClosedEventHandler formClosedEventHandler;

        /// <summary>
        /// Référence vers la fenêtre principale de l'application.
        /// </summary>
        private NotItApplication notItApplication;
		#endregion // Variables locales

		#region Construction / Initialisation
        /// <summary>
        /// Constructeur privé du singleton.
        /// </summary>
		private FormManager()
		{
            notItApplication = null;
            formClosedEventHandler = new FormClosedEventHandler(formClosed);
            listBarVisible = false;
            toolBarVisible = false;
            listBarTopMost = false;
		}
		#endregion // Construction / Initialisation

		#region Accès à l'instance
        /// <summary>
        /// Obtient l'instance unique du FormManager.
        /// </summary>
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
		#endregion // Accès à l'instance

        #region ListBar

        #region Evènement ListBarVisibleChanged
        /// <summary>
        /// Délégué de réponse à l'évènement ListBarVisibleChanged.
        /// </summary>
        /// <param name="source">ListBar.</param>
        /// <param name="e">Modification de la visibilité de la ListBar.</param>
        public delegate void ListBarVisibleChangedEventHandler(object source, ListBarVisibleChangedArgs e);

        /// <summary>
        /// Evènement de changement de visibilité de la ListBar.
        /// Déclenché lorsque la ListBar est affichée/masquée.
        /// </summary>
        public event ListBarVisibleChangedEventHandler ListBarVisibleChanged;

        /// <summary>
        /// Arguments de l'évènement ListBarVisibleChanged.
        /// Fournit la visibilité courante de la ListBar.
        /// </summary>
        public class ListBarVisibleChangedArgs
        {
            /// <summary>
            /// Valeur indiquant si la ListBar est visible.
            /// </summary>
            bool visible;

            /// <summary>
            /// Obtient une valeur indiquant si la ListBar est visible.
            /// </summary>
            public bool Visible
            {
                get
                {
                    return (visible);
                }
            }

            /// <summary>
            /// Construction par défaut.
            /// </summary>
            /// <param name="visible">Valeur indiquant si la ListBar est actuellement visible.</param>
            public ListBarVisibleChangedArgs(bool visible)
            {
                this.visible = visible;
            }
        }
        #endregion // Evènement ListBarVisibleChanged

        /// <summary>
        /// Obtient ou définis une valeur indiquant si la ListBar est visible.
        /// </summary>
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

        /// <summary>
        /// Déclenche l'évènement ListBarVisibleChanged.
        /// </summary>
        private void FireListBarVisibleChanged()
        {
            ListBarVisibleChangedEventHandler listBarVisibleChanged = ListBarVisibleChanged;
            if (listBarVisibleChanged != null)
            {
                listBarVisibleChanged(this, new ListBarVisibleChangedArgs(listBarVisible));
            }
        }

        /// <summary>
        /// Affiche la ListBar.
        /// </summary>
        private void ShowListBar()
        {
            if (listBar == null)
            {
                Settings.Settings settings = SettingManager.Instance.Settings;
                listBar = new NotItListBar();
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

        /// <summary>
        /// Masque la ListBar.
        /// </summary>
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

        /// <summary>
        /// Définis une valeur indiquant si la ListBar doit apparaitre dans la barre des tâches.
        /// </summary>
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

        #region Evènement ToolBarVisibleChanged
        /// <summary>
        /// Délégué de réponse à l'évènement ToolBarVisibleChanged.
        /// </summary>
        /// <param name="source">ListBar.</param>
        /// <param name="e">Modification de la visibilité de la ListBar.</param>
        public delegate void ToolBarVisibleChangedEventHandler(object source, ToolBarVisibleChangedArgs e);

        /// <summary>
        /// Evènement de changement de visibilité de la ToolBar.
        /// Déclenché lorsque la ToolBar est affichée/masquée.
        /// </summary>
        public event ToolBarVisibleChangedEventHandler ToolBarVisibleChanged;

        /// <summary>
        /// Arguments de l'évènement ToolBarVisibleChanged.
        /// Fournit la visibilité courante de la ToolBar.
        /// </summary>
        public class ToolBarVisibleChangedArgs
        {
            /// <summary>
            /// Valeur indiquant si la ToolBar est visible.
            /// </summary>
            bool visible;

            /// <summary>
            /// Obtient une valeur indiquant si la ToolBar est visible.
            /// </summary>
            public bool Visible
            {
                get
                {
                    return (visible);
                }
            }

            /// <summary>
            /// Constructeur par défaut.
            /// </summary>
            /// <param name="visible">Valeur indiquant si la ToolBar est actuellement visible.</param>
            public ToolBarVisibleChangedArgs(bool visible)
            {
                this.visible = visible;
            }
        }
        #endregion // Evènement ToolBarVisibleChanged

        /// <summary>
        /// Obtient ou définis une valeur indiquant si la ToolBar est visible.
        /// </summary>
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

        /// <summary>
        /// Déclenche l'évènement ToolBarVisibleChanged.
        /// </summary>
        private void FireToolBarVisibleChanged()
        {
            ToolBarVisibleChangedEventHandler toolBarVisibleChanged = ToolBarVisibleChanged;
            if (toolBarVisibleChanged != null)
            {
                toolBarVisibleChanged(this, new ToolBarVisibleChangedArgs(toolBarVisible));
            }
        }

        /// <summary>
        /// Affiche la ToolBar.
        /// </summary>
        private void ShowToolBar()
        {
            if (toolBar == null)
            {
                Settings.Settings settings = SettingManager.Instance.Settings;
                toolBar = new NotItToolBar();
                toolBar.NotItApplication = notItApplication;
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

        /// <summary>
        /// Masque la ToolBar.
        /// </summary>
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

        /// <summary>
        /// Définis une valeur indiquant si la ToolBar doit être affichée dans la barre des tâches.
        /// </summary>
        public bool ToolBarShowInTaskBar
        {
            set
            {
                toolBar.ShowInTaskbar = value;
            }
        }
        #endregion // ToolBar

        #region Activation / Désactivation de l'affichage TopMost
        /// <summary>
        /// Autorise ou bloque l'affichage des Forms en TopMost.
        /// </summary>
        /// <param name="enable"><c>true</c> pour autoriser l'affichage en TopMost, <c>false</c> pour le désactiver.</param>
        public void EnableTopMostForms(bool enable)
        {
            if (enable)
            {
                // On autorise l'affiche en TopMost des fenêtres.
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
                // On désactive l'affichage TopMost des fenêtres concernées.
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
        #endregion // Activation / Désactivation de l'affichage TopMost

        #region Gestion de la configuration
        /// <summary>
        /// Sauvegarde la configuration courante :
        /// - Position et taille de la ListBar
        /// - Position et taille de la ToolBar
        /// </summary>
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
        /// <summary>
        /// Fermeture de l'application : on ferme proprement les fenêtres gérées par le FormManager.
        /// </summary>
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

        /// <summary>
        /// Réponse aux évènements de fermetures des fenêtres :
        /// - ListBar
        /// - ToolBar
        /// Lors la fermeture d'une fenêtre, la propriété Visible est modifiée à <c>false</c>.
        /// </summary>
        /// <param name="sender">ListBar ou ToolBar.</param>
        /// <param name="e">Fermeture de la fenêtre.</param>
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

        #region Propriétés
        /// <summary>
        /// Définis la référence vers la fenêtre principale de l'application.
        /// </summary>
        public NotItApplication NotItApplication
        {
            set
            {
                notItApplication = value;
                if (toolBar != null)
                {
                    toolBar.NotItApplication = notItApplication;
                }
            }
        }
        #endregion // Propriétés
    }
}
