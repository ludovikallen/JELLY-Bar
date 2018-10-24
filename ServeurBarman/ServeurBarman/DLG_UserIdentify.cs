using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServeurBarman
{
    public partial class DLG_UserIdentify : MetroFramework.Forms.MetroForm
    {
        private TextBox Tbx_Pass;
        private Label label1;
        private Button Btn_Pass;
        public bool check;
        public DLG_UserIdentify()
        {
            InitializeComponent();
        }

        private void Btn_Pass_Click(object sender, EventArgs e)
        {
            check =PassCheck();
        }

        private bool PassCheck()
        {
            string savedPasswordHash = System.IO.File.ReadAllText(@"C:\Users\Sanogo Mael\S_U.jelly");
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(Tbx_Pass.Text, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++) { 
                if (hashBytes[i + 16] != hash[i]) { 
                    MessageBox.Show("Mauvais mot de passe!");
                    //throw new UnauthorizedAccessException();
                    return false;
                }
            }
            return true;
        }

        private void InitializeComponent()
        {
            this.Tbx_Pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Pass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Tbx_Pass
            // 
            this.Tbx_Pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_Pass.Location = new System.Drawing.Point(142, 45);
            this.Tbx_Pass.Name = "Tbx_Pass";
            this.Tbx_Pass.PasswordChar = '*';
            this.Tbx_Pass.Size = new System.Drawing.Size(150, 26);
            this.Tbx_Pass.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mot de passe";
            // 
            // Btn_Pass
            // 
            this.Btn_Pass.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_Pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Pass.Location = new System.Drawing.Point(201, 81);
            this.Btn_Pass.Name = "Btn_Pass";
            this.Btn_Pass.Size = new System.Drawing.Size(91, 26);
            this.Btn_Pass.TabIndex = 3;
            this.Btn_Pass.Text = "Valider";
            this.Btn_Pass.UseVisualStyleBackColor = true;
            // 
            // DLG_UserIdentify
            // 
            this.ClientSize = new System.Drawing.Size(383, 132);
            this.Controls.Add(this.Tbx_Pass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_Pass);
            this.Name = "DLG_UserIdentify";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
