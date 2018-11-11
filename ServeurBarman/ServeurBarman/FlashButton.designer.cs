namespace FlashButton
{
    partial class FlashButton
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
            this.SuspendLayout();
            // 
            // FlashButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "FlashButton";
            this.Size = new System.Drawing.Size(89, 87);
            this.Load += new System.EventHandler(this.FlashButton_Load);
            this.EnabledChanged += new System.EventHandler(this.FlashButton_EnabledChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FlashButton_MouseDown);
            this.MouseEnter += new System.EventHandler(this.FlashButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.FlashButton_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FlashButton_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
