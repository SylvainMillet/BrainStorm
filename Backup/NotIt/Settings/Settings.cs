using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Nikoui.NotIt.Settings
{
    /// <summary>
    /// Param�tres de l'application.
    /// </summary>
    [Serializable]
    public class Settings
    {
        #region Variables locales
        /// <summary>
        /// Fichier stockant les NotIts.
        /// </summary>
        private string notItsFile;

        /// <summary>
        /// Valeur indiquant si la ListBar doit �tre affich�e dans la TaskBar.
        /// </summary>
        private bool showListBarInTaskBar;

        /// <summary>
        /// Position initiale de la ListBar.
        /// </summary>
        private Point listBarLocation;

        /// <summary>
        /// Taille initiale de la ListBar.
        /// </summary>
        private Size listBarSize;

        /// <summary>
        /// Valeur indiquant si la ListBar est visible au d�marrage de l'application.
        /// </summary>
        private bool showListBar;

        /// <summary>
        /// Position initiale de la ToolBar.
        /// </summary>
        private Point toolBarLocation;

        /// <summary>
        /// Taille initiale de la ToolBar.
        /// </summary>
        private Size toolBarSize;

        /// <summary>
        /// Valeur indiquant si la ToolBar est visible au d�marrage de l'application.
        /// </summary>
        private bool showToolBar;

        /// <summary>
        /// Valeur indiquant si la ToolBar est affich�e en mode vertical.
        /// </summary>
        private bool verticalToolBar;
        #endregion // Variables locales

        #region Propri�t�s
        /// <summary>
        /// Obtient ou d�finis le fichier stockant les NotIts.
        /// </summary>
        public string NotItsFile
        {
            get
            {
                return (notItsFile);
            }
            set
            {
                notItsFile = value;
            }
        }

        /// <summary>
        /// Obtient ou d�finis une valeur indiquant si la ListBar doit �tre affich�e dans la TaskBar.
        /// </summary>
        public bool ShowListBarInTaskBar
        {
            get
            {
                return (showListBarInTaskBar);
            }
            set
            {
                showListBarInTaskBar = value;
            }
        }

        /// <summary>
        /// Obtient ou d�finis la position initiale de la ListBar.
        /// </summary>
        public Point ListBarLocation
        {
            get
            {
                return (listBarLocation);
            }
            set
            {
                listBarLocation = value;
            }
        }

        /// <summary>
        /// Obtient ou d�finis la taille initiale de la ListBar.
        /// </summary>
        public Size ListBarSize
        {
            get
            {
                return (listBarSize);
            }
            set
            {
                listBarSize = value;
            }
        }

        /// <summary>
        /// Obtient ou d�finis une valeur indiquant si la ListBar est visible au d�marrage de l'application.
        /// </summary>
        public bool ShowListBar
        {
            get
            {
                return (showListBar);
            }
            set
            {
                showListBar = value;
            }
        }

        /// <summary>
        /// Obtient ou d�finis la position initiale de la ToolBar.
        /// </summary>
        public Point ToolBarLocation
        {
            get
            {
                return (toolBarLocation);
            }
            set
            {
                toolBarLocation = value;
            }
        }

        /// <summary>
        /// Obtient ou d�finis la taille initiale de la ToolBar.
        /// </summary>
        public Size ToolBarSize
        {
            get
            {
                return (toolBarSize);
            }
            set
            {
                toolBarSize = value;
            }
        }

        /// <summary>
        /// Obtient ou d�finis une valeur indiquant si la ToolBar est visible au d�marrage de l'application.
        /// </summary>
        public bool ShowToolBar
        {
            get
            {
                return (showToolBar);
            }
            set
            {
                showToolBar = value;
            }
        }

        /// <summary>
        /// Obtient ou d�finis une valeur indiquant si la ToolBar est affich�e en mode vertical.
        /// </summary>
        public bool VerticalToolBar
        {
            get
            {
                return (verticalToolBar);
            }
            set
            {
                verticalToolBar = value;
            }
        }
        #endregion // Propri�t�s

        #region Construction / Initialisation
        /// <summary>
        /// Constructeur par d�faut. Initialisation des param�tres par d�faut.
        /// </summary>
        public Settings()
        {
            notItsFile = ".\\NotIts.nots";
            showListBarInTaskBar = true;
            listBarLocation = new Point(0, 0);
            listBarSize = new Size(100, 200);
            showListBar = true;
            toolBarLocation = new Point(100, 0);
            toolBarSize = new Size(222, 67);
            verticalToolBar = false;
            showToolBar = true;
        }
        #endregion // Construction / Initialisation
    }
}
