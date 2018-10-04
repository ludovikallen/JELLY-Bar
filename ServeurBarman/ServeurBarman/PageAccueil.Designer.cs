namespace ServeurBarman
{
    partial class PageAccueil
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTN_Developers = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.BTN_Setting = new System.Windows.Forms.Button();
            this.BTN_AddCup = new System.Windows.Forms.Button();
            this.BTN_AddDrink = new System.Windows.Forms.Button();
            this.BTN_Welcome = new System.Windows.Forms.Button();
            this.SidePanel = new System.Windows.Forms.Panel();
            this.PBX_Logo = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PBX_EtatDeconnecté = new System.Windows.Forms.PictureBox();
            this.PBX_EtatConnecté = new System.Windows.Forms.PictureBox();
            this.LBX_WaitingList = new System.Windows.Forms.ListBox();
            this.add_Drinks1 = new ServeurBarman.Add_Drinks();
            this.addCups1 = new ServeurBarman.AddCups();
            this.setting1 = new ServeurBarman.Setting();
            this.welcomePage1 = new ServeurBarman.WelcomePage();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBX_Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBX_EtatDeconnecté)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBX_EtatConnecté)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.BTN_Developers);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.BTN_Setting);
            this.panel1.Controls.Add(this.BTN_AddCup);
            this.panel1.Controls.Add(this.BTN_AddDrink);
            this.panel1.Controls.Add(this.BTN_Welcome);
            this.panel1.Controls.Add(this.SidePanel);
            this.panel1.Controls.Add(this.PBX_Logo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 525);
            this.panel1.TabIndex = 0;
            // 
            // BTN_Developers
            // 
            this.BTN_Developers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Developers.FlatAppearance.BorderSize = 0;
            this.BTN_Developers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Developers.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Developers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Developers.Location = new System.Drawing.Point(12, 478);
            this.BTN_Developers.Name = "BTN_Developers";
            this.BTN_Developers.Size = new System.Drawing.Size(31, 35);
            this.BTN_Developers.TabIndex = 2;
            this.BTN_Developers.Text = "?";
            this.BTN_Developers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Developers.UseVisualStyleBackColor = true;
            this.BTN_Developers.Click += new System.EventHandler(this.BTN_Developers_Click);
            // 
            // button6
            // 
            this.button6.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(78, 478);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(126, 35);
            this.button6.TabIndex = 2;
            this.button6.Text = "Deconnexion";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // BTN_Setting
            // 
            this.BTN_Setting.FlatAppearance.BorderSize = 0;
            this.BTN_Setting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Setting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Setting.Location = new System.Drawing.Point(12, 253);
            this.BTN_Setting.Name = "BTN_Setting";
            this.BTN_Setting.Size = new System.Drawing.Size(182, 35);
            this.BTN_Setting.TabIndex = 2;
            this.BTN_Setting.Text = "Paramètre";
            this.BTN_Setting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Setting.UseVisualStyleBackColor = true;
            this.BTN_Setting.Click += new System.EventHandler(this.BTN_Setting_Click);
            // 
            // BTN_AddCup
            // 
            this.BTN_AddCup.FlatAppearance.BorderSize = 0;
            this.BTN_AddCup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_AddCup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_AddCup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_AddCup.Location = new System.Drawing.Point(13, 212);
            this.BTN_AddCup.Name = "BTN_AddCup";
            this.BTN_AddCup.Size = new System.Drawing.Size(181, 35);
            this.BTN_AddCup.TabIndex = 2;
            this.BTN_AddCup.Text = "Ajouter des verres";
            this.BTN_AddCup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_AddCup.UseVisualStyleBackColor = true;
            this.BTN_AddCup.Click += new System.EventHandler(this.BTN_AddCup_Click);
            // 
            // BTN_AddDrink
            // 
            this.BTN_AddDrink.FlatAppearance.BorderSize = 0;
            this.BTN_AddDrink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_AddDrink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_AddDrink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_AddDrink.Location = new System.Drawing.Point(12, 171);
            this.BTN_AddDrink.Name = "BTN_AddDrink";
            this.BTN_AddDrink.Size = new System.Drawing.Size(182, 35);
            this.BTN_AddDrink.TabIndex = 2;
            this.BTN_AddDrink.Text = "Ajouter des drinks";
            this.BTN_AddDrink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_AddDrink.UseVisualStyleBackColor = true;
            this.BTN_AddDrink.Click += new System.EventHandler(this.BTN_AddDrink_Click);
            // 
            // BTN_Welcome
            // 
            this.BTN_Welcome.FlatAppearance.BorderSize = 0;
            this.BTN_Welcome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Welcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Welcome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Welcome.Location = new System.Drawing.Point(13, 130);
            this.BTN_Welcome.Name = "BTN_Welcome";
            this.BTN_Welcome.Size = new System.Drawing.Size(181, 35);
            this.BTN_Welcome.TabIndex = 2;
            this.BTN_Welcome.Text = "Accueil";
            this.BTN_Welcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Welcome.UseVisualStyleBackColor = true;
            this.BTN_Welcome.Click += new System.EventHandler(this.BTN_Welcome_Click);
            // 
            // SidePanel
            // 
            this.SidePanel.BackColor = System.Drawing.Color.Blue;
            this.SidePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SidePanel.Location = new System.Drawing.Point(3, 130);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(10, 35);
            this.SidePanel.TabIndex = 1;
            // 
            // PBX_Logo
            // 
            this.PBX_Logo.BackgroundImage = global::ServeurBarman.Properties.Resources.JELLY_Bar_Logo;
            this.PBX_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PBX_Logo.Location = new System.Drawing.Point(3, 3);
            this.PBX_Logo.Name = "PBX_Logo";
            this.PBX_Logo.Size = new System.Drawing.Size(204, 104);
            this.PBX_Logo.TabIndex = 1;
            this.PBX_Logo.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(210, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(659, 10);
            this.panel3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(217, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Commandes en attente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(432, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Usager connecté :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ServeurBarman.Properties.Resources.emergency;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(769, 162);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 83);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // PBX_EtatDeconnecté
            // 
            this.PBX_EtatDeconnecté.BackgroundImage = global::ServeurBarman.Properties.Resources.point_rouge;
            this.PBX_EtatDeconnecté.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PBX_EtatDeconnecté.Location = new System.Drawing.Point(807, 86);
            this.PBX_EtatDeconnecté.Name = "PBX_EtatDeconnecté";
            this.PBX_EtatDeconnecté.Size = new System.Drawing.Size(56, 50);
            this.PBX_EtatDeconnecté.TabIndex = 3;
            this.PBX_EtatDeconnecté.TabStop = false;
            // 
            // PBX_EtatConnecté
            // 
            this.PBX_EtatConnecté.BackgroundImage = global::ServeurBarman.Properties.Resources.point_vert;
            this.PBX_EtatConnecté.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PBX_EtatConnecté.Location = new System.Drawing.Point(807, 86);
            this.PBX_EtatConnecté.Name = "PBX_EtatConnecté";
            this.PBX_EtatConnecté.Size = new System.Drawing.Size(56, 50);
            this.PBX_EtatConnecté.TabIndex = 3;
            this.PBX_EtatConnecté.TabStop = false;
            // 
            // LBX_WaitingList
            // 
            this.LBX_WaitingList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBX_WaitingList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBX_WaitingList.FormattingEnabled = true;
            this.LBX_WaitingList.ItemHeight = 20;
            this.LBX_WaitingList.Location = new System.Drawing.Point(213, 70);
            this.LBX_WaitingList.Name = "LBX_WaitingList";
            this.LBX_WaitingList.ScrollAlwaysVisible = true;
            this.LBX_WaitingList.Size = new System.Drawing.Size(296, 184);
            this.LBX_WaitingList.Sorted = true;
            this.LBX_WaitingList.TabIndex = 11;
            // 
            // add_Drinks1
            // 
            this.add_Drinks1.Location = new System.Drawing.Point(210, 252);
            this.add_Drinks1.MaximumSize = new System.Drawing.Size(659, 274);
            this.add_Drinks1.MinimumSize = new System.Drawing.Size(659, 274);
            this.add_Drinks1.Name = "add_Drinks1";
            this.add_Drinks1.Size = new System.Drawing.Size(659, 274);
            this.add_Drinks1.TabIndex = 10;
            // 
            // addCups1
            // 
            this.addCups1.Location = new System.Drawing.Point(210, 252);
            this.addCups1.MaximumSize = new System.Drawing.Size(659, 274);
            this.addCups1.MinimumSize = new System.Drawing.Size(659, 274);
            this.addCups1.Name = "addCups1";
            this.addCups1.Size = new System.Drawing.Size(659, 274);
            this.addCups1.TabIndex = 9;
            // 
            // setting1
            // 
            this.setting1.Location = new System.Drawing.Point(210, 252);
            this.setting1.MaximumSize = new System.Drawing.Size(659, 274);
            this.setting1.MinimumSize = new System.Drawing.Size(659, 274);
            this.setting1.Name = "setting1";
            this.setting1.Size = new System.Drawing.Size(659, 274);
            this.setting1.TabIndex = 8;
            // 
            // welcomePage1
            // 
            this.welcomePage1.Location = new System.Drawing.Point(210, 252);
            this.welcomePage1.MaximumSize = new System.Drawing.Size(659, 274);
            this.welcomePage1.MinimumSize = new System.Drawing.Size(659, 274);
            this.welcomePage1.Name = "welcomePage1";
            this.welcomePage1.Size = new System.Drawing.Size(659, 274);
            this.welcomePage1.TabIndex = 7;
            // 
            // PageAccueil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(869, 525);
            this.Controls.Add(this.LBX_WaitingList);
            this.Controls.Add(this.add_Drinks1);
            this.Controls.Add(this.addCups1);
            this.Controls.Add(this.setting1);
            this.Controls.Add(this.welcomePage1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.PBX_EtatDeconnecté);
            this.Controls.Add(this.PBX_EtatConnecté);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "PageAccueil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Accueil";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PageAccueil_FormClosing);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PBX_Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBX_EtatDeconnecté)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBX_EtatConnecté)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTN_Developers;
        private System.Windows.Forms.Button BTN_Setting;
        private System.Windows.Forms.Button BTN_AddCup;
        private System.Windows.Forms.Button BTN_AddDrink;
        private System.Windows.Forms.Button BTN_Welcome;
        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.PictureBox PBX_Logo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox PBX_EtatConnecté;
        private System.Windows.Forms.PictureBox PBX_EtatDeconnecté;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private WelcomePage welcomePage1;
        private Setting setting1;
        private AddCups addCups1;
        private Add_Drinks add_Drinks1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ListBox LBX_WaitingList;
    }
}