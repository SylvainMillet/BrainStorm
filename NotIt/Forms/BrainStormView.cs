using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Smilly.BrainStorm.Forms
{
    /// Représentation d'une BrainStorm (vue).
    public partial class BrainStormView : Form
    {
        #region Variable locales
        /// Identifiant de la BrainStorm représentée.
        private int modelId;

        /// Coordonnées d'origine de la vue (au début d'un déplacement).
        private Point origin;

        /// Référence vers le controler de BrainStorms.
        private BrainStormControler controler;

        /// Le titre est en cours d'édition.
        private bool isTitleEdited;

        /// Les détails de la BrainStorm sont en cours d'édition.
        private bool isDetailsEdited;

        /// Sauvegarde temporaire du curseur pendant un déplacement.
        private Cursor previousCursor;

        private int typeView;

        #region Constantes
        /// Attribut de bordure des fenêtres
        private const int WS_BORDER = 0x00800000;
        #endregion // Constantes

        #region Réponse aux évènements
        /// Réponse aux évènements StatusChanged.
        private BrainStorm.StatusChangedEventHandler statusChangedEventHandler;

        /// Réponse aux évènements Hided.
        private BrainStorm.HidedEventHandler hidedEventHandler;

        /// Réponse aux évènements Showed.

        private BrainStorm.ShowedEventHandler showedEventHandler;
        #endregion // Réponse aux évènements

        #endregion // Variable locales

        #region Construction / Initialisation
        /// Constructeur par défaut.
        /// Non utilisé. Fournit pour permettre l'affichage dans le designer.
        public BrainStormView()
        {
            isTitleEdited = false;
            isDetailsEdited = false;
            InitializeComponent();
        }

        /// Construction d'une vue pour une BrainStorm.
        /// <param name="model">BrainStorm à représenter.</param>
        public BrainStormView(BrainStorm model, int type)
        {
            typeView = type;
            isTitleEdited = false;
            isDetailsEdited = false;
            modelId = model.Id;
            InitializeComponent();
            // Mise à jour de la vue à partir de la BrainStorm.
            UpdateView(model);
            // Abonnement aux évènements de la BrainStorm.
            statusChangedEventHandler = new BrainStorm.StatusChangedEventHandler(model_StatusChanged);
            showedEventHandler = new BrainStorm.ShowedEventHandler(model_Showed);
            hidedEventHandler = new BrainStorm.HidedEventHandler(model_Hided);
            model.StatusChanged += statusChangedEventHandler;
            model.Showed += showedEventHandler;
            model.Hided += hidedEventHandler;
            // Obtention d'une référence vers le controler de BrainStorms.
            controler = BrainStormControler.Instance;
            // Création du fond transparent
            SetTransparentBackgroundImage();
            detailsTextBox.LostFocus += new EventHandler(detailsTextBox_LostFocus);
            detailsLabel.MouseEnter += new EventHandler(label_MouseEnter);
            detailsLabel.MouseLeave += new EventHandler(label_MouseLeave);
            titleTextBox.LostFocus += new EventHandler(titleTextBox_LostFocus);
            titleLabel.MouseEnter += new EventHandler(label_MouseEnter);
            titleLabel.MouseLeave += new EventHandler(label_MouseLeave);
        }   

        /// Ajout d'un paramètre à la fenêtre pour masquer les bordures de fenêtre en mode "toolBar".
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~WS_BORDER;
                return cp;
            }
        }

        /// Définition de la transparence sur l'image de fond de la fenêtre.
        private void SetTransparentBackgroundImage()
        {
            Bitmap bmp = new Bitmap(this.BackgroundImage);
            // Le pixel en haut à gauche sert de référence pour la couleur transparente.
            bmp.MakeTransparent();
            this.BackgroundImage = bmp;
        }
        #endregion // Construction / Initialisation

        #region Réponse aux évènements de la BrainStorm
        /// Mise en premier plan de la BrainStorm.
        /// <param name="source">BrainStorm.</param>
        void model_Showed(object source)
        {
            Visible = true;
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            BringToFront();
        }

        /// Masquage de la BrainStorm.
        /// <param name="source">BrainStorm.</param>
        void model_Hided(object source)
        {
            Visible = false;
        }

        /// Modification des propriétés de la BrainStorm.
        /// <param name="source">BrainStorm.</param>
        void model_StatusChanged(object source)
        {
            UpdateView((BrainStorm)source);
        }
        #endregion // Réponse aux évènements de la BrainStorm

        #region Rafraichissement de la vue

        /// Mise à jour de la vue en fonction des propriétés de la BrainStorm.

        /// <param name="model">BrainStorm à représenter.</param>
        private void UpdateView(BrainStorm model)
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
        /// L'utilisateur clique sur la vue, on se prépare à la déplacer.
        /// Le déplacement se fait en cliquant avec le bouton gauche sur la vue.
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

        /// Déplacement de la souris, si le bouton gauche est enfoncé, on déplace la vue.
        /// <param name="e">Evènement de déplacement de la souris.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int xDelta = e.X - origin.X;
                int yDelta = e.Y - origin.Y;
                Point newLocation = new Point(Left + xDelta, Top + yDelta);
                // Déplacement effectif de la BrainStorm par le controler.
                controler.Move(modelId, newLocation);
            }
            base.OnMouseMove(e);
        }

        /// La souris survole un label, modification du curseur.
        /// <param name="sender">Labels titleLabel ou detailsLabel.</param>
        /// <param name="e">Début de survole.</param>
        void label_MouseEnter(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            previousCursor = label.Cursor;
            label.Cursor = Cursors.Hand;
            base.OnMouseEnter(e);
        }

        /// La souris ne survole plus le label, modification du curseur.
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

        /// Début de survol du curseur sur la vue.
        /// <param name="sender">BrainStormView.</param>
        /// <param name="e">Entrée du curseur sur le contrôle.</param>
        private void BrainStormView_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.SizeAll;
        }

        /// Fin du survol du curseur sur la vue.
        /// <param name="sender">BrainStormView.</param>
        /// <param name="e">Sortie du curseur du contrôle.</param>
        private void BrainStormView_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        #endregion // Réponse aux évènements de souris

        #region Détachement de la vue

        /// On détache la vue de la BrainStorm.

        /// <param name="model">BrainStorm représentée.</param>
        public void Detach(BrainStorm model)
        {
            // On retire les "event handlers"
            model.Hided -= hidedEventHandler;
            model.Showed -= showedEventHandler;
            model.StatusChanged -= statusChangedEventHandler;
        }  
        #endregion // Détachement de la vue      

        #region Edition du titre de la BrainStorm
        /// L'utilisateur à cliqué sur le titre de la BrainStorm, on entre en mode édition du titre.
        /// <param name="sender">Label titleLabel.</param>
        /// <param name="e">Evènement de clique.</param>
        private void titleLabel_Click(object sender, EventArgs e)
        {
            EnterTitleEditing();
        }

        /// Entre dans le mode édition du titre : on remplace le label par un text box.
        private void EnterTitleEditing()
        {
            isTitleEdited = true; ;
            titleTextBox.Text = titleLabel.Text;
            titleTextBox.Visible = true;
            titleLabel.Visible = false;
            titleTextBox.Focus();
        }

        /// Sort du mode édition du titre : on cache le textbox et on affiche a nouveau le label.
        /// Le changement de titre est envoyé au controler.
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

        /// On récupère l'appuie sur la touche "Entrée" dans le textbox d'édition du titre, 
        /// pour mettre fin à l'édition du titre et passer à l'édition des détails.
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

        /// Le TextBox d'édition perd le focus, on met fin à l'édition du titre.
        /// <param name="sender">TextBox titleTextBox.</param>
        /// <param name="e">Perte de focus.</param>
        void titleTextBox_LostFocus(object sender, EventArgs e)
        {
            LeaveTitleEditing();
        }
        #endregion // Edition du titre de la BrainStorm

        #region Edition des détails de la BrainStorm
        /// L'utilisateur à cliqué sur les détails de la BrainStorm, on entre en mode édition des détails.
        /// <param name="sender">Label detailsLabel.</param>
        /// <param name="e">Clique.</param>
        private void detailsLabel_Click(object sender, EventArgs e)
        {
            EnterDetailsEditing();
        }

        /// Entre dans le mode édition des détails : on remplace le Label par un TextBox.
        private void EnterDetailsEditing()
        {
            isDetailsEdited = true;
            detailsTextBox.Text = detailsLabel.Text;
            detailsTextBox.Visible = true;
            detailsLabel.Visible = false;
            detailsTextBox.Focus();
        }

        /// Sort du mode édition des détails : on cache le TextBox et on affiche a nouveau le Label.
        /// Le changement de détails est envoyé au controler.
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

        /// Le TextBox d'édition perd le focus, on met fin à l'édition des détails.
        /// <param name="sender">TextBox detailsTextBox.</param>
        /// <param name="e">Perte de focus.</param>
        void detailsTextBox_LostFocus(object sender, EventArgs e)
        {
            LeaveDetailsEditing();
        }
        #endregion // Edition des détails de la BrainStorm

        #region Entrées du menu contextuel
        /// Modification de la propriété "épinglée" de la BrainStorm.
        /// <param name="sender">Entrée de menu pinToolStripMenuItem.</param>
        /// <param name="e">Clique</param>
        private void pinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool previousState = pinToolStripMenuItem.Checked;
            controler.Pin(modelId, !previousState);
        }

        /// Suppression de la BrainStorm.
        /// <param name="sender">Entrée de menu deleteToolStripMenuItem.</param>
        /// <param name="e">Clique</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.Delete(modelId);
        }

        /// Création d'une nouvelle BrainStorm.
        /// <param name="sender">Entrée de menu newToolStripMenuItem.</param>
        /// <param name="e">Clique</param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.New(typeView);
        }
        #endregion // Entrées du menu contextuel
    }
}