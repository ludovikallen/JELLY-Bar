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
            this.TBX_User.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBX_User.ForeColor = System.Drawing.Color.Silver;
            this.TBX_User.Location = new System.Drawing.Point(112, 312);
            this.TBX_User.Name = "TBX_User";
            this.TBX_User.Size = new System.Drawing.Size(210, 32);
            this.TBX_User.TabIndex = 1;
            this.TBX_User.Text = "barman";
            // 
            // TBX_Pwd
            // 
            this.TBX_Pwd.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBX_Pwd.ForeColor = System.Drawing.Color.Silver;
            this.TBX_Pwd.Location = new System.Drawing.Point(112, 360);
            this.TBX_Pwd.Name = "TBX_Pwd";
            this.TBX_Pwd.Size = new System.Drawing.Size(210, 32);
            this.TBX_Pwd.TabIndex = 1;
            this.TBX_Pwd.Text = "projet";
            this.TBX_Pwd.UseSystemPasswordChar = true;
            // 
            // BTN_Logon
            // 
            this.BTN_Logon.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Logon.Location = new System.Drawing.Point(146, 427);
            this.BTN_Logon.Name = "BTN_Logon";
            this.BTN_Logon.Size = new System.Drawing.Size(142, 50);
            this.BTN_Logon.TabIndex = 2;
            this.BTN_Logon.Text = "Connexion";
            this.BTN_Logon.UseVisualStyleBackColor = true;
            this.BTN_Logon.TextChanged += new System.EventHandler(this.BTN_Logon_TextChanged);
            this.BTN_Logon.Click += new System.EventHandler(this.BTN_Logon_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(66, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 100);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bienvenue \r\nJELLY-Bar";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ServeurBarman.Properties.Resources.JELLY_Bar_Logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(90, 142);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(252, 151);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // PageConnexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 500);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_Logon);
            this.Controls.Add(this.TBX_Pwd);
            this.Controls.Add(this.TBX_User);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(430, 500);
            this.MinimumSize = new System.Drawing.Size(430, 500);
            this.Name = "PageConnexion";
            this.Load += new System.EventHandler(this.PageConnexion_Load);
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

