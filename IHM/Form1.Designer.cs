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
            this.pBImgRef = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pBVerite = new System.Windows.Forms.PictureBox();
            this.btnTraitement = new System.Windows.Forms.Button();
            this.pbRes = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbResIOU = new System.Windows.Forms.Label();
            this.lbResHaus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbMoyenne = new System.Windows.Forms.Label();
            this.btImg = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ofdImg = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pBImgRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBVerite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRes)).BeginInit();
            this.SuspendLayout();
            // 
            // pBImgRef
            // 
            this.pBImgRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBImgRef.Location = new System.Drawing.Point(12, 68);
            this.pBImgRef.Name = "pBImgRef";
            this.pBImgRef.Size = new System.Drawing.Size(300, 280);
            this.pBImgRef.TabIndex = 0;
            this.pBImgRef.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Image de référence";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 438);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vérité terrain";
            // 
            // pBVerite
            // 
            this.pBVerite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBVerite.Location = new System.Drawing.Point(12, 469);
            this.pBVerite.Name = "pBVerite";
            this.pBVerite.Size = new System.Drawing.Size(300, 280);
            this.pBVerite.TabIndex = 2;
            this.pBVerite.TabStop = false;
            // 
            // btnTraitement
            // 
            this.btnTraitement.Location = new System.Drawing.Point(348, 396);
            this.btnTraitement.Name = "btnTraitement";
            this.btnTraitement.Size = new System.Drawing.Size(84, 31);
            this.btnTraitement.TabIndex = 4;
            this.btnTraitement.Text = "Traitement";
            this.btnTraitement.UseVisualStyleBackColor = true;
            this.btnTraitement.Click += new System.EventHandler(this.btnTraitement_Click);
            // 
            // pbRes
            // 
            this.pbRes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbRes.Location = new System.Drawing.Point(490, 68);
            this.pbRes.Name = "pbRes";
            this.pbRes.Size = new System.Drawing.Size(300, 280);
            this.pbRes.TabIndex = 5;
            this.pbRes.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(618, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Résultat Image";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(487, 570);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Résultat IOU";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(487, 621);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Résultat distance de Hausdorff";
            // 
            // lbResIOU
            // 
            this.lbResIOU.AutoSize = true;
            this.lbResIOU.Location = new System.Drawing.Point(696, 570);
            this.lbResIOU.Name = "lbResIOU";
            this.lbResIOU.Size = new System.Drawing.Size(0, 13);
            this.lbResIOU.TabIndex = 9;
            // 
            // lbResHaus
            // 
            this.lbResHaus.AutoSize = true;
            this.lbResHaus.Location = new System.Drawing.Point(696, 621);
            this.lbResHaus.Name = "lbResHaus";
            this.lbResHaus.Size = new System.Drawing.Size(0, 13);
            this.lbResHaus.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(487, 678);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Moyenne";
            // 
            // lbMoyenne
            // 
            this.lbMoyenne.AutoSize = true;
            this.lbMoyenne.Location = new System.Drawing.Point(696, 678);
            this.lbMoyenne.Name = "lbMoyenne";
            this.lbMoyenne.Size = new System.Drawing.Size(0, 13);
            this.lbMoyenne.TabIndex = 12;
            // 
            // btImg
            // 
            this.btImg.Location = new System.Drawing.Point(35, 396);
            this.btImg.Name = "btImg";
            this.btImg.Size = new System.Drawing.Size(87, 31);
            this.btImg.TabIndex = 13;
            this.btImg.Text = "Ouverture";
            this.btImg.UseVisualStyleBackColor = true;
            this.btImg.Click += new System.EventHandler(this.btImg_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(370, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(8, 8);
            this.button2.TabIndex = 14;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ofdImg
            // 
            this.ofdImg.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 761);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btImg);
            this.Controls.Add(this.lbMoyenne);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbResHaus);
            this.Controls.Add(this.lbResIOU);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbRes);
            this.Controls.Add(this.btnTraitement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pBVerite);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pBImgRef);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pBImgRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBVerite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pBImgRef;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pBVerite;
        private System.Windows.Forms.Button btnTraitement;
        private System.Windows.Forms.PictureBox pbRes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbResIOU;
        private System.Windows.Forms.Label lbResHaus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbMoyenne;
        private System.Windows.Forms.Button btImg;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog ofdImg;
    }
}

