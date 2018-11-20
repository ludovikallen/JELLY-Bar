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
using System.IO;

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
        bool enService;
        bool servir = false;
        private Task serviceClient = Task.Delay(0);
        private Task arreter = Task.Delay(0);
        string path1, path2;


        public PageAccueil()
        {
            InitializeComponent();
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
                if (serviceClient.IsCompleted)
                {
                    DLG_Settings dlg = new DLG_Settings();
                    DialogResult dlg_result = dlg.ShowDialog();
                }
                else
                {
                    base2Donnees.ErreurBD = "Accès impossible à paramètres, robot en activité!!";
                }
            }
            else
            {
                base2Donnees.ErreurBD = "Robot non connecté...";
            }
        }

        private void BTN_Developers_Click(object sender, EventArgs e)
        {
            Developers dlg = new Developers();
            DialogResult dlg_result = dlg.ShowDialog();
        }

        private void PageAccueil_Load(object sender, EventArgs e)
        {
            service = new Commande();
            robot = CRS_A255.Instance;
            welcomePage1.BringToFront();
            Init_PageAcceuil();
            btn_Servir.Enabled = false;
            fbtn_Halt.Enabled = false;
            btn_Supp.Visible = false;
            LBX_WaitingList.SelectedIndex = -1;

            service.onCommandeEnCours += (s, events) => this.Invoke((MethodInvoker)(() => lb_CommandeEnCours.Text = service.CommandeEnCours));
            base2Donnees.onErreurBD_Detectee += (s, events) => this.Invoke((MethodInvoker)(() =>
            {
                if (lbx_Avertissement.Items.Count != 0)
                {
                    int x = lbx_Avertissement.Items.Count - 1;
                    if (!base2Donnees.ErreurBD.Equals(lbx_Avertissement.Items[x]))
                    {
                        lbx_Avertissement.Items.Add(base2Donnees.ErreurBD);
                        lbx_Avertissement.TopIndex = lbx_Avertissement.Items.Count - 1;
                    }
                }
                else
                {
                    lbx_Avertissement.Items.Add(base2Donnees.ErreurBD);
                    lbx_Avertissement.TopIndex = lbx_Avertissement.Items.Count - 1;
                }

            }));

            base2Donnees.onCommandeChange += (s, events) => this.Invoke((MethodInvoker)(() =>
            {
                foreach (var contenu in numcommande)
                    LBX_WaitingList.Items.Add(contenu.Item1);
            }));
        }

        private void Init_PageAcceuil()
        {
            BTN_Setting.Enabled = true;
            base2Donnees = DataBase.instance_bd;
            path1 = Path.GetFullPath("...\\...\\Resources\\stop1.jpg");
            path2 = Path.GetFullPath("...\\...\\Resources\\stop1Balcked.jpg");

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(500);
                    Refresh_WaitingList();
                    Thread.Sleep(500);
                }
            });
        }

        /// <summary>
        /// Ici, on charge toutes les commandes disponible dans la table commande de la bd,
        /// puis on détermine le nombre de clients.
        /// </summary>
        private void Refresh_WaitingList()
        {
            lock (accessLock)
            {
                if (numcommande.Count != base2Donnees.ListeCommande().Count)
                {
                    this.Invoke((MethodInvoker)(() => LBX_WaitingList.Items.Clear()));
                    numcommande = base2Donnees.ListeCommande();
                    base2Donnees.Numcommande = numcommande;
                }
            }
        }


        // BOUTON DE SUPPRESSION DES COMMANDES DISPONIBLES DANS LA TABLE COMMANDE
        private void Btn_ResetCommande_Click(object sender, EventArgs e)
        {
            LBX_WaitingList.SelectedIndex = -1;
            if (numcommande.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Êtes-vous certain de vouloir vider la liste de commande?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    base2Donnees.SupprimerCommande();
                }
            }
        }

        // FERME LA CONNEXION À LA BASE DE DONNÉES
        private void deconnexion_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void Btn_Servir_Click(object sender, EventArgs e)
        {
            if (estConnecté)
            {
                if (!servir)
                {
                    enService = true;
                    servir = true;
                    btn_Servir.Text = "Arrêter service";

                    serviceClient = Task.Run(() =>
                    {
                        while (enService)
                        {
                            service.ServirClient(numcommande);
                        }
                    });
                }
                else
                {
                    if (arreter.IsCompleted)
                    {
                        arreter = Task.Run(() =>
                        {
                            enService = false; // arrete le thread
                            while (!serviceClient.IsCompleted) { } // attend que le thread arrete
                            servir = false; // active l'option pour repartir le thread
                        });
                    }
                    btn_Servir.Text = "Servir";
                }
            }
            else
            {
                base2Donnees.ErreurBD = "veuillez d'abord, svp connecter le robot!";
            }
        }

        // ICI ON SUUPRIME LA COMMANDE SELECTIONNÉE
        private void btn_Supp_Click(object sender, EventArgs e)
        {
            if (LBX_WaitingList.SelectedIndex != 1)
            {
                var x = LBX_WaitingList.SelectedItem.ToString();
                base2Donnees.SupprimerCommande(int.Parse(x));
            }
            LBX_WaitingList.SelectedIndex = -1;
        }

        private void Btn_ConnexionRobot_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                // On établie la connexion avec le robot
                //while (!robot.Connected)
                //{
                    robot.ConnexionRobot();
                    Thread.Sleep(2000);
                //}
                estConnecté = true;
                this.Invoke((MethodInvoker)(() => LBX_WaitingList.SelectedIndex = -1));
                this.Invoke((MethodInvoker)(() => fbtn_Halt.Enabled = true));
                this.Invoke((MethodInvoker)(() => btn_Servir.Enabled = true));
                this.Invoke((MethodInvoker)(() => btn_ConnexionRobot.Enabled = false));
            });

        }

        private void LBX_WaitingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LBX_WaitingList.SelectedIndex != -1)
                btn_Supp.Visible = true;
            else
                btn_Supp.Visible = false;
        }

        private void Lbx_Avertissement_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbx_Avertissement.SelectedIndex = -1;
        }

        private void PageAccueil_Click(object sender, EventArgs e)
        {
            lbx_Avertissement.SelectedIndex = -1;
        }

        private void fbtn_Halt_Click(object sender, EventArgs e)
        {
            if (estConnecté)
            {
                fbtn_Halt.Enabled = false;
                enService = false;
                estConnecté = false;
                btn_ConnexionRobot.Enabled = true;
                btn_Servir.Enabled = false;
                robot.Deconnexion();
                btn_Servir.Text = "Servir";
                servir = false;
                Task.Run(() =>
                {
                    robot.ConnexionRobot();
                });
            }
        }

        private void fbtn_Halt_EnabledChanged(object sender, EventArgs e)
        {
            if (fbtn_Halt.Enabled)
                fbtn_Halt.BackgroundImage = Image.FromFile(path1);
            else
                fbtn_Halt.BackgroundImage = Image.FromFile(path2);
        }
    }
}