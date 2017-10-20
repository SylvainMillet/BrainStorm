using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using Nikoui.NotIt.Settings;

namespace Nikoui.NotIt
{
    /// <summary>
    /// Controler de NotIts.
    /// Gère la collection de NotIt.
    /// Les NotIts sont stockées dans un unique fichier.
    /// La sauvegarde est effectuée lors de la libération du controler.
    /// </summary>
    public sealed class NotItControler : IDisposable
    {
		#region Variables locales
        /// <summary>
        /// Instance unique du NotItControler.
        /// </summary>
		private static volatile NotItControler instance = null;

        /// <summary>
        /// Objet de synchronisation utiliser pour l'accès au singleton.
        /// </summary>
		private static object syncRoot = new Object();

        /// <summary>
        /// Dictionnaire de NotIt, indexé par l'identifiant des NotIts.
        /// </summary>
        private Dictionary<int, NotIt> notIts;

        /// <summary>
        /// Chemin du fichier stockant les NotIts à la fermeture de l'application.
        /// </summary>
        private string notItsFile;

        /// <summary>
        /// Largeur d'une vue de NotIt.
        /// </summary>
        private int viewWidth;

        /// <summary>
        /// Sauvegarde temporaire de l'état de NotIts (épinglées ou non).
        /// </summary>
        private Dictionary<int, bool> pinnedNotIts;
		#endregion // Variables locales

		#region Construction / Initialisation
        /// <summary>
        /// Constructeur privé du singleton.
        /// </summary>
		private NotItControler()
		{
            notIts = new Dictionary<int, NotIt>();
            pinnedNotIts = new Dictionary<int, bool>();
            notItsFile = SettingManager.Instance.Settings.NotItsFile;
            RetrieveViewWidth();
		}

        /// <summary>
        /// On crée une vue de NotIt pour connaître sa largeur.        
        /// </summary>
        /// <remarks>
        /// La vue n'est pas utilisée, elle est tout de suite libérée.
        /// </remarks>
        private void RetrieveViewWidth()
        {
            Forms.NotItView view = new Nikoui.NotIt.Forms.NotItView();
            viewWidth = view.Width;
            view.Dispose();
        }
		#endregion // Construction / Initialisation

		#region Accès au singleton
        /// <summary>
        /// Obtient l'instance unique du NotItControler.
        /// </summary>
		public static NotItControler Instance
		{
			get
			{
    			if (instance == null)
    			{
    				lock (syncRoot)
    				{
        				if (instance == null)
        				{
                            // Premier appel, création de l'instance.
        					instance = new NotItControler();
                            instance.Load();
        				}
    				}
    			}
    			return instance;
			}
		}
		#endregion // Accès au singleton

        #region Actions sur une NotIt
        /// <summary>
        /// Nouvelle NotIt.
        /// </summary>
        /// <returns>Identifiant de la NotIt créée.</returns>
        public int New()
        {
            NotIt notIt = new NotIt(FreeId());
            notIts.Add(notIt.Id, notIt);
            // Notification de la création
            NotItAddedEventHandler notItAdded = NotItAdded;
            if (notItAdded != null)
            {
                notItAdded(this, new NotItAddedArgs(notIt));
            }
            return (notIt.Id);
        }

        /// <summary>
        /// Obtient un identifiant non utilisé.
        /// </summary>
        /// <returns>Identifiant libre.</returns>
        private int FreeId()
        {
            int freeId = 0;
            while (notIts.ContainsKey(freeId))
            {
                freeId++;
            }
            return (freeId);
        }

        /// <summary>
        /// Déplace une NotIt vers les coordonnées spécifiées.
        /// </summary>
        /// <param name="id">Identifiant de la NotIt à déplacer.</param>
        /// <param name="location">Coordonnées de destination de la NotIt.</param>
        public void Move(int id, Point location)
        {
            NotIt notIt = notIts[id];
            notIt.Location = location;
        }

        /// <summary>
        /// Définis le titre d'une NotIt.
        /// </summary>
        /// <param name="id">Identifiant de la NotIt à modifier.</param>
        /// <param name="title">Titre de la NotIt à appliquer.</param>
        public void SetTitle(int id, string title)
        {
            NotIt notIt = notIts[id];
            notIt.Title = title;
        }

        /// <summary>
        /// Définis les détails de la NotIt.
        /// </summary>
        /// <param name="id">Identifiant de la NotIt à modifier.</param>
        /// <param name="details">Détails de la NotIt à appliquer.</param>
        public void SetDetails(int id, string details)
        {
            NotIt notIt = notIts[id];
            notIt.Details = details;
        }

        /// <summary>
        /// Suppression d'une NotIt.
        /// </summary>
        /// <param name="id">Identifiant de la NotIt à supprimer.</param>
        public void Delete(int id)
        {
            NotIt notIt = notIts[id];
            notIt.DettachView();
            notIts.Remove(id);
            // Notification de la suppression.
            NotItRemovedEventHandler notItRemoved = NotItRemoved;
            if (notItRemoved != null)
            {
                notItRemoved(this, new NotItRemovedArgs(id));
            }
        }

        /// <summary>
        /// Définis si la NotIt est épinglée.
        /// </summary>
        /// <param name="id">Identifiant de la NotIt à modifier.</param>
        /// <param name="pinned">Valeur indiquant si la NotIt est épinglée.</param>
        public void Pin(int id, bool pinned)
        {
            NotIt notIt = notIts[id];
            notIt.Pinned = pinned;
        }
        #endregion // Actions sur une NotIt

        #region Actions sur la collection de NotIts
        /// <summary>
        /// Affichage au premier plan de toutes les NotIts.
        /// </summary>
        public void ShowAll()
        {
            foreach (NotIt notIt in notIts.Values)
            {
                notIt.Show();
            }
        }

        /// <summary>
        /// Masquage de toutes les NotIts.
        /// </summary>
        public void HideAll()
        {
            foreach (NotIt notIt in notIts.Values)
            {
                notIt.Hide();
            }
        }

        /// <summary>
        /// On désactive temporairement la propriété d'épinglage des NotIts (aucune NotIt épinglée). 
        /// A utiliser pour permettre l'affichage d'une MessageBox ou d'une Dialog en premier plan.
        /// </summary>
        /// <remarks>
        /// Les valeurs des propriétés d'épinglage sont sauvegardées avant d'être désactivée, pour 
        /// permettre leur restauration.
        /// </remarks>
        public void DisableTopMostNotIts()
        {
            pinnedNotIts.Clear();
            foreach (NotIt notIt in notIts.Values)
            {
                // On stocke l'état courant de la propriété d'épinglage de la NotIt.
                pinnedNotIts.Add(notIt.Id, notIt.Pinned);
                // La NotIt n'est plus épinglée.
                notIt.Pinned = false;
            }
        }

        /// <summary>
        /// Restaure l'état de la propriétés d'épinglage de toutes les NotIts.
        /// A utiliser après l'appel à DisableTopMostNotIts pour revenir à l'état initial.
        /// </summary>
        public void EnableTopMostNotIts()
        {
            Dictionary<int, bool>.Enumerator enumerator;
            enumerator = pinnedNotIts.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Restauration de la propriété d'épinglage de la NotIt.
                notIts[(int)enumerator.Current.Key].Pinned = (bool)enumerator.Current.Value;
            }
        }
        #endregion // Actions sur la collection de NotIts

        #region Chargement / Sauvegarde des NotIts
        /// <summary>
        /// Sauvegarde des NotIts dans un fichier.
        /// </summary>
        /// <param name="exiting"><c>true</c> si l'application est fermée après la sauvegarde
        /// <c>false</c> si l'application doit rester utilisable après la sauvegarde.</param>
        private void Save(bool exiting)
        {
            // On détache les vues avant de sauvegarder les NotIts
            // (pour supprimer les "event handlers")
            DettachViews();
            // Sérialisation de la collection de NotIts.
            FileStream stream = new FileStream(notItsFile, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, notIts);
            stream.Close();
            if (!exiting)
            {
                // On continuer à utiliser l'application après la sauvegarde,
                // on attache à nouveau les vues aux NotIts.
                AttachViews();
            }
        }

        /// <summary>
        /// Charge le dictionnaire de NotIts depuis un fichier.
        /// </summary>
        private void Load()
        {
            if (File.Exists(notItsFile))
            {
                // Désérialisation de la collection de NotIts depuis un fichier.
                FileStream stream = new FileStream(notItsFile, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    notIts = (Dictionary<int, NotIt>)formatter.Deserialize(stream);
                    // On attache les vues aux NotIts
                    // (pour restaurer les "event handlers")
                    AttachViews();
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    // Impossible de désérialiser le fichier source.
                }
                finally
                {
                    stream.Close();
                }
            }
            else
            {
                // Pas de fichier de sauvegarde.
            }
        }

        /// <summary>
        /// Attache toutes les vues aux NotIts.
        /// </summary>
        private void AttachViews()
        {
            foreach (NotIt notIt in notIts.Values)
            {
                notIt.AttachView();
            }
        }

        /// <summary>
        /// Détache toutes les vues des NotIts.
        /// </summary>
        /// <remarks>
        /// Détacher les vues permet la sérialisation des NotIts (les vues n'étant pas sérialisable).
        /// </remarks>
        private void DettachViews()
        {
            foreach (NotIt notIt in notIts.Values)
            {
                notIt.DettachView();
            }
        }

        /// <summary>
        /// Obtient ou définis le nom du fichier stockant les NotIts.
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
        #endregion // Chargement / Sauvegarde des NotIts

        #region Destruction / Libération
        /// <summary>
        /// Libération de l'objet, on sauvegarde le dictionnaire de NotIts.
        /// </summary>
        public void Dispose()
        {
            Save(true);
            // Pas d'appel au descruteur.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destruction de l'objet, on sauvegarde le dictionnaire de NotIts.
        /// </summary>
        ~NotItControler()
        {
            Save(true);
        }
        #endregion // Destruction / Libération

        #region Evènements

        #region Evènement NotItAdded
        /// <summary>
        /// Argument d'évènement d'ajout d'une NotIt.
        /// Fournit la NotIt nouvellement créée.
        /// </summary>
        public class NotItAddedArgs
        {
            /// <summary>
            /// NotIt venant d'être ajoutée.
            /// </summary>
            private NotIt notIt;

            /// <summary>
            /// Obtient la NotIt venant d'être ajoutée.
            /// </summary>
            public NotIt NotIt
            {
                get
                {
                    return (notIt);
                }
            }

            /// <summary>
            /// Construction de l'argument, en précisant la NotIt concernée.
            /// </summary>
            /// <param name="notIt">NotIt venant d'être ajoutée.</param>
            public NotItAddedArgs(NotIt notIt)
            {
                this.notIt = notIt;
            }
        }

        /// <summary>
        /// Délégué de réponse aux évènements d'ajout d'une NotIt.
        /// </summary>
        /// <param name="sender">NotItControler.</param>
        /// <param name="e">Argument d'ajout de NotIt.</param>
        public delegate void NotItAddedEventHandler(object sender, NotItAddedArgs e);

        /// <summary>
        /// Evènement d'ajout de NotIt.
        /// Déclenché lors la création d'une nouvelle NotIt par le NotItControler.
        /// </summary>
        public event NotItAddedEventHandler NotItAdded;
        #endregion // Evènement NotItAdded

        #region Evènement NotItRemoved
        /// <summary>
        /// Argument d'évènement de suppression d'une NotIt.
        /// Fournit l'identifiant de la NotIt supprimée.
        /// </summary>
        public class NotItRemovedArgs
        {
            /// <summary>
            /// Identifiant de la NotIt supprimée.
            /// </summary>
            private int notItId;

            /// <summary>
            /// Obtient l'identifiant de la NotIt supprimée.
            /// </summary>
            public int NotItId
            {
                get
                {
                    return (notItId);
                }
            }

            /// <summary>
            /// Construction d'un argument de suppression, précisant l'identifiant 
            /// de la NotIt supprimée.
            /// </summary>
            /// <param name="notItId">Identifiant de la NotIt supprimée.</param>
            public NotItRemovedArgs(int notItId)
            {
                this.notItId = notItId;
            }
        }

        /// <summary>
        /// Délégué de réponse aux évènements de suppression d'une NotIt.
        /// </summary>
        /// <param name="sender">NotItControler.</param>
        /// <param name="e">Argument de suppression de NotIt.</param>
        public delegate void NotItRemovedEventHandler(object sender, NotItRemovedArgs e);

        /// <summary>
        /// Evènement de suppression de NotIt.
        /// Déclenché lors la suppression d'une NotIt par le NotItControler.
        /// </summary>
        public event NotItRemovedEventHandler NotItRemoved;
        #endregion // Evènement NotItRemoved

        #endregion // Evènements

        #region Propriétés
        /// <summary>
        /// Obtient la liste des NotIts gérées par le NotItControler.
        /// </summary>
        public List<NotIt> NotIts
        {
            get
            {
                List<NotIt> loadedNotIts = new List<NotIt>();
                // Création d'une liste à partir du dictionnaire.
                foreach (NotIt notIt in notIts.Values)
                {
                    loadedNotIts.Add(notIt);
                }
                return (loadedNotIts);
            }
        }

        /// <summary>
        /// Obtient la largeur d'une vue de NotIt.
        /// </summary>
        public int ViewWidth
        {
            get
            {
                return (viewWidth);
            }
        }
        #endregion // Propriétés
    }
}
