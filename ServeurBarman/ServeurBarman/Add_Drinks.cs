using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ServeurBarman
{
    public partial class Add_Drinks : UserControl
    {
        public OracleConnection connexion;
        public Add_Drinks()
        {
            InitializeComponent();
        }

        private void AddDrink()
        {
            try
            {
                string cmd = "insert into ingredient values(" + Int32.Parse(Tbx_CodeBouteille.Text) + "," + Int32.Parse(Tbx_Posx.Text) + "," 
                    + Int32.Parse(Tbx_Posy.Text) + "," + Int32.Parse(Tbx_Posz.Text) + "," + "'1'" + "," + Int32.Parse(Tbx_Quantity.Text) + ",'" 
                    + Rtb_Description.Text + "','" + Tbx_NomBouteille.Text + "')";

                OracleCommand disc = new OracleCommand(cmd, connexion);
                disc.ExecuteNonQuery();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message); }
        }

        private void Btn_Valider_Click(object sender, EventArgs e)
        {
            AddDrink();
            try
            {
                string cmd1 = "commit";
                OracleCommand disc1 = new OracleCommand(cmd1, connexion);
                disc1.ExecuteNonQuery();
            }
            catch (Exception) { MessageBox.Show(" Échec de l'enregistrement."); }
        }

        private void Tbx_CodeBouteille_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
