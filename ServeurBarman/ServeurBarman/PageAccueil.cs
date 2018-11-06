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
        bool estConnecté { get; set; }
        CRS_A255 robot = CRS_A255.Instance;
        private readonly object accessLock = new object();
        List<(int, int)> numcommande = new List<(int, int)>();
        DataBase base2Donnees;
        Commande service;
        string commandeEnCours;
        int item1, item2;
        int compteur;
        bool enService;


        public PageAccueil()
        {
            InitializeComponent();
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
            if (estConnecté && enService)
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
            enService = false;
        }

        private void PageAccueil_Load(object sender, EventArgs e)
        {
            welcomePage1.activiteRobot = "Connexion établie avec la base de donnée...";
            welcomePage1.BringToFront();
            Init_PageAcceuil();
            timer2.Start();
            timer2.Enabled = true;
            lbFinishiCommande.Location = new Point(pnlDonnees.Width - compteur, lbFinishiCommande.Location.Y);
            compteur++;
            btn_Servir.Enabled = false;
        }


        private void Init_PageAcceuil()
        {
            BTN_Setting.Enabled = true;
            base2Donnees = DataBase.instance_bd;
            lbFinishiCommande.Font = new Font("Arial", 30, FontStyle.Bold);

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    Show_WaitingDrinksList();
                    Thread.Sleep(1000);
                }
            });

            Task.Run(() => base2Donnees.ListeCommande());
            timer1.Start();
            timer1.Enabled = true;
        }

        private void Show_WaitingDrinksList()
        {
            if (numcommande.Count != base2Donnees.ListeCommande().Count)
            {
                Refresh_WaitingList();

                foreach (var e in numcommande)
                    this.Invoke((MethodInvoker)(() => LBX_WaitingList.Items.Add(e.Item1)));
            }
        }

        /// <summary>
        /// Ici, on charge toutes les commandes disponible dans la table commande de la bd,
        /// puis on détermine le nombre de clients.
        /// </summary>
        private void Refresh_WaitingList()
        {
            lock (accessLock)
            {
                numcommande.Clear();
                LBX_WaitingList.Items.Clear();
                numcommande = base2Donnees.ListeCommande();
                item1 = numcommande[0].Item1;
                item2 = numcommande[0].Item2;
            }
        }

        /// <summary>
        /// Cette méthode permet de servir le client sur la base des commandes disponible 
        /// dans la liste d'attante tout en distinguant le type de commande
        /// </summary>
        //private void ServirClient()
        //{
        //    while (true)
        //    {
        //        if (item2 == 0)
        //        {
        //            service = new Commande(item2);
        //            var p = service.TypeReel();
        //            if (robot.EnMarche())
        //            {
        //                List<(Position, int)> ing = p.Ingredients(item1);

        //                if (robot.MakeDrink(ing.ToList()))
        //                {
        //                    this.Invoke((MethodInvoker)(() => base2Donnees.SupprimerCommande(item1)));
        //                    this.Invoke((MethodInvoker)(() => lb_CommandeEnCours.Text = item1.ToString()));

        //                    if (commandeEnCours != null)
        //                        this.Invoke((MethodInvoker)(() => lbFinishiCommande.Text = "Commande numéro " + commandeEnCours + " terminée!"));

        //                    commandeEnCours = lb_CommandeEnCours.Text.ToString();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            /*
        //             * IL S'AGIT D'UN SHOOTER
        //             */
        //            service = new Commande(item2);
        //            var p = service.TypeReel();

        //            this.Invoke((MethodInvoker)(() => base2Donnees.SupprimerCommande(LBX_WaitingList.Items[0].ToString())));
        //            if (commandeEnCours != null)
        //                this.Invoke((MethodInvoker)(() => lbFinishiCommande.Text = "Commande numéro " + commandeEnCours + " terminée!"));
        //            commandeEnCours = lb_CommandeEnCours.Text.ToString();
        //        }
        //    }
        //}

        private void mBtnConnexionRobot_Click(object sender, EventArgs e)
        {
            // On établie la connexion avec le robot
            robot.ConnexionRobot();
            System.Threading.Thread.Sleep(2000);
            if (robot.Connected)
            {
                btn_Servir.Enabled = true;
                estConnecté = true;
                MessageBox.Show("Connexion robot réussie");
                System.Threading.Thread.Sleep(5000);
            }
            else
            {
                MessageBox.Show("Connexion robot impossible");
                robot.Deconnexion();
            }
        }

        private void Btn_ResetCommande_Click(object sender, EventArgs e)
        {
            base2Donnees.SupprimerCommande();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (lbFinishiCommande.Location.X == 0)
            {
                compteur = 0;
                lbFinishiCommande.Location = new Point(pnlDonnees.Width - compteur, lbFinishiCommande.Location.Y);
                compteur++;
            }
            else
            {
                lbFinishiCommande.Location = new Point(pnlDonnees.Width - compteur, lbFinishiCommande.Location.Y);
                compteur++;
            }
        }

        private void pbx_Halt_Click(object sender, EventArgs e)
        {
            robot.Halt();
        }

        private void deconnexion_Click(object sender, EventArgs e)
        {
            base2Donnees.FermerConnexion();
        }

        private void btn_Servir_Click(object sender, EventArgs e)
        {
            if (btn_Servir.Text == "Servir" && estConnecté)
            {
                service = new Commande();
                enService = true;
                btn_Servir.Text = "Arrêter service";

                Task.Run(() =>
                {
                    while (enService)
                    {
                        if (Int32.Parse(base2Donnees.NombreDeVerreRouge()) != 0)
                        {
                            if (base2Donnees.Ingredient_Est_Disponible(item1))
                            {
                                if (lb_CommandeEnCours.Text != item1.ToString())
                                {
                                    this.Invoke((MethodInvoker)(() => lb_CommandeEnCours.Text = item1.ToString()));
                                    commandeEnCours = lb_CommandeEnCours.Text.ToString();
                                }
                                service.ServirClient(item1, item2) ;

                                if (commandeEnCours != null && commandeEnCours != "")
                                    this.Invoke((MethodInvoker)(() => lbFinishiCommande.Text = "Commande numéro " + commandeEnCours + " terminée!"));

                                // supprime la commande terminée
                                base2Donnees.SupprimerCommande(item1);

                                this.Invoke((MethodInvoker)(() => lb_CommandeEnCours.Text = ""));
                                commandeEnCours = "";
                                // prépare la nouvelle commande
                                System.Threading.Thread.Sleep(500);

                                //if (tache.IsCompleted)
                                //{
                                //    if (commandeEnCours != null && commandeEnCours != "")
                                //        this.Invoke((MethodInvoker)(() => lbFinishiCommande.Text = "Commande numéro " + commandeEnCours + " terminée!"));

                                //    // supprime la commande terminée
                                //    base2Donnees.SupprimerCommande(item1);

                                //    // prépare la nouvelle commande
                                //    //System.Threading.Thread.Sleep(500);
                                //    //service = new Commande();

                                //    //tache = service.ServirClient(item1, item2);

                                //    //this.Invoke((MethodInvoker)(() => lb_CommandeEnCours.Text = item1.ToString()));
                                //    //commandeEnCours = lb_CommandeEnCours.Text.ToString();
                                //}
                            }
                        }
                    }
                });
            }
            else
            {
                enService = false;
                btn_Servir.Text = "Servir";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();

            int one = rand.Next(0, 255);
            int two = rand.Next(0, 255);
            int three = rand.Next(0, 255);
            int four = rand.Next(0, 255);
            lB_DateTime.Text = DateTime.Now.ToString("hh:mm:ss");
            lbFinishiCommande.ForeColor = Color.FromArgb(one, two, three, four);
        }
    }
}