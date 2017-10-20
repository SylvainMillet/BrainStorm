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
    /// Classe statique d'amorçage de l'application NotIt.
    /// Initialize l'application et la démarre.
    /// </summary>
    static class Program
    {
        #region Main
        /// <summary>
        /// Point d'entrée de l'application.
        /// Démarre l'application. La fenêtre principale est affichée pendant l'initialisation, 
        /// puis elle est réduite. Une icône de notification permet d'accéder aux fonctionnalités 
        /// de l'application.
        /// Lors du chargement de l'application, les NotIts sont chargés en mémoire.
        /// Les modifications effectuées sur les NotIts sont sauvegardées uniquement lorsque 
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
                // Une autre instance tourne déjà
                MessageBox.Show(Resources.AlreadyRunning, Resources.AlreadyRunning, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion // Main

        #region Private static methods
        /// <summary>
        /// Renvoie une valeur indiquant si l'instance en cours est la seule instance de l'application chargée.
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
        /// Initialisation des paramètres de l'application.
        /// </summary>
        private static void InitializeSettings()
        {
            SettingManager.Instance.ConfigFile = ".\\NotIts.cfg";
        }

        /// <summary>
        /// Démarrage effectif de l'application.
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