using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Smilly.BrainStorm.Forms;
using Smilly.BrainStorm.Properties;

namespace Smilly.BrainStorm
{
    /// Représentation d'une BrainStorm.
    /// Une BrainStorm possède :
    /// - un titre.
    /// - des détails (contenu principal de la BrainStorm).
    /// - des coordonnées sur l'écran.
    /// - un id unique.
    /// - une représentation (vue).
    /// - un type (couleur change).
    [Serializable]
    public class BrainStorm
    {

        #region Variables locales
        /// Titre de la BrainStorm.
        private string title;

        /// Détails de la BrainStorm.
        private string details;

        /// Coordonnées sur l'écran où afficher la BrainStorm.
        private Point location;

        /// Représentation de la BrainStorm (vue).
        [NonSerialized]
        private BrainStormView view;

        /// Identifiant unique de la BrainStorm.
        private int id;

        /// Indique si la BrainStorm est épinglée.
        private bool pinned;

        /// Type unique de la BrainStorm.
        private int type;

        #endregion // Variables locales

        #region Propriétés
        /// Obtient ou définis le titre de la BrainStorm.
        public string Title
        {
            get
            {
                return (title);
            }
            set
            {
                title = value;
                // La BrainStorm a été modifiée, notification du changement.
                FireStatusChanged();
            }
        }


        /// Obtient ou définis les détails de la BrainStorm.

        public string Details
        {
            get
            {
                return (details);
            }
            set
            {
                details = value;
                // La BrainStorm à été modifiée, notification du changement.
                FireStatusChanged();
            }
        }


        /// Obtient ou définis les coordonnées sur l'écran où afficher la BrainStorm.

        public Point Location
        {
            get
            {
                return (location);
            }
            set
            {
                location = value;
                // La BrainStorm à été modifiée, notification du changement.
                FireStatusChanged();
            }
        }


        /// Obtient ou définis le type de la BrainStorm.

        public int Type
        {
            get
            {
                return (type);
            }
            set
            {
                type = value;
                // La BrainStorm à été modifiée, notification du changement.
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


        /// Obtient ou définis une valeur indiquant si la BrainStorm est épinglée.

        public bool Pinned
        {
            get
            {
                return (pinned);
            }
            set
            {
                pinned = value;
                // La BrainStorm à été modifiée, notification du changement.
                FireStatusChanged();
            }
        }
        #endregion // Propriétés

        #region Construction / Initialisation
        /// Constructeur par défaut.
        /// Construction d'une BrainStorm à partir d'un identifiant unique (fournit par le controller).
        /// <param name="id">Identifiant unique de la BrainStorm à construire.</param>
        public BrainStorm(int id, int type)
        {
            this.id = id;
            this.type = type;
            SetDefaultSettings();
            // On associe une vue à la BrainStorm.
            AttachView();
            CenterViewOnCursor();
        }

        /// Initialize les variables locales avec les valeurs par défaut.
        private void SetDefaultSettings()
        {
            title = Resources.DefaultTitle;
            details = Resources.DefaultDetails;
            pinned = false;
            location = System.Windows.Forms.Cursor.Position;
        }

        /// Décalle la BrainStorm pour qu'elle soit créée de façon à être "centrée" sur le curseur.
        private void CenterViewOnCursor()
        {
            Point centerPoint = location;
            centerPoint.X -= view.Width / 2;
            centerPoint.Y -= view.Height / 2;
            Location = centerPoint;
        }
        #endregion // Construction / Initialisation

        #region Gestion de la vue associée
        /// Associe une vue à la BrainStorm.
        public void AttachView()
        {
            if (view == null)
            {
                view = new BrainStormView(this, type);
                view.Show();
            }
        }

        /// Détache la vue associée de la BrainStorm.
        public void DettachView()
        {
            if (view != null)
            {
                view.Detach(this);
                view.Close();
                view = null;
            }
        }
        #endregion // Gestion de la vue associée

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

        #region Evènements

        #region Evènement Showed
        /// Délégué de réponse à l'évènement Showed.
        /// <param name="source">BrainStorm source de l'évènement.</param>
        public delegate void ShowedEventHandler(object source);

        /// Evènement Showed.
        /// Déclenché lorsque une requête d'affichage au premier plan est appliquée à la BrainStorm.
        public event ShowedEventHandler Showed;
        #endregion // Evènement Showed

        #region Evènement Hided
        /// Délégué de réponse à l'évènement Hided.
        /// <param name="source">BrainStorm source de l'évènement.</param>
        public delegate void HidedEventHandler(object source);

        /// Evènement Hided.
        /// Déclenché lorsque une requête de masquage est appliquée à la BrainStorm.
        public event HidedEventHandler Hided;
        #endregion // Evènement Hided

        #region Evènement StatusChanged
        /// Déclenche l'évènement StatusChanged.
        private void FireStatusChanged()
        {
            StatusChangedEventHandler statusChanged = StatusChanged;
            if (statusChanged != null)
            {
                statusChanged(this);
            }
        }

        /// Délégué de réponse à l'évènement StatusChanged.
        /// <param name="source">BrainStorm source de l'évènement.</param>
        public delegate void StatusChangedEventHandler(object source);

        /// Evènement StatusChanged.
        /// Déclenché lorsque une ou plusieurs propriétés de la BrainStorm sont modifiées.
        public event StatusChangedEventHandler StatusChanged;
        #endregion // Evènement StatusChanged

        #endregion // Evènements
    }
}
