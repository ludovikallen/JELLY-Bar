﻿namespace ServeurBarman
{
    partial class Add_Drinks
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
            this.Tbx_Posx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Tbx_NomBouteille = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Tbx_CodeBouteille = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Tbx_Quantity = new System.Windows.Forms.TextBox();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Valider = new System.Windows.Forms.Button();
            this.Tbx_Posy = new System.Windows.Forms.TextBox();
            this.Tbx_Posz = new System.Windows.Forms.TextBox();
            this.Rtb_Description = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Tbx_Posx
            // 
            this.Tbx_Posx.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_Posx.Location = new System.Drawing.Point(160, 119);
            this.Tbx_Posx.Name = "Tbx_Posx";
            this.Tbx_Posx.Size = new System.Drawing.Size(63, 26);
            this.Tbx_Posx.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(95, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Position";
            // 
            // Tbx_NomBouteille
            // 
            this.Tbx_NomBouteille.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_NomBouteille.Location = new System.Drawing.Point(160, 65);
            this.Tbx_NomBouteille.Name = "Tbx_NomBouteille";
            this.Tbx_NomBouteille.Size = new System.Drawing.Size(63, 26);
            this.Tbx_NomBouteille.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(80, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nom drink";
            // 
            // Tbx_CodeBouteille
            // 
            this.Tbx_CodeBouteille.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_CodeBouteille.Location = new System.Drawing.Point(160, 11);
            this.Tbx_CodeBouteille.Name = "Tbx_CodeBouteille";
            this.Tbx_CodeBouteille.Size = new System.Drawing.Size(63, 26);
            this.Tbx_CodeBouteille.TabIndex = 7;
            this.Tbx_CodeBouteille.TextChanged += new System.EventHandler(this.Tbx_CodeBouteille_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Code Bouteille";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(38, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "Quantité du drink";
            // 
            // Tbx_Quantity
            // 
            this.Tbx_Quantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_Quantity.Location = new System.Drawing.Point(160, 173);
            this.Tbx_Quantity.Name = "Tbx_Quantity";
            this.Tbx_Quantity.Size = new System.Drawing.Size(63, 26);
            this.Tbx_Quantity.TabIndex = 5;
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Cancel.Location = new System.Drawing.Point(529, 234);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(114, 37);
            this.Btn_Cancel.TabIndex = 8;
            this.Btn_Cancel.Text = "Annuler";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // Btn_Valider
            // 
            this.Btn_Valider.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Valider.Location = new System.Drawing.Point(409, 234);
            this.Btn_Valider.Name = "Btn_Valider";
            this.Btn_Valider.Size = new System.Drawing.Size(114, 37);
            this.Btn_Valider.TabIndex = 8;
            this.Btn_Valider.Text = "Valider";
            this.Btn_Valider.UseVisualStyleBackColor = true;
            this.Btn_Valider.Click += new System.EventHandler(this.Btn_Valider_Click);
            // 
            // Tbx_Posy
            // 
            this.Tbx_Posy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_Posy.Location = new System.Drawing.Point(229, 119);
            this.Tbx_Posy.Name = "Tbx_Posy";
            this.Tbx_Posy.Size = new System.Drawing.Size(63, 26);
            this.Tbx_Posy.TabIndex = 5;
            // 
            // Tbx_Posz
            // 
            this.Tbx_Posz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_Posz.Location = new System.Drawing.Point(298, 119);
            this.Tbx_Posz.Name = "Tbx_Posz";
            this.Tbx_Posz.Size = new System.Drawing.Size(63, 26);
            this.Tbx_Posz.TabIndex = 5;
            // 
            // Rtb_Description
            // 
            this.Rtb_Description.Location = new System.Drawing.Point(410, 35);
            this.Rtb_Description.Name = "Rtb_Description";
            this.Rtb_Description.Size = new System.Drawing.Size(233, 164);
            this.Rtb_Description.TabIndex = 9;
            this.Rtb_Description.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(185, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "x";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(251, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(322, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "z";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(406, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 24);
            this.label8.TabIndex = 11;
            this.label8.Text = "Description";
            // 
            // Add_Drinks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Rtb_Description);
            this.Controls.Add(this.Btn_Valider);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Tbx_Quantity);
            this.Controls.Add(this.Tbx_Posz);
            this.Controls.Add(this.Tbx_Posy);
            this.Controls.Add(this.Tbx_Posx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Tbx_NomBouteille);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Tbx_CodeBouteille);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(659, 274);
            this.MinimumSize = new System.Drawing.Size(659, 274);
            this.Name = "Add_Drinks";
            this.Size = new System.Drawing.Size(659, 274);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Tbx_Posx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Tbx_NomBouteille;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Tbx_CodeBouteille;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Tbx_Quantity;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_Valider;
        private System.Windows.Forms.TextBox Tbx_Posy;
        private System.Windows.Forms.TextBox Tbx_Posz;
        private System.Windows.Forms.RichTextBox Rtb_Description;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}
