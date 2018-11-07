namespace ServeurBarman
{
    partial class AddCups
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Valider = new System.Windows.Forms.Button();
            this.btn_Annuler = new System.Windows.Forms.Button();
            this.TB_NbVerre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.erreur = new System.Windows.Forms.Label();
            this.lbShooter = new System.Windows.Forms.Label();
            this.PnlShooter = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PnlShooter)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Valider
            // 
            this.btn_Valider.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Valider.Location = new System.Drawing.Point(210, 208);
            this.btn_Valider.Name = "btn_Valider";
            this.btn_Valider.Size = new System.Drawing.Size(143, 37);
            this.btn_Valider.TabIndex = 23;
            this.btn_Valider.Text = "Valider";
            this.btn_Valider.UseVisualStyleBackColor = true;
            this.btn_Valider.Click += new System.EventHandler(this.btn_Valider_Click);
            // 
            // btn_Annuler
            // 
            this.btn_Annuler.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Annuler.Location = new System.Drawing.Point(367, 208);
            this.btn_Annuler.Name = "btn_Annuler";
            this.btn_Annuler.Size = new System.Drawing.Size(143, 37);
            this.btn_Annuler.TabIndex = 24;
            this.btn_Annuler.Text = "Annuler";
            this.btn_Annuler.UseVisualStyleBackColor = true;
            // 
            // TB_NbVerre
            // 
            this.TB_NbVerre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_NbVerre.Location = new System.Drawing.Point(195, 97);
            this.TB_NbVerre.Name = "TB_NbVerre";
            this.TB_NbVerre.Size = new System.Drawing.Size(143, 26);
            this.TB_NbVerre.TabIndex = 22;
            this.TB_NbVerre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_NbVerre_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "Nombre de verres";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(59, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 18);
            this.label1.TabIndex = 21;
            this.label1.Text = "Type de verres";
            // 
            // erreur
            // 
            this.erreur.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.erreur.ForeColor = System.Drawing.Color.Red;
            this.erreur.Location = new System.Drawing.Point(337, 97);
            this.erreur.Name = "erreur";
            this.erreur.Size = new System.Drawing.Size(24, 26);
            this.erreur.TabIndex = 28;
            this.erreur.Text = "*";
            this.erreur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbShooter
            // 
            this.lbShooter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbShooter.Location = new System.Drawing.Point(195, 46);
            this.lbShooter.Name = "lbShooter";
            this.lbShooter.Size = new System.Drawing.Size(143, 25);
            this.lbShooter.TabIndex = 29;
            this.lbShooter.Text = "label3";
            this.lbShooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlShooter
            // 
            this.PnlShooter.BackgroundImage = global::ServeurBarman.Properties.Resources.shooter;
            this.PnlShooter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnlShooter.Location = new System.Drawing.Point(367, 29);
            this.PnlShooter.Name = "PnlShooter";
            this.PnlShooter.Size = new System.Drawing.Size(143, 149);
            this.PnlShooter.TabIndex = 25;
            this.PnlShooter.TabStop = false;
            // 
            // AddCups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbShooter);
            this.Controls.Add(this.erreur);
            this.Controls.Add(this.PnlShooter);
            this.Controls.Add(this.btn_Valider);
            this.Controls.Add(this.btn_Annuler);
            this.Controls.Add(this.TB_NbVerre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(548, 274);
            this.MinimumSize = new System.Drawing.Size(548, 274);
            this.Name = "AddCups";
            this.Size = new System.Drawing.Size(548, 274);
            this.Load += new System.EventHandler(this.AddCups_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PnlShooter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox PnlShooter;
        private System.Windows.Forms.Button btn_Valider;
        private System.Windows.Forms.Button btn_Annuler;
        private System.Windows.Forms.TextBox TB_NbVerre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label erreur;
        private System.Windows.Forms.Label lbShooter;
    }
}
