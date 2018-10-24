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
        public WelcomePage()
        {
            InitializeComponent();
            
        }

        public void Init_UserUI()
        {
            mLB_CustomNumber.Text = nombreClient;
            mLB_NombreDeBouteille.Text = nombreBouteille;
            mLB_NombreDeVerre.Text = nombreVerre;
            mLB_NombreDeShooter.Text = nombreShooter;
            LBX_Activities.Items.Add(activiteRobot);
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
