using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Nikoui.NotIt.Settings
{
    /// <summary>
    /// Gestion des paramètres de l'application.
    /// Permet la lecture, la modification et la sauvegarde des paramètres de l'application.
    /// </summary>
    public sealed class SettingManager
    {
		#region Variables locales
        /// <summary>
        /// Instance unique du SettingManager.
        /// </summary>
		private static volatile SettingManager instance = null;

        /// <summary>
        /// Objet de synchronisation, pour assurer l'unicité du singleton.
        /// </summary>
		private static object syncRoot = new Object();

        /// <summary>
        /// Fichier de configuration par défaut.
        /// </summary>
        private const string defaultConfigFile = ".\\NotIts.cfg";

        /// <summary>
        /// Fichier de configuration courant.
        /// </summary>
        private string configFile;

        /// <summary>
        /// Paramètres de l'application.
        /// </summary>
        private Settings settings;
		#endregion // Variables locales

		#region Construction / Initialisation
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
		private SettingManager()
		{
		}
		#endregion // Construction / Initialisation

        #region Accès à l'instance unique du SettingManager
        /// <summary>
        /// Obtient l'instance unique du SettingManager.
        /// </summary>
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
        /// <summary>
        /// Charge la configuration de l'application.
        /// La configuration est chargée depuis le fichier de configuration courant.
        /// </summary>
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

        /// <summary>
        /// Sauvegarde la configuration courante de l'application.
        /// La configuration est sauvegardée dans le fichier de configuration courant.
        /// </summary>
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
        /// <summary>
        /// Définis le fichier de configuration à utiliser.
        /// </summary>
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

        /// <summary>
        /// Obtient les paramètres de l'application.
        /// </summary>
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
