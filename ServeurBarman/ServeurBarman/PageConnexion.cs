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
        DataBase base2Donnees { get; set; }
   
        public PageConnexion()
        {
            InitializeComponent();
            
        }

        private void BTN_Logon_Click(object sender, EventArgs e)
        {
            Connexion_BD();
        }

        public void Connexion_BD()
        {
            base2Donnees.Connexion(TBX_User.Text, TBX_Pwd.Text); // Ouvrir Connexion
            if (base2Donnees.EtatBaseDonnées.State.ToString().Equals("Open"))
            {
                lbCheckConnexion.Visible = false;
                PageAccueil dlgPageAccueil = new PageAccueil();
                this.Hide();
                DialogResult dlg_result = dlgPageAccueil.ShowDialog();
                base2Donnees.FermerConnexion(); // Fermer connexion
                this.Close();
            }
            else
            {
                lbCheckConnexion.Visible = true;
            }
        }

        private void BTN_Logon_TextChanged(object sender, EventArgs e)
        {
            //if (!connexion.State.Equals("Open"))
            //    BTN_Logon.Text = "Connexion";
        }

        private void PageConnexion_Load(object sender, EventArgs e)
        {
            base2Donnees = DataBase.instance_bd;
            lbCheckConnexion.Visible = false;
        }
    }
}
