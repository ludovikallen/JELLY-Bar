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
        
        OracleConnection connexion;
   
        public PageConnexion()
        {
            InitializeComponent();
        }


        private void Initializer_DataBase()
        {
            connexion = new OracleConnection();
            try
            {
                string dsource = "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 205.237.244.251)(PORT = 1521)) (CONNECT_DATA =(SERVICE_NAME = orcl.clg.qc.ca)))";
                string bd = "Data Source=" + dsource + ";User id=" + TBX_User.Text + ";Password=" + TBX_Pwd.Text;
                connexion.ConnectionString = bd;
                connexion.Open();
                MessageBox.Show("Connecté avec succès!!!!");
            }
            catch (Exception) { MessageBox.Show("Erreur de connexion!!!"); }
        }

        private void BTN_Logon_Click(object sender, EventArgs e)
        {
            Initializer_DataBase();
            Connexion();
            this.Show();
        }

        public void Connexion()
        {
            PageAccueil dlg = new PageAccueil();
            dlg.connexion = connexion;
            BTN_Logon.Text = "Deconnexion";
            this.Hide();
            DialogResult dlg_result = dlg.ShowDialog();
        }

        private void BTN_Logon_TextChanged(object sender, EventArgs e)
        {
            if (!connexion.State.Equals("Open"))
                BTN_Logon.Text = "Connexion";
        }
    }
}
