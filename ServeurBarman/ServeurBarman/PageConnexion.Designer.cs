namespace ServeurBarman
{
    partial class PageConnexion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageConnexion));
            this.TBX_User = new System.Windows.Forms.TextBox();
            this.TBX_Pwd = new System.Windows.Forms.TextBox();
            this.BTN_Logon = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TBX_User
            // 
            this.TBX_User.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBX_User.ForeColor = System.Drawing.Color.Silver;
            this.TBX_User.Location = new System.Drawing.Point(94, 249);
            this.TBX_User.Name = "TBX_User";
            this.TBX_User.Size = new System.Drawing.Size(152, 27);
            this.TBX_User.TabIndex = 1;
            this.TBX_User.Text = "barman";
            // 
            // TBX_Pwd
            // 
            this.TBX_Pwd.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBX_Pwd.ForeColor = System.Drawing.Color.Silver;
            this.TBX_Pwd.Location = new System.Drawing.Point(94, 282);
            this.TBX_Pwd.Name = "TBX_Pwd";
            this.TBX_Pwd.Size = new System.Drawing.Size(152, 27);
            this.TBX_Pwd.TabIndex = 1;
            this.TBX_Pwd.Text = "projet";
            this.TBX_Pwd.UseSystemPasswordChar = true;
            // 
            // BTN_Logon
            // 
            this.BTN_Logon.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Logon.Location = new System.Drawing.Point(94, 324);
            this.BTN_Logon.Name = "BTN_Logon";
            this.BTN_Logon.Size = new System.Drawing.Size(132, 30);
            this.BTN_Logon.TabIndex = 2;
            this.BTN_Logon.Text = "Connexion";
            this.BTN_Logon.UseVisualStyleBackColor = true;
            this.BTN_Logon.TextChanged += new System.EventHandler(this.BTN_Logon_TextChanged);
            this.BTN_Logon.Click += new System.EventHandler(this.BTN_Logon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(119, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bienvenue ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ServeurBarman.Properties.Resources.JELLY_Bar_Logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(94, 145);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 75);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // PageConnexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_Logon);
            this.Controls.Add(this.TBX_Pwd);
            this.Controls.Add(this.TBX_User);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(344, 450);
            this.MinimumSize = new System.Drawing.Size(344, 450);
            this.Name = "PageConnexion";
            this.Text = "Page de connexion";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox TBX_User;
        private System.Windows.Forms.TextBox TBX_Pwd;
        private System.Windows.Forms.Button BTN_Logon;
        private System.Windows.Forms.Label label1;
    }
}

