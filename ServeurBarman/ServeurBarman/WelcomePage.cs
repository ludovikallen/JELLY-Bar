using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ServeurBarman
{
    public partial class WelcomePage : UserControl
    {

        public string nombreClient;
        public string nombreBouteille;
        public string nombreVerre;
        public string nombreShooter;
        public string activiteRobot;
        DataBase baseDonnees;
        public WelcomePage()
        {
            InitializeComponent();
            baseDonnees = DataBase.instance_bd;
            Task.Run(()=> Init_UserUI());

        }

        public void Init_UserUI()
        {
            while (true)
            {
                mLB_CustomNumber.Text = baseDonnees.ListeCommande().Count.ToString();
                mLB_NombreDeBouteille.Text = baseDonnees.NombreIngredients();
                mLB_NombreDeVerre.Text = baseDonnees.NombreDeVerreRouge();
                mLB_NombreDeShooter.Text = baseDonnees.NombreDeShooter();
            }
            //LBX_Activities.Items.Add(activiteRobot);
        }

        private void WelcomePage_Load(object sender, EventArgs e)
        {

        }

        private void TBX_NombreClient_TextChanged(object sender, EventArgs e)
        {
            Init_UserUI();
        }
    }
}
