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
using System.Speech.Synthesis;

namespace ServeurBarman
{

    public partial class PageAccueil : MetroFramework.Forms.MetroForm
    {
        bool estConnecté { get; set; }
        CRS_A255 robot;
        private readonly object accessLock = new object();
        List<(int, int)> numcommande = new List<(int, int)>();
        DataBase base2Donnees;
        Commande service;
        string commandePrecedente;
        int item1, item2;
        int compteur;
        bool enService;
        SpeechSynthesizer read;


        public PageAccueil()
        {
            InitializeComponent();
            welcomePage1.activiteRobot = "Ingrédient de la commande numéro " + item1.ToString() + " non disponible";
        }

        private void BTN_Welcome_Click(object sender, EventArgs e)
        {
            welcomePage1.BringToFront();
        }

        private void BTN_AddCup_Click(object sender, EventArgs e)
        {
            addCups1.BringToFront();
        }

        private void BTN_Setting_Click(object sender, EventArgs e)
        {
            if (estConnecté)
            {
                if (!enService)
                {
                    DLG_Settings dlg = new DLG_Settings();
                    DialogResult dlg_result = dlg.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Impossible d'atteindre Paramètres, car le robot est en activité!!");
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
            enService = false;
        }

        private void PageAccueil_Load(object sender, EventArgs e)
        {
            read = new SpeechSynthesizer();
            welcomePage1.BringToFront();
            Init_PageAcceuil();
            timer2.Start();
            timer2.Enabled = true;
            lbFinishiCommande.Location = new Point(pnlDonnees.Width - compteur, lbFinishiCommande.Location.Y);
            compteur++;
            btn_Servir.Enabled = false;
            pbx_Halt.Enabled = false;
            robot = CRS_A255.Instance;
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
                    Thread.Sleep(500);
                    Show_WaitingDrinksList();
                    Thread.Sleep(500);
                }
            });

            //Task.Run(() => base2Donnees.ListeCommande());
            timer1.Start();
            timer1.Enabled = true;
        }

        // AFFICHE DANS L'INTERFACE LA LISTE DES COMMANDES EN ATTENTE
        private void Show_WaitingDrinksList()
        {
            if (numcommande.Count != base2Donnees.ListeCommande().Count)
            {
                Refresh_WaitingList();

                foreach (var e in numcommande)
                    this.Invoke((MethodInvoker)(() => LBX_WaitingList.Items.Add(e.Item1)));
            }
            //else if (base2Donnees.ListeCommande().Count == 0)
            //{
            //    LBX_WaitingList.Items.Clear();
            //}
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
                this.Invoke((MethodInvoker)(() => LBX_WaitingList.Items.Clear()));
                numcommande = base2Donnees.ListeCommande();
                if (numcommande.Count != 0)
                {
                    item1 = numcommande[0].Item1;
                    item2 = numcommande[0].Item2;
                }
            }
        }

        /// <summary>
        /// Cette méthode permet de servir le client sur la base des commandes disponible 
        /// dans la liste d'attante tout en distinguant le type de commande
        /// </summary>
        private void ServirClient()
        {
            while (enService)
            {
                if (base2Donnees.Ingredient_Est_Disponible(item1))
                {
                    if (item2 == 0)
                    {
                        if (Int32.Parse(base2Donnees.NombreDeVerreRouge()) != 0)
                        {
                            service = new Commande(item2);
                            var p = service.TypeReel();
                            if (robot.EnMarche())
                            {
                                List<(Position, int)> ing = p.Ingredients(item1);

                                if (robot.MakeDrink(ing.ToList()))
                                {
                                    read.SpeakAsync("Commande normale numéro " + item1.ToString() + " en cours");
                                    base2Donnees.SupprimerCommande(item1);
                                    this.Invoke((MethodInvoker)(() => lb_CommandeEnCours.Text = item1.ToString()));

                                    if (commandePrecedente != null)
                                    {
                                        this.Invoke((MethodInvoker)(() => lbFinishiCommande.Text = "Commande numéro " + commandePrecedente + " terminée!"));
                                        read.SpeakAsync(lbFinishiCommande.Text);
                                    }

                                    this.Invoke((MethodInvoker)(() => commandePrecedente = lb_CommandeEnCours.Text.ToString()));
                                }
                            }
                            while (!robot.EnMarche()) ; // ON S'ASSURE QUE LE ROBOT TERMINE LA TACHE EN COURS

                            this.Invoke((MethodInvoker)(() => lb_CommandeEnCours.Text = ""));

                            // ACTIONS NECESSAIRES SUR LA DERNIÈRE COMMANDE
                            if (numcommande.Count == 0)
                            {
                                read.SpeakAsync("Commande numéro " + commandePrecedente + " terminée!");
                                commandePrecedente = null;
                            }
                        }
                        else
                        {
                            welcomePage1.activiteRobot = "Plus de verres rouge, veuillez en ajouter svp!";
                            read.SpeakAsync("Manque de verres rouge");
                        }
                    }
                    else if(item2==1)
                    {
                        /*
                         * IL S'AGIT D'UN SHOOTER
                         */
                        if (base2Donnees.VerreShooterSuffisant(item1))
                        {
                            service = new Commande(item2);
                            var p = service.TypeReel();
                            List<(Position, int)> ing = p.Ingredients(item1);
                            if (robot.MakeDrink(ing.ToList()))
                            {
                                read.SpeakAsync("Shooter numéro " + item1.ToString() + " en cours");
                                base2Donnees.SupprimerCommande(item1);
                                lb_CommandeEnCours.Text = item1.ToString();

                                if (commandePrecedente != null)
                                {
                                    this.Invoke((MethodInvoker)(() => lbFinishiCommande.Text = "Commande numéro " + commandePrecedente + " terminée!"));
                                    read.SpeakAsync(lbFinishiCommande.Text);
                                }
                                commandePrecedente = lb_CommandeEnCours.Text.ToString();
                            }
                        }
                        else
                        {
                            welcomePage1.activiteRobot = "Nombre de verre shooter insuffisant";
                            read.SpeakAsync(welcomePage1.activiteRobot);
                        }
                    }
                }
                else
                {
                    base2Donnees.SupprimerCommande(item1);
                    welcomePage1.activiteRobot = "Ingrédient de la commande numéro " + item1.ToString() + " non disponible";
                    read.SpeakAsync("Commande numéro " + item1.ToString() + " supprimé");
                }
            }
        }

        // BOUTON DE CONNEXION AU ROBOT
        private void mBtnConnexionRobot_Click(object sender, EventArgs e)
        {
            // On établie la connexion avec le robot
            robot.ConnexionRobot();
            System.Threading.Thread.Sleep(2000); // CECI PERMET DE S'ASSURER QUE LE ROBOT SE CONNECTE PARFAITEMENT 
                                                 // SINON UNE EXCEPTION EST LEVÉE
            if (robot.Connected)
            {
                estConnecté = true;
                pbx_Halt.Enabled = true;
                btn_Servir.Enabled = true;
                mBtnConnexionRobot.Enabled = false;
            }
            else
            {
                welcomePage1.activiteRobot = "Connexion robot impossible";
                robot.Deconnexion();
            }
        }

        // BOUTON DE SUPPRESSION DES COMMANDES DISPONIBLES DANS LA TABLE COMMANDE
        private void Btn_ResetCommande_Click(object sender, EventArgs e)
        {
            if (numcommande.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Êtes-vous certain de vouloir vider la liste de commande?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    base2Donnees.SupprimerCommande();
                }
            }
            else
            {
                welcomePage1.activiteRobot = "Connexion robot impossible";
            }
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

        // ARRET D'URGENCE DU ROBOT LORSQUE LE BOUTON EST CLICKÉ 
        private void pbx_Halt_Click(object sender, EventArgs e)
        {
            if (estConnecté)
                robot.Halt();
        }

        // FERME LA CONNEXION À LA BASE DE DONNÉES
        private void deconnexion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Servir_Click(object sender, EventArgs e)
        {
            if (estConnecté)
            {
                if (btn_Servir.Text == "Servir")
                {
                    //service = new Commande();
                    enService = true;
                    btn_Servir.Text = "Arrêter service";
                    Task.Run(() => ServirClient());
                }
                else
                {
                    enService = false;
                    btn_Servir.Text = "Servir";
                }
            }
            else
            {
                MessageBox.Show("veuillez d'abord connecter le robot svp");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Random rand = new Random();
            //int one = rand.Next(0, 255);
            //int two = rand.Next(0, 255);
            //int three = rand.Next(0, 255);
            //int four = rand.Next(0, 255);
            lB_DateTime.Text = DateTime.Now.ToString("hh:mm:ss");
            //lbFinishiCommande.ForeColor = Color.FromArgb(one, two, three, four);
        }
    }
}