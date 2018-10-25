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
        DataBase db;
        public Add_Drinks()
        {
            InitializeComponent();
            db = DataBase.instance_bd;
        }

        private void AddDrink()
        {
            
            
        }

        private void Btn_Valider_Click(object sender, EventArgs e)
        {
            List<string> ajoutIngredient = new List<string>();

            ajoutIngredient.Add(Tbx_CodeBouteille.Text);
            ajoutIngredient.Add(Tbx_Posx.Text);
            ajoutIngredient.Add(Tbx_Posy.Text);
            ajoutIngredient.Add(Tbx_Posz.Text);
            ajoutIngredient.Add(Tbx_Quantity.Text);
            ajoutIngredient.Add(Rtb_Description.Text);
            ajoutIngredient.Add(Tbx_NomBouteille.Text);

            db.AjouterIngredients(ajoutIngredient);
            
        }

        private void Tbx_CodeBouteille_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
