using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

using Nikoui.NotIt.Forms;
using Nikoui.NotIt.Settings;
using Nikoui.NotIt.Properties;

namespace Nikoui.NotIt
{
    /// <summary>
    /// Classe statique d'amor�age de l'application NotIt.
    /// Initialize l'application et la d�marre.
    /// </summary>
    static class Program
    {
        #region Main
        /// <summary>
        /// Point d'entr�e de l'application.
        /// D�marre l'application. La fen�tre principale est affich�e pendant l'initialisation, 
        /// puis elle est r�duite. Une ic�ne de notification permet d'acc�der aux fonctionnalit�s 
        /// de l'application.
        /// Lors du chargement de l'application, les NotIts sont charg�s en m�moire.
        /// Les modifications effectu�es sur les NotIts sont sauvegard�es uniquement lorsque 
        /// l'application se termine.
        /// </summary>
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
        /// <summary>
        /// Renvoie une valeur indiquant si l'instance en cours est la seule instance de l'application charg�e.
        /// </summary>
        /// <returns><c>true</c> si l'instance en cours est unique, <c>false</c> s'il existe 
        /// au moins une autre instance de l'application en cours.</returns>
        private static bool IsUniqueInstance()
        {
            bool isUniqueInstance;
            string currentProcessName = Process.GetCurrentProcess().ProcessName;
            isUniqueInstance = (Process.GetProcessesByName(currentProcessName).Length == 1);
            return (isUniqueInstance);
        }

        /// <summary>
        /// Initialisation des param�tres de l'application.
        /// </summary>
        private static void InitializeSettings()
        {
            SettingManager.Instance.ConfigFile = ".\\NotIts.cfg";
        }

        /// <summary>
        /// D�marrage effectif de l'application.
        /// </summary>
        private static void Run()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            NotItApplication notItApplication = new NotItApplication();
            FormManager.Instance.NotItApplication = notItApplication;
            Application.Run(notItApplication);
        }
        #endregion // Private static methods        
    }
}