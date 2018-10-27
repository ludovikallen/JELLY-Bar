using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bras_Robot;
using Oracle.ManagedDataAccess.Client;

namespace ServeurBarman
{
    public partial class AddCups : UserControl
    {
        public OracleConnection connexion;
        public AddCups()
        {
            InitializeComponent();
            Cbx_TypeVerre.Items.Add("Verre Rouge");
            Cbx_TypeVerre.Items.Add("Verre Shooter");
            Cbx_TypeVerre.SelectedIndex=1;
        }

        private void BTN_Valider_Click(object sender, EventArgs e)
        {
            int nbverre = 0;
            int.TryParse(TB_NbVerre.Text, out nbverre);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
