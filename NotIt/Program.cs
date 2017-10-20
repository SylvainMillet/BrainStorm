using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

using Smilly.BrainStorm.Forms;
using Smilly.BrainStorm.Settings;
using Smilly.BrainStorm.Properties;

namespace Smilly.BrainStorm
{
    /// <summary>
    /// Classe statique d'amor�age de l'application BrainStorm.
    /// Initialize l'application et la d�marre.
    /// </summary>
    static class Program
    {
        #region Main

        /// Point d'entr�e de l'application.
        /// D�marre l'application. La fen�tre principale est affich�e pendant l'initialisation, 
        /// puis elle est r�duite. Une ic�ne de notification permet d'acc�der aux fonctionnalit�s 
        /// de l'application.
        /// Lors du chargement de l'application, les BrainStorms sont charg�s en m�moire.
        /// Les modifications effectu�es sur les BrainStorms sont sauvegard�es uniquement lorsque 
        /// l'application se termine.

        [STAThread]
        static void Main()
        {
            if (IsUniqueInstance())
            {
                // Une seule instance en cours, on peut continuer
                InitializeSettings();
                Run();
            }
            else
            {
                // Une autre instance tourne d�j�
                MessageBox.Show(Resources.AlreadyRunning, Resources.AlreadyRunning, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion // Main

        #region Private static methods

        /// Renvoie une valeur indiquant si l'instance en cours est la seule instance de l'application charg�e.

        /// <returns><c>true</c> si l'instance en cours est unique, <c>false</c> s'il existe 
        /// au moins une autre instance de l'application en cours.</returns>
        private static bool IsUniqueInstance()
        {
            bool isUniqueInstance;
            string currentProcessName = Process.GetCurrentProcess().ProcessName;
            isUniqueInstance = (Process.GetProcessesByName(currentProcessName).Length == 1);
            return (isUniqueInstance);
        }


        /// Initialisation des param�tres de l'application.

        private static void InitializeSettings()
        {
            SettingManager.Instance.ConfigFile = ".\\BrainStorms.cfg";
        }


        /// D�marrage effectif de l'application.

        private static void Run()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BrainStormApplication brainStormApplication = new BrainStormApplication();
            FormManager.Instance.BrainStormApplication = brainStormApplication;
            Application.Run(brainStormApplication);
        }
        #endregion // Private static methods        
    }
}