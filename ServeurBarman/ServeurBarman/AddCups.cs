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
            DialogResult dlg = MessageBox.Show("Êtes-vous sûr de vouloir enrégister?", "Confirmation", MessageBoxButtons.YesNo);

            if (dlg == DialogResult.Yes)
            {
                if (TB_NbVerre.Text != "")
                {
                    int i = Int32.Parse(TB_NbVerre.Text);
                    bd.AjouterShooter(i);
                }
                else
                {
                    erreurs();
                }
            }
        }

        private void AddCups_Load(object sender, EventArgs e)
        {
            Cbx_TypeVerre.Items.Add("Verre Rouge");
            Cbx_TypeVerre.Items.Add("Verre Shooter");
            Cbx_TypeVerre.SelectedIndex = 1;
            erreur.Visible = false;
            bd = DataBase.instance_bd;
        }

        private void Cbx_TypeVerre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Cbx_TypeVerre.SelectedIndex==1)
            {
                TB_NbVerre.Enabled = true;
                btn_Annuler.Enabled = true;
                btn_Valider.Enabled = true;
                PnlShooter.Visible = true;
                PnlVerreRouge.Visible = false;
            }
            else
            {
                TB_NbVerre.Enabled = false;
                btn_Annuler.Enabled = false;
                btn_Valider.Enabled = false;
                PnlShooter.Visible = false;
                PnlVerreRouge.Visible = true;
            }
        }
    }
}
