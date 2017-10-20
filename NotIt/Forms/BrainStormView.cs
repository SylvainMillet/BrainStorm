using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Smilly.BrainStorm.Forms
{
    /// Repr�sentation d'une BrainStorm (vue).
    public partial class BrainStormView : Form
    {
        #region Variable locales
        /// Identifiant de la BrainStorm repr�sent�e.
        private int modelId;

        /// Coordonn�es d'origine de la vue (au d�but d'un d�placement).
        private Point origin;

        /// R�f�rence vers le controler de BrainStorms.
        private BrainStormControler controler;

        /// Le titre est en cours d'�dition.
        private bool isTitleEdited;

        /// Les d�tails de la BrainStorm sont en cours d'�dition.
        private bool isDetailsEdited;

        /// Sauvegarde temporaire du curseur pendant un d�placement.
        private Cursor previousCursor;

        private int typeView;

        #region Constantes
        /// Attribut de bordure des fen�tres
        private const int WS_BORDER = 0x00800000;
        #endregion // Constantes

        #region R�ponse aux �v�nements
        /// R�ponse aux �v�nements StatusChanged.
        private BrainStorm.StatusChangedEventHandler statusChangedEventHandler;

        /// R�ponse aux �v�nements Hided.
        private BrainStorm.HidedEventHandler hidedEventHandler;

        /// R�ponse aux �v�nements Showed.

        private BrainStorm.ShowedEventHandler showedEventHandler;
        #endregion // R�ponse aux �v�nements

        #endregion // Variable locales

        #region Construction / Initialisation
        /// Constructeur par d�faut.
        /// Non utilis�. Fournit pour permettre l'affichage dans le designer.
        public BrainStormView()
        {
            isTitleEdited = false;
            isDetailsEdited = false;
            InitializeComponent();
        }

        /// Construction d'une vue pour une BrainStorm.
        /// <param name="model">BrainStorm � repr�senter.</param>
        public BrainStormView(BrainStorm model, int type)
        {
            typeView = type;
            isTitleEdited = false;
            isDetailsEdited = false;
            modelId = model.Id;
            InitializeComponent();
            // Mise � jour de la vue � partir de la BrainStorm.
            UpdateView(model);
            // Abonnement aux �v�nements de la BrainStorm.
            statusChangedEventHandler = new BrainStorm.StatusChangedEventHandler(model_StatusChanged);
            showedEventHandler = new BrainStorm.ShowedEventHandler(model_Showed);
            hidedEventHandler = new BrainStorm.HidedEventHandler(model_Hided);
            model.StatusChanged += statusChangedEventHandler;
            model.Showed += showedEventHandler;
            model.Hided += hidedEventHandler;
            // Obtention d'une r�f�rence vers le controler de BrainStorms.
            controler = BrainStormControler.Instance;
            // Cr�ation du fond transparent
            SetTransparentBackgroundImage();
            detailsTextBox.LostFocus += new EventHandler(detailsTextBox_LostFocus);
            detailsLabel.MouseEnter += new EventHandler(label_MouseEnter);
            detailsLabel.MouseLeave += new EventHandler(label_MouseLeave);
            titleTextBox.LostFocus += new EventHandler(titleTextBox_LostFocus);
            titleLabel.MouseEnter += new EventHandler(label_MouseEnter);
            titleLabel.MouseLeave += new EventHandler(label_MouseLeave);
        }   

        /// Ajout d'un param�tre � la fen�tre pour masquer les bordures de fen�tre en mode "toolBar".
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~WS_BORDER;
                return cp;
            }
        }

        /// D�finition de la transparence sur l'image de fond de la fen�tre.
        private void SetTransparentBackgroundImage()
        {
            Bitmap bmp = new Bitmap(this.BackgroundImage);
            // Le pixel en haut � gauche sert de r�f�rence pour la couleur transparente.
            bmp.MakeTransparent();
            this.BackgroundImage = bmp;
        }
        #endregion // Construction / Initialisation

        #region R�ponse aux �v�nements de la BrainStorm
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

        /// Modification des propri�t�s de la BrainStorm.
        /// <param name="source">BrainStorm.</param>
        void model_StatusChanged(object source)
        {
            UpdateView((BrainStorm)source);
        }
        #endregion // R�ponse aux �v�nements de la BrainStorm

        #region Rafraichissement de la vue

        /// Mise � jour de la vue en fonction des propri�t�s de la BrainStorm.

        /// <param name="model">BrainStorm � repr�senter.</param>
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

        #region R�ponse aux �v�nements de souris
        /// L'utilisateur clique sur la vue, on se pr�pare � la d�placer.
        /// Le d�placement se fait en cliquant avec le bouton gauche sur la vue.
        /// <param name="e">Ev�nement de d�but de clique de souris.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Il s'agit du bouton gauche de la souris, on initialise le d�placement.
                origin = e.Location;
                //previousCursor = Cursor.Current;
                //Cursor.Current = Cursors.SizeAll;
            }
            LeaveTitleEditing();
            LeaveDetailsEditing();
            base.OnMouseDown(e);
        }

        /// D�placement de la souris, si le bouton gauche est enfonc�, on d�place la vue.
        /// <param name="e">Ev�nement de d�placement de la souris.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int xDelta = e.X - origin.X;
                int yDelta = e.Y - origin.Y;
                Point newLocation = new Point(Left + xDelta, Top + yDelta);
                // D�placement effectif de la BrainStorm par le controler.
                controler.Move(modelId, newLocation);
            }
            base.OnMouseMove(e);
        }

        /// La souris survole un label, modification du curseur.
        /// <param name="sender">Labels titleLabel ou detailsLabel.</param>
        /// <param name="e">D�but de survole.</param>
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

        /// D�but de survol du curseur sur la vue.
        /// <param name="sender">BrainStormView.</param>
        /// <param name="e">Entr�e du curseur sur le contr�le.</param>
        private void BrainStormView_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.SizeAll;
        }

        /// Fin du survol du curseur sur la vue.
        /// <param name="sender">BrainStormView.</param>
        /// <param name="e">Sortie du curseur du contr�le.</param>
        private void BrainStormView_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        #endregion // R�ponse aux �v�nements de souris

        #region D�tachement de la vue

        /// On d�tache la vue de la BrainStorm.

        /// <param name="model">BrainStorm repr�sent�e.</param>
        public void Detach(BrainStorm model)
        {
            // On retire les "event handlers"
            model.Hided -= hidedEventHandler;
            model.Showed -= showedEventHandler;
            model.StatusChanged -= statusChangedEventHandler;
        }  
        #endregion // D�tachement de la vue      

        #region Edition du titre de la BrainStorm
        /// L'utilisateur � cliqu� sur le titre de la BrainStorm, on entre en mode �dition du titre.
        /// <param name="sender">Label titleLabel.</param>
        /// <param name="e">Ev�nement de clique.</param>
        private void titleLabel_Click(object sender, EventArgs e)
        {
            EnterTitleEditing();
        }

        /// Entre dans le mode �dition du titre : on remplace le label par un text box.
        private void EnterTitleEditing()
        {
            isTitleEdited = true; ;
            titleTextBox.Text = titleLabel.Text;
            titleTextBox.Visible = true;
            titleLabel.Visible = false;
            titleTextBox.Focus();
        }

        /// Sort du mode �dition du titre : on cache le textbox et on affiche a nouveau le label.
        /// Le changement de titre est envoy� au controler.
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

        /// On r�cup�re l'appuie sur la touche "Entr�e" dans le textbox d'�dition du titre, 
        /// pour mettre fin � l'�dition du titre et passer � l'�dition des d�tails.
        /// <param name="sender">Textbox titleTextBox.</param>
        /// <param name="e">Appuie sur une touche.</param>
        private void titleTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // L'utilisateur appluie sur Entr�e, on valide l'�dition.
                LeaveTitleEditing();
                EnterDetailsEditing();
                e.Handled = true;
            }
        }

        /// Le TextBox d'�dition perd le focus, on met fin � l'�dition du titre.
        /// <param name="sender">TextBox titleTextBox.</param>
        /// <param name="e">Perte de focus.</param>
        void titleTextBox_LostFocus(object sender, EventArgs e)
        {
            LeaveTitleEditing();
        }
        #endregion // Edition du titre de la BrainStorm

        #region Edition des d�tails de la BrainStorm
        /// L'utilisateur � cliqu� sur les d�tails de la BrainStorm, on entre en mode �dition des d�tails.
        /// <param name="sender">Label detailsLabel.</param>
        /// <param name="e">Clique.</param>
        private void detailsLabel_Click(object sender, EventArgs e)
        {
            EnterDetailsEditing();
        }

        /// Entre dans le mode �dition des d�tails : on remplace le Label par un TextBox.
        private void EnterDetailsEditing()
        {
            isDetailsEdited = true;
            detailsTextBox.Text = detailsLabel.Text;
            detailsTextBox.Visible = true;
            detailsLabel.Visible = false;
            detailsTextBox.Focus();
        }

        /// Sort du mode �dition des d�tails : on cache le TextBox et on affiche a nouveau le Label.
        /// Le changement de d�tails est envoy� au controler.
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

        /// Le TextBox d'�dition perd le focus, on met fin � l'�dition des d�tails.
        /// <param name="sender">TextBox detailsTextBox.</param>
        /// <param name="e">Perte de focus.</param>
        void detailsTextBox_LostFocus(object sender, EventArgs e)
        {
            LeaveDetailsEditing();
        }
        #endregion // Edition des d�tails de la BrainStorm

        #region Entr�es du menu contextuel
        /// Modification de la propri�t� "�pingl�e" de la BrainStorm.
        /// <param name="sender">Entr�e de menu pinToolStripMenuItem.</param>
        /// <param name="e">Clique</param>
        private void pinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool previousState = pinToolStripMenuItem.Checked;
            controler.Pin(modelId, !previousState);
        }

        /// Suppression de la BrainStorm.
        /// <param name="sender">Entr�e de menu deleteToolStripMenuItem.</param>
        /// <param name="e">Clique</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.Delete(modelId);
        }

        /// Cr�ation d'une nouvelle BrainStorm.
        /// <param name="sender">Entr�e de menu newToolStripMenuItem.</param>
        /// <param name="e">Clique</param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controler.New(typeView);
        }
        #endregion // Entr�es du menu contextuel
    }
}