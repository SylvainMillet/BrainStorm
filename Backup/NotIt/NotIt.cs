using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Nikoui.NotIt.Forms;
using Nikoui.NotIt.Properties;

namespace Nikoui.NotIt
{
    /// <summary>
    /// Représentation d'une NotIt.
    /// Une NotIt possède :
    /// - un titre.
    /// - des détails (contenu principal de la NotIt).
    /// - des coordonnées sur l'écran.
    /// - un id unique.
    /// - une représentation (vue).
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
        /// Détails de la NotIt.
        /// </summary>
        private string details;

        /// <summary>
        /// Coordonnées sur l'écran où afficher la NotIt.
        /// </summary>
        private Point location;

        /// <summary>
        /// Représentation de la NotIt (vue).
        /// </summary>
        [NonSerialized]
        private NotItView view;

        /// <summary>
        /// Identifiant unique de la NotIt.
        /// </summary>
        private int id;

        /// <summary>
        /// Indique si la NotIt est épinglée.
        /// </summary>
        private bool pinned;
        #endregion // Variables locales

        #region Propriétés
        /// <summary>
        /// Obtient ou définis le titre de la NotIt.
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
                // La NotIt à été modifiée, notification du changement.
                FireStatusChanged();
            }
        }

        /// <summary>
        /// Obtient ou définis les détails de la NotIt.
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
                // La NotIt à été modifiée, notification du changement.
                FireStatusChanged();
            }
        }

        /// <summary>
        /// Obtient ou définis les coordonnées sur l'écran où afficher la NotIt.
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
                // La NotIt à été modifiée, notification du changement.
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
        /// Obtient ou définis une valeur indiquant si la NotIt est épinglée.
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
                // La NotIt à été modifiée, notification du changement.
                FireStatusChanged();
            }
        }
        #endregion // Propriétés

        #region Construction / Initialisation
        /// <summary>
        /// Constructeur par défaut.
        /// Construction d'une NotIt à partir d'un identifiant unique (fournit par le contrôler).
        /// </summary>
        /// <param name="id">Identifiant unique de la NotIt à construire.</param>
        public NotIt(int id)
        {
            this.id = id;
            SetDefaultSettings();
            // On associe une vue à la NotIt.
            AttachView();
            CenterViewOnCursor();
        }

        /// <summary>
        /// Initialize les variables locales avec les valeurs par défaut.
        /// </summary>
        private void SetDefaultSettings()
        {
            title = Resources.DefaultTitle;
            details = Resources.DefaultDetails;
            pinned = false;
            location = System.Windows.Forms.Cursor.Position;
        }

        /// <summary>
        /// Décalle la NotIt pour qu'elle soit créée de façon à être "centrée" sur le curseur.
        /// </summary>
        private void CenterViewOnCursor()
        {
            Point centerPoint = location;
            centerPoint.X -= view.Width / 2;
            centerPoint.Y -= view.Height / 2;
            Location = centerPoint;
        }
        #endregion // Construction / Initialisation

        #region Gestion de la vue associée
        /// <summary>
        /// Associe une vue à la NotIt.
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
        /// Détache la vue associée de la NotIt.
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
        #endregion // Gestion de la vue associée

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

        #region Evènements

        #region Evènement Showed
        /// <summary>
        /// Délégué de réponse à l'évènement Showed.
        /// </summary>
        /// <param name="source">NotIt source de l'évènement.</param>
        public delegate void ShowedEventHandler(object source);

        /// <summary>
        /// Evènement Showed.
        /// Déclenché lorsque une requête d'affichage au premier plan est appliquée à la NotIt.
        /// </summary>
        public event ShowedEventHandler Showed;
        #endregion // Evènement Showed

        #region Evènement Hided
        /// <summary>
        /// Délégué de réponse à l'évènement Hided.
        /// </summary>
        /// <param name="source">NotIt source de l'évènement.</param>
        public delegate void HidedEventHandler(object source);

        /// <summary>
        /// Evènement Hided.
        /// Déclenché lorsque une requête de masquage est appliquée à la NotIt.
        /// </summary>
        public event HidedEventHandler Hided;
        #endregion // Evènement Hided

        #region Evènement StatusChanged
        /// <summary>
        /// Déclenche l'évènement StatusChanged.
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
        /// Délégué de réponse à l'évènement StatusChanged.
        /// </summary>
        /// <param name="source">NotIt source de l'évènement.</param>
        public delegate void StatusChangedEventHandler(object source);

        /// <summary>
        /// Evènement StatusChanged.
        /// Déclenché lorsque une ou plusieurs propriétés de la NotIt sont modifiées.
        /// </summary>
        public event StatusChangedEventHandler StatusChanged;
        #endregion // Evènement StatusChanged

        #endregion // Evènements
    }
}
