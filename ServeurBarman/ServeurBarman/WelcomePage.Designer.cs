﻿namespace ServeurBarman
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
            this.mLB_NombreDeShooter = new MetroFramework.Controls.MetroLabel();
            this.mLB_NombreDeVerre = new MetroFramework.Controls.MetroLabel();
            this.mLB_NombreDeBouteille = new MetroFramework.Controls.MetroLabel();
            this.mLB_CustomNumber = new MetroFramework.Controls.MetroLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mLB_NombreDeShooter
            // 
            this.mLB_NombreDeShooter.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mLB_NombreDeShooter.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mLB_NombreDeShooter.Location = new System.Drawing.Point(245, 165);
            this.mLB_NombreDeShooter.Name = "mLB_NombreDeShooter";
            this.mLB_NombreDeShooter.Size = new System.Drawing.Size(68, 25);
            this.mLB_NombreDeShooter.TabIndex = 19;
            this.mLB_NombreDeShooter.Text = "                   ";
            this.mLB_NombreDeShooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mLB_NombreDeVerre
            // 
            this.mLB_NombreDeVerre.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mLB_NombreDeVerre.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mLB_NombreDeVerre.Location = new System.Drawing.Point(245, 116);
            this.mLB_NombreDeVerre.Name = "mLB_NombreDeVerre";
            this.mLB_NombreDeVerre.Size = new System.Drawing.Size(68, 25);
            this.mLB_NombreDeVerre.TabIndex = 20;
            this.mLB_NombreDeVerre.Text = "                   ";
            this.mLB_NombreDeVerre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mLB_NombreDeBouteille
            // 
            this.mLB_NombreDeBouteille.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mLB_NombreDeBouteille.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mLB_NombreDeBouteille.Location = new System.Drawing.Point(245, 66);
            this.mLB_NombreDeBouteille.Name = "mLB_NombreDeBouteille";
            this.mLB_NombreDeBouteille.Size = new System.Drawing.Size(68, 25);
            this.mLB_NombreDeBouteille.TabIndex = 21;
            this.mLB_NombreDeBouteille.Text = "                   ";
            this.mLB_NombreDeBouteille.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mLB_CustomNumber
            // 
            this.mLB_CustomNumber.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mLB_CustomNumber.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mLB_CustomNumber.Location = new System.Drawing.Point(245, 15);
            this.mLB_CustomNumber.Name = "mLB_CustomNumber";
            this.mLB_CustomNumber.Size = new System.Drawing.Size(68, 25);
            this.mLB_CustomNumber.TabIndex = 22;
            this.mLB_CustomNumber.Text = "                   ";
            this.mLB_CustomNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(97, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 18);
            this.label4.TabIndex = 15;
            this.label4.Text = "Nombre de shooters";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(115, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 18);
            this.label3.TabIndex = 16;
            this.label3.Text = "Nombre de verres";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(94, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "Nombre de bouteilles";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 18);
            this.label1.TabIndex = 18;
            this.label1.Text = "Nombre de commande en attente";
            // 
            // WelcomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mLB_NombreDeShooter);
            this.Controls.Add(this.mLB_NombreDeVerre);
            this.Controls.Add(this.mLB_NombreDeBouteille);
            this.Controls.Add(this.mLB_CustomNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(439, 208);
            this.MinimumSize = new System.Drawing.Size(439, 208);
            this.Name = "WelcomePage";
            this.Size = new System.Drawing.Size(439, 208);
            this.Load += new System.EventHandler(this.WelcomePage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel mLB_NombreDeShooter;
        private MetroFramework.Controls.MetroLabel mLB_NombreDeVerre;
        private MetroFramework.Controls.MetroLabel mLB_NombreDeBouteille;
        private MetroFramework.Controls.MetroLabel mLB_CustomNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
