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
        bool EstConnecté { get; set; }
        static bool check;
        public OracleConnection connexion;
        List<(Position, int)> listeIngredients = new List<(Position, int)>();
        List<List<(Position, int)>> ListcommandeRobot = new List<List<(Position, int)>>();
        CRS_A255 robot = CRS_A255.Instance;
        List<(Position, int)> list = new List<(Position, int)>();
        private readonly object accessLock = new object();
        List<(int, int)> numcommande = new List<(int, int)>();
        DataBase b;
        Commande comm;
        

        public PageAccueil()
        {
            InitializeComponent();
            b = DataBase.instance_bd;
            

            connexion = new OracleConnection();
            PBX_EtatDeconnecté.Visible = true;
            check = true;
            Task.Run(() =>
            {
                while (check)
                {
                    Thread.Sleep(1000);
                    Show_WaitingDrinksList();
                    Thread.Sleep(1000);
                }
            });
            welcomePage1.activiteRobot = "Connexion établie avec la base de donnée...";
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
            if (EstConnecté)
            {
                DLG_Settings dlg = new DLG_Settings();
                DialogResult dlg_result = dlg.ShowDialog();
            }
            else
            {
                MessageBox.Show("Robot non connecté...");
            }
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
            BTN_Setting.Enabled = true;
        }
        private void Show_WaitingDrinksList()
        {
            Boolean check1 = true;
            if (check)
                // Premiere vérificcation de la liste de commande 
                if (LBX_WaitingList.Items.Count == 0)
                {
                    numcommande.Clear();
                    this.Invoke((MethodInvoker)(() => LBX_WaitingList.Items.Clear()));
                    Refresh_WaitingList();
                    foreach (var e in numcommande)
                    {
                        this.Invoke((MethodInvoker)(() => LBX_WaitingList.Items.Add(e.Item1)));
                    }
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
                            this.Invoke((MethodInvoker)(() => LBX_WaitingList.Items.Clear()));
                            Refresh_WaitingList();
                            foreach (var e in numcommande)
                            {
                                this.Invoke((MethodInvoker)(() => LBX_WaitingList.Items.Add(e.Item1)));
                            }
                        }
                        else if (!LBX_WaitingList.Items[i].Equals(numcommande[i].Item1))
                            check1 = false;
                    }

                    if (!check1)
                    {
                        this.Invoke((MethodInvoker)(() => LBX_WaitingList.Items.Clear()));
                        foreach (var e in numcommande)
                        {
                            this.Invoke((MethodInvoker)(() => LBX_WaitingList.Items.Add(e.Item1)));
                        }
                    }
                }
        }

        // Ici, on charge toutes les commandes disponible dans la table commande de la bd,
        // puis on détermine le nombre de clients.
        private void Refresh_WaitingList()
        {
            numcommande = b.ListeCommande();
        }

        /// <summary>
        /// Cette méthode permet de servir le client sur la base des commandes disponible 
        /// dans la liste d'attante
        /// </summary>
        private void ServirClient()
        {

            while (true)
            {
                if (Int32.Parse(welcomePage1.nombreVerre) >= 1)
                {
                    if (numcommande[0].Item2 == 0)
                    {
                        var p = comm.TypeReel();
                        if (robot.EnMarche())
                        {
                            if (robot.MakeDrink(p.ListerIngredients(numcommande[0].Item1)))
                            {
                                string cmd = "delete from commande where numcommande=" + LBX_WaitingList.Items[0].ToString();
                                listeIngredients.Clear();

                                try
                                {
                                    OracleCommand delete = new OracleCommand(cmd, b.EtatBaseDonnées);
                                    delete.CommandType = CommandType.Text;
                                    delete.ExecuteNonQuery();
                                }
                                catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

                                robot.AjouterCup(Int32.Parse(welcomePage1.nombreVerre));
                                string updateVerreRougeCommand = "update verrerouge set nbverre =" + welcomePage1.nombreVerre;
                                try
                                {
                                    OracleCommand updateVerreRouge = new OracleCommand(updateVerreRougeCommand, b.EtatBaseDonnées);
                                    updateVerreRouge.CommandType = CommandType.Text;
                                    updateVerreRouge.ExecuteNonQuery();
                                }
                                catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }
                            }
                        }
                    }
                    else
                    {
                        /*
                         * IL S'AGIT D'UN SHOOTER
                         */
                        var p = comm.TypeReel(); 
                        
                        
                        string cmd = "delete from commande where numcommande=" + LBX_WaitingList.Items[0].ToString();
                        listeIngredients.Clear();

                        try
                        {
                            OracleCommand delete = new OracleCommand(cmd, b.EtatBaseDonnées);
                            delete.CommandType = CommandType.Text;
                            delete.ExecuteNonQuery();
                        }
                        catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

                        welcomePage1.nombreShooter = (Int32.Parse(welcomePage1.nombreShooter) - 1).ToString();

                        string updateVerreRougeCommand = "update verreshooter set nbshooter =" + welcomePage1.nombreShooter;
                        try
                        {
                            OracleCommand updateVerreRouge = new OracleCommand(updateVerreRougeCommand, b.EtatBaseDonnées);
                            updateVerreRouge.CommandType = CommandType.Text;
                            updateVerreRouge.ExecuteNonQuery();
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
                EstConnecté = true;
                MessageBox.Show("Connexion robot réussie");
                //BTN_Setting.Enabled = true;
                Task.Run(() => FlashLight());
                Task.Run(() => ServirClient());
            }
            else
            {
                MessageBox.Show("Connexion robot impossible");
                robot.Deconnexion();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            b.FermerConnexion();
        }

        private void FlashLight()
        {
            this.Invoke((MethodInvoker)(() => PBX_EtatDeconnecté.Visible = false));
            while (true)
            {
                this.Invoke((MethodInvoker)(() => PBX_EtatConnecté.Visible = true));
                Thread.Sleep(1000);
                this.Invoke((MethodInvoker)(() => PBX_EtatConnecté.Visible = false));
                Thread.Sleep(1000);
            }
        }

        private void BtnResetCommande_Click(object sender, EventArgs e)
        {
            string cmd = "delete from commande";
            try
            {
                OracleCommand delete = new OracleCommand(cmd, b.EtatBaseDonnées);
                delete.CommandType = CommandType.Text;
                delete.ExecuteNonQuery();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }
        }
    }
}