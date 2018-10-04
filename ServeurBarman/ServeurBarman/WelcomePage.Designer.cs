namespace ServeurBarman
{
    partial class WelcomePage
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
            this.label1 = new System.Windows.Forms.Label();
            this.TBX_NombreClient = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TbxNombreBouteille = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TbxNombreVerres = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre de client en ligne";
            // 
            // TBX_NombreClient
            // 
            this.TBX_NombreClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBX_NombreClient.Location = new System.Drawing.Point(204, 17);
            this.TBX_NombreClient.Name = "TBX_NombreClient";
            this.TBX_NombreClient.Size = new System.Drawing.Size(64, 26);
            this.TBX_NombreClient.TabIndex = 1;
            this.TBX_NombreClient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TBX_NombreClient.TextChanged += new System.EventHandler(this.TBX_NombreClient_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nombre de bouteilles";
            // 
            // TbxNombreBouteille
            // 
            this.TbxNombreBouteille.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TbxNombreBouteille.Location = new System.Drawing.Point(204, 76);
            this.TbxNombreBouteille.Name = "TbxNombreBouteille";
            this.TbxNombreBouteille.Size = new System.Drawing.Size(64, 26);
            this.TbxNombreBouteille.TabIndex = 1;
            this.TbxNombreBouteille.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(66, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nombre de verres";
            // 
            // TbxNombreVerres
            // 
            this.TbxNombreVerres.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TbxNombreVerres.Location = new System.Drawing.Point(204, 130);
            this.TbxNombreVerres.Name = "TbxNombreVerres";
            this.TbxNombreVerres.Size = new System.Drawing.Size(64, 26);
            this.TbxNombreVerres.TabIndex = 1;
            this.TbxNombreVerres.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(413, 42);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(243, 225);
            this.listBox1.TabIndex = 2;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(413, 17);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(93, 19);
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "Activités robot";
            // 
            // WelcomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.TbxNombreVerres);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TbxNombreBouteille);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TBX_NombreClient);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(659, 274);
            this.MinimumSize = new System.Drawing.Size(659, 274);
            this.Name = "WelcomePage";
            this.Size = new System.Drawing.Size(659, 274);
            this.Load += new System.EventHandler(this.WelcomePage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TBX_NombreClient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbxNombreBouteille;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbxNombreVerres;
        private System.Windows.Forms.ListBox listBox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
    }
}
