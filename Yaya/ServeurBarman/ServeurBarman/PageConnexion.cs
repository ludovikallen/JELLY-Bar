using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServeurBarman
{
    public partial class PageConnexion : MetroFramework.Forms.MetroForm
    {
        public PageConnexion()
        {
            InitializeComponent();
        }

        private void BTN_Logon_Click(object sender, EventArgs e)
        {
            PageAccueil dlg = new PageAccueil();

            DialogResult dlg_result = dlg.ShowDialog();
        }
    }
}
