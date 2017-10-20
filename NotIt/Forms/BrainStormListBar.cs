using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Smilly.BrainStorm;
using Smilly.BrainStorm.Settings;

namespace Smilly.BrainStorm.Forms
{
    /// <summary>
    /// ListBar, affichant la liste des BrainStorms.
    /// Permet de lister toutes les BrainStorms et d'intéragir avec elles.
    /// </summary>
    public partial class BrainStormListBar : Form
    {
        #region Variables locales
        /// Référence vers le controler de BrainStorms.
        private BrainStormControler controler;

        /// Réponse aux évènements d'ajout de BrainStorm.
        private BrainStormControler.BrainStormAddedEventHandler brainStormAddedEventHandler;

        /// Réponse aux évènements de suppression de BrainStorm.
        private BrainStormControler.BrainStormRemovedEventHandler brainStormRemovedEventHandler;

        /// Réponse aux évènements de modification de BrainStorm.
        private BrainStorm.StatusChangedEventHandler statusChangedEventHandler;

        private int type = 2;
        #endregion // Variables locales

        #region Construction / Initialisation
        /// Construction de la ListBar.
        public BrainStormListBar()
        {
            InitializeComponent();
            controler = BrainStormControler.Instance;
            InitializeEventHandlers();
            FillList();
            RegisterToControlerEvents();
            Location = SettingManager.Instance.Settings.ListBarLocation;
            Size = SettingManager.Instance.Settings.ListBarSize;
        }

        /// Initialisations des "event handlers".
        private void InitializeEventHandlers()
        {
            brainStormAddedEventHandler = new BrainStormControler.BrainStormAddedEventHandler(controler_BrainStormAdded);
            brainStormRemovedEventHandler = new BrainStormControler.BrainStormRemovedEventHandler(controler_BrainStormRemoved);
            statusChangedEventHandler = new BrainStorm.StatusChangedEventHandler(BrainStorm_StatusChanged);
        }

        /// Abonnement aux évènements du BrainStormControler.
        private void RegisterToControlerEvents()
        {
            controler.BrainStormAdded += brainStormAddedEventHandler;
            controler.BrainStormRemoved += brainStormRemovedEventHandler;
        }

        /// Initialise la liste des BrainStorms.
        private void FillList()
        {
            List<BrainStorm> brainStorms;
            brainStorms = controler.BrainStorms;
            foreach (BrainStorm brainStorm in brainStorms)
            {
                AddBrainStorm(brainStorm);
            }
        }
        #endregion // Construction / Initialisation

        #region Fermeture de la fenêtre

        /// La fenêtre à été fermée, on sauvegarde la taille et sa position.

        /// <param name="sender">ListBar.</param>
        /// <param name="e">Fermeture.</param>
        private void BrainStormListBar_FormClosed(object sender, FormClosedEventArgs e)
        {
            SettingManager.Instance.Settings.ListBarLocation = Location;
            SettingManager.Instance.Settings.ListBarSize = Size;
            SettingManager.Instance.Save();
            UnregisterFromControlerEvents();
            UnregisterFromBrainStormEvents();
        }


        /// Désabonnement des évènements du BrainStormControler.

        private void UnregisterFromControlerEvents()
        {
            controler.BrainStormAdded -= brainStormAddedEventHandler;
            controler.BrainStormRemoved -= brainStormRemovedEventHandler;
        }


        /// Désabonnement des évènements des BrainStorms.

        private void UnregisterFromBrainStormEvents()
        {
            foreach (ListViewItem item in listView.Items)
            {
                BrainStorm brainStorm = (BrainStorm)item.Tag;
                brainStorm.StatusChanged -= statusChangedEventHandler;
            }
        }
        #endregion // Fermeture de la fenêtre

        #region Réponse aux évènements de l'application

        #region Ajout / Suppression de BrainStorms

        /// Suppression d'une BrainStorm, mise à jour de la liste.
        /// <param name="sender">BrainStormControler.</param>
        /// <param name="e">Suppression d'une BrainStorm.</param>
        void controler_BrainStormRemoved(object sender, BrainStormControler.BrainStormRemovedArgs e)
        {
            BrainStorm brainStorm = (BrainStorm)listView.Items[e.BrainStormId.ToString()].Tag;
            brainStorm.StatusChanged -= statusChangedEventHandler;
            listView.Items.RemoveByKey(e.BrainStormId.ToString());
        }

        /// Ajout d'une BrainStorm, mise à jour de la liste.
        /// <param name="sender">BrainStormControler.</param>
        /// <param name="e">Ajout d'une BrainStorm.</param>
        void controler_BrainStormAdded(object sender, BrainStormControler.BrainStormAddedArgs e)
        {
            AddBrainStorm(e.BrainStorm);
        }

        /// Ajoute la BrainStorm à la liste.
        /// <param name="brainStorm">BrainStorm à ajouter.</param>
        private void AddBrainStorm(BrainStorm brainStorm)
        {
            ListViewItem item = new ListViewItem();
            item.Text = brainStorm.Title;
            item.Tag = brainStorm;
            item.Name = brainStorm.Id.ToString();
            item.ImageIndex = 0;
            listView.Items.Add(item);
            // On s'abonne aux évènements de la BrainStorm.
            brainStorm.StatusChanged += statusChangedEventHandler;
            brainStorm.Show();
        }
        #endregion // Ajout / Suppression de BrainStorms

        #region Modification d'une BrainStorm

        /// Modification d'une BrainStorm, on met à jour son item dans la liste.

        /// <param name="source">BrainStorm.</param>
        void BrainStorm_StatusChanged(object source)
        {
            BrainStorm brainStorm = (BrainStorm)source;
            ListViewItem item = listView.Items[brainStorm.Id.ToString()];
            item.Text = brainStorm.Title;
            item.Tag = brainStorm;
        }
        #endregion // Modification d'une BrainStorm

        #endregion // Réponse aux évènements de l'application

        #region Sélection d'une BrainStorm

        /// Sélection d'une ou plusieurs BrainStorms.
        /// <param name="sender">ListView.</param>
        /// <param name="e">Changement de sélection.</param>
        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            foreach (ListViewItem item in listView.SelectedItems)
            {
                // On affiche au premier plan les BrainStorms sélectionnées.
                BrainStorm brainStorm = (BrainStorm)item.Tag;
                brainStorm.Show();
                Focus();
            }
            if (listView.SelectedItems.Count == 0)
            {
                deletedToolStripMenuItem.Enabled = false;
            }
            else
            {
                deletedToolStripMenuItem.Enabled = true;
            }
        }
        #endregion // Sélection d'une BrainStorm

        #region Action sur une BrainStorm

        /// Suppression des BrainStorms sélectionnées.
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void deletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.SelectedItems)
            {
                controler.Delete(int.Parse(item.Name));
            }
        }

        /// Création d'une BrainStorm.
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = controler.New(type);
            Point location = NewNotLocation();
            controler.Move(id, location);
        }

        /// Calcul la position de la BrainStorm nouvellement créée, pour éviter qu'elle n'aparaisse 
        /// sous la ListBar.
        /// <returns>Position où placer la nouvelle BrainStorm.</returns>
        private Point NewNotLocation()
        {
            int margin = 10;
            Point location = Cursor.Position;
            location.X = Left + Width + margin;
            if (location.X + controler.ViewWidth > Screen.GetWorkingArea(this).Width - margin)
            {
                location.X = Left - controler.ViewWidth - margin;
            }
            return location;
        }
        #endregion // Action sur une BrainStorm

        #region Actions sur la ListBar
        /// Bascule l'affichage de la List Bar en mode "TopMost" ou "Normal".

        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }

        /// Place la ListBar sur la bordure supérieure de l'écran, centrée horizontalement.

        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Top = 0;
            Left = (Screen.GetWorkingArea(this).Width - Width) / 2;
        }

        /// Place la ListBar sur la bordure inférieure de l'écran, centrée horizontalement.
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void bottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Top = Screen.GetWorkingArea(this).Height - Height;
            Left = (Screen.GetWorkingArea(this).Width - Width) / 2;
        }

        /// Place la ListBar sur la bordure gauche de l'écran, centrée verticalement.
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Left = 0;
            Top = (Screen.GetWorkingArea(this).Height - Height) / 2;
        }

        /// Place la ListBar sur la bordure droite de l'écran, centrée verticalement.
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Left = Screen.GetWorkingArea(this).Width - Width;
            Top = (Screen.GetWorkingArea(this).Height - Height) / 2;
        }
        #endregion // Actions sur la ListBar

        #region Actions sur la collection de BrainStorms
        /// Affiche toutes les BrainStorms au premier plan.
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.ShowAll();
        }

        private void memosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            controler.ShowMemo();
        }

        private void agrementsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            controler.ShowAgrement();
        }

        private void ideasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            controler.ShowIdea();
        }

        private void notsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            controler.ShowNot();
        }

        /// Cache toutes les BrainStorms.
        /// <param name="sender">Entrée du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void hideAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.HideAll();
        }

        private void memosToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            controler.HideMemo();
        }

        private void agrementsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            controler.HideAgrement();
        }

        private void ideasToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            controler.HideIdea();
        }

        private void notsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            controler.HideNot();
        }

        #endregion // Actions sur la collection de BrainStorms
        
    }
}
