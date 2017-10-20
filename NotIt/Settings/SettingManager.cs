using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Smilly.BrainStorm.Settings
{
    /// <summary>
    /// Gestion des param�tres de l'application.
    /// Permet la lecture, la modification et la sauvegarde des param�tres de l'application.
    /// </summary>
    public sealed class SettingManager
    {
		#region Variables locales

        /// Instance unique du SettingManager.

		private static volatile SettingManager instance = null;


        /// Objet de synchronisation, pour assurer l'unicit� du singleton.

		private static object syncRoot = new Object();


        /// Fichier de configuration par d�faut.

        private const string defaultConfigFile = ".\\BrainStorms.cfg";


        /// Fichier de configuration courant.

        private string configFile;


        /// Param�tres de l'application.

        private Settings settings;
		#endregion // Variables locales

		#region Construction / Initialisation

        /// Constructeur par d�faut.

		private SettingManager()
		{
		}
		#endregion // Construction / Initialisation

        #region Acc�s � l'instance unique du SettingManager

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

        /// Charge la configuration de l'application.
        /// La configuration est charg�e depuis le fichier de configuration courant.

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


        /// Sauvegarde la configuration courante de l'application.
        /// La configuration est sauvegard�e dans le fichier de configuration courant.

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

        /// D�finis le fichier de configuration � utiliser.

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


        /// Obtient les param�tres de l'application.

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
