using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Nikoui.NotIt.Settings
{
    /// <summary>
    /// Gestion des param�tres de l'application.
    /// Permet la lecture, la modification et la sauvegarde des param�tres de l'application.
    /// </summary>
    public sealed class SettingManager
    {
		#region Variables locales
        /// <summary>
        /// Instance unique du SettingManager.
        /// </summary>
		private static volatile SettingManager instance = null;

        /// <summary>
        /// Objet de synchronisation, pour assurer l'unicit� du singleton.
        /// </summary>
		private static object syncRoot = new Object();

        /// <summary>
        /// Fichier de configuration par d�faut.
        /// </summary>
        private const string defaultConfigFile = ".\\NotIts.cfg";

        /// <summary>
        /// Fichier de configuration courant.
        /// </summary>
        private string configFile;

        /// <summary>
        /// Param�tres de l'application.
        /// </summary>
        private Settings settings;
		#endregion // Variables locales

		#region Construction / Initialisation
        /// <summary>
        /// Constructeur par d�faut.
        /// </summary>
		private SettingManager()
		{
		}
		#endregion // Construction / Initialisation

        #region Acc�s � l'instance unique du SettingManager
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
                            // Premier appel, cr�ation de l'instance.
            			    instance = new SettingManager();
        				}
    				}
    			}
    			return instance;
			}
		}
        #endregion // Acc�s � l'instance unique du SettingManager

        #region Sauvegarde / Chargement de la configuration
        /// <summary>
        /// Charge la configuration de l'application.
        /// La configuration est charg�e depuis le fichier de configuration courant.
        /// </summary>
        public void Load()
        {
            if (configFile == "")
            {
                // Fichier de configuration non sp�cifi�, on utilise le fichier par d�faut.
                configFile = defaultConfigFile;
            }
            if (File.Exists(configFile))
            {
                // D�s�rialisation de la configuration depuis le fichier.
                FileStream stream = new FileStream(configFile, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    settings = (Settings)formatter.Deserialize(stream);
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    // Impossible de d�s�rialiser le fichier.
                }
                catch (InvalidCastException)
                {
                    // Impossible de d�s�rialiser le fichier.
                }
                finally
                {
                    stream.Close();
                }
            }
            if(settings == null)
            {
                // Configuration non disponible,
                // utilisation de la configuration par d�faut
                settings = new Settings();
            }
        }

        /// <summary>
        /// Sauvegarde la configuration courante de l'application.
        /// La configuration est sauvegard�e dans le fichier de configuration courant.
        /// </summary>
        public void Save()
        {
            if (configFile == "")
            {
                // Fichier de configuration non sp�cifi�, on utilise le fichier par d�faut.
                configFile = defaultConfigFile;
            }
            // S�rialisation de la configuration dans un fichier.
            FileStream stream = new FileStream(configFile, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, settings);
            stream.Close();
        }
        #endregion // Sauvegarde / Chargement de la configuration

        #region Propri�t�s
        /// <summary>
        /// D�finis le fichier de configuration � utiliser.
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
        /// Obtient les param�tres de l'application.
        /// </summary>
        public Settings Settings
        {
            get
            {
                if (settings == null)
                {
                    // Chargement des param�tres.
                    Load();
                }
                return (settings);
            }
        }
        #endregion // Propri�t�s
    }
}
