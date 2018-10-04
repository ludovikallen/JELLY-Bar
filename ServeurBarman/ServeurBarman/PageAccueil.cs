using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServeurBarman
{
    public partial class PageAccueil : Form
    {
        static Boolean check = true;
        List<Drinks> drink = new List<Drinks>();

        public PageAccueil()
        {
            InitializeComponent();
            SidePanel.Height = BTN_Welcome.Height;
            SidePanel.Top = BTN_Welcome.Top;
            PBX_EtatDeconnecté.Visible = false;
            Thread t = new Thread(() => {
                while (check)
                {
                    PBX_EtatConnecté.Visible = true;
                    Thread.Sleep(500);
                    PBX_EtatConnecté.Visible = false;
                    Thread.Sleep(500);
                }
            });

            Thread t1 = new Thread(() =>
              {
                  while (check)
                  {
                      LoadDrinks();
                      Thread.Sleep(1500);
                  }
              });

            t.Start();
            t1.Start();
        }

        private void BTN_Welcome_Click(object sender, EventArgs e)
        {
            SidePanel.Height = BTN_Welcome.Height;
            SidePanel.Top = BTN_Welcome.Top;
            welcomePage1.BringToFront();
        }

        private void BTN_AddDrink_Click(object sender, EventArgs e)
        {
            SidePanel.Height = BTN_AddDrink.Height;
            SidePanel.Top = BTN_AddDrink.Top;
            add_Drinks1.BringToFront();
        }

        private void BTN_AddCup_Click(object sender, EventArgs e)
        {
            SidePanel.Height = BTN_AddCup.Height;
            SidePanel.Top = BTN_AddCup.Top;
            addCups1.BringToFront();
        }

        private void BTN_Setting_Click(object sender, EventArgs e)
        {
            SidePanel.Height = BTN_Setting.Height;
            SidePanel.Top = BTN_Setting.Top;
            setting1.BringToFront();
        }

        private void BTN_Developers_Click(object sender, EventArgs e)
        {
            Developers dlg = new Developers();

            DialogResult dlg_result = dlg.ShowDialog();
        }

        private void PageAccueil_FormClosing(object sender, FormClosingEventArgs e)
        {
            check = false;
        }

        public void LoadDrinks()
        {
            drink = DataBaseAccess.LoadDrink();
            LBX_WaitingList.DataSource = null;
            LBX_WaitingList.DataSource = drink;
            LBX_WaitingList.DisplayMember = ("NomRecette");
        }
    }
}
