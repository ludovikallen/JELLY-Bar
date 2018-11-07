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
        DataBase bd;
        public AddCups()
        {
            InitializeComponent();
        }

        public void erreurs()
        {
            TB_NbVerre.Text.Trim();
            if(TB_NbVerre.Text=="")
            {
                erreur.Visible = true;
            }
        }

        private void btn_Valider_Click(object sender, EventArgs e)
        {
           
                if (TB_NbVerre.Text != ""&&Int32.Parse(TB_NbVerre.Text)<=6&& Int32.Parse(TB_NbVerre.Text)>=1)
                {
                    int i = Int32.Parse(TB_NbVerre.Text);
                    bd.AjouterShooter(ref i);
                    TB_NbVerre.Text = "";
                }
                else
                {
                    erreurs();
                }
        }

        private void AddCups_Load(object sender, EventArgs e)
        {
            lbShooter.Text = "Verre Shooter";
            erreur.Visible = false;
            bd = DataBase.instance_bd;
        }

        private void TB_NbVerre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
