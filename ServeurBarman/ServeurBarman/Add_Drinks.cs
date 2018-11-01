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

            if(!erreurs())
                db.AjouterIngredients(ajoutIngredient);
        }

        private bool erreurs()
        {
            if (Tbx_CodeBouteille.Text != "" && Tbx_NomBouteille.Text != "" && Tbx_Posx.Text != "" &&
                Tbx_Posy.Text != "" && Tbx_Posz.Text != "" && Tbx_Quantity.Text != "")
                return false;

            return true;
        }

        private void Tbx_NomBouteille_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_CodeBouteille.Text != "")
                erreurCode.Visible = false;
            else
                erreurCode.Visible = true;

            if (Tbx_NomBouteille.Text != "")
                erreurDrink.Visible = false;
            else
                erreurDrink.Visible = true;

            if (Tbx_Posx.Text != "")
                erreurPosX.Visible = false;
            else
                erreurPosX.Visible = true;

            if (Tbx_Posy.Text != "")
                erreurPosY.Visible = false;
            else
                erreurPosY.Visible = true;

            if (Tbx_Posz.Text != "")
                erreurPosZ.Visible = false;
            else
                erreurPosZ.Visible = true;

            if (Tbx_Quantity.Text != "")
                erreurQty.Visible = false;
            else
                erreurQty.Visible = true;
        }

        private void Add_Drinks_Load(object sender, EventArgs e)
        {
            db = DataBase.instance_bd;
            erreurCode.Visible = false;
            erreurDrink.Visible = false;
            erreurPosX.Visible = false;
            erreurPosY.Visible = false;
            erreurPosZ.Visible = false;
            erreurQty.Visible = false;
        }
    }
}
