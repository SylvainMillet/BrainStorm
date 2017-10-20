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
    /// G�re la collection de NotIt.
    /// Les NotIts sont stock�es dans un unique fichier.
    /// La sauvegarde est effectu�e lors de la lib�ration du controler.
    /// </summary>
    public sealed class NotItControler : IDisposable
    {
		#region Variables locales
        /// <summary>
        /// Instance unique du NotItControler.
        /// </summary>
		private static volatile NotItControler instance = null;

        /// <summary>
        /// Objet de synchronisation utiliser pour l'acc�s au singleton.
        /// </summary>
		private static object syncRoot = new Object();

        /// <summary>
        /// Dictionnaire de NotIt, index� par l'identifiant des NotIts.
        /// </summary>
        private Dictionary<int, NotIt> notIts;

        /// <summary>
        /// Chemin du fichier stockant les NotIts � la fermeture de l'application.
        /// </summary>
        private string notItsFile;

        /// <summary>
        /// Largeur d'une vue de NotIt.
        /// </summary>
        private int viewWidth;

        /// <summary>
        /// Sauvegarde temporaire de l'�tat de NotIts (�pingl�es ou non).
        /// </summary>
        private Dictionary<int, bool> pinnedNotIts;
		#endregion // Variables locales

		#region Construction / Initialisation
        /// <summary>
        /// Constructeur priv� du singleton.
        /// </summary>
		private NotItControler()
		{
            notIts = new Dictionary<int, NotIt>();
            pinnedNotIts = new Dictionary<int, bool>();
            notItsFile = SettingManager.Instance.Settings.NotItsFile;
            RetrieveViewWidth();
		}

        /// <summary>
        /// On cr�e une vue de NotIt pour conna�tre sa largeur.        
        /// </summary>
        /// <remarks>
        /// La vue n'est pas utilis�e, elle est tout de suite lib�r�e.
        /// </remarks>
        private void RetrieveViewWidth()
        {
            Forms.NotItView view = new Nikoui.NotIt.Forms.NotItView();
            viewWidth = view.Width;
            view.Dispose();
        }
		#endregion // Construction / Initialisation

		#region Acc�s au singleton
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
                            // Premier appel, cr�ation de l'instance.
        					instance = new NotItControler();
                            instance.Load();
        				}
    				}
    			}
    			return instance;
			}
		}
		#endregion // Acc�s au singleton

        #region Actions sur une NotIt
        /// <summary>
        /// Nouvelle NotIt.
        /// </summary>
        /// <returns>Identifiant de la NotIt cr��e.</returns>
        public int New()
        {
            NotIt notIt = new NotIt(FreeId());
            notIts.Add(notIt.Id, notIt);
            // Notification de la cr�ation
            NotItAddedEventHandler notItAdded = NotItAdded;
            if (notItAdded != null)
            {
                notItAdded(this, new NotItAddedArgs(notIt));
            }
            return (notIt.Id);
        }

        /// <summary>
        /// Obtient un identifiant non utilis�.
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
        /// D�place une NotIt vers les coordonn�es sp�cifi�es.
        /// </summary>
        /// <param name="id">Identifiant de la NotIt � d�placer.</param>
        /// <param name="location">Coordonn�es de destination de la NotIt.</param>
        public void Move(int id, Point location)
        {
            NotIt notIt = notIts[id];
            notIt.Location = location;
        }

        /// <summary>
        /// D�finis le titre d'une NotIt.
        /// </summary>
        /// <param name="id">Identifiant de la NotIt � modifier.</param>
        /// <param name="title">Titre de la NotIt � appliquer.</param>
        public void SetTitle(int id, string title)
        {
            NotIt notIt = notIts[id];
            notIt.Title = title;
        }

        /// <summary>
        /// D�finis les d�tails de la NotIt.
        /// </summary>
        /// <param name="id">Identifiant de la NotIt � modifier.</param>
        /// <param name="details">D�tails de la NotIt � appliquer.</param>
        public void SetDetails(int id, string details)
        {
            NotIt notIt = notIts[id];
            notIt.Details = details;
        }

        /// <summary>
        /// Suppression d'une NotIt.
        /// </summary>
        /// <param name="id">Identifiant de la NotIt � supprimer.</param>
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
        /// D�finis si la NotIt est �pingl�e.
        /// </summary>
        /// <param name="id">Identifiant de la NotIt � modifier.</param>
        /// <param name="pinned">Valeur indiquant si la NotIt est �pingl�e.</param>
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
        /// On d�sactive temporairement la propri�t� d'�pinglage des NotIts (aucune NotIt �pingl�e). 
        /// A utiliser pour permettre l'affichage d'une MessageBox ou d'une Dialog en premier plan.
        /// </summary>
        /// <remarks>
        /// Les valeurs des propri�t�s d'�pinglage sont sauvegard�es avant d'�tre d�sactiv�e, pour 
        /// permettre leur restauration.
        /// </remarks>
        public void DisableTopMostNotIts()
        {
            pinnedNotIts.Clear();
            foreach (NotIt notIt in notIts.Values)
            {
                // On stocke l'�tat courant de la propri�t� d'�pinglage de la NotIt.
                pinnedNotIts.Add(notIt.Id, notIt.Pinned);
                // La NotIt n'est plus �pingl�e.
                notIt.Pinned = false;
            }
        }

        /// <summary>
        /// Restaure l'�tat de la propri�t�s d'�pinglage de toutes les NotIts.
        /// A utiliser apr�s l'appel � DisableTopMostNotIts pour revenir � l'�tat initial.
        /// </summary>
        public void EnableTopMostNotIts()
        {
            Dictionary<int, bool>.Enumerator enumerator;
            enumerator = pinnedNotIts.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Restauration de la propri�t� d'�pinglage de la NotIt.
                notIts[(int)enumerator.Current.Key].Pinned = (bool)enumerator.Current.Value;
            }
        }
        #endregion // Actions sur la collection de NotIts

        #region Chargement / Sauvegarde des NotIts
        /// <summary>
        /// Sauvegarde des NotIts dans un fichier.
        /// </summary>
        /// <param name="exiting"><c>true</c> si l'application est ferm�e apr�s la sauvegarde
        /// <c>false</c> si l'application doit rester utilisable apr�s la sauvegarde.</param>
        private void Save(bool exiting)
        {
            // On d�tache les vues avant de sauvegarder les NotIts
            // (pour supprimer les "event handlers")
            DettachViews();
            // S�rialisation de la collection de NotIts.
            FileStream stream = new FileStream(notItsFile, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, notIts);
            stream.Close();
            if (!exiting)
            {
                // On continuer � utiliser l'application apr�s la sauvegarde,
                // on attache � nouveau les vues aux NotIts.
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
                // D�s�rialisation de la collection de NotIts depuis un fichier.
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
                    // Impossible de d�s�rialiser le fichier source.
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
        /// D�tache toutes les vues des NotIts.
        /// </summary>
        /// <remarks>
        /// D�tacher les vues permet la s�rialisation des NotIts (les vues n'�tant pas s�rialisable).
        /// </remarks>
        private void DettachViews()
        {
            foreach (NotIt notIt in notIts.Values)
            {
                notIt.DettachView();
            }
        }

        /// <summary>
        /// Obtient ou d�finis le nom du fichier stockant les NotIts.
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

        #region Destruction / Lib�ration
        /// <summary>
        /// Lib�ration de l'objet, on sauvegarde le dictionnaire de NotIts.
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
        #endregion // Destruction / Lib�ration

        #region Ev�nements

        #region Ev�nement NotItAdded
        /// <summary>
        /// Argument d'�v�nement d'ajout d'une NotIt.
        /// Fournit la NotIt nouvellement cr��e.
        /// </summary>
        public class NotItAddedArgs
        {
            /// <summary>
            /// NotIt venant d'�tre ajout�e.
            /// </summary>
            private NotIt notIt;

            /// <summary>
            /// Obtient la NotIt venant d'�tre ajout�e.
            /// </summary>
            public NotIt NotIt
            {
                get
                {
                    return (notIt);
                }
            }

            /// <summary>
            /// Construction de l'argument, en pr�cisant la NotIt concern�e.
            /// </summary>
            /// <param name="notIt">NotIt venant d'�tre ajout�e.</param>
            public NotItAddedArgs(NotIt notIt)
            {
                this.notIt = notIt;
            }
        }

        /// <summary>
        /// D�l�gu� de r�ponse aux �v�nements d'ajout d'une NotIt.
        /// </summary>
        /// <param name="sender">NotItControler.</param>
        /// <param name="e">Argument d'ajout de NotIt.</param>
        public delegate void NotItAddedEventHandler(object sender, NotItAddedArgs e);

        /// <summary>
        /// Ev�nement d'ajout de NotIt.
        /// D�clench� lors la cr�ation d'une nouvelle NotIt par le NotItControler.
        /// </summary>
        public event NotItAddedEventHandler NotItAdded;
        #endregion // Ev�nement NotItAdded

        #region Ev�nement NotItRemoved
        /// <summary>
        /// Argument d'�v�nement de suppression d'une NotIt.
        /// Fournit l'identifiant de la NotIt supprim�e.
        /// </summary>
        public class NotItRemovedArgs
        {
            /// <summary>
            /// Identifiant de la NotIt supprim�e.
            /// </summary>
            private int notItId;

            /// <summary>
            /// Obtient l'identifiant de la NotIt supprim�e.
            /// </summary>
            public int NotItId
            {
                get
                {
                    return (notItId);
                }
            }

            /// <summary>
            /// Construction d'un argument de suppression, pr�cisant l'identifiant 
            /// de la NotIt supprim�e.
            /// </summary>
            /// <param name="notItId">Identifiant de la NotIt supprim�e.</param>
            public NotItRemovedArgs(int notItId)
            {
                this.notItId = notItId;
            }
        }

        /// <summary>
        /// D�l�gu� de r�ponse aux �v�nements de suppression d'une NotIt.
        /// </summary>
        /// <param name="sender">NotItControler.</param>
        /// <param name="e">Argument de suppression de NotIt.</param>
        public delegate void NotItRemovedEventHandler(object sender, NotItRemovedArgs e);

        /// <summary>
        /// Ev�nement de suppression de NotIt.
        /// D�clench� lors la suppression d'une NotIt par le NotItControler.
        /// </summary>
        public event NotItRemovedEventHandler NotItRemoved;
        #endregion // Ev�nement NotItRemoved

        #endregion // Ev�nements

        #region Propri�t�s
        /// <summary>
        /// Obtient la liste des NotIts g�r�es par le NotItControler.
        /// </summary>
        public List<NotIt> NotIts
        {
            get
            {
                List<NotIt> loadedNotIts = new List<NotIt>();
                // Cr�ation d'une liste � partir du dictionnaire.
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
        #endregion // Propri�t�s
    }
}
