using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Smilly.BrainStorm.Settings
{
    /// <summary>
    /// Paramètres de l'application.
    /// </summary>
    [Serializable]
    public class Settings
    {
        #region Variables locales

        /// Fichier stockant les BrainStorms.

        private string brainStormsFile;


        /// Valeur indiquant si la ListBar doit être affichée dans la TaskBar.

        private bool showListBarInTaskBar;


        /// Position initiale de la ListBar.

        private Point listBarLocation;


        /// Taille initiale de la ListBar.

        private Size listBarSize;


        /// Valeur indiquant si la ListBar est visible au démarrage de l'application.

        private bool showListBar;


        /// Position initiale de la ToolBar.

        private Point toolBarLocation;


        /// Taille initiale de la ToolBar.

        private Size toolBarSize;


        /// Valeur indiquant si la ToolBar est visible au démarrage de l'application.

        private bool showToolBar;


        /// Valeur indiquant si la ToolBar est affichée en mode vertical.

        private bool verticalToolBar;
        #endregion // Variables locales

        #region Propriétés

        /// Obtient ou définis le fichier stockant les BrainStorms.

        public string BrainStormsFile
        {
            get
            {
                return (brainStormsFile);
            }
            set
            {
                brainStormsFile = value;
            }
        }


        /// Obtient ou définis une valeur indiquant si la ListBar doit être affichée dans la TaskBar.

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


        /// Obtient ou définis la position initiale de la ListBar.

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


        /// Obtient ou définis la taille initiale de la ListBar.

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


        /// Obtient ou définis une valeur indiquant si la ListBar est visible au démarrage de l'application.

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


        /// Obtient ou définis la position initiale de la ToolBar.

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


        /// Obtient ou définis la taille initiale de la ToolBar.

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


        /// Obtient ou définis une valeur indiquant si la ToolBar est visible au démarrage de l'application.

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


        /// Obtient ou définis une valeur indiquant si la ToolBar est affichée en mode vertical.

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
        #endregion // Propriétés

        #region Construction / Initialisation

        /// Constructeur par défaut. Initialisation des paramètres par défaut.

        public Settings()
        {
            brainStormsFile = ".\\BrainStorms.nots";
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
