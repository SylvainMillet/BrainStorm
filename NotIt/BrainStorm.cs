using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Smilly.BrainStorm.Forms;
using Smilly.BrainStorm.Properties;

namespace Smilly.BrainStorm
{
    /// Repr�sentation d'une BrainStorm.
    /// Une BrainStorm poss�de :
    /// - un titre.
    /// - des d�tails (contenu principal de la BrainStorm).
    /// - des coordonn�es sur l'�cran.
    /// - un id unique.
    /// - une repr�sentation (vue).
    /// - un type (couleur change).
    [Serializable]
    public class BrainStorm
    {

        #region Variables locales
        /// Titre de la BrainStorm.
        private string title;

        /// D�tails de la BrainStorm.
        private string details;

        /// Coordonn�es sur l'�cran o� afficher la BrainStorm.
        private Point location;

        /// Repr�sentation de la BrainStorm (vue).
        [NonSerialized]
        private BrainStormView view;

        /// Identifiant unique de la BrainStorm.
        private int id;

        /// Indique si la BrainStorm est �pingl�e.
        private bool pinned;

        /// Type unique de la BrainStorm.
        private int type;

        #endregion // Variables locales

        #region Propri�t�s
        /// Obtient ou d�finis le titre de la BrainStorm.
        public string Title
        {
            get
            {
                return (title);
            }
            set
            {
                title = value;
                // La BrainStorm a �t� modifi�e, notification du changement.
                FireStatusChanged();
            }
        }


        /// Obtient ou d�finis les d�tails de la BrainStorm.

        public string Details
        {
            get
            {
                return (details);
            }
            set
            {
                details = value;
                // La BrainStorm � �t� modifi�e, notification du changement.
                FireStatusChanged();
            }
        }


        /// Obtient ou d�finis les coordonn�es sur l'�cran o� afficher la BrainStorm.

        public Point Location
        {
            get
            {
                return (location);
            }
            set
            {
                location = value;
                // La BrainStorm � �t� modifi�e, notification du changement.
                FireStatusChanged();
            }
        }


        /// Obtient ou d�finis le type de la BrainStorm.

        public int Type
        {
            get
            {
                return (type);
            }
            set
            {
                type = value;
                // La BrainStorm � �t� modifi�e, notification du changement.
                FireStatusChanged();
            }
        }


        /// Obtient l'identifiant unique de la BrainStorm.

        public int Id
        {
            get
            {
                return (id);
            }
        }


        /// Obtient ou d�finis une valeur indiquant si la BrainStorm est �pingl�e.

        public bool Pinned
        {
            get
            {
                return (pinned);
            }
            set
            {
                pinned = value;
                // La BrainStorm � �t� modifi�e, notification du changement.
                FireStatusChanged();
            }
        }
        #endregion // Propri�t�s

        #region Construction / Initialisation
        /// Constructeur par d�faut.
        /// Construction d'une BrainStorm � partir d'un identifiant unique (fournit par le controller).
        /// <param name="id">Identifiant unique de la BrainStorm � construire.</param>
        public BrainStorm(int id, int type)
        {
            this.id = id;
            this.type = type;
            SetDefaultSettings();
            // On associe une vue � la BrainStorm.
            AttachView();
            CenterViewOnCursor();
        }

        /// Initialize les variables locales avec les valeurs par d�faut.
        private void SetDefaultSettings()
        {
            title = Resources.DefaultTitle;
            details = Resources.DefaultDetails;
            pinned = false;
            location = System.Windows.Forms.Cursor.Position;
        }

        /// D�calle la BrainStorm pour qu'elle soit cr��e de fa�on � �tre "centr�e" sur le curseur.
        private void CenterViewOnCursor()
        {
            Point centerPoint = location;
            centerPoint.X -= view.Width / 2;
            centerPoint.Y -= view.Height / 2;
            Location = centerPoint;
        }
        #endregion // Construction / Initialisation

        #region Gestion de la vue associ�e
        /// Associe une vue � la BrainStorm.
        public void AttachView()
        {
            if (view == null)
            {
                view = new BrainStormView(this, type);
                view.Show();
            }
        }

        /// D�tache la vue associ�e de la BrainStorm.
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

        #region Affichage de la BrainStorm
        /// Affiche la BrainStorm en premier plan.
        public void Show()
        {
            ShowedEventHandler showed = Showed;
            if (showed != null)
            {
                showed(this);
            }
        }

        /// Masque la BrainStorm.
        public void Hide()
        {
            HidedEventHandler hided = Hided;
            if (hided != null)
            {
                hided(this);
            }
        }
        #endregion // Affichage de la BrainStorm

        #region Ev�nements

        #region Ev�nement Showed
        /// D�l�gu� de r�ponse � l'�v�nement Showed.
        /// <param name="source">BrainStorm source de l'�v�nement.</param>
        public delegate void ShowedEventHandler(object source);

        /// Ev�nement Showed.
        /// D�clench� lorsque une requ�te d'affichage au premier plan est appliqu�e � la BrainStorm.
        public event ShowedEventHandler Showed;
        #endregion // Ev�nement Showed

        #region Ev�nement Hided
        /// D�l�gu� de r�ponse � l'�v�nement Hided.
        /// <param name="source">BrainStorm source de l'�v�nement.</param>
        public delegate void HidedEventHandler(object source);

        /// Ev�nement Hided.
        /// D�clench� lorsque une requ�te de masquage est appliqu�e � la BrainStorm.
        public event HidedEventHandler Hided;
        #endregion // Ev�nement Hided

        #region Ev�nement StatusChanged
        /// D�clenche l'�v�nement StatusChanged.
        private void FireStatusChanged()
        {
            StatusChangedEventHandler statusChanged = StatusChanged;
            if (statusChanged != null)
            {
                statusChanged(this);
            }
        }

        /// D�l�gu� de r�ponse � l'�v�nement StatusChanged.
        /// <param name="source">BrainStorm source de l'�v�nement.</param>
        public delegate void StatusChangedEventHandler(object source);

        /// Ev�nement StatusChanged.
        /// D�clench� lorsque une ou plusieurs propri�t�s de la BrainStorm sont modifi�es.
        public event StatusChangedEventHandler StatusChanged;
        #endregion // Ev�nement StatusChanged

        #endregion // Ev�nements
    }
}
