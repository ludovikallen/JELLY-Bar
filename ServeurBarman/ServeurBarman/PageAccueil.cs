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
        static Boolean check;
        List<Drinks> drink = new List<Drinks>();
        Liaison lier = new Liaison();
        Thread t1;
        Thread t;

        public PageAccueil()
        {
            InitializeComponent();
            PBX_EtatDeconnecté.Visible = false;
            check = true;

            t = new Thread(() => {
                while (check)
                {
                    PBX_EtatConnecté.Visible = true;
                    Thread.Sleep(500);
                    PBX_EtatConnecté.Visible = false;
                    Thread.Sleep(500);
                }
            });

            t1 = new Thread(() =>
              {
                  while (check)
                  {
                      LoadDrinks();
                      foreach (var d in drink)
                          lier.NombreClient.Add(d.ToString());
                      Thread.Sleep(2000);
                  }
              });

            t.Start();
            t1.Start();
            
        }

        private void BTN_Welcome_Click(object sender, EventArgs e)
        {
            welcomePage1.BringToFront();
        }

        private void BTN_AddDrink_Click(object sender, EventArgs e)
        {
            add_Drinks1.BringToFront();
        }

        private void BTN_AddCup_Click(object sender, EventArgs e)
        {
            addCups1.BringToFront();
        }

        private void BTN_Setting_Click(object sender, EventArgs e)
        {
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
            LBX_WaitingList.DataSource = null;
            LBX_WaitingList.DataSource = drink;
            LBX_WaitingList.DisplayMember = ("NomRecette");
            welcomePage1.Init_UserUI();
        }

        private void PageAccueil_Load(object sender, EventArgs e)
        {
            welcomePage1.BringToFront();
        }
    }
}
