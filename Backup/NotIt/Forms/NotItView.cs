using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nikoui.NotIt.Forms
{
    /// <summary>
    /// Représentation d'une NotIt (vue).
    /// </summary>
    public partial class NotItView : Form
    {
        #region Variable locales
        /// <summary>
        /// Identifiant de la NotIt représentée.
        /// </summary>
        private int modelId;

        /// <summary>
        /// Coordonnées d'origine de la vue (au début d'un déplacement).
        /// </summary>
        private Point origin;

        /// <summary>
        /// Référence vers le controler de NotIts.
        /// </summary>
        private NotItControler controler;

        /// <summary>
        /// Le titre est en cours d'édition.
        /// </summary>
        private bool isTitleEdited;

        /// <summary>
        /// Les détails de la NotIt sont en cours d'édition.
        /// </summary>
        private bool isDetailsEdited;

        /// <summary>
        /// Sauvegarde temporaire du curseur pendant un déplacement.
        /// </summary>
        private Cursor previousCursor;

        #region Constantes
        /// <summary>
        /// Attribut de bordure des fenêtres
        /// </summary>
        private const int WS_BORDER = 0x00800000;
        #endregion // Constantes

        #region Réponse aux évènements
        /// <summary>
        /// Réponse aux évènements StatusChanged.
        /// </summary>
        private NotIt.StatusChangedEventHandler statusChangedEventHandler;

        /// <summary>
        /// Réponse aux évènements Hided.
        /// </summary>
        private NotIt.HidedEventHandler hidedEventHandler;

        /// <summary>
        /// Réponse aux évènements Showed.
        /// </summary>
        private NotIt.ShowedEventHandler showedEventHandler;
        #endregion // Réponse aux évènements

        #endregion // Variable locales

        #region Construction / Initialisation
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <remarks>
        /// Non utilisé. Fournit pour permettre l'affichage dans le designer.
        /// </remarks>
        public NotItView()
        {
            isTitleEdited = false;
            isDetailsEdited = false;
            InitializeComponent();
        }

        /// <summary>
        /// Construction d'une vue pour une NotIt.
        /// </summary>
        /// <param name="model">NotIt à représenter.</param>
        public NotItView(NotIt model)
        {
            isTitleEdited = false;
            isDetailsEdited = false;
            modelId = model.Id;
            InitializeComponent();
            // Mise à jour de la vue à partir de la NotIt.
            UpdateView(model);
            // Abonnement aux évènements de la NotIt.
            statusChangedEventHandler = new NotIt.StatusChangedEventHandler(model_StatusChanged);
            showedEventHandler = new NotIt.ShowedEventHandler(model_Showed);
            hidedEventHandler = new NotIt.HidedEventHandler(model_Hided);
            model.StatusChanged += statusChangedEventHandler;
            model.Showed += showedEventHandler;
            model.Hided += hidedEventHandler;
            // Obtention d'une référence vers le controler de NotIts.
            controler = NotItControler.Instance;
            // Création du fond transparent
            SetTransparentBackgroundImage();
            detailsTextBox.LostFocus += new EventHandler(detailsTextBox_LostFocus);
            detailsLabel.MouseEnter += new EventHandler(label_MouseEnter);
            detailsLabel.MouseLeave += new EventHandler(label_MouseLeave);
            titleTextBox.LostFocus += new EventHandler(titleTextBox_LostFocus);
            titleLabel.MouseEnter += new EventHandler(label_MouseEnter);
            titleLabel.MouseLeave += new EventHandler(label_MouseLeave);
        }   

        /// <summary>
        /// Ajout d'un paramètre à la fenêtre pour masquer les bordures de fenêtre en mode "toolBar".
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~WS_BORDER;
                return cp;
            }
        }

        /// <summary>
        /// Définition de la transparence sur l'image de fond de la fenêtre.
        /// </summary>
        private void SetTransparentBackgroundImage()
        {
            Bitmap bmp = new Bitmap(this.BackgroundImage);
            // Le pixel en haut à gauche sert de référence pour la couleur transparente.
            bmp.MakeTransparent();
            this.BackgroundImage = bmp;
        }
        #endregion // Construction / Initialisation

        #region Réponse aux évènements de la NotIt
        /// <summary>
        /// Mise en premier plan de la NotIt.
        /// </summary>
        /// <param name="source">NotIt.</param>
        void model_Showed(object source)
        {
            Visible = true;
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            BringToFront();
        }

        /// <summary>
        /// Masquage de la NotIt.
        /// </summary>
        /// <param name="source">NotIt.</param>
        void model_Hided(object source)
        {
            Visible = false;
        }

        /// <summary>
        /// Modification des propriétés de la NotIt.
        /// </summary>
        /// <param name="source">NotIt.</param>
        void model_StatusChanged(object source)
        {
            UpdateView((NotIt)source);
        }
        #endregion // Réponse aux évènements de la NotIt

        #region Rafraichissement de la vue
        /// <summary>
        /// Mise à jour de la vue en fonction des propriétés de la NotIt.
        /// </summary>
        /// <param name="model">NotIt à représenter.</param>
        private void UpdateView(NotIt model)
        {
            Location = model.Location;
            Name = string.Format("{0}_{1}", model.Title, model.Id);
            titleLabel.Text = model.Title;
            titleLabel.Left = (Width - titleLabel.Width) / 2;
            toolTip.SetToolTip(titleLabel, titleLabel.Text);
            detailsLabel.Text = model.Details;
            toolTip.SetToolTip(detailsLabel, detailsLabel.Text);
            TopMost = model.Pinned;
            pinToolStripMenuItem.Checked = model.Pinned;
        }
        #endregion // Rafraichissement de la vue

        #region Réponse aux évènements de souris
        /// <summary>
        /// L'utilisateur clique sur la vue, on se prépare à la déplacer.
        /// </summary>
        /// <remarks>
        /// Le déplacement se fait en cliquant avec le bouton gauche sur la vue.
        /// </remarks>
        /// <param name="e">Evènement de début de clique de souris.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Il s'agit du bouton gauche de la souris, on initialise le déplacement.
                origin = e.Location;
                //previousCursor = Cursor.Current;
                //Cursor.Current = Cursors.SizeAll;
            }
            LeaveTitleEditing();
            LeaveDetailsEditing();
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Déplacement de la souris, si le bouton gauche est enfoncé, on déplace la vue.
        /// </summary>
        /// <param name="e">Evènement de déplacement de la souris.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int xDelta = e.X - origin.X;
                int yDelta = e.Y - origin.Y;
                Point newLocation = new Point(Left + xDelta, Top + yDelta);
                // Déplacement effectif de la NotIt par le controler.
                controler.Move(modelId, newLocation);
            }
            base.OnMouseMove(e);
        }

        /// <summary>
        /// La souris survole un label, modification du curseur.
        /// </summary>
        /// <param name="sender">Labels titleLabel ou detailsLabel.</param>
        /// <param name="e">Début de survole.</param>
        void label_MouseEnter(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            previousCursor = label.Cursor;
            label.Cursor = Cursors.Hand;
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// La souris ne survole plus le label, modification du curseur.
        /// </summary>
        /// <param name="sender">Labels titleLabel ou detailsLabel.</param>
        /// <param name="e">Fin de survole.</param>
        void label_MouseLeave(object sender, EventArgs e)
        {
            if (previousCursor != null)
            {
                Label label = (Label)sender;
                label.Cursor = previousCursor;
                previousCursor = null;
            }
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Début de survol du curseur sur la vue.
        /// </summary>
        /// <param name="sender">NotItView.</param>
        /// <param name="e">Entrée du curseur sur le contrôle.</param>
        private void NotItView_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.SizeAll;
        }

        /// <summary>
        /// Fin du survol du curseur sur la vue.
        /// </summary>
        /// <param name="sender">NotItView.</param>
        /// <param name="e">Sortie du curseur du contrôle.</param>
        private void NotItView_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        #endregion // Réponse aux évènements de souris

        #region Détachement de la vue
        /// <summary>
        /// On détache la vue de la NotIt.
        /// </summary>
        /// <param name="model">NotIt représentée.</param>
        public void Detach(NotIt model)
        {
            // On retire les "event handlers"
            model.Hided -= hidedEventHandler;
            model.Showed -= showedEventHandler;
            model.StatusChanged -= statusChangedEventHandler;
        }  
        #endregion // Détachement de la vue      

        #region Edition du titre de la NotIt
        /// <summary>
        /// L'utilisateur à cliqué sur le titre de la NotIt, on entre en mode édition du titre.
        /// </summary>
        /// <param name="sender">Label titleLabel.</param>
        /// <param name="e">Evènement de clique.</param>
        private void titleLabel_Click(object sender, EventArgs e)
        {
            EnterTitleEditing();
        }

        /// <summary>
        /// Entre dans le mode édition du titre : on remplace le label par un text box.
        /// </summary>
        private void EnterTitleEditing()
        {
            isTitleEdited = true; ;
            titleTextBox.Text = titleLabel.Text;
            titleTextBox.Visible = true;
            titleLabel.Visible = false;
            titleTextBox.Focus();
        }

        /// <summary>
        /// Sort du mode édition du titre : on cache le textbox et on affiche a nouveau le label.
        /// Le changement de titre est envoyé au controler.
        /// </summary>
        private void LeaveTitleEditing()
        {
            if (isTitleEdited)
            {
                isTitleEdited = false;
                titleLabel.Visible = true;
                titleTextBox.Visible = false;
                controler.SetTitle(modelId, titleTextBox.Text);
            }
        }

        /// <summary>
        /// On récupère l'appuie sur la touche "Entrée" dans le textbox d'édition du titre, 
        /// pour mettre fin à l'édition du titre et passer à l'édition des détails.
        /// </summary>
        /// <param name="sender">Textbox titleTextBox.</param>
        /// <param name="e">Appuie sur une touche.</param>
        private void titleTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // L'utilisateur appluie sur Entrée, on valide l'édition.
                LeaveTitleEditing();
                EnterDetailsEditing();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Le TextBox d'édition perd le focus, on met fin à l'édition du titre.
        /// </summary>
        /// <param name="sender">TextBox titleTextBox.</param>
        /// <param name="e">Perte de focus.</param>
        void titleTextBox_LostFocus(object sender, EventArgs e)
        {
            LeaveTitleEditing();
        }
        #endregion // Edition du titre de la NotIt

        #region Edition des détails de la NotIt
        /// <summary>
        /// L'utilisateur à cliqué sur les détails de la NotIt, on entre en mode édition des détails.
        /// </summary>
        /// <param name="sender">Label detailsLabel.</param>
        /// <param name="e">Clique.</param>
        private void detailsLabel_Click(object sender, EventArgs e)
        {
            EnterDetailsEditing();
        }

        /// <summary>
        /// Entre dans le mode édition des détails : on remplace le Label par un TextBox.
        /// </summary>
        private void EnterDetailsEditing()
        {
            isDetailsEdited = true;
            detailsTextBox.Text = detailsLabel.Text;
            detailsTextBox.Visible = true;
            detailsLabel.Visible = false;
            detailsTextBox.Focus();
        }

        /// <summary>
        /// Sort du mode édition des détails : on cache le TextBox et on affiche a nouveau le Label.
        /// Le changement de détails est envoyé au controler.
        /// </summary>
        private void LeaveDetailsEditing()
        {
            if (isDetailsEdited)
            {
                isDetailsEdited = false;
                detailsLabel.Visible = true;
                detailsTextBox.Visible = false;
                controler.SetDetails(modelId, detailsTextBox.Text);
            }
        }

        /// <summary>
        /// Le TextBox d'édition perd le focus, on met fin à l'édition des détails.
        /// </summary>
        /// <param name="sender">TextBox detailsTextBox.</param>
        /// <param name="e">Perte de focus.</param>
        void detailsTextBox_LostFocus(object sender, EventArgs e)
        {
            LeaveDetailsEditing();
        }
        #endregion // Edition des détails de la NotIt

        #region Entrées du menu contextuel
        /// <summary>
        /// Modification de la propriété "épinglée" de la NotIt.
        /// </summary>
        /// <param name="sender">Entrée de menu pinToolStripMenuItem.</param>
        /// <param name="e">Clique</param>
        private void pinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool previousState = pinToolStripMenuItem.Checked;
            controler.Pin(modelId, !previousState);
        }

        /// <summary>
        /// Suppression de la NotIt.
        /// </summary>
        /// <param name="sender">Entrée de menu deleteToolStripMenuItem.</param>
        /// <param name="e">Clique</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.Delete(modelId);
        }

        /// <summary>
        /// Création d'une nouvelle NotIt.
        /// </summary>
        /// <param name="sender">Entrée de menu newToolStripMenuItem.</param>
        /// <param name="e">Clique</param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.New();
        }
        #endregion // Entrées du menu contextuel
    }
}