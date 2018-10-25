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
        int nombreDeverre;
        private readonly object accessLock = new object();
        List<(int, int)> numcommande = new List<(int, int)>();

        public PageAccueil()
        {
            InitializeComponent();

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
            add_Drinks1.connexion = connexion;
            add_Drinks1.BringToFront();
        }

        private void BTN_AddCup_Click(object sender, EventArgs e)
        {
            addCups1.connexion = connexion;
            addCups1.BringToFront();
        }

        private void BTN_Setting_Click(object sender, EventArgs e)
        {
            if (EstConnecté)
            {
                DLG_UserIdentify dlg_User = new DLG_UserIdentify();
                DialogResult dlg_User_result = dlg_User.ShowDialog();

                if (dlg_User_result == DialogResult.OK)
                {
                    if (dlg_User.check && EstConnecté)
                    {
                        DLG_Settings dlg = new DLG_Settings();
                        DialogResult dlg_result = dlg.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Vérifier votre connexion ou mot de passe...");
                    }
                }
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
            NumeroCommande();
            ListerIngredients();
            NombreDeDrinks();
        }

        private void NumeroCommande()
        {
            string cmd = "select numcommande,shooter from commande";
            try
            {
                OracleCommand listeDiv = new OracleCommand(cmd, connexion);
                listeDiv.CommandType = CommandType.Text;
                OracleDataReader divisionReader = listeDiv.ExecuteReader();
                while (divisionReader.Read())
                {
                    numcommande.Add((divisionReader.GetInt32(0), divisionReader.GetInt32(1)));
                }
                divisionReader.Close();
                listeIngredients.Clear();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }
        }


        private void ListerIngredients()
        {
            for (int i = 0; i < numcommande.Count; ++i)
            {
                for (int j = 0; j < numcommande.Count; ++j)
                    if (numcommande[i].Item1 == numcommande[j].Item1 && i != j)
                        numcommande.Remove(numcommande[j]);
            }

            lock (accessLock)
            {
                if (numcommande.Count > 0)
                {
                    string cmd = "select e.POSITIONX,e.POSITIONY,e.POSITIONZ,c.QTY from ingredient e inner join commande c on e.codebouteille=c.ingredient where c.numcommande=" + numcommande[0].Item1.ToString();
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

                        this.Invoke((MethodInvoker)(() => welcomePage1.Init_UserUI()));
                    }
                    catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }
                }
            }
        }

        private void NombreDeDrinks()
        {
            /*
             * NOMBRE DE BOUTEILLE
             */
            string cmd = "select count(*) from ingredient";
            OracleCommand listeDiv = new OracleCommand(cmd, connexion);
            listeDiv.CommandType = CommandType.Text;
            OracleDataReader divisionReader = listeDiv.ExecuteReader();
            try
            {
                while (divisionReader.Read())
                {
                    welcomePage1.nombreBouteille = divisionReader.GetInt32(0).ToString();
                }
                divisionReader.Close();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            /*
             * NOMBRE DE VERRE ROUGE
             */
            string cmd1 = "select nbverre from verrerouge";
            OracleCommand listeDiv1 = new OracleCommand(cmd1, connexion);
            listeDiv1.CommandType = CommandType.Text;
            OracleDataReader divisionReader1 = listeDiv1.ExecuteReader();
            try
            {
                while (divisionReader1.Read())
                {
                    welcomePage1.nombreVerre = divisionReader1.GetInt32(0).ToString();
                }
                divisionReader1.Close();
                robot.NbCup = Int32.Parse(welcomePage1.nombreVerre);
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            /*
             * NOMBRE DE SHOOTER
             */
            string cmd2 = "select nbshooter from verreshooter";
            OracleCommand listeDiv2 = new OracleCommand(cmd2, connexion);
            listeDiv2.CommandType = CommandType.Text;
            OracleDataReader divisionReader2 = listeDiv2.ExecuteReader();
            try
            {
                while (divisionReader2.Read())
                {
                    welcomePage1.nombreShooter = divisionReader2.GetInt32(0).ToString();
                }
                divisionReader1.Close();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            this.Invoke((MethodInvoker)(() => welcomePage1.Init_UserUI()));
        }

        /// <summary>
        /// Cette méthode permet de servir le client sur la base des commandes disponible 
        /// dans la liste d'attante
        /// </summary>
        private void ServirClient()
        {
            while (true)
            {
                if (Int32.Parse(welcomePage1.nombreVerre) >= 1 && listeIngredients.Count >= 1)
                {
                    if (numcommande[0].Item2 == 0)
                    {
                        if (robot.EnMarche())
                        {
                            welcomePage1.activiteRobot = "Commande en cours de service...";

                            if(robot.MakeDrink(listeIngredients))
                            {
                                string cmd = "delete from commande where numcommande=" + LBX_WaitingList.Items[0].ToString();
                                listeIngredients.Clear();

                                try
                                {
                                    OracleCommand delete = new OracleCommand(cmd, connexion);
                                    delete.CommandType = CommandType.Text;
                                    delete.ExecuteNonQuery();
                                }
                                catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

                                welcomePage1.nombreVerre = (Int32.Parse(welcomePage1.nombreVerre) - 1).ToString();

                                robot.NbCup = Int32.Parse(welcomePage1.nombreVerre);
                                string updateVerreRougeCommand = "update verrerouge set nbverre =" + welcomePage1.nombreVerre;
                                try
                                {
                                    OracleCommand updateVerreRouge = new OracleCommand(updateVerreRougeCommand, connexion);
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

                        string cmd = "delete from commande where numcommande=" + LBX_WaitingList.Items[0].ToString();
                        listeIngredients.Clear();

                        try
                        {
                            OracleCommand delete = new OracleCommand(cmd, connexion);
                            delete.CommandType = CommandType.Text;
                            delete.ExecuteNonQuery();
                        }
                        catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

                        welcomePage1.nombreShooter = (Int32.Parse(welcomePage1.nombreShooter) - 1).ToString();

                        string updateVerreRougeCommand = "update verreshooter set nbshooter =" + welcomePage1.nombreShooter;
                        try
                        {
                            OracleCommand updateVerreRouge = new OracleCommand(updateVerreRougeCommand, connexion);
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
            connexion.Close();
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
                OracleCommand delete = new OracleCommand(cmd, connexion);
                delete.CommandType = CommandType.Text;
                delete.ExecuteNonQuery();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }
        }
    }
}