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
        
        PageConnexion pageC;
        string ChaineConnexion;
        OracleConnection conn;
        public PageConnexion()
        {
            InitializeComponent();
           ChaineConnexion = "Data Source=(DESCRIPTION="
  + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)"
 + "(HOST=222.333.444.555)(PORT=1521)))"
 + "(CONNECT_DATA=(SERVICE_NAME=ORCL.clg.qc.ca)));"
  + "User Id=user;Password=password";
            conn = new OracleConnection(ChaineConnexion);
            conn.ConnectionString = ChaineConnexion;
        }

        private void BTN_Logon_Click(object sender, EventArgs e)
        {
            pageC = new PageConnexion();
            pageC.Hide();
            PageAccueil dlg = new PageAccueil();
            DialogResult dlg_result = dlg.ShowDialog();
            
        }
    }
}
