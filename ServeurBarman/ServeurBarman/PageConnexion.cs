using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ServeurBarman
{
    public partial class PageConnexion : MetroFramework.Forms.MetroForm
    {
        DataBase b { get; set; }
   
        public PageConnexion()
        {
            InitializeComponent();
            b = DataBase.instance_bd;
        }

        private void BTN_Logon_Click(object sender, EventArgs e)
        {
            Connexion_BD();
            this.Show();
        }

        public void Connexion_BD()
        {
            b.Connexion(TBX_User.Text, TBX_Pwd.Text); // Ouvrir Connexion
            PageAccueil dlgPageAccueil = new PageAccueil();
            this.Hide();
            DialogResult dlg_result = dlgPageAccueil.ShowDialog();
            b.FermerConnexion(); // Fermer connexion
            this.Close();
        }

        private void BTN_Logon_TextChanged(object sender, EventArgs e)
        {
            //if (!connexion.State.Equals("Open"))
            //    BTN_Logon.Text = "Connexion";
        }
    }
}
