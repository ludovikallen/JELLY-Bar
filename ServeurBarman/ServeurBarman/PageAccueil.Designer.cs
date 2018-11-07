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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageAccueil));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTN_Developers = new System.Windows.Forms.Button();
            this.deconnexion = new System.Windows.Forms.Button();
            this.BTN_Setting = new System.Windows.Forms.Button();
            this.BTN_AddCup = new System.Windows.Forms.Button();
            this.BTN_Welcome = new System.Windows.Forms.Button();
            this.PBX_Logo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LBX_WaitingList = new System.Windows.Forms.ListBox();
            this.pnlBar = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mBtnConnexionRobot = new MetroFramework.Controls.MetroButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbFinishiCommande = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_CommandeEnCours = new System.Windows.Forms.Label();
            this.pbx_Halt = new System.Windows.Forms.PictureBox();
            this.Btn_ResetCommande = new System.Windows.Forms.Button();
            this.pnlDonnees = new System.Windows.Forms.Panel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btn_Servir = new System.Windows.Forms.Button();
            this.addCups1 = new ServeurBarman.AddCups();
            this.welcomePage1 = new ServeurBarman.WelcomePage();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBX_Logo)).BeginInit();
            this.pnlBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Halt)).BeginInit();
            this.pnlDonnees.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.BTN_Developers);
            this.panel1.Controls.Add(this.deconnexion);
            this.panel1.Controls.Add(this.BTN_Setting);
            this.panel1.Controls.Add(this.BTN_AddCup);
            this.panel1.Controls.Add(this.BTN_Welcome);
            this.panel1.Controls.Add(this.PBX_Logo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 611);
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
            this.BTN_Developers.Location = new System.Drawing.Point(25, 571);
            this.BTN_Developers.Name = "BTN_Developers";
            this.BTN_Developers.Size = new System.Drawing.Size(36, 37);
            this.BTN_Developers.TabIndex = 2;
            this.BTN_Developers.Text = "?";
            this.BTN_Developers.UseVisualStyleBackColor = true;
            this.BTN_Developers.Click += new System.EventHandler(this.BTN_Developers_Click);
            // 
            // deconnexion
            // 
            this.deconnexion.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.deconnexion.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.deconnexion.FlatAppearance.BorderSize = 0;
            this.deconnexion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deconnexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deconnexion.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.deconnexion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.deconnexion.Location = new System.Drawing.Point(111, 572);
            this.deconnexion.Name = "deconnexion";
            this.deconnexion.Size = new System.Drawing.Size(126, 35);
            this.deconnexion.TabIndex = 2;
            this.deconnexion.Text = "Deconnexion";
            this.deconnexion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.deconnexion.UseVisualStyleBackColor = true;
            this.deconnexion.Click += new System.EventHandler(this.deconnexion_Click);
            // 
            // BTN_Setting
            // 
            this.BTN_Setting.FlatAppearance.BorderSize = 0;
            this.BTN_Setting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Setting.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BTN_Setting.Image = global::ServeurBarman.Properties.Resources.if_setting_18141171;
            this.BTN_Setting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_Setting.Location = new System.Drawing.Point(2, 338);
            this.BTN_Setting.Name = "BTN_Setting";
            this.BTN_Setting.Size = new System.Drawing.Size(182, 35);
            this.BTN_Setting.TabIndex = 2;
            this.BTN_Setting.Text = "       Paramètres";
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
            this.BTN_AddCup.Image = global::ServeurBarman.Properties.Resources.Google_Noto_Emoji_Food_Drink_32443_cup_with_straw;
            this.BTN_AddCup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_AddCup.Location = new System.Drawing.Point(2, 272);
            this.BTN_AddCup.Name = "BTN_AddCup";
            this.BTN_AddCup.Size = new System.Drawing.Size(260, 35);
            this.BTN_AddCup.TabIndex = 2;
            this.BTN_AddCup.Text = "       Ajouter verres à shooter";
            this.BTN_AddCup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_AddCup.UseVisualStyleBackColor = true;
            this.BTN_AddCup.Click += new System.EventHandler(this.BTN_AddCup_Click);
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
            this.BTN_Welcome.Location = new System.Drawing.Point(2, 206);
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
            this.PBX_Logo.Size = new System.Drawing.Size(270, 175);
            this.PBX_Logo.TabIndex = 1;
            this.PBX_Logo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(296, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Commandes en attente";
            // 
            // LBX_WaitingList
            // 
            this.LBX_WaitingList.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBX_WaitingList.FormattingEnabled = true;
            this.LBX_WaitingList.ItemHeight = 25;
            this.LBX_WaitingList.Location = new System.Drawing.Point(296, 122);
            this.LBX_WaitingList.Name = "LBX_WaitingList";
            this.LBX_WaitingList.ScrollAlwaysVisible = true;
            this.LBX_WaitingList.Size = new System.Drawing.Size(247, 204);
            this.LBX_WaitingList.TabIndex = 11;
            // 
            // pnlBar
            // 
            this.pnlBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBar.BackColor = System.Drawing.Color.Black;
            this.pnlBar.Controls.Add(this.panel4);
            this.pnlBar.Location = new System.Drawing.Point(288, 332);
            this.pnlBar.Name = "pnlBar";
            this.pnlBar.Size = new System.Drawing.Size(954, 10);
            this.pnlBar.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(123, 44);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(132, 238);
            this.panel4.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Gold;
            this.panel2.Location = new System.Drawing.Point(290, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(878, 10);
            this.panel2.TabIndex = 12;
            // 
            // mBtnConnexionRobot
            // 
            this.mBtnConnexionRobot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mBtnConnexionRobot.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mBtnConnexionRobot.Location = new System.Drawing.Point(786, 120);
            this.mBtnConnexionRobot.Name = "mBtnConnexionRobot";
            this.mBtnConnexionRobot.Size = new System.Drawing.Size(194, 78);
            this.mBtnConnexionRobot.TabIndex = 13;
            this.mBtnConnexionRobot.Text = "Connexion Robot";
            this.mBtnConnexionRobot.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.mBtnConnexionRobot.UseSelectable = true;
            this.mBtnConnexionRobot.Click += new System.EventHandler(this.mBtnConnexionRobot_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbFinishiCommande
            // 
            this.lbFinishiCommande.Location = new System.Drawing.Point(18, 8);
            this.lbFinishiCommande.Name = "lbFinishiCommande";
            this.lbFinishiCommande.Size = new System.Drawing.Size(695, 40);
            this.lbFinishiCommande.TabIndex = 15;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Location = new System.Drawing.Point(287, 601);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(323, 10);
            this.panel5.TabIndex = 20;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(600, 332);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 276);
            this.panel3.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(330, 384);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(223, 24);
            this.label3.TabIndex = 22;
            this.label3.Text = "Commande en cours...";
            // 
            // lb_CommandeEnCours
            // 
            this.lb_CommandeEnCours.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_CommandeEnCours.Location = new System.Drawing.Point(334, 418);
            this.lb_CommandeEnCours.Name = "lb_CommandeEnCours";
            this.lb_CommandeEnCours.Size = new System.Drawing.Size(210, 122);
            this.lb_CommandeEnCours.TabIndex = 23;
            this.lb_CommandeEnCours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbx_Halt
            // 
            this.pbx_Halt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbx_Halt.BackgroundImage = global::ServeurBarman.Properties.Resources.stop1;
            this.pbx_Halt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbx_Halt.Location = new System.Drawing.Point(1065, 241);
            this.pbx_Halt.Name = "pbx_Halt";
            this.pbx_Halt.Size = new System.Drawing.Size(94, 83);
            this.pbx_Halt.TabIndex = 3;
            this.pbx_Halt.TabStop = false;
            this.pbx_Halt.Click += new System.EventHandler(this.pbx_Halt_Click);
            // 
            // Btn_ResetCommande
            // 
            this.Btn_ResetCommande.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_ResetCommande.Location = new System.Drawing.Point(549, 277);
            this.Btn_ResetCommande.Name = "Btn_ResetCommande";
            this.Btn_ResetCommande.Size = new System.Drawing.Size(140, 49);
            this.Btn_ResetCommande.TabIndex = 24;
            this.Btn_ResetCommande.Text = "Reset liste commande";
            this.Btn_ResetCommande.UseVisualStyleBackColor = true;
            this.Btn_ResetCommande.Click += new System.EventHandler(this.Btn_ResetCommande_Click);
            // 
            // pnlDonnees
            // 
            this.pnlDonnees.Controls.Add(this.lbFinishiCommande);
            this.pnlDonnees.Location = new System.Drawing.Point(296, 614);
            this.pnlDonnees.Name = "pnlDonnees";
            this.pnlDonnees.Size = new System.Drawing.Size(861, 57);
            this.pnlDonnees.TabIndex = 26;
            // 
            // timer2
            // 
            this.timer2.Interval = 1;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btn_Servir
            // 
            this.btn_Servir.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Servir.Location = new System.Drawing.Point(786, 207);
            this.btn_Servir.Name = "btn_Servir";
            this.btn_Servir.Size = new System.Drawing.Size(194, 69);
            this.btn_Servir.TabIndex = 27;
            this.btn_Servir.Text = "Servir";
            this.btn_Servir.UseVisualStyleBackColor = true;
            this.btn_Servir.Click += new System.EventHandler(this.btn_Servir_Click);
            // 
            // addCups1
            // 
            this.addCups1.Location = new System.Drawing.Point(616, 345);
            this.addCups1.MaximumSize = new System.Drawing.Size(548, 274);
            this.addCups1.MinimumSize = new System.Drawing.Size(548, 274);
            this.addCups1.Name = "addCups1";
            this.addCups1.Size = new System.Drawing.Size(548, 274);
            this.addCups1.TabIndex = 18;
            // 
            // welcomePage1
            // 
            this.welcomePage1.Location = new System.Drawing.Point(616, 345);
            this.welcomePage1.MaximumSize = new System.Drawing.Size(548, 274);
            this.welcomePage1.MinimumSize = new System.Drawing.Size(548, 274);
            this.welcomePage1.Name = "welcomePage1";
            this.welcomePage1.Size = new System.Drawing.Size(548, 274);
            this.welcomePage1.TabIndex = 16;
            // 
            // PageAccueil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 691);
            this.Controls.Add(this.btn_Servir);
            this.Controls.Add(this.pnlDonnees);
            this.Controls.Add(this.Btn_ResetCommande);
            this.Controls.Add(this.lb_CommandeEnCours);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.addCups1);
            this.Controls.Add(this.welcomePage1);
            this.Controls.Add(this.mBtnConnexionRobot);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlBar);
            this.Controls.Add(this.LBX_WaitingList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbx_Halt);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PageAccueil";
            this.Text = "    Accueil";
            this.Load += new System.EventHandler(this.PageAccueil_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PBX_Logo)).EndInit();
            this.pnlBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Halt)).EndInit();
            this.pnlDonnees.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTN_Developers;
        private System.Windows.Forms.Button BTN_Setting;
        private System.Windows.Forms.Button BTN_AddCup;
        private System.Windows.Forms.Button BTN_Welcome;
        private System.Windows.Forms.PictureBox PBX_Logo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbx_Halt;
        private System.Windows.Forms.Button deconnexion;
        private System.Windows.Forms.ListBox LBX_WaitingList;
        private System.Windows.Forms.Panel pnlBar;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroButton mBtnConnexionRobot;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbFinishiCommande;
        private WelcomePage welcomePage1;
        private AddCups addCups1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_CommandeEnCours;
        private System.Windows.Forms.Button Btn_ResetCommande;
        private System.Windows.Forms.Panel pnlDonnees;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btn_Servir;
    }
}