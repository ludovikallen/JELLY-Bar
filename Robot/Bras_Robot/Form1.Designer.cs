namespace Bras_Robot
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
            this.BTN_HALT = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TB_BASE_ANGLE = new System.Windows.Forms.NumericUpDown();
            this.BTN_BASE_GAUCHE = new System.Windows.Forms.Button();
            this.BTN_BASE_DROITE = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TB_POIGNET_ANGLE = new System.Windows.Forms.NumericUpDown();
            this.BTN_POIGNET_ROTATION_GAUCHE = new System.Windows.Forms.Button();
            this.BTN_POIGNET_ROTATION_DROITE = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TB_EPAULE_ANGLE = new System.Windows.Forms.NumericUpDown();
            this.BTN_EPAULE_GAUCHE = new System.Windows.Forms.Button();
            this.BTN_EPAULE_DROITE = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TB_MAIN_ANGLE = new System.Windows.Forms.NumericUpDown();
            this.BTN_MAIN_GAUCHE = new System.Windows.Forms.Button();
            this.BTN_MAIN_DROITE = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TB_COUDE_ANGLE = new System.Windows.Forms.NumericUpDown();
            this.BTN_COUDE_GAUCHE = new System.Windows.Forms.Button();
            this.BTN_COUDE_DROITE = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.TB_PINCE_SPEED = new System.Windows.Forms.TrackBar();
            this.BTN_PINCE_CLOSE = new System.Windows.Forms.Button();
            this.BTN_PINCE_OPEN = new System.Windows.Forms.Button();
            this.BTN_CONNEXION = new System.Windows.Forms.Button();
            this.BTN_HOME = new System.Windows.Forms.Button();
            this.BTN_READY = new System.Windows.Forms.Button();
            this.BTN_RECORD = new System.Windows.Forms.Button();
            this.BTN_SAVE = new System.Windows.Forms.Button();
            this.LB_COMMAND = new System.Windows.Forms.ListBox();
            this.TB_COMMAND = new System.Windows.Forms.TextBox();
            this.BTN_PLAY = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NUD_SPEED = new System.Windows.Forms.NumericUpDown();
            this.BTN_Ok = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.TB_CONSOLE = new System.Windows.Forms.TextBox();
            this.LB_CONSOLE = new System.Windows.Forms.ListBox();
            this.BTN_CONSOLE = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.BTN_POS_EXECUTE = new System.Windows.Forms.Button();
            this.TB_POS = new System.Windows.Forms.TextBox();
            this.LB_POS = new System.Windows.Forms.ListBox();
            this.BTN_POS_SAVE = new System.Windows.Forms.Button();
            this.BTN_DECONNEXION = new System.Windows.Forms.Button();
            this.BTN_Test = new System.Windows.Forms.Button();
            this.BTN_Reset = new System.Windows.Forms.Button();
            this.TB_Z = new System.Windows.Forms.TextBox();
            this.TB_Y = new System.Windows.Forms.TextBox();
            this.TB_X = new System.Windows.Forms.TextBox();
            this.BTN_Pos = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TB_BASE_ANGLE)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TB_POIGNET_ANGLE)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TB_EPAULE_ANGLE)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TB_MAIN_ANGLE)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TB_COUDE_ANGLE)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TB_PINCE_SPEED)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_SPEED)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN_HALT
            // 
            this.BTN_HALT.BackColor = System.Drawing.Color.Red;
            this.BTN_HALT.Font = new System.Drawing.Font("Wide Latin", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_HALT.ForeColor = System.Drawing.Color.Black;
            this.BTN_HALT.Location = new System.Drawing.Point(257, 355);
            this.BTN_HALT.Name = "BTN_HALT";
            this.BTN_HALT.Size = new System.Drawing.Size(207, 118);
            this.BTN_HALT.TabIndex = 10;
            this.BTN_HALT.Text = "HALT";
            this.BTN_HALT.UseVisualStyleBackColor = false;
            this.BTN_HALT.Click += new System.EventHandler(this.BTN_HALT_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TB_BASE_ANGLE);
            this.groupBox1.Controls.Add(this.BTN_BASE_GAUCHE);
            this.groupBox1.Controls.Add(this.BTN_BASE_DROITE);
            this.groupBox1.Location = new System.Drawing.Point(37, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BASE";
            // 
            // TB_BASE_ANGLE
            // 
            this.TB_BASE_ANGLE.Location = new System.Drawing.Point(16, 60);
            this.TB_BASE_ANGLE.Maximum = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.TB_BASE_ANGLE.Name = "TB_BASE_ANGLE";
            this.TB_BASE_ANGLE.Size = new System.Drawing.Size(75, 20);
            this.TB_BASE_ANGLE.TabIndex = 2;
            this.TB_BASE_ANGLE.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // BTN_BASE_GAUCHE
            // 
            this.BTN_BASE_GAUCHE.Location = new System.Drawing.Point(16, 19);
            this.BTN_BASE_GAUCHE.Name = "BTN_BASE_GAUCHE";
            this.BTN_BASE_GAUCHE.Size = new System.Drawing.Size(75, 23);
            this.BTN_BASE_GAUCHE.TabIndex = 1;
            this.BTN_BASE_GAUCHE.Text = "GAUCHE";
            this.BTN_BASE_GAUCHE.UseVisualStyleBackColor = true;
            this.BTN_BASE_GAUCHE.Click += new System.EventHandler(this.BTN_BASE_GAUCHE_Click);
            // 
            // BTN_BASE_DROITE
            // 
            this.BTN_BASE_DROITE.Location = new System.Drawing.Point(108, 19);
            this.BTN_BASE_DROITE.Name = "BTN_BASE_DROITE";
            this.BTN_BASE_DROITE.Size = new System.Drawing.Size(75, 23);
            this.BTN_BASE_DROITE.TabIndex = 0;
            this.BTN_BASE_DROITE.Text = "DROITE";
            this.BTN_BASE_DROITE.UseVisualStyleBackColor = true;
            this.BTN_BASE_DROITE.Click += new System.EventHandler(this.BTN_BASE_DROITE_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TB_POIGNET_ANGLE);
            this.groupBox2.Controls.Add(this.BTN_POIGNET_ROTATION_GAUCHE);
            this.groupBox2.Controls.Add(this.BTN_POIGNET_ROTATION_DROITE);
            this.groupBox2.Location = new System.Drawing.Point(261, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(203, 104);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "POIGNET ROTATION";
            // 
            // TB_POIGNET_ANGLE
            // 
            this.TB_POIGNET_ANGLE.Location = new System.Drawing.Point(16, 60);
            this.TB_POIGNET_ANGLE.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.TB_POIGNET_ANGLE.Name = "TB_POIGNET_ANGLE";
            this.TB_POIGNET_ANGLE.Size = new System.Drawing.Size(75, 20);
            this.TB_POIGNET_ANGLE.TabIndex = 3;
            this.TB_POIGNET_ANGLE.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // BTN_POIGNET_ROTATION_GAUCHE
            // 
            this.BTN_POIGNET_ROTATION_GAUCHE.Location = new System.Drawing.Point(16, 19);
            this.BTN_POIGNET_ROTATION_GAUCHE.Name = "BTN_POIGNET_ROTATION_GAUCHE";
            this.BTN_POIGNET_ROTATION_GAUCHE.Size = new System.Drawing.Size(75, 23);
            this.BTN_POIGNET_ROTATION_GAUCHE.TabIndex = 1;
            this.BTN_POIGNET_ROTATION_GAUCHE.Text = "GAUCHE";
            this.BTN_POIGNET_ROTATION_GAUCHE.UseVisualStyleBackColor = true;
            this.BTN_POIGNET_ROTATION_GAUCHE.Click += new System.EventHandler(this.BTN_POIGNET_ROTATION_GAUCHE_Click);
            // 
            // BTN_POIGNET_ROTATION_DROITE
            // 
            this.BTN_POIGNET_ROTATION_DROITE.Location = new System.Drawing.Point(108, 19);
            this.BTN_POIGNET_ROTATION_DROITE.Name = "BTN_POIGNET_ROTATION_DROITE";
            this.BTN_POIGNET_ROTATION_DROITE.Size = new System.Drawing.Size(75, 23);
            this.BTN_POIGNET_ROTATION_DROITE.TabIndex = 0;
            this.BTN_POIGNET_ROTATION_DROITE.Text = "DROITE";
            this.BTN_POIGNET_ROTATION_DROITE.UseVisualStyleBackColor = true;
            this.BTN_POIGNET_ROTATION_DROITE.Click += new System.EventHandler(this.BTN_POIGNET_ROTATION_DROITE_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TB_EPAULE_ANGLE);
            this.groupBox3.Controls.Add(this.BTN_EPAULE_GAUCHE);
            this.groupBox3.Controls.Add(this.BTN_EPAULE_DROITE);
            this.groupBox3.Location = new System.Drawing.Point(37, 137);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(203, 104);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EPAULE";
            // 
            // TB_EPAULE_ANGLE
            // 
            this.TB_EPAULE_ANGLE.Location = new System.Drawing.Point(16, 62);
            this.TB_EPAULE_ANGLE.Maximum = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.TB_EPAULE_ANGLE.Name = "TB_EPAULE_ANGLE";
            this.TB_EPAULE_ANGLE.Size = new System.Drawing.Size(75, 20);
            this.TB_EPAULE_ANGLE.TabIndex = 3;
            this.TB_EPAULE_ANGLE.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // BTN_EPAULE_GAUCHE
            // 
            this.BTN_EPAULE_GAUCHE.Location = new System.Drawing.Point(16, 19);
            this.BTN_EPAULE_GAUCHE.Name = "BTN_EPAULE_GAUCHE";
            this.BTN_EPAULE_GAUCHE.Size = new System.Drawing.Size(75, 23);
            this.BTN_EPAULE_GAUCHE.TabIndex = 1;
            this.BTN_EPAULE_GAUCHE.Text = "GAUCHE";
            this.BTN_EPAULE_GAUCHE.UseVisualStyleBackColor = true;
            this.BTN_EPAULE_GAUCHE.Click += new System.EventHandler(this.BTN_EPAULE_GAUCHE_Click);
            // 
            // BTN_EPAULE_DROITE
            // 
            this.BTN_EPAULE_DROITE.Location = new System.Drawing.Point(108, 19);
            this.BTN_EPAULE_DROITE.Name = "BTN_EPAULE_DROITE";
            this.BTN_EPAULE_DROITE.Size = new System.Drawing.Size(75, 23);
            this.BTN_EPAULE_DROITE.TabIndex = 0;
            this.BTN_EPAULE_DROITE.Text = "DROITE";
            this.BTN_EPAULE_DROITE.UseVisualStyleBackColor = true;
            this.BTN_EPAULE_DROITE.Click += new System.EventHandler(this.BTN_EPAULE_DROITE_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TB_MAIN_ANGLE);
            this.groupBox4.Controls.Add(this.BTN_MAIN_GAUCHE);
            this.groupBox4.Controls.Add(this.BTN_MAIN_DROITE);
            this.groupBox4.Location = new System.Drawing.Point(261, 137);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(203, 104);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "MAIN";
            // 
            // TB_MAIN_ANGLE
            // 
            this.TB_MAIN_ANGLE.Location = new System.Drawing.Point(16, 62);
            this.TB_MAIN_ANGLE.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.TB_MAIN_ANGLE.Name = "TB_MAIN_ANGLE";
            this.TB_MAIN_ANGLE.Size = new System.Drawing.Size(75, 20);
            this.TB_MAIN_ANGLE.TabIndex = 3;
            this.TB_MAIN_ANGLE.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // BTN_MAIN_GAUCHE
            // 
            this.BTN_MAIN_GAUCHE.Location = new System.Drawing.Point(16, 19);
            this.BTN_MAIN_GAUCHE.Name = "BTN_MAIN_GAUCHE";
            this.BTN_MAIN_GAUCHE.Size = new System.Drawing.Size(75, 23);
            this.BTN_MAIN_GAUCHE.TabIndex = 1;
            this.BTN_MAIN_GAUCHE.Text = "GAUCHE";
            this.BTN_MAIN_GAUCHE.UseVisualStyleBackColor = true;
            this.BTN_MAIN_GAUCHE.Click += new System.EventHandler(this.BTN_MAIN_GAUCHE_Click);
            // 
            // BTN_MAIN_DROITE
            // 
            this.BTN_MAIN_DROITE.Location = new System.Drawing.Point(108, 19);
            this.BTN_MAIN_DROITE.Name = "BTN_MAIN_DROITE";
            this.BTN_MAIN_DROITE.Size = new System.Drawing.Size(75, 23);
            this.BTN_MAIN_DROITE.TabIndex = 0;
            this.BTN_MAIN_DROITE.Text = "DROITE";
            this.BTN_MAIN_DROITE.UseVisualStyleBackColor = true;
            this.BTN_MAIN_DROITE.Click += new System.EventHandler(this.BTN_MAIN_DROITE_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.TB_COUDE_ANGLE);
            this.groupBox5.Controls.Add(this.BTN_COUDE_GAUCHE);
            this.groupBox5.Controls.Add(this.BTN_COUDE_DROITE);
            this.groupBox5.Location = new System.Drawing.Point(37, 247);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(203, 102);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "COUDE";
            // 
            // TB_COUDE_ANGLE
            // 
            this.TB_COUDE_ANGLE.Location = new System.Drawing.Point(16, 59);
            this.TB_COUDE_ANGLE.Maximum = new decimal(new int[] {
            130,
            0,
            0,
            0});
            this.TB_COUDE_ANGLE.Name = "TB_COUDE_ANGLE";
            this.TB_COUDE_ANGLE.Size = new System.Drawing.Size(75, 20);
            this.TB_COUDE_ANGLE.TabIndex = 3;
            this.TB_COUDE_ANGLE.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // BTN_COUDE_GAUCHE
            // 
            this.BTN_COUDE_GAUCHE.Location = new System.Drawing.Point(16, 19);
            this.BTN_COUDE_GAUCHE.Name = "BTN_COUDE_GAUCHE";
            this.BTN_COUDE_GAUCHE.Size = new System.Drawing.Size(75, 23);
            this.BTN_COUDE_GAUCHE.TabIndex = 1;
            this.BTN_COUDE_GAUCHE.Text = "GAUCHE";
            this.BTN_COUDE_GAUCHE.UseVisualStyleBackColor = true;
            this.BTN_COUDE_GAUCHE.Click += new System.EventHandler(this.BTN_COUDE_GAUCHE_Click);
            // 
            // BTN_COUDE_DROITE
            // 
            this.BTN_COUDE_DROITE.Location = new System.Drawing.Point(108, 19);
            this.BTN_COUDE_DROITE.Name = "BTN_COUDE_DROITE";
            this.BTN_COUDE_DROITE.Size = new System.Drawing.Size(75, 23);
            this.BTN_COUDE_DROITE.TabIndex = 0;
            this.BTN_COUDE_DROITE.Text = "DROITE";
            this.BTN_COUDE_DROITE.UseVisualStyleBackColor = true;
            this.BTN_COUDE_DROITE.Click += new System.EventHandler(this.BTN_COUDE_DROITE_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.TB_PINCE_SPEED);
            this.groupBox6.Controls.Add(this.BTN_PINCE_CLOSE);
            this.groupBox6.Controls.Add(this.BTN_PINCE_OPEN);
            this.groupBox6.Location = new System.Drawing.Point(261, 247);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(203, 102);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "PINCE";
            // 
            // TB_PINCE_SPEED
            // 
            this.TB_PINCE_SPEED.Location = new System.Drawing.Point(16, 48);
            this.TB_PINCE_SPEED.Maximum = 100;
            this.TB_PINCE_SPEED.Name = "TB_PINCE_SPEED";
            this.TB_PINCE_SPEED.Size = new System.Drawing.Size(167, 45);
            this.TB_PINCE_SPEED.TabIndex = 3;
            this.TB_PINCE_SPEED.Value = 100;
            // 
            // BTN_PINCE_CLOSE
            // 
            this.BTN_PINCE_CLOSE.Location = new System.Drawing.Point(16, 19);
            this.BTN_PINCE_CLOSE.Name = "BTN_PINCE_CLOSE";
            this.BTN_PINCE_CLOSE.Size = new System.Drawing.Size(75, 23);
            this.BTN_PINCE_CLOSE.TabIndex = 2;
            this.BTN_PINCE_CLOSE.Text = "CLOSE";
            this.BTN_PINCE_CLOSE.UseVisualStyleBackColor = true;
            this.BTN_PINCE_CLOSE.Click += new System.EventHandler(this.BTN_PINCE_CLOSE_Click);
            // 
            // BTN_PINCE_OPEN
            // 
            this.BTN_PINCE_OPEN.Location = new System.Drawing.Point(108, 19);
            this.BTN_PINCE_OPEN.Name = "BTN_PINCE_OPEN";
            this.BTN_PINCE_OPEN.Size = new System.Drawing.Size(75, 23);
            this.BTN_PINCE_OPEN.TabIndex = 1;
            this.BTN_PINCE_OPEN.Text = "OPEN";
            this.BTN_PINCE_OPEN.UseVisualStyleBackColor = true;
            this.BTN_PINCE_OPEN.Click += new System.EventHandler(this.BTN_PINCE_ACTION_Click);
            // 
            // BTN_CONNEXION
            // 
            this.BTN_CONNEXION.BackColor = System.Drawing.Color.Red;
            this.BTN_CONNEXION.Font = new System.Drawing.Font("Wide Latin", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_CONNEXION.ForeColor = System.Drawing.Color.Black;
            this.BTN_CONNEXION.Location = new System.Drawing.Point(37, 355);
            this.BTN_CONNEXION.Name = "BTN_CONNEXION";
            this.BTN_CONNEXION.Size = new System.Drawing.Size(203, 118);
            this.BTN_CONNEXION.TabIndex = 6;
            this.BTN_CONNEXION.Text = "CONNEXION";
            this.BTN_CONNEXION.UseVisualStyleBackColor = false;
            this.BTN_CONNEXION.Click += new System.EventHandler(this.BTN_CONNEXION_Click);
            // 
            // BTN_HOME
            // 
            this.BTN_HOME.Location = new System.Drawing.Point(483, 46);
            this.BTN_HOME.Name = "BTN_HOME";
            this.BTN_HOME.Size = new System.Drawing.Size(75, 61);
            this.BTN_HOME.TabIndex = 7;
            this.BTN_HOME.Text = "HOME";
            this.BTN_HOME.UseVisualStyleBackColor = true;
            this.BTN_HOME.Click += new System.EventHandler(this.BTN_HOME_Click);
            // 
            // BTN_READY
            // 
            this.BTN_READY.Location = new System.Drawing.Point(564, 46);
            this.BTN_READY.Name = "BTN_READY";
            this.BTN_READY.Size = new System.Drawing.Size(75, 61);
            this.BTN_READY.TabIndex = 8;
            this.BTN_READY.Text = "READY";
            this.BTN_READY.UseVisualStyleBackColor = true;
            this.BTN_READY.Click += new System.EventHandler(this.BTN_READY_Click);
            // 
            // BTN_RECORD
            // 
            this.BTN_RECORD.Location = new System.Drawing.Point(483, 153);
            this.BTN_RECORD.Name = "BTN_RECORD";
            this.BTN_RECORD.Size = new System.Drawing.Size(75, 39);
            this.BTN_RECORD.TabIndex = 11;
            this.BTN_RECORD.Text = "RECORD";
            this.BTN_RECORD.UseVisualStyleBackColor = true;
            this.BTN_RECORD.Click += new System.EventHandler(this.BTN_RECORD_Click);
            // 
            // BTN_SAVE
            // 
            this.BTN_SAVE.Location = new System.Drawing.Point(586, 325);
            this.BTN_SAVE.Name = "BTN_SAVE";
            this.BTN_SAVE.Size = new System.Drawing.Size(53, 22);
            this.BTN_SAVE.TabIndex = 12;
            this.BTN_SAVE.Text = "SAVE";
            this.BTN_SAVE.UseVisualStyleBackColor = true;
            this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_Click);
            // 
            // LB_COMMAND
            // 
            this.LB_COMMAND.FormattingEnabled = true;
            this.LB_COMMAND.Location = new System.Drawing.Point(483, 199);
            this.LB_COMMAND.Name = "LB_COMMAND";
            this.LB_COMMAND.Size = new System.Drawing.Size(156, 121);
            this.LB_COMMAND.TabIndex = 13;
            // 
            // TB_COMMAND
            // 
            this.TB_COMMAND.Location = new System.Drawing.Point(483, 327);
            this.TB_COMMAND.Name = "TB_COMMAND";
            this.TB_COMMAND.Size = new System.Drawing.Size(97, 20);
            this.TB_COMMAND.TabIndex = 14;
            // 
            // BTN_PLAY
            // 
            this.BTN_PLAY.Location = new System.Drawing.Point(564, 153);
            this.BTN_PLAY.Name = "BTN_PLAY";
            this.BTN_PLAY.Size = new System.Drawing.Size(75, 39);
            this.BTN_PLAY.TabIndex = 15;
            this.BTN_PLAY.Text = "PLAY";
            this.BTN_PLAY.UseVisualStyleBackColor = true;
            this.BTN_PLAY.Click += new System.EventHandler(this.BTN_PLAY_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(489, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "SPEED %";
            // 
            // NUD_SPEED
            // 
            this.NUD_SPEED.Location = new System.Drawing.Point(483, 127);
            this.NUD_SPEED.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.NUD_SPEED.Name = "NUD_SPEED";
            this.NUD_SPEED.Size = new System.Drawing.Size(75, 20);
            this.NUD_SPEED.TabIndex = 17;
            this.NUD_SPEED.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // BTN_Ok
            // 
            this.BTN_Ok.Location = new System.Drawing.Point(564, 127);
            this.BTN_Ok.Name = "BTN_Ok";
            this.BTN_Ok.Size = new System.Drawing.Size(41, 23);
            this.BTN_Ok.TabIndex = 18;
            this.BTN_Ok.Text = "Ok";
            this.BTN_Ok.UseVisualStyleBackColor = true;
            this.BTN_Ok.Click += new System.EventHandler(this.BTN_Ok_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.TB_CONSOLE);
            this.groupBox7.Controls.Add(this.LB_CONSOLE);
            this.groupBox7.Controls.Add(this.BTN_CONSOLE);
            this.groupBox7.Location = new System.Drawing.Point(658, 27);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(228, 180);
            this.groupBox7.TabIndex = 20;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "CONSOLE";
            // 
            // TB_CONSOLE
            // 
            this.TB_CONSOLE.Location = new System.Drawing.Point(15, 147);
            this.TB_CONSOLE.Name = "TB_CONSOLE";
            this.TB_CONSOLE.Size = new System.Drawing.Size(119, 20);
            this.TB_CONSOLE.TabIndex = 17;
            this.TB_CONSOLE.TextChanged += new System.EventHandler(this.TB_CONSOLE_TextChanged);
            // 
            // LB_CONSOLE
            // 
            this.LB_CONSOLE.FormattingEnabled = true;
            this.LB_CONSOLE.Location = new System.Drawing.Point(15, 19);
            this.LB_CONSOLE.Name = "LB_CONSOLE";
            this.LB_CONSOLE.Size = new System.Drawing.Size(197, 121);
            this.LB_CONSOLE.TabIndex = 16;
            this.LB_CONSOLE.SelectedIndexChanged += new System.EventHandler(this.LB_CONSOLE_SelectedIndexChanged);
            // 
            // BTN_CONSOLE
            // 
            this.BTN_CONSOLE.Location = new System.Drawing.Point(140, 146);
            this.BTN_CONSOLE.Name = "BTN_CONSOLE";
            this.BTN_CONSOLE.Size = new System.Drawing.Size(72, 22);
            this.BTN_CONSOLE.TabIndex = 15;
            this.BTN_CONSOLE.Text = "EXECUTE";
            this.BTN_CONSOLE.UseVisualStyleBackColor = true;
            this.BTN_CONSOLE.Click += new System.EventHandler(this.BTN_CONSOLE_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.BTN_POS_EXECUTE);
            this.groupBox8.Controls.Add(this.TB_POS);
            this.groupBox8.Controls.Add(this.LB_POS);
            this.groupBox8.Controls.Add(this.BTN_POS_SAVE);
            this.groupBox8.Location = new System.Drawing.Point(658, 213);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(228, 215);
            this.groupBox8.TabIndex = 21;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "POSITION";
            // 
            // BTN_POS_EXECUTE
            // 
            this.BTN_POS_EXECUTE.Location = new System.Drawing.Point(35, 173);
            this.BTN_POS_EXECUTE.Name = "BTN_POS_EXECUTE";
            this.BTN_POS_EXECUTE.Size = new System.Drawing.Size(72, 22);
            this.BTN_POS_EXECUTE.TabIndex = 18;
            this.BTN_POS_EXECUTE.Text = "EXECUTE";
            this.BTN_POS_EXECUTE.UseVisualStyleBackColor = true;
            this.BTN_POS_EXECUTE.Click += new System.EventHandler(this.BTN_POS_EXECUTE_Click);
            // 
            // TB_POS
            // 
            this.TB_POS.Location = new System.Drawing.Point(15, 147);
            this.TB_POS.Name = "TB_POS";
            this.TB_POS.Size = new System.Drawing.Size(197, 20);
            this.TB_POS.TabIndex = 17;
            // 
            // LB_POS
            // 
            this.LB_POS.FormattingEnabled = true;
            this.LB_POS.Location = new System.Drawing.Point(15, 19);
            this.LB_POS.Name = "LB_POS";
            this.LB_POS.Size = new System.Drawing.Size(197, 121);
            this.LB_POS.TabIndex = 16;
            // 
            // BTN_POS_SAVE
            // 
            this.BTN_POS_SAVE.Location = new System.Drawing.Point(59, 201);
            this.BTN_POS_SAVE.Name = "BTN_POS_SAVE";
            this.BTN_POS_SAVE.Size = new System.Drawing.Size(123, 22);
            this.BTN_POS_SAVE.TabIndex = 15;
            this.BTN_POS_SAVE.Text = "CALIBRATEON";
            this.BTN_POS_SAVE.UseVisualStyleBackColor = true;
            this.BTN_POS_SAVE.Click += new System.EventHandler(this.BTN_POS_SAVE_Click);
            // 
            // BTN_DECONNEXION
            // 
            this.BTN_DECONNEXION.Location = new System.Drawing.Point(785, 453);
            this.BTN_DECONNEXION.Name = "BTN_DECONNEXION";
            this.BTN_DECONNEXION.Size = new System.Drawing.Size(104, 23);
            this.BTN_DECONNEXION.TabIndex = 24;
            this.BTN_DECONNEXION.Text = "DECONNEXION";
            this.BTN_DECONNEXION.UseVisualStyleBackColor = true;
            this.BTN_DECONNEXION.Click += new System.EventHandler(this.BTN_DECONNEXION_Click);
            // 
            // BTN_Test
            // 
            this.BTN_Test.Location = new System.Drawing.Point(483, 372);
            this.BTN_Test.Name = "BTN_Test";
            this.BTN_Test.Size = new System.Drawing.Size(75, 23);
            this.BTN_Test.TabIndex = 25;
            this.BTN_Test.Text = "Test";
            this.BTN_Test.UseVisualStyleBackColor = true;
            this.BTN_Test.Click += new System.EventHandler(this.BTN_Test_Click);
            // 
            // BTN_Reset
            // 
            this.BTN_Reset.Location = new System.Drawing.Point(564, 372);
            this.BTN_Reset.Name = "BTN_Reset";
            this.BTN_Reset.Size = new System.Drawing.Size(75, 23);
            this.BTN_Reset.TabIndex = 26;
            this.BTN_Reset.Text = "RESET";
            this.BTN_Reset.UseVisualStyleBackColor = true;
            this.BTN_Reset.Click += new System.EventHandler(this.BTN_Reset_Click);
            // 
            // TB_Z
            // 
            this.TB_Z.Location = new System.Drawing.Point(483, 453);
            this.TB_Z.Name = "TB_Z";
            this.TB_Z.Size = new System.Drawing.Size(97, 20);
            this.TB_Z.TabIndex = 27;
            // 
            // TB_Y
            // 
            this.TB_Y.Location = new System.Drawing.Point(483, 427);
            this.TB_Y.Name = "TB_Y";
            this.TB_Y.Size = new System.Drawing.Size(97, 20);
            this.TB_Y.TabIndex = 28;
            // 
            // TB_X
            // 
            this.TB_X.Location = new System.Drawing.Point(483, 401);
            this.TB_X.Name = "TB_X";
            this.TB_X.Size = new System.Drawing.Size(97, 20);
            this.TB_X.TabIndex = 29;
            // 
            // BTN_Pos
            // 
            this.BTN_Pos.Location = new System.Drawing.Point(586, 434);
            this.BTN_Pos.Name = "BTN_Pos";
            this.BTN_Pos.Size = new System.Drawing.Size(75, 23);
            this.BTN_Pos.TabIndex = 30;
            this.BTN_Pos.Text = "pos";
            this.BTN_Pos.UseVisualStyleBackColor = true;
            this.BTN_Pos.Click += new System.EventHandler(this.BTN_Pos_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(717, 453);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "ajouter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(621, 463);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 32;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 485);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BTN_Pos);
            this.Controls.Add(this.TB_X);
            this.Controls.Add(this.TB_Y);
            this.Controls.Add(this.TB_Z);
            this.Controls.Add(this.BTN_Reset);
            this.Controls.Add(this.BTN_Test);
            this.Controls.Add(this.BTN_DECONNEXION);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.BTN_Ok);
            this.Controls.Add(this.NUD_SPEED);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_PLAY);
            this.Controls.Add(this.TB_COMMAND);
            this.Controls.Add(this.LB_COMMAND);
            this.Controls.Add(this.BTN_SAVE);
            this.Controls.Add(this.BTN_RECORD);
            this.Controls.Add(this.BTN_HALT);
            this.Controls.Add(this.BTN_READY);
            this.Controls.Add(this.BTN_HOME);
            this.Controls.Add(this.BTN_CONNEXION);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TB_BASE_ANGLE)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TB_POIGNET_ANGLE)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TB_EPAULE_ANGLE)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TB_MAIN_ANGLE)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TB_COUDE_ANGLE)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TB_PINCE_SPEED)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_SPEED)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BTN_BASE_GAUCHE;
        private System.Windows.Forms.Button BTN_BASE_DROITE;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BTN_POIGNET_ROTATION_GAUCHE;
        private System.Windows.Forms.Button BTN_POIGNET_ROTATION_DROITE;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BTN_EPAULE_GAUCHE;
        private System.Windows.Forms.Button BTN_EPAULE_DROITE;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button BTN_MAIN_GAUCHE;
        private System.Windows.Forms.Button BTN_MAIN_DROITE;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button BTN_COUDE_GAUCHE;
        private System.Windows.Forms.Button BTN_COUDE_DROITE;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button BTN_PINCE_OPEN;
        private System.Windows.Forms.Button BTN_CONNEXION;
        private System.Windows.Forms.Button BTN_PINCE_CLOSE;
        private System.Windows.Forms.TrackBar TB_PINCE_SPEED;
        private System.Windows.Forms.NumericUpDown TB_BASE_ANGLE;
        private System.Windows.Forms.NumericUpDown TB_POIGNET_ANGLE;
        private System.Windows.Forms.NumericUpDown TB_EPAULE_ANGLE;
        private System.Windows.Forms.NumericUpDown TB_MAIN_ANGLE;
        private System.Windows.Forms.NumericUpDown TB_COUDE_ANGLE;
        private System.Windows.Forms.Button BTN_HOME;
        private System.Windows.Forms.Button BTN_READY;
        private System.Windows.Forms.Button BTN_RECORD;
        private System.Windows.Forms.Button BTN_SAVE;
        private System.Windows.Forms.ListBox LB_COMMAND;
        private System.Windows.Forms.TextBox TB_COMMAND;
        private System.Windows.Forms.Button BTN_PLAY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NUD_SPEED;
        private System.Windows.Forms.Button BTN_Ok;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox TB_CONSOLE;
        private System.Windows.Forms.ListBox LB_CONSOLE;
        private System.Windows.Forms.Button BTN_CONSOLE;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button BTN_POS_EXECUTE;
        private System.Windows.Forms.TextBox TB_POS;
        private System.Windows.Forms.ListBox LB_POS;
        private System.Windows.Forms.Button BTN_POS_SAVE;
        private System.Windows.Forms.Button BTN_DECONNEXION;
        private System.Windows.Forms.Button BTN_HALT;
        private System.Windows.Forms.Button BTN_Test;
        private System.Windows.Forms.Button BTN_Reset;
        private System.Windows.Forms.TextBox TB_Z;
        private System.Windows.Forms.TextBox TB_Y;
        private System.Windows.Forms.TextBox TB_X;
        private System.Windows.Forms.Button BTN_Pos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

