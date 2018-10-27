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
            this.Cbx_TypeVerre = new System.Windows.Forms.ComboBox();
            this.PnlShooter = new System.Windows.Forms.PictureBox();
            this.PnlVerreRouge = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.TB_NbVerre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PnlShooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PnlVerreRouge)).BeginInit();
            this.SuspendLayout();
            // 
            // Cbx_TypeVerre
            // 
            this.Cbx_TypeVerre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cbx_TypeVerre.FormattingEnabled = true;
            this.Cbx_TypeVerre.Location = new System.Drawing.Point(195, 47);
            this.Cbx_TypeVerre.Name = "Cbx_TypeVerre";
            this.Cbx_TypeVerre.Size = new System.Drawing.Size(142, 28);
            this.Cbx_TypeVerre.Sorted = true;
            this.Cbx_TypeVerre.TabIndex = 27;
            // 
            // PnlShooter
            // 
            this.PnlShooter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PnlShooter.Location = new System.Drawing.Point(367, 16);
            this.PnlShooter.Name = "PnlShooter";
            this.PnlShooter.Size = new System.Drawing.Size(143, 149);
            this.PnlShooter.TabIndex = 25;
            this.PnlShooter.TabStop = false;
            // 
            // PnlVerreRouge
            // 
            this.PnlVerreRouge.BackgroundImage = global::ServeurBarman.Properties.Resources.red_cup;
            this.PnlVerreRouge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PnlVerreRouge.Location = new System.Drawing.Point(367, 16);
            this.PnlVerreRouge.Name = "PnlVerreRouge";
            this.PnlVerreRouge.Size = new System.Drawing.Size(143, 149);
            this.PnlVerreRouge.TabIndex = 26;
            this.PnlVerreRouge.TabStop = false;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(210, 208);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 37);
            this.button2.TabIndex = 23;
            this.button2.Text = "Valider";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(367, 208);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 37);
            this.button1.TabIndex = 24;
            this.button1.Text = "Annuler";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TB_NbVerre
            // 
            this.TB_NbVerre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_NbVerre.Location = new System.Drawing.Point(195, 97);
            this.TB_NbVerre.Name = "TB_NbVerre";
            this.TB_NbVerre.Size = new System.Drawing.Size(143, 26);
            this.TB_NbVerre.TabIndex = 22;
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
            // AddCups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Cbx_TypeVerre);
            this.Controls.Add(this.PnlShooter);
            this.Controls.Add(this.PnlVerreRouge);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TB_NbVerre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(548, 274);
            this.MinimumSize = new System.Drawing.Size(548, 274);
            this.Name = "AddCups";
            this.Size = new System.Drawing.Size(548, 274);
            ((System.ComponentModel.ISupportInitialize)(this.PnlShooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PnlVerreRouge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Cbx_TypeVerre;
        private System.Windows.Forms.PictureBox PnlShooter;
        private System.Windows.Forms.PictureBox PnlVerreRouge;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TB_NbVerre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
