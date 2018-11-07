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
        }

        public void Init_UserUI()
        {
            while (true)
            {
                //
                this.Invoke((MethodInvoker)(() => mLB_NombreDeBouteille.Text = baseDonnees.NombreIngredients()));
                this.Invoke((MethodInvoker)(() => mLB_NombreDeVerre.Text = baseDonnees.NombreDeVerreRouge()));
                this.Invoke((MethodInvoker)(() => mLB_NombreDeShooter.Text = baseDonnees.NombreDeShooter()));
                this.Invoke((MethodInvoker)(() => mLB_CustomNumber.Text = baseDonnees.ListeCommande().Count.ToString()));

                //this.Invoke((MethodInvoker)(() =>
                //{
                //    if (activiteRobot != null && !activiteRobot.Equals("") && LBX_Activities.Items.Count!=0 )
                //        if(!activiteRobot.Equals(LBX_Activities.Items[0].ToString()))
                //            LBX_Activities.Items.Add(activiteRobot);
                //}));
            }
        }

        private void WelcomePage_Load(object sender, EventArgs e)
        {
            Task.Run(() => Init_UserUI());
        }

        private void TBX_NombreClient_TextChanged(object sender, EventArgs e)
        {
            Init_UserUI();
        }

        private void LBX_Activities_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}
