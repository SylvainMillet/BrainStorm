using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using Smilly.BrainStorm.Settings;

namespace Smilly.BrainStorm
{
    /// Controler de BrainStorms.
    /// G�re la collection de BrainStorm.
    /// Les BrainStorms sont stock�es dans un unique fichier.
    /// La sauvegarde est effectu�e lors de la lib�ration du controler.
    public sealed class BrainStormControler : IDisposable
    {
		#region Variables locales
        /// Instance unique du BrainStormControler.
		private static volatile BrainStormControler instance = null;

        /// Objet de synchronisation utiliser pour l'acc�s au singleton.
		private static object syncRoot = new Object();

        /// Dictionnaire de BrainStorm, index� par l'identifiant des BrainStorms.
        private Dictionary<int, BrainStorm> brainStorms;

        /// Chemin du fichier stockant les BrainStorms � la fermeture de l'application.
        private string brainStormsFile;

        /// Largeur d'une vue de BrainStorm.
        private int viewWidth;

        /// Sauvegarde temporaire de l'�tat de BrainStorms (�pingl�es ou non).
        private Dictionary<int, bool> pinnedBrainStorms;
		#endregion // Variables locales

		#region Construction / Initialisation
        /// Constructeur priv� du singleton.
		private BrainStormControler()
		{
            brainStorms = new Dictionary<int, BrainStorm>();
            pinnedBrainStorms = new Dictionary<int, bool>();
            brainStormsFile = SettingManager.Instance.Settings.BrainStormsFile;
            RetrieveViewWidth();
		}

        /// On cr�e une vue de BrainStorm pour conna�tre sa largeur.  
        /// La vue n'est pas utilis�e, elle est tout de suite lib�r�e.
        private void RetrieveViewWidth()
        {
            Forms.BrainStormView view = new Smilly.BrainStorm.Forms.BrainStormView();
            viewWidth = view.Width;
            view.Dispose();
        }
		#endregion // Construction / Initialisation

		#region Acc�s au singleton
        /// Obtient l'instance unique du BrainStormControler.
		public static BrainStormControler Instance
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
        					instance = new BrainStormControler();
                            instance.Load();
        				}
    				}
    			}
    			return instance;
			}
		}
		#endregion // Acc�s au singleton

        #region Actions sur une BrainStorm
        /// Nouvelle BrainStorm.
        /// <returns>Identifiant de la BrainStorm cr��e.</returns>
        public int New(int type)
        {
            BrainStorm brainStorm = new BrainStorm(FreeId(), type);
            brainStorms.Add(brainStorm.Id, brainStorm);
            // Notification de la cr�ation
            BrainStormAddedEventHandler brainStormAdded = BrainStormAdded;
            if (brainStormAdded != null)
            {
                brainStormAdded(this, new BrainStormAddedArgs(brainStorm));
            }
            return (brainStorm.Id);
        }

        /// Obtient un identifiant non utilis�.
        /// <returns>Identifiant libre.</returns>
        private int FreeId()
        {
            int freeId = 0;
            while (brainStorms.ContainsKey(freeId))
            {
                freeId++;
            }
            return (freeId);
        }

        /// D�place une BrainStorm vers les coordonn�es sp�cifi�es.
        /// <param name="id">Identifiant de la BrainStorm � d�placer.</param>
        /// <param name="location">Coordonn�es de destination de la BrainStorm.</param>
        public void Move(int id, Point location)
        {
            BrainStorm brainStorm = brainStorms[id];
            brainStorm.Location = location;
        }

        /// D�finis le titre d'une BrainStorm.
        /// <param name="id">Identifiant de la BrainStorm � modifier.</param>
        /// <param name="title">Titre de la BrainStorm � appliquer.</param>
        public void SetTitle(int id, string title)
        {
            BrainStorm brainStorm = brainStorms[id];
            brainStorm.Title = title;
        }

        /// D�finis les d�tails de la BrainStorm.
        /// <param name="id">Identifiant de la BrainStorm � modifier.</param>
        /// <param name="details">D�tails de la BrainStorm � appliquer.</param>
        public void SetDetails(int id, string details)
        {
            BrainStorm brainStorm = brainStorms[id];
            brainStorm.Details = details;
        }

        /// D�finis les d�tails de la BrainStorm.
        /// <param name="id">Identifiant de la BrainStorm � modifier.</param>
        /// <param name="type">Type de la BrainStorm � appliquer.</param>
        public void SetType(int id, int type)
        {
            BrainStorm brainStorm = brainStorms[id];
            brainStorm.Type = type;
        }

        /// Suppression d'une BrainStorm.
        /// <param name="id">Identifiant de la BrainStorm � supprimer.</param>
        public void Delete(int id)
        {
            BrainStorm brainStorm = brainStorms[id];
            brainStorm.DettachView();
            brainStorms.Remove(id);
            // Notification de la suppression.
            BrainStormRemovedEventHandler brainStormRemoved = BrainStormRemoved;
            if (brainStormRemoved != null)
            {
                brainStormRemoved(this, new BrainStormRemovedArgs(id));
            }
        }

        /// D�finis si la BrainStorm est �pingl�e.
        /// <param name="id">Identifiant de la BrainStorm � modifier.</param>
        /// <param name="pinned">Valeur indiquant si la BrainStorm est �pingl�e.</param>
        public void Pin(int id, bool pinned)
        {
            BrainStorm brainStorm = brainStorms[id];
            brainStorm.Pinned = pinned;
        }
        #endregion // Actions sur une BrainStorm

        #region Actions sur la collection de BrainStorms
        /// Affichage au premier plan de toutes les BrainStorms.
        public void ShowAll()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                brainStorm.Show();
            }
        }
        public void ShowMemo()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                if (brainStorm.Type == 1)
                {
                    brainStorm.Show();
                }
            }
        }

        public void ShowAgrement()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                if (brainStorm.Type == 2)
                {
                    brainStorm.Show();
                }
            }
        }

        public void ShowIdea()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                if (brainStorm.Type == 3)
                {
                    brainStorm.Show();
                }
            }
        }

        public void ShowNot()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                if (brainStorm.Type == 4)
                {
                    brainStorm.Show();
                }
            }
        }


        /// Masquage de toutes les BrainStorms.
        public void HideAll()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                brainStorm.Hide();
            }
        }

        public void HideMemo()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                if (brainStorm.Type == 1)
                {
                    brainStorm.Hide();
                }    
            }
        }

        public void HideAgrement()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                if (brainStorm.Type == 2)
                {
                    brainStorm.Hide();
                }
            }
        }

        public void HideIdea()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                if (brainStorm.Type == 3)
                {
                    brainStorm.Hide();
                }
            }
        }

        public void HideNot()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                if (brainStorm.Type == 4)
                {
                    brainStorm.Hide();
                }
            }
        }

        /// On d�sactive temporairement la propri�t� d'�pinglage des BrainStorms (aucune BrainStorm �pingl�e). 
        /// A utiliser pour permettre l'affichage d'une MessageBox ou d'une Dialog en premier plan.
        /// Les valeurs des propri�t�s d'�pinglage sont sauvegard�es avant d'�tre d�sactiv�e, pour 
        /// permettre leur restauration.
        public void DisableTopMostBrainStorms()
        {
            pinnedBrainStorms.Clear();
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                // On stocke l'�tat courant de la propri�t� d'�pinglage de la BrainStorm.
                pinnedBrainStorms.Add(brainStorm.Id, brainStorm.Pinned);
                // La BrainStorm n'est plus �pingl�e.
                brainStorm.Pinned = false;
            }
        }

        /// Restaure l'�tat de la propri�t�s d'�pinglage de toutes les BrainStorms.
        /// A utiliser apr�s l'appel � DisableTopMostBrainStorms pour revenir � l'�tat initial.
        public void EnableTopMostBrainStorms()
        {
            Dictionary<int, bool>.Enumerator enumerator;
            enumerator = pinnedBrainStorms.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Restauration de la propri�t� d'�pinglage de la BrainStorm.
                brainStorms[(int)enumerator.Current.Key].Pinned = (bool)enumerator.Current.Value;
            }
        }
        #endregion // Actions sur la collection de BrainStorms

        #region Chargement / Sauvegarde des BrainStorms
        /// Sauvegarde des BrainStorms dans un fichier.
        /// <param name="exiting"><c>true</c> si l'application est ferm�e apr�s la sauvegarde
        /// <c>false</c> si l'application doit rester utilisable apr�s la sauvegarde.</param>
        private void Save(bool exiting)
        {
            // On d�tache les vues avant de sauvegarder les BrainStorms
            // (pour supprimer les "event handlers")
            DettachViews();
            // S�rialisation de la collection de BrainStorms.
            FileStream stream = new FileStream(brainStormsFile, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, brainStorms);
            stream.Close();
            if (!exiting)
            {
                // On continuer � utiliser l'application apr�s la sauvegarde,
                // on attache � nouveau les vues aux BrainStorms.
                AttachViews();
            }
        }

        /// Charge le dictionnaire de BrainStorms depuis un fichier.
        private void Load()
        {
            if (File.Exists(brainStormsFile))
            {
                // D�s�rialisation de la collection de BrainStorms depuis un fichier.
                FileStream stream = new FileStream(brainStormsFile, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    brainStorms = (Dictionary<int, BrainStorm>)formatter.Deserialize(stream);
                    // On attache les vues aux BrainStorms
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

        /// Attache toutes les vues aux BrainStorms.
        private void AttachViews()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                brainStorm.AttachView();
            }
        }

        /// D�tache toutes les vues des BrainStorms.
        /// D�tacher les vues permet la s�rialisation des BrainStorms (les vues n'�tant pas s�rialisable).
        private void DettachViews()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                brainStorm.DettachView();
            }
        }

        /// Obtient ou d�finis le nom du fichier stockant les BrainStorms.
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
        #endregion // Chargement / Sauvegarde des BrainStorms

        #region Destruction / Lib�ration
        /// Lib�ration de l'objet, on sauvegarde le dictionnaire de BrainStorms.
        public void Dispose()
        {
            Save(true);
            // Pas d'appel au descruteur.
            GC.SuppressFinalize(this);
        }

        /// Destruction de l'objet, on sauvegarde le dictionnaire de BrainStorms.
        ~BrainStormControler()
        {
            Save(true);
        }
        #endregion // Destruction / Lib�ration

        #region Ev�nements

        #region Ev�nement BrainStormAdded
        /// Argument d'�v�nement d'ajout d'une BrainStorm.
        /// Fournit la BrainStorm nouvellement cr��e.
        public class BrainStormAddedArgs
        {
            /// BrainStorm venant d'�tre ajout�e.
            private BrainStorm brainStorm;

            /// Obtient la BrainStorm venant d'�tre ajout�e.
            public BrainStorm BrainStorm
            {
                get
                {
                    return (brainStorm);
                }
            }

            /// Construction de l'argument, en pr�cisant la BrainStorm concern�e.
            /// <param name="brainStorm">BrainStorm venant d'�tre ajout�e.</param>
            public BrainStormAddedArgs(BrainStorm brainStorm)
            {
                this.brainStorm = brainStorm;
            }
        }

        /// D�l�gu� de r�ponse aux �v�nements d'ajout d'une BrainStorm.
        /// <param name="sender">BrainStormControler.</param>
        /// <param name="e">Argument d'ajout de BrainStorm.</param>
        public delegate void BrainStormAddedEventHandler(object sender, BrainStormAddedArgs e);

        /// Ev�nement d'ajout de BrainStorm.
        /// D�clench� lors la cr�ation d'une nouvelle BrainStorm par le BrainStormControler.
        public event BrainStormAddedEventHandler BrainStormAdded;
        #endregion // Ev�nement BrainStormAdded

        #region Ev�nement BrainStormRemoved
        /// Argument d'�v�nement de suppression d'une BrainStorm.
        /// Fournit l'identifiant de la BrainStorm supprim�e.
        public class BrainStormRemovedArgs
        {
            /// Identifiant de la BrainStorm supprim�e.
            private int brainStormId;

            /// Obtient l'identifiant de la BrainStorm supprim�e.
            public int BrainStormId
            {
                get
                {
                    return (brainStormId);
                }
            }

            /// Construction d'un argument de suppression, pr�cisant l'identifiant 
            /// de la BrainStorm supprim�e.
            /// <param name="brainStormId">Identifiant de la BrainStorm supprim�e.</param>
            public BrainStormRemovedArgs(int brainStormId)
            {
                this.brainStormId = brainStormId;
            }
        }

        /// D�l�gu� de r�ponse aux �v�nements de suppression d'une BrainStorm.
        /// <param name="sender">BrainStormControler.</param>
        /// <param name="e">Argument de suppression de BrainStorm.</param>
        public delegate void BrainStormRemovedEventHandler(object sender, BrainStormRemovedArgs e);

        /// Ev�nement de suppression de BrainStorm.
        /// D�clench� lors la suppression d'une BrainStorm par le BrainStormControler.
        public event BrainStormRemovedEventHandler BrainStormRemoved;
        #endregion // Ev�nement BrainStormRemoved

        #endregion // Ev�nements

        #region Propri�t�s
        /// Obtient la liste des BrainStorms g�r�es par le BrainStormControler.
        public List<BrainStorm> BrainStorms
        {
            get
            {
                List<BrainStorm> loadedBrainStorms = new List<BrainStorm>();
                // Cr�ation d'une liste � partir du dictionnaire.
                foreach (BrainStorm brainStorm in brainStorms.Values)
                {
                    loadedBrainStorms.Add(brainStorm);
                }
                return (loadedBrainStorms);
            }
        }

        /// Obtient la largeur d'une vue de BrainStorm.
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
