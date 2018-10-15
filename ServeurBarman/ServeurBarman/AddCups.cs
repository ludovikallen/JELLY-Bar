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

namespace ServeurBarman
{
    public partial class AddCups : UserControl
    {
        public AddCups()
        {
            InitializeComponent();
        }

        private void BTN_Valider_Click(object sender, EventArgs e)
        {
            int nbverre = 0;
            int.TryParse(TB_NbVerre.Text, out nbverre);
            CRS_A255.Instance.AjouterCup(nbverre);
        }
    }
}
