using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Nikoui.NotIt;
using Nikoui.NotIt.Settings;

namespace Nikoui.NotIt.Forms
{
    /// <summary>
    /// ListBar, affichant la liste des NotIts.
    /// Permet de lister toutes les NotIts et d'int�ragir avec elles.
    /// </summary>
    public partial class NotItListBar : Form
    {
        #region Variables locales
        /// <summary>
        /// R�f�rence vers le controler de NotIts.
        /// </summary>
        private NotItControler controler;

        /// <summary>
        /// R�ponse aux �v�nements d'ajout de NotIt.
        /// </summary>
        private NotItControler.NotItAddedEventHandler notItAddedEventHandler;

        /// <summary>
        /// R�ponse aux �v�nements de suppression de NotIt.
        /// </summary>
        private NotItControler.NotItRemovedEventHandler notItRemovedEventHandler;

        /// <summary>
        /// R�ponse aux �v�nements de modification de NotIt.
        /// </summary>
        private NotIt.StatusChangedEventHandler statusChangedEventHandler;
        #endregion // Variables locales

        #region Construction / Initialisation
        /// <summary>
        /// Construction de la ListBar.
        /// </summary>
        public NotItListBar()
        {
            InitializeComponent();
            controler = NotItControler.Instance;
            InitializeEventHandlers();
            FillList();
            RegisterToControlerEvents();
            Location = SettingManager.Instance.Settings.ListBarLocation;
            Size = SettingManager.Instance.Settings.ListBarSize;
        }

        /// <summary>
        /// Initialisations des "event handlers".
        /// </summary>
        private void InitializeEventHandlers()
        {
            notItAddedEventHandler = new NotItControler.NotItAddedEventHandler(controler_NotItAdded);
            notItRemovedEventHandler = new NotItControler.NotItRemovedEventHandler(controler_NotItRemoved);
            statusChangedEventHandler = new NotIt.StatusChangedEventHandler(NotIt_StatusChanged);
        }

        /// <summary>
        /// Abonnement aux �v�nements du NotItControler.
        /// </summary>
        private void RegisterToControlerEvents()
        {
            controler.NotItAdded += notItAddedEventHandler;
            controler.NotItRemoved += notItRemovedEventHandler;
        }

        /// <summary>
        /// Initialise la liste des NotIts.
        /// </summary>
        private void FillList()
        {
            List<NotIt> notIts;
            notIts = controler.NotIts;
            foreach (NotIt notIt in notIts)
            {
                AddNotIt(notIt);
            }
        }
        #endregion // Construction / Initialisation

        #region Fermeture de la fen�tre
        /// <summary>
        /// La fen�tre � �t� ferm�e, on sauvegarde la taille et sa position.
        /// </summary>
        /// <param name="sender">ListBar.</param>
        /// <param name="e">Fermeture.</param>
        private void NotItListBar_FormClosed(object sender, FormClosedEventArgs e)
        {
            SettingManager.Instance.Settings.ListBarLocation = Location;
            SettingManager.Instance.Settings.ListBarSize = Size;
            SettingManager.Instance.Save();
            UnregisterFromControlerEvents();
            UnregisterFromNotItEvents();
        }

        /// <summary>
        /// D�sabonnement des �v�nements du NotItControler.
        /// </summary>
        private void UnregisterFromControlerEvents()
        {
            controler.NotItAdded -= notItAddedEventHandler;
            controler.NotItRemoved -= notItRemovedEventHandler;
        }

        /// <summary>
        /// D�sabonnement des �v�nements des NotIts.
        /// </summary>
        private void UnregisterFromNotItEvents()
        {
            foreach (ListViewItem item in listView.Items)
            {
                NotIt notIt = (NotIt)item.Tag;
                notIt.StatusChanged -= statusChangedEventHandler;
            }
        }
        #endregion // Fermeture de la fen�tre

        #region R�ponse aux �v�nements de l'application

        #region Ajout / Suppression de NotIts
        /// <summary>
        /// Suppression d'une NotIt, mise � jour de la liste.
        /// </summary>
        /// <param name="sender">NotItControler.</param>
        /// <param name="e">Suppression d'une NotIt.</param>
        void controler_NotItRemoved(object sender, NotItControler.NotItRemovedArgs e)
        {
            NotIt notIt = (NotIt)listView.Items[e.NotItId.ToString()].Tag;
            notIt.StatusChanged -= statusChangedEventHandler;
            listView.Items.RemoveByKey(e.NotItId.ToString());
        }

        /// <summary>
        /// Ajout d'une NotIt, mise � jour de la liste.
        /// </summary>
        /// <param name="sender">NotItControler.</param>
        /// <param name="e">Ajout d'une NotIt.</param>
        void controler_NotItAdded(object sender, NotItControler.NotItAddedArgs e)
        {
            AddNotIt(e.NotIt);
        }

        /// <summary>
        /// Ajoute la NotIt � la liste.
        /// </summary>
        /// <param name="notIt">NotIt � ajouter.</param>
        private void AddNotIt(NotIt notIt)
        {
            ListViewItem item = new ListViewItem();
            item.Text = notIt.Title;
            item.Tag = notIt;
            item.Name = notIt.Id.ToString();
            item.ImageIndex = 0;
            listView.Items.Add(item);
            // On s'abonne aux �v�nements de la NotIt.
            notIt.StatusChanged += statusChangedEventHandler;
            notIt.Show();
        }
        #endregion // Ajout / Suppression de NotIts

        #region Modification d'une NotIt
        /// <summary>
        /// Modification d'une NotIt, on met � jour son item dans la liste.
        /// </summary>
        /// <param name="source">NotIt.</param>
        void NotIt_StatusChanged(object source)
        {
            NotIt notIt = (NotIt)source;
            ListViewItem item = listView.Items[notIt.Id.ToString()];
            item.Text = notIt.Title;
            item.Tag = notIt;
        }
        #endregion // Modification d'une NotIt

        #endregion // R�ponse aux �v�nements de l'application

        #region S�lection d'une NotIt
        /// <summary>
        /// S�lection d'une ou plusieurs NotIts.
        /// </summary>
        /// <param name="sender">ListView.</param>
        /// <param name="e">Changement de s�lection.</param>
        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            foreach (ListViewItem item in listView.SelectedItems)
            {
                // On affiche au premier plan les NotIts s�lectionn�es.
                NotIt notIt = (NotIt)item.Tag;
                notIt.Show();
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
        #endregion // S�lection d'une NotIt

        #region Action sur une NotIt
        /// <summary>
        /// Suppression des NotIts s�lectionn�es.
        /// </summary>
        /// <param name="sender">Entr�e du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void deletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.SelectedItems)
            {
                controler.Delete(int.Parse(item.Name));
            }
        }

        /// <summary>
        /// Cr�ation d'une NotIt.
        /// </summary>
        /// <param name="sender">Entr�e du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            int id = controler.New();
            Point location = NewNotLocation();
            controler.Move(id, location);
        }

        /// <summary>
        /// Calcul la position de la NotIt nouvellement cr��e, pour �viter qu'elle n'aparaisse 
        /// sous la ListBar.
        /// </summary>
        /// <returns>Position o� placer la nouvelle NotIt.</returns>
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
        #endregion // Action sur une NotIt

        #region Actions sur la ListBar
        /// <summary>
        /// Bascule l'affichage de la List Bar en mode "TopMost" ou "Normal".
        /// </summary>
        /// <param name="sender">Entr�e du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Place la ListBar sur la bordure sup�rieure de l'�cran, centr�e horizontalement.
        /// </summary>
        /// <param name="sender">Entr�e du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Top = 0;
            Left = (Screen.GetWorkingArea(this).Width - Width) / 2;
        }

        /// <summary>
        /// Place la ListBar sur la bordure inf�rieure de l'�cran, centr�e horizontalement.
        /// </summary>
        /// <param name="sender">Entr�e du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void bottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Top = Screen.GetWorkingArea(this).Height - Height;
            Left = (Screen.GetWorkingArea(this).Width - Width) / 2;
        }

        /// <summary>
        /// Place la ListBar sur la bordure gauche de l'�cran, centr�e verticalement.
        /// </summary>
        /// <param name="sender">Entr�e du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Left = 0;
            Top = (Screen.GetWorkingArea(this).Height - Height) / 2;
        }

        /// <summary>
        /// Place la ListBar sur la bordure droite de l'�cran, centr�e verticalement.
        /// </summary>
        /// <param name="sender">Entr�e du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Left = Screen.GetWorkingArea(this).Width - Width;
            Top = (Screen.GetWorkingArea(this).Height - Height) / 2;
        }
        #endregion // Actions sur la ListBar

        #region Actions sur la collection de NotIts
        /// <summary>
        /// Affiche toutes les NotIts au premier plan.
        /// </summary>
        /// <param name="sender">Entr�e du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.ShowAll();
        }

        /// <summary>
        /// Cache toutes les NotIts.
        /// </summary>
        /// <param name="sender">Entr�e du menu contextuel.</param>
        /// <param name="e">Clique.</param>
        private void hideAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.HideAll();
        }
        #endregion // Actions sur la collection de NotIts
    }
}
