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
    /// Gère la collection de BrainStorm.
    /// Les BrainStorms sont stockées dans un unique fichier.
    /// La sauvegarde est effectuée lors de la libération du controler.
    public sealed class BrainStormControler : IDisposable
    {
		#region Variables locales
        /// Instance unique du BrainStormControler.
		private static volatile BrainStormControler instance = null;

        /// Objet de synchronisation utiliser pour l'accès au singleton.
		private static object syncRoot = new Object();

        /// Dictionnaire de BrainStorm, indexé par l'identifiant des BrainStorms.
        private Dictionary<int, BrainStorm> brainStorms;

        /// Chemin du fichier stockant les BrainStorms à la fermeture de l'application.
        private string brainStormsFile;

        /// Largeur d'une vue de BrainStorm.
        private int viewWidth;

        /// Sauvegarde temporaire de l'état de BrainStorms (épinglées ou non).
        private Dictionary<int, bool> pinnedBrainStorms;
		#endregion // Variables locales

		#region Construction / Initialisation
        /// Constructeur privé du singleton.
		private BrainStormControler()
		{
            brainStorms = new Dictionary<int, BrainStorm>();
            pinnedBrainStorms = new Dictionary<int, bool>();
            brainStormsFile = SettingManager.Instance.Settings.BrainStormsFile;
            RetrieveViewWidth();
		}

        /// On crée une vue de BrainStorm pour connaître sa largeur.  
        /// La vue n'est pas utilisée, elle est tout de suite libérée.
        private void RetrieveViewWidth()
        {
            Forms.BrainStormView view = new Smilly.BrainStorm.Forms.BrainStormView();
            viewWidth = view.Width;
            view.Dispose();
        }
		#endregion // Construction / Initialisation

		#region Accès au singleton
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
                            // Premier appel, création de l'instance.
        					instance = new BrainStormControler();
                            instance.Load();
        				}
    				}
    			}
    			return instance;
			}
		}
		#endregion // Accès au singleton

        #region Actions sur une BrainStorm
        /// Nouvelle BrainStorm.
        /// <returns>Identifiant de la BrainStorm créée.</returns>
        public int New(int type)
        {
            BrainStorm brainStorm = new BrainStorm(FreeId(), type);
            brainStorms.Add(brainStorm.Id, brainStorm);
            // Notification de la création
            BrainStormAddedEventHandler brainStormAdded = BrainStormAdded;
            if (brainStormAdded != null)
            {
                brainStormAdded(this, new BrainStormAddedArgs(brainStorm));
            }
            return (brainStorm.Id);
        }

        /// Obtient un identifiant non utilisé.
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

        /// Déplace une BrainStorm vers les coordonnées spécifiées.
        /// <param name="id">Identifiant de la BrainStorm à déplacer.</param>
        /// <param name="location">Coordonnées de destination de la BrainStorm.</param>
        public void Move(int id, Point location)
        {
            BrainStorm brainStorm = brainStorms[id];
            brainStorm.Location = location;
        }

        /// Définis le titre d'une BrainStorm.
        /// <param name="id">Identifiant de la BrainStorm à modifier.</param>
        /// <param name="title">Titre de la BrainStorm à appliquer.</param>
        public void SetTitle(int id, string title)
        {
            BrainStorm brainStorm = brainStorms[id];
            brainStorm.Title = title;
        }

        /// Définis les détails de la BrainStorm.
        /// <param name="id">Identifiant de la BrainStorm à modifier.</param>
        /// <param name="details">Détails de la BrainStorm à appliquer.</param>
        public void SetDetails(int id, string details)
        {
            BrainStorm brainStorm = brainStorms[id];
            brainStorm.Details = details;
        }

        /// Définis les détails de la BrainStorm.
        /// <param name="id">Identifiant de la BrainStorm à modifier.</param>
        /// <param name="type">Type de la BrainStorm à appliquer.</param>
        public void SetType(int id, int type)
        {
            BrainStorm brainStorm = brainStorms[id];
            brainStorm.Type = type;
        }

        /// Suppression d'une BrainStorm.
        /// <param name="id">Identifiant de la BrainStorm à supprimer.</param>
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

        /// Définis si la BrainStorm est épinglée.
        /// <param name="id">Identifiant de la BrainStorm à modifier.</param>
        /// <param name="pinned">Valeur indiquant si la BrainStorm est épinglée.</param>
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

        /// On désactive temporairement la propriété d'épinglage des BrainStorms (aucune BrainStorm épinglée). 
        /// A utiliser pour permettre l'affichage d'une MessageBox ou d'une Dialog en premier plan.
        /// Les valeurs des propriétés d'épinglage sont sauvegardées avant d'être désactivée, pour 
        /// permettre leur restauration.
        public void DisableTopMostBrainStorms()
        {
            pinnedBrainStorms.Clear();
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                // On stocke l'état courant de la propriété d'épinglage de la BrainStorm.
                pinnedBrainStorms.Add(brainStorm.Id, brainStorm.Pinned);
                // La BrainStorm n'est plus épinglée.
                brainStorm.Pinned = false;
            }
        }

        /// Restaure l'état de la propriétés d'épinglage de toutes les BrainStorms.
        /// A utiliser après l'appel à DisableTopMostBrainStorms pour revenir à l'état initial.
        public void EnableTopMostBrainStorms()
        {
            Dictionary<int, bool>.Enumerator enumerator;
            enumerator = pinnedBrainStorms.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Restauration de la propriété d'épinglage de la BrainStorm.
                brainStorms[(int)enumerator.Current.Key].Pinned = (bool)enumerator.Current.Value;
            }
        }
        #endregion // Actions sur la collection de BrainStorms

        #region Chargement / Sauvegarde des BrainStorms
        /// Sauvegarde des BrainStorms dans un fichier.
        /// <param name="exiting"><c>true</c> si l'application est fermée après la sauvegarde
        /// <c>false</c> si l'application doit rester utilisable après la sauvegarde.</param>
        private void Save(bool exiting)
        {
            // On détache les vues avant de sauvegarder les BrainStorms
            // (pour supprimer les "event handlers")
            DettachViews();
            // Sérialisation de la collection de BrainStorms.
            FileStream stream = new FileStream(brainStormsFile, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, brainStorms);
            stream.Close();
            if (!exiting)
            {
                // On continuer à utiliser l'application après la sauvegarde,
                // on attache à nouveau les vues aux BrainStorms.
                AttachViews();
            }
        }

        /// Charge le dictionnaire de BrainStorms depuis un fichier.
        private void Load()
        {
            if (File.Exists(brainStormsFile))
            {
                // Désérialisation de la collection de BrainStorms depuis un fichier.
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

        /// Attache toutes les vues aux BrainStorms.
        private void AttachViews()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                brainStorm.AttachView();
            }
        }

        /// Détache toutes les vues des BrainStorms.
        /// Détacher les vues permet la sérialisation des BrainStorms (les vues n'étant pas sérialisable).
        private void DettachViews()
        {
            foreach (BrainStorm brainStorm in brainStorms.Values)
            {
                brainStorm.DettachView();
            }
        }

        /// Obtient ou définis le nom du fichier stockant les BrainStorms.
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

        #region Destruction / Libération
        /// Libération de l'objet, on sauvegarde le dictionnaire de BrainStorms.
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
        #endregion // Destruction / Libération

        #region Evènements

        #region Evènement BrainStormAdded
        /// Argument d'évènement d'ajout d'une BrainStorm.
        /// Fournit la BrainStorm nouvellement créée.
        public class BrainStormAddedArgs
        {
            /// BrainStorm venant d'être ajoutée.
            private BrainStorm brainStorm;

            /// Obtient la BrainStorm venant d'être ajoutée.
            public BrainStorm BrainStorm
            {
                get
                {
                    return (brainStorm);
                }
            }

            /// Construction de l'argument, en précisant la BrainStorm concernée.
            /// <param name="brainStorm">BrainStorm venant d'être ajoutée.</param>
            public BrainStormAddedArgs(BrainStorm brainStorm)
            {
                this.brainStorm = brainStorm;
            }
        }

        /// Délégué de réponse aux évènements d'ajout d'une BrainStorm.
        /// <param name="sender">BrainStormControler.</param>
        /// <param name="e">Argument d'ajout de BrainStorm.</param>
        public delegate void BrainStormAddedEventHandler(object sender, BrainStormAddedArgs e);

        /// Evènement d'ajout de BrainStorm.
        /// Déclenché lors la création d'une nouvelle BrainStorm par le BrainStormControler.
        public event BrainStormAddedEventHandler BrainStormAdded;
        #endregion // Evènement BrainStormAdded

        #region Evènement BrainStormRemoved
        /// Argument d'évènement de suppression d'une BrainStorm.
        /// Fournit l'identifiant de la BrainStorm supprimée.
        public class BrainStormRemovedArgs
        {
            /// Identifiant de la BrainStorm supprimée.
            private int brainStormId;

            /// Obtient l'identifiant de la BrainStorm supprimée.
            public int BrainStormId
            {
                get
                {
                    return (brainStormId);
                }
            }

            /// Construction d'un argument de suppression, précisant l'identifiant 
            /// de la BrainStorm supprimée.
            /// <param name="brainStormId">Identifiant de la BrainStorm supprimée.</param>
            public BrainStormRemovedArgs(int brainStormId)
            {
                this.brainStormId = brainStormId;
            }
        }

        /// Délégué de réponse aux évènements de suppression d'une BrainStorm.
        /// <param name="sender">BrainStormControler.</param>
        /// <param name="e">Argument de suppression de BrainStorm.</param>
        public delegate void BrainStormRemovedEventHandler(object sender, BrainStormRemovedArgs e);

        /// Evènement de suppression de BrainStorm.
        /// Déclenché lors la suppression d'une BrainStorm par le BrainStormControler.
        public event BrainStormRemovedEventHandler BrainStormRemoved;
        #endregion // Evènement BrainStormRemoved

        #endregion // Evènements

        #region Propriétés
        /// Obtient la liste des BrainStorms gérées par le BrainStormControler.
        public List<BrainStorm> BrainStorms
        {
            get
            {
                List<BrainStorm> loadedBrainStorms = new List<BrainStorm>();
                // Création d'une liste à partir du dictionnaire.
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
        #endregion // Propriétés

    }
}
