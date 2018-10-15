using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bras_Robot;

namespace ServeurBarman
{
    public partial class PageAccueil : MetroFramework.Forms.MetroForm
    {
        static Boolean check;
        public OracleConnection connexion;
        int count = 0;
        List<(Position, int)> listeIngredients = new List<(Position, int)>();
        List<List<(Position, int)>> ListcommandeRobot = new List<List<(Position, int)>>();
        CRS_A255 robot = CRS_A255.Instance;
        List<(Position, int)> list = new List<(Position, int)>();
        List<int> numcommande = new List<int>();

        public PageAccueil()
        {
            InitializeComponent();
            connexion = new OracleConnection();
            PBX_EtatDeconnecté.Visible = true;
            check = true;
            new Thread(async () =>
            {
                while (check)
                {
                    await Task.Delay(1000);
                    Show_WaitingDrinksList();
                    await Task.Delay(1000);
                }
            }).Start();
        }

        private void BTN_Welcome_Click(object sender, EventArgs e)
        {
            welcomePage1.BringToFront();
            //Show_WaitingDrinksList();
        }

        private void BTN_AddDrink_Click(object sender, EventArgs e)
        {
            add_Drinks1.connexion = connexion;
            add_Drinks1.BringToFront();
        }

        private void BTN_AddCup_Click(object sender, EventArgs e)
        {
            addCups1.BringToFront();
        }

        private void BTN_Setting_Click(object sender, EventArgs e)
        {
            DLG_Settings dlg = new DLG_Settings();
            DialogResult dlg_result = dlg.ShowDialog();
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

        private void PageAccueil_Load(object sender, EventArgs e)
        {
            welcomePage1.BringToFront();
            Init_PageAcceuil();
        }

        private void Init_PageAcceuil()
        {
            BTN_Setting.Enabled = false;
        }

        private void Show_WaitingDrinksList()
        {
            Boolean check1 = true;
            if (check)
                // Premiere vérificcation de la liste de commande 
                if (LBX_WaitingList.Items.Count == 0)
                {
                    numcommande.Clear();
                    LBX_WaitingList.Items.Clear();
                    Refresh_WaitingList();
                    foreach (var e in numcommande)
                        LBX_WaitingList.Items.Add(e);
                }
                else
                {
                    // Ici, on vérifie si une nouvelle commande a été ajoutée à la liste d'attente
                    numcommande.Clear();
                    Refresh_WaitingList();

                    for (int i = 0; i < numcommande.Count; ++i)
                    {
                        if (LBX_WaitingList.Items.Count != numcommande.Count)
                        {
                            numcommande.Clear();
                            LBX_WaitingList.Items.Clear();
                            Refresh_WaitingList();
                            foreach (var e in numcommande)
                                LBX_WaitingList.Items.Add(e);
                        }
                        else if (!LBX_WaitingList.Items[i].Equals(numcommande[i]))
                            check1 = false;
                    }

                    if (!check1)
                    {
                        LBX_WaitingList.Items.Clear();
                        foreach (var e in numcommande)
                            LBX_WaitingList.Items.Add(e);
                    }
                }
        }

        // Ici, on charge toutes les commandes disponible dans la table commande de la bd,
        // puis on détermine le nombre de clients.
        private void Refresh_WaitingList()
        {
            NumeroCommande();
            ListerIngredients();
        }

        private void NumeroCommande()
        {
            string cmd = "select numcommande from commande";
            try
            {
                OracleCommand listeDiv = new OracleCommand(cmd, connexion);
                listeDiv.CommandType = CommandType.Text;
                OracleDataReader divisionReader = listeDiv.ExecuteReader();
                while (divisionReader.Read())
                {
                    numcommande.Add(divisionReader.GetInt32(0));
                }
                divisionReader.Close();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }
        }


        private void ListerIngredients()
        {
            count = 0;
            for (int i = 0; i < numcommande.Count; ++i)
            {
                for (int j = 0; j < numcommande.Count; ++j)
                    if (numcommande[i] == numcommande[j] && i != j)
                        numcommande.Remove(numcommande[j]);
            }

            if (numcommande.Count > 0)
            {
                string cmd = "select e.POSITIONX,e.POSITIONY,e.POSITIONZ,c.QTY from ingredient e inner join commande c on e.codebouteille=c.ingredient where c.numcommande=" + numcommande[0].ToString();
                OracleCommand listeDiv = new OracleCommand(cmd, connexion);
                listeDiv.CommandType = CommandType.Text;
                OracleDataReader divisionReader = listeDiv.ExecuteReader();
                try
                {
                    while (divisionReader.Read())
                    {
                        listeIngredients.Add((new Position(divisionReader.GetInt32(0), divisionReader.GetInt32(1), divisionReader.GetInt32(2)), divisionReader.GetInt32(3)));
                    }
                    divisionReader.Close();

                    welcomePage1.nombreClient = numcommande.Count.ToString();
                    welcomePage1.Init_UserUI();
                }
                catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }
            }
        }

        /// <summary>
        /// Cette méthode permet de servir le client sur la base des commandes disponible 
        /// dans la liste d'attante
        /// </summary>
        private void ServirClient()
        {
            while (true)
            {
                if (numcommande.Count > 0)
                {
                    if (robot.EnMarche())
                    {
                        robot.MakeDrink(listeIngredients);

                        string cmd = "delete from commande where numcommande=" + numcommande[0].ToString();
                        listeIngredients.Clear();

                        try
                        {
                            OracleCommand delete = new OracleCommand(cmd, connexion);
                            delete.CommandType = CommandType.Text;
                            delete.ExecuteNonQuery();
                        }
                        catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }
                    }
                }
            }
        }

        private void mBtnConnexionRobot_Click(object sender, EventArgs e)
        {
            // On établie la connexion avec le robot
            robot.ConnexionRobot();
            System.Threading.Thread.Sleep(2000);
            if (robot.Connected)
            {
                MessageBox.Show("Connexion robot réussie");
                BTN_Setting.Enabled = true;
                ConnexionRobot();
                ServirClient();
            }
            else
            {
                MessageBox.Show("Connexion robot impossible");
                robot.Deconnexion();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            connexion.Close();
        }

        private void ConnexionRobot()
        {
            PBX_EtatDeconnecté.Visible = false;
            Task.Run(async () =>
            {
                while (check)
                {
                    PBX_EtatConnecté.Visible = true;
                    await Task.Delay(1000);
                    PBX_EtatConnecté.Visible = false;
                    await Task.Delay(1000);
                }
            });
        }
    }
}