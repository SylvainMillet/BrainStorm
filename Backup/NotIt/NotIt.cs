using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Nikoui.NotIt.Forms;
using Nikoui.NotIt.Properties;

namespace Nikoui.NotIt
{
    /// <summary>
    /// Repr�sentation d'une NotIt.
    /// Une NotIt poss�de :
    /// - un titre.
    /// - des d�tails (contenu principal de la NotIt).
    /// - des coordonn�es sur l'�cran.
    /// - un id unique.
    /// - une repr�sentation (vue).
    /// </summary>
    [Serializable]
    public class NotIt
    {
        #region Variables locales
        /// <summary>
        /// Titre de la NotIt.
        /// </summary>
        private string title;

        /// <summary>
        /// D�tails de la NotIt.
        /// </summary>
        private string details;

        /// <summary>
        /// Coordonn�es sur l'�cran o� afficher la NotIt.
        /// </summary>
        private Point location;

        /// <summary>
        /// Repr�sentation de la NotIt (vue).
        /// </summary>
        [NonSerialized]
        private NotItView view;

        /// <summary>
        /// Identifiant unique de la NotIt.
        /// </summary>
        private int id;

        /// <summary>
        /// Indique si la NotIt est �pingl�e.
        /// </summary>
        private bool pinned;
        #endregion // Variables locales

        #region Propri�t�s
        /// <summary>
        /// Obtient ou d�finis le titre de la NotIt.
        /// </summary>
        public string Title
        {
            get
            {
                return (title);
            }
            set
            {
                title = value;
                // La NotIt � �t� modifi�e, notification du changement.
                FireStatusChanged();
            }
        }

        /// <summary>
        /// Obtient ou d�finis les d�tails de la NotIt.
        /// </summary>
        public string Details
        {
            get
            {
                return (details);
            }
            set
            {
                details = value;
                // La NotIt � �t� modifi�e, notification du changement.
                FireStatusChanged();
            }
        }

        /// <summary>
        /// Obtient ou d�finis les coordonn�es sur l'�cran o� afficher la NotIt.
        /// </summary>
        public Point Location
        {
            get
            {
                return (location);
            }
            set
            {
                location = value;
                // La NotIt � �t� modifi�e, notification du changement.
                FireStatusChanged();
            }
        }

        /// <summary>
        /// Obtient l'identifiant unique de la NotIt.
        /// </summary>
        public int Id
        {
            get
            {
                return (id);
            }
        }

        /// <summary>
        /// Obtient ou d�finis une valeur indiquant si la NotIt est �pingl�e.
        /// </summary>
        public bool Pinned
        {
            get
            {
                return (pinned);
            }
            set
            {
                pinned = value;
                // La NotIt � �t� modifi�e, notification du changement.
                FireStatusChanged();
            }
        }
        #endregion // Propri�t�s

        #region Construction / Initialisation
        /// <summary>
        /// Constructeur par d�faut.
        /// Construction d'une NotIt � partir d'un identifiant unique (fournit par le contr�ler).
        /// </summary>
        /// <param name="id">Identifiant unique de la NotIt � construire.</param>
        public NotIt(int id)
        {
            this.id = id;
            SetDefaultSettings();
            // On associe une vue � la NotIt.
            AttachView();
            CenterViewOnCursor();
        }

        /// <summary>
        /// Initialize les variables locales avec les valeurs par d�faut.
        /// </summary>
        private void SetDefaultSettings()
        {
            title = Resources.DefaultTitle;
            details = Resources.DefaultDetails;
            pinned = false;
            location = System.Windows.Forms.Cursor.Position;
        }

        /// <summary>
        /// D�calle la NotIt pour qu'elle soit cr��e de fa�on � �tre "centr�e" sur le curseur.
        /// </summary>
        private void CenterViewOnCursor()
        {
            Point centerPoint = location;
            centerPoint.X -= view.Width / 2;
            centerPoint.Y -= view.Height / 2;
            Location = centerPoint;
        }
        #endregion // Construction / Initialisation

        #region Gestion de la vue associ�e
        /// <summary>
        /// Associe une vue � la NotIt.
        /// </summary>
        public void AttachView()
        {
            if (view == null)
            {
                view = new NotItView(this);
                view.Show();
            }
        }

        /// <summary>
        /// D�tache la vue associ�e de la NotIt.
        /// </summary>
        public void DettachView()
        {
            if (view != null)
            {
                view.Detach(this);
                view.Close();
                view = null;
            }
        }
        #endregion // Gestion de la vue associ�e

        #region Affichage de la NotIt
        /// <summary>
        /// Affiche la NotIt en premier plan.
        /// </summary>
        public void Show()
        {
            ShowedEventHandler showed = Showed;
            if (showed != null)
            {
                showed(this);
            }
        }

        /// <summary>
        /// Masque la NotIt.
        /// </summary>
        public void Hide()
        {
            HidedEventHandler hided = Hided;
            if (hided != null)
            {
                hided(this);
            }
        }
        #endregion // Affichage de la NotIt

        #region Ev�nements

        #region Ev�nement Showed
        /// <summary>
        /// D�l�gu� de r�ponse � l'�v�nement Showed.
        /// </summary>
        /// <param name="source">NotIt source de l'�v�nement.</param>
        public delegate void ShowedEventHandler(object source);

        /// <summary>
        /// Ev�nement Showed.
        /// D�clench� lorsque une requ�te d'affichage au premier plan est appliqu�e � la NotIt.
        /// </summary>
        public event ShowedEventHandler Showed;
        #endregion // Ev�nement Showed

        #region Ev�nement Hided
        /// <summary>
        /// D�l�gu� de r�ponse � l'�v�nement Hided.
        /// </summary>
        /// <param name="source">NotIt source de l'�v�nement.</param>
        public delegate void HidedEventHandler(object source);

        /// <summary>
        /// Ev�nement Hided.
        /// D�clench� lorsque une requ�te de masquage est appliqu�e � la NotIt.
        /// </summary>
        public event HidedEventHandler Hided;
        #endregion // Ev�nement Hided

        #region Ev�nement StatusChanged
        /// <summary>
        /// D�clenche l'�v�nement StatusChanged.
        /// </summary>
        private void FireStatusChanged()
        {
            StatusChangedEventHandler statusChanged = StatusChanged;
            if (statusChanged != null)
            {
                statusChanged(this);
            }
        }

        /// <summary>
        /// D�l�gu� de r�ponse � l'�v�nement StatusChanged.
        /// </summary>
        /// <param name="source">NotIt source de l'�v�nement.</param>
        public delegate void StatusChangedEventHandler(object source);

        /// <summary>
        /// Ev�nement StatusChanged.
        /// D�clench� lorsque une ou plusieurs propri�t�s de la NotIt sont modifi�es.
        /// </summary>
        public event StatusChangedEventHandler StatusChanged;
        #endregion // Ev�nement StatusChanged

        #endregion // Ev�nements
    }
}
