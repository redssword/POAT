namespace IHM
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.pBImgRefSc = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pBVeriteSc = new System.Windows.Forms.PictureBox();
			this.btnTraitement = new System.Windows.Forms.Button();
			this.pbResSc = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lbResIOUSc = new System.Windows.Forms.Label();
			this.lbMoyIOUSc = new System.Windows.Forms.Label();
			this.ofdImg = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageUniqueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dossierDimageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.défilementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.démarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.arréterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FBDDir = new System.Windows.Forms.FolderBrowserDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.lbMoyIOUIn = new System.Windows.Forms.Label();
			this.lbResIOUIn = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.pbResIN = new System.Windows.Forms.PictureBox();
			this.label14 = new System.Windows.Forms.Label();
			this.pBVeriteIn = new System.Windows.Forms.PictureBox();
			this.label15 = new System.Windows.Forms.Label();
			this.pBImgRefIn = new System.Windows.Forms.PictureBox();
			this.TimerAff = new System.Windows.Forms.Timer(this.components);
			this.TbAff = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.pBImgRefSc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pBVeriteSc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbResSc)).BeginInit();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbResIN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pBVeriteIn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pBImgRefIn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TbAff)).BeginInit();
			this.SuspendLayout();
			// 
			// pBImgRefSc
			// 
			this.pBImgRefSc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pBImgRefSc.Location = new System.Drawing.Point(119, 68);
			this.pBImgRefSc.Name = "pBImgRefSc";
			this.pBImgRefSc.Size = new System.Drawing.Size(230, 230);
			this.pBImgRefSc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pBImgRefSc.TabIndex = 0;
			this.pBImgRefSc.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(180, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(115, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Image de référence Sc";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(574, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Vérité terrain";
			// 
			// pBVeriteSc
			// 
			this.pBVeriteSc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pBVeriteSc.Location = new System.Drawing.Point(490, 68);
			this.pBVeriteSc.Name = "pBVeriteSc";
			this.pBVeriteSc.Size = new System.Drawing.Size(230, 230);
			this.pBVeriteSc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pBVeriteSc.TabIndex = 2;
			this.pBVeriteSc.TabStop = false;
			// 
			// btnTraitement
			// 
			this.btnTraitement.Location = new System.Drawing.Point(577, 356);
			this.btnTraitement.Name = "btnTraitement";
			this.btnTraitement.Size = new System.Drawing.Size(84, 31);
			this.btnTraitement.TabIndex = 4;
			this.btnTraitement.Text = "Traitement";
			this.btnTraitement.UseVisualStyleBackColor = true;
			this.btnTraitement.Click += new System.EventHandler(this.btnTraitement_Click);
			// 
			// pbResSc
			// 
			this.pbResSc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbResSc.Location = new System.Drawing.Point(826, 68);
			this.pbResSc.Name = "pbResSc";
			this.pbResSc.Size = new System.Drawing.Size(230, 230);
			this.pbResSc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbResSc.TabIndex = 5;
			this.pbResSc.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(954, 30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Résultat Image";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(1129, 125);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Résultat IOU";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(1129, 176);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(89, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Moyenne IOU Sc";
			// 
			// lbResIOUSc
			// 
			this.lbResIOUSc.AutoSize = true;
			this.lbResIOUSc.Location = new System.Drawing.Point(1338, 125);
			this.lbResIOUSc.Name = "lbResIOUSc";
			this.lbResIOUSc.Size = new System.Drawing.Size(0, 13);
			this.lbResIOUSc.TabIndex = 9;
			// 
			// lbMoyIOUSc
			// 
			this.lbMoyIOUSc.AutoSize = true;
			this.lbMoyIOUSc.Location = new System.Drawing.Point(1338, 176);
			this.lbMoyIOUSc.Name = "lbMoyIOUSc";
			this.lbMoyIOUSc.Size = new System.Drawing.Size(0, 13);
			this.lbMoyIOUSc.TabIndex = 10;
			// 
			// ofdImg
			// 
			this.ofdImg.FileName = "openFileDialog1";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.défilementToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1429, 24);
			this.menuStrip1.TabIndex = 15;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fichierToolStripMenuItem
			// 
			this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageUniqueToolStripMenuItem,
            this.dossierDimageToolStripMenuItem});
			this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
			this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.fichierToolStripMenuItem.Text = "Fichier";
			// 
			// imageUniqueToolStripMenuItem
			// 
			this.imageUniqueToolStripMenuItem.Name = "imageUniqueToolStripMenuItem";
			this.imageUniqueToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.imageUniqueToolStripMenuItem.Text = "Image Unique";
			this.imageUniqueToolStripMenuItem.Click += new System.EventHandler(this.imageUniqueToolStripMenuItem_Click);
			// 
			// dossierDimageToolStripMenuItem
			// 
			this.dossierDimageToolStripMenuItem.Name = "dossierDimageToolStripMenuItem";
			this.dossierDimageToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.dossierDimageToolStripMenuItem.Text = "Dossier d\'image";
			this.dossierDimageToolStripMenuItem.Click += new System.EventHandler(this.dossierDimageToolStripMenuItem_Click);
			// 
			// défilementToolStripMenuItem
			// 
			this.défilementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.démarToolStripMenuItem,
            this.arréterToolStripMenuItem});
			this.défilementToolStripMenuItem.Name = "défilementToolStripMenuItem";
			this.défilementToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
			this.défilementToolStripMenuItem.Text = "Défilement";
			// 
			// démarToolStripMenuItem
			// 
			this.démarToolStripMenuItem.Name = "démarToolStripMenuItem";
			this.démarToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.démarToolStripMenuItem.Text = "Démarrer";
			this.démarToolStripMenuItem.Click += new System.EventHandler(this.démarToolStripMenuItem_Click);
			// 
			// arréterToolStripMenuItem
			// 
			this.arréterToolStripMenuItem.Name = "arréterToolStripMenuItem";
			this.arréterToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.arréterToolStripMenuItem.Text = "Arréter";
			this.arréterToolStripMenuItem.Click += new System.EventHandler(this.arréterToolStripMenuItem_Click);
			// 
			// lbMoyIOUIn
			// 
			this.lbMoyIOUIn.AutoSize = true;
			this.lbMoyIOUIn.Location = new System.Drawing.Point(1338, 575);
			this.lbMoyIOUIn.Name = "lbMoyIOUIn";
			this.lbMoyIOUIn.Size = new System.Drawing.Size(0, 13);
			this.lbMoyIOUIn.TabIndex = 25;
			// 
			// lbResIOUIn
			// 
			this.lbResIOUIn.AutoSize = true;
			this.lbResIOUIn.Location = new System.Drawing.Point(1338, 524);
			this.lbResIOUIn.Name = "lbResIOUIn";
			this.lbResIOUIn.Size = new System.Drawing.Size(0, 13);
			this.lbResIOUIn.TabIndex = 24;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(1129, 575);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(85, 13);
			this.label11.TabIndex = 23;
			this.label11.Text = "Moyenne IOU In";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(1129, 524);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(68, 13);
			this.label12.TabIndex = 22;
			this.label12.Text = "Résultat IOU";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(954, 429);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(78, 13);
			this.label13.TabIndex = 21;
			this.label13.Text = "Résultat Image";
			// 
			// pbResIN
			// 
			this.pbResIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbResIN.Location = new System.Drawing.Point(826, 467);
			this.pbResIN.Name = "pbResIN";
			this.pbResIN.Size = new System.Drawing.Size(230, 230);
			this.pbResIN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbResIN.TabIndex = 20;
			this.pbResIN.TabStop = false;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(574, 431);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(66, 13);
			this.label14.TabIndex = 19;
			this.label14.Text = "Vérité terrain";
			// 
			// pBVeriteIn
			// 
			this.pBVeriteIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pBVeriteIn.Location = new System.Drawing.Point(490, 467);
			this.pBVeriteIn.Name = "pBVeriteIn";
			this.pBVeriteIn.Size = new System.Drawing.Size(230, 230);
			this.pBVeriteIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pBVeriteIn.TabIndex = 18;
			this.pBVeriteIn.TabStop = false;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(180, 436);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(111, 13);
			this.label15.TabIndex = 17;
			this.label15.Text = "Image de référence In";
			// 
			// pBImgRefIn
			// 
			this.pBImgRefIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pBImgRefIn.Location = new System.Drawing.Point(119, 467);
			this.pBImgRefIn.Name = "pBImgRefIn";
			this.pBImgRefIn.Size = new System.Drawing.Size(230, 230);
			this.pBImgRefIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pBImgRefIn.TabIndex = 16;
			this.pBImgRefIn.TabStop = false;
			// 
			// TimerAff
			// 
			this.TimerAff.Tick += new System.EventHandler(this.TimerAff_Tick);
			// 
			// TbAff
			// 
			this.TbAff.Location = new System.Drawing.Point(74, 356);
			this.TbAff.Maximum = 1000;
			this.TbAff.Minimum = 10;
			this.TbAff.Name = "TbAff";
			this.TbAff.Size = new System.Drawing.Size(370, 45);
			this.TbAff.TabIndex = 28;
			this.TbAff.Value = 50;
			this.TbAff.ValueChanged += new System.EventHandler(this.TbAff_ValueChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1429, 761);
			this.Controls.Add(this.TbAff);
			this.Controls.Add(this.lbMoyIOUIn);
			this.Controls.Add(this.lbResIOUIn);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.pbResIN);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.pBVeriteIn);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.pBImgRefIn);
			this.Controls.Add(this.lbMoyIOUSc);
			this.Controls.Add(this.lbResIOUSc);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.pbResSc);
			this.Controls.Add(this.btnTraitement);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pBVeriteSc);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pBImgRefSc);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.pBImgRefSc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pBVeriteSc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbResSc)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbResIN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pBVeriteIn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pBImgRefIn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TbAff)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pBImgRefSc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pBVeriteSc;
        private System.Windows.Forms.Button btnTraitement;
        private System.Windows.Forms.PictureBox pbResSc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbResIOUSc;
        private System.Windows.Forms.Label lbMoyIOUSc;
        private System.Windows.Forms.OpenFileDialog ofdImg;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem imageUniqueToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dossierDimageToolStripMenuItem;
		private System.Windows.Forms.FolderBrowserDialog FBDDir;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Label lbMoyIOUIn;
		private System.Windows.Forms.Label lbResIOUIn;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.PictureBox pbResIN;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.PictureBox pBVeriteIn;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.PictureBox pBImgRefIn;
		private System.Windows.Forms.Timer TimerAff;
		private System.Windows.Forms.TrackBar TbAff;
		private System.Windows.Forms.ToolStripMenuItem défilementToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem démarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem arréterToolStripMenuItem;
	}
}

