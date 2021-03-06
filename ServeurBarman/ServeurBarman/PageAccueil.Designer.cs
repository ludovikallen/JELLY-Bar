﻿namespace ServeurBarman
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageAccueil));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTN_Developers = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.BTN_Setting = new System.Windows.Forms.Button();
            this.BTN_AddCup = new System.Windows.Forms.Button();
            this.BTN_AddDrink = new System.Windows.Forms.Button();
            this.BTN_Welcome = new System.Windows.Forms.Button();
            this.PBX_Logo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LBX_WaitingList = new System.Windows.Forms.ListBox();
            this.pnlBar = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mBtnConnexionRobot = new MetroFramework.Controls.MetroButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PBX_EtatDeconnecté = new System.Windows.Forms.PictureBox();
            this.PBX_EtatConnecté = new System.Windows.Forms.PictureBox();
            this.add_Drinks1 = new ServeurBarman.Add_Drinks();
            this.addCups1 = new ServeurBarman.AddCups();
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
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.BTN_Developers);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.BTN_Setting);
            this.panel1.Controls.Add(this.BTN_AddCup);
            this.panel1.Controls.Add(this.BTN_AddDrink);
            this.panel1.Controls.Add(this.BTN_Welcome);
            this.panel1.Controls.Add(this.PBX_Logo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 527);
            this.panel1.TabIndex = 0;
            // 
            // BTN_Developers
            // 
            this.BTN_Developers.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BTN_Developers.FlatAppearance.BorderSize = 0;
            this.BTN_Developers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Developers.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Developers.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BTN_Developers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Developers.Location = new System.Drawing.Point(13, 487);
            this.BTN_Developers.Name = "BTN_Developers";
            this.BTN_Developers.Size = new System.Drawing.Size(67, 37);
            this.BTN_Developers.TabIndex = 2;
            this.BTN_Developers.Text = "?";
            this.BTN_Developers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Developers.UseVisualStyleBackColor = true;
            this.BTN_Developers.Click += new System.EventHandler(this.BTN_Developers_Click);
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button6.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(99, 488);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(126, 35);
            this.button6.TabIndex = 2;
            this.button6.Text = "Deconnexion";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // BTN_Setting
            // 
            this.BTN_Setting.FlatAppearance.BorderSize = 0;
            this.BTN_Setting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Setting.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BTN_Setting.Image = global::ServeurBarman.Properties.Resources.if_setting_18141171;
            this.BTN_Setting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Setting.Location = new System.Drawing.Point(3, 291);
            this.BTN_Setting.Name = "BTN_Setting";
            this.BTN_Setting.Size = new System.Drawing.Size(182, 35);
            this.BTN_Setting.TabIndex = 2;
            this.BTN_Setting.Text = "       Paramètre";
            this.BTN_Setting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Setting.UseVisualStyleBackColor = true;
            this.BTN_Setting.Click += new System.EventHandler(this.BTN_Setting_Click);
            // 
            // BTN_AddCup
            // 
            this.BTN_AddCup.FlatAppearance.BorderSize = 0;
            this.BTN_AddCup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_AddCup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_AddCup.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BTN_AddCup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_AddCup.Location = new System.Drawing.Point(3, 251);
            this.BTN_AddCup.Name = "BTN_AddCup";
            this.BTN_AddCup.Size = new System.Drawing.Size(214, 35);
            this.BTN_AddCup.TabIndex = 2;
            this.BTN_AddCup.Text = "       Ajouter des verres";
            this.BTN_AddCup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_AddCup.UseVisualStyleBackColor = true;
            this.BTN_AddCup.Click += new System.EventHandler(this.BTN_AddCup_Click);
            // 
            // BTN_AddDrink
            // 
            this.BTN_AddDrink.FlatAppearance.BorderSize = 0;
            this.BTN_AddDrink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_AddDrink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_AddDrink.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BTN_AddDrink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_AddDrink.Location = new System.Drawing.Point(2, 210);
            this.BTN_AddDrink.Name = "BTN_AddDrink";
            this.BTN_AddDrink.Size = new System.Drawing.Size(215, 35);
            this.BTN_AddDrink.TabIndex = 2;
            this.BTN_AddDrink.Text = "       Ajouter des drinks";
            this.BTN_AddDrink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_AddDrink.UseVisualStyleBackColor = true;
            this.BTN_AddDrink.Click += new System.EventHandler(this.BTN_AddDrink_Click);
            // 
            // BTN_Welcome
            // 
            this.BTN_Welcome.BackgroundImage = global::ServeurBarman.Properties.Resources._2000px_Go_home_svg;
            this.BTN_Welcome.FlatAppearance.BorderSize = 0;
            this.BTN_Welcome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Welcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Welcome.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BTN_Welcome.Image = global::ServeurBarman.Properties.Resources.if_go_home_1187701;
            this.BTN_Welcome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Welcome.Location = new System.Drawing.Point(3, 169);
            this.BTN_Welcome.Name = "BTN_Welcome";
            this.BTN_Welcome.Size = new System.Drawing.Size(181, 35);
            this.BTN_Welcome.TabIndex = 2;
            this.BTN_Welcome.Text = "      Accueil";
            this.BTN_Welcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Welcome.UseVisualStyleBackColor = true;
            this.BTN_Welcome.Click += new System.EventHandler(this.BTN_Welcome_Click);
            // 
            // PBX_Logo
            // 
            this.PBX_Logo.BackgroundImage = global::ServeurBarman.Properties.Resources.JELLY_Bar_Logo;
            this.PBX_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PBX_Logo.Location = new System.Drawing.Point(0, 0);
            this.PBX_Logo.Name = "PBX_Logo";
            this.PBX_Logo.Size = new System.Drawing.Size(246, 153);
            this.PBX_Logo.TabIndex = 1;
            this.PBX_Logo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(272, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Commandes en attente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(634, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Usager connecté :";
            // 
            // LBX_WaitingList
            // 
            this.LBX_WaitingList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBX_WaitingList.FormattingEnabled = true;
            this.LBX_WaitingList.ItemHeight = 20;
            this.LBX_WaitingList.Location = new System.Drawing.Point(272, 120);
            this.LBX_WaitingList.Name = "LBX_WaitingList";
            this.LBX_WaitingList.ScrollAlwaysVisible = true;
            this.LBX_WaitingList.Size = new System.Drawing.Size(230, 164);
            this.LBX_WaitingList.TabIndex = 11;
            // 
            // pnlBar
            // 
            this.pnlBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBar.BackColor = System.Drawing.Color.Black;
            this.pnlBar.Location = new System.Drawing.Point(247, 290);
            this.pnlBar.Name = "pnlBar";
            this.pnlBar.Size = new System.Drawing.Size(726, 10);
            this.pnlBar.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Gold;
            this.panel2.Location = new System.Drawing.Point(266, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(709, 10);
            this.panel2.TabIndex = 12;
            // 
            // mBtnConnexionRobot
            // 
            this.mBtnConnexionRobot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mBtnConnexionRobot.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mBtnConnexionRobot.Location = new System.Drawing.Point(779, 140);
            this.mBtnConnexionRobot.Name = "mBtnConnexionRobot";
            this.mBtnConnexionRobot.Size = new System.Drawing.Size(194, 55);
            this.mBtnConnexionRobot.TabIndex = 13;
            this.mBtnConnexionRobot.Text = "Connexion Robot";
            this.mBtnConnexionRobot.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.mBtnConnexionRobot.UseSelectable = true;
            this.mBtnConnexionRobot.Click += new System.EventHandler(this.mBtnConnexionRobot_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::ServeurBarman.Properties.Resources.stop1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(779, 201);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 83);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // PBX_EtatDeconnecté
            // 
            this.PBX_EtatDeconnecté.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PBX_EtatDeconnecté.BackgroundImage = global::ServeurBarman.Properties.Resources.point_rouge;
            this.PBX_EtatDeconnecté.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PBX_EtatDeconnecté.Location = new System.Drawing.Point(879, 201);
            this.PBX_EtatDeconnecté.Name = "PBX_EtatDeconnecté";
            this.PBX_EtatDeconnecté.Size = new System.Drawing.Size(94, 83);
            this.PBX_EtatDeconnecté.TabIndex = 3;
            this.PBX_EtatDeconnecté.TabStop = false;
            // 
            // PBX_EtatConnecté
            // 
            this.PBX_EtatConnecté.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PBX_EtatConnecté.BackgroundImage = global::ServeurBarman.Properties.Resources.point_vert;
            this.PBX_EtatConnecté.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PBX_EtatConnecté.Location = new System.Drawing.Point(879, 201);
            this.PBX_EtatConnecté.Name = "PBX_EtatConnecté";
            this.PBX_EtatConnecté.Size = new System.Drawing.Size(94, 83);
            this.PBX_EtatConnecté.TabIndex = 3;
            this.PBX_EtatConnecté.TabStop = false;
            // 
            // add_Drinks1
            // 
            this.add_Drinks1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.add_Drinks1.Location = new System.Drawing.Point(294, 313);
            this.add_Drinks1.MaximumSize = new System.Drawing.Size(659, 274);
            this.add_Drinks1.MinimumSize = new System.Drawing.Size(659, 274);
            this.add_Drinks1.Name = "add_Drinks1";
            this.add_Drinks1.Size = new System.Drawing.Size(659, 274);
            this.add_Drinks1.TabIndex = 10;
            // 
            // addCups1
            // 
            this.addCups1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addCups1.Location = new System.Drawing.Point(294, 313);
            this.addCups1.MaximumSize = new System.Drawing.Size(659, 274);
            this.addCups1.MinimumSize = new System.Drawing.Size(659, 274);
            this.addCups1.Name = "addCups1";
            this.addCups1.Size = new System.Drawing.Size(659, 274);
            this.addCups1.TabIndex = 9;
            // 
            // welcomePage1
            // 
            this.welcomePage1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.welcomePage1.Location = new System.Drawing.Point(294, 313);
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
            this.ClientSize = new System.Drawing.Size(973, 607);
            this.Controls.Add(this.mBtnConnexionRobot);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlBar);
            this.Controls.Add(this.LBX_WaitingList);
            this.Controls.Add(this.add_Drinks1);
            this.Controls.Add(this.addCups1);
            this.Controls.Add(this.welcomePage1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.PBX_EtatDeconnecté);
            this.Controls.Add(this.PBX_EtatConnecté);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PageAccueil";
            this.Text = "    Accueil";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PageAccueil_FormClosing);
            this.Load += new System.EventHandler(this.PageAccueil_Load);
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
        private System.Windows.Forms.PictureBox PBX_Logo;
        private System.Windows.Forms.PictureBox PBX_EtatConnecté;
        private System.Windows.Forms.PictureBox PBX_EtatDeconnecté;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private WelcomePage welcomePage1;
        private AddCups addCups1;
        private Add_Drinks add_Drinks1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ListBox LBX_WaitingList;
        private System.Windows.Forms.Panel pnlBar;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroButton mBtnConnexionRobot;
    }
}