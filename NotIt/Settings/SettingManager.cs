using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Smilly.BrainStorm.Settings
{
    /// <summary>
    /// Gestion des paramètres de l'application.
    /// Permet la lecture, la modification et la sauvegarde des paramètres de l'application.
    /// </summary>
    public sealed class SettingManager
    {
		#region Variables locales

        /// Instance unique du SettingManager.

		private static volatile SettingManager instance = null;


        /// Objet de synchronisation, pour assurer l'unicité du singleton.

		private static object syncRoot = new Object();


        /// Fichier de configuration par défaut.

        private const string defaultConfigFile = ".\\BrainStorms.cfg";


        /// Fichier de configuration courant.

        private string configFile;


        /// Paramètres de l'application.

        private Settings settings;
		#endregion // Variables locales

		#region Construction / Initialisation

        /// Constructeur par défaut.

		private SettingManager()
		{
		}
		#endregion // Construction / Initialisation

        #region Accès à l'instance unique du SettingManager

        /// Obtient l'instance unique du SettingManager.

		public static SettingManager Instance
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
            			    instance = new SettingManager();
        				}
    				}
    			}
    			return instance;
			}
		}
        #endregion // Accès à l'instance unique du SettingManager

        #region Sauvegarde / Chargement de la configuration

        /// Charge la configuration de l'application.
        /// La configuration est chargée depuis le fichier de configuration courant.

        public void Load()
        {
            if (configFile == "")
            {
                // Fichier de configuration non spécifié, on utilise le fichier par défaut.
                configFile = defaultConfigFile;
            }
            if (File.Exists(configFile))
            {
                // Désérialisation de la configuration depuis le fichier.
                FileStream stream = new FileStream(configFile, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    settings = (Settings)formatter.Deserialize(stream);
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    // Impossible de désérialiser le fichier.
                }
                catch (InvalidCastException)
                {
                    // Impossible de désérialiser le fichier.
                }
                finally
                {
                    stream.Close();
                }
            }
            if(settings == null)
            {
                // Configuration non disponible,
                // utilisation de la configuration par défaut
                settings = new Settings();
            }
        }


        /// Sauvegarde la configuration courante de l'application.
        /// La configuration est sauvegardée dans le fichier de configuration courant.

        public void Save()
        {
            if (configFile == "")
            {
                // Fichier de configuration non spécifié, on utilise le fichier par défaut.
                configFile = defaultConfigFile;
            }
            // Sérialisation de la configuration dans un fichier.
            FileStream stream = new FileStream(configFile, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, settings);
            stream.Close();
        }
        #endregion // Sauvegarde / Chargement de la configuration

        #region Propriétés

        /// Définis le fichier de configuration à utiliser.

        /// <remarks>
        /// La modification du fichier de configuration entraine le chargement 
        /// de la configuration depuis le nouveau fichier de configuration.
        /// </remarks>
        public string ConfigFile
        {
            set
            {
                string previousFile = configFile;
                configFile = value;
                if (!configFile.Equals(previousFile))
                {
                    // Rechargement de la configuration.
                    Load();
                }
            }
        }


        /// Obtient les paramètres de l'application.

        public Settings Settings
        {
            get
            {
                if (settings == null)
                {
                    // Chargement des paramètres.
                    Load();
                }
                return (settings);
            }
        }
        #endregion // Propriétés
    }
}
