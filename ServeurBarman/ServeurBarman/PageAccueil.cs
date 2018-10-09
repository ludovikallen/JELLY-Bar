﻿using Oracle.ManagedDataAccess.Client;
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
        CRS_A255 robot;
        List<string> commande = new List<string>();
        List<(Position,int)> commandeRobot=new List<(Position, int)>();


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
                    //Show_WaitingDrinksList();
                    Thread.Sleep(1000);
                }
            });
        }

        private void BTN_Welcome_Click(object sender, EventArgs e)
        {
            welcomePage1.BringToFront();
            Show_WaitingDrinksList();
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
                if (LBX_WaitingList.Items.Count == 0 || LBX_WaitingList.Items.Count != commande.Count)
                {
                    commande.Clear();
                    LBX_WaitingList.Items.Clear();
                    Refresh_WaitingList();
                    foreach (var e in commande)
                        LBX_WaitingList.Items.Add(e);
                }
                else
                {
                    commande.Clear();
                    Refresh_WaitingList();
                    commande.Sort();

                    for (int i = 0; i < commande.Count; ++i)
                    {
                        if (LBX_WaitingList.Items.Count != commande.Count)
                        {
                            commande.Clear();
                            LBX_WaitingList.Items.Clear();
                            Refresh_WaitingList();
                            foreach (var e in commande)
                                LBX_WaitingList.Items.Add(e);
                        }
                        else if (!LBX_WaitingList.Items[i].Equals(commande[i]))
                            check1 = false;
                    }

                    if (!check1)
                    {
                        LBX_WaitingList.Items.Clear();
                        foreach (var e in commande)
                            LBX_WaitingList.Items.Add(e);
                    }
                }
        }


        private void Refresh_WaitingList()
        {
            count = 0;
            string cmd = "select e.descriptions, e.POSITIONX,e.POSITIONY,e.POSITIONZ,c.QTY from ingredient e inner join commande c on e.codebouteille=c.ingredient";
            try
            {
                OracleCommand listeDiv = new OracleCommand(cmd, connexion);
                listeDiv.CommandType = CommandType.Text;
                OracleDataReader divisionReader = listeDiv.ExecuteReader();
                Console.WriteLine(divisionReader.ToString());
                while (divisionReader.Read())
                {
                    count++;
                    commande.Add(divisionReader.GetString(0));
                    // Une tuple contenant l'emplacement et la quantité de l'ingrédient
                    commandeRobot.Add((new Position(divisionReader.GetInt32(1), divisionReader.GetInt32(2), divisionReader.GetInt32(3)), divisionReader.GetInt32(4)));
                }
                
                divisionReader.Close();
                welcomePage1.nombreClient = count.ToString();
                welcomePage1.Init_UserUI();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }
        }

        private void ServirClient()
        {
            foreach(var e in commandeRobot)
            {
                robot.MakeDrink(commandeRobot);
            }
        }

        private void mBtnConnexionRobot_Click(object sender, EventArgs e)
        {
            BTN_Setting.Enabled = true;
            robot.Connexion();  
            if (robot.Connected)
            {
                MessageBox.Show("Connexion robot réussie");
                ConnexionRobot();
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
            Task.Run(() =>
            {
                while (check)
                {
                    PBX_EtatConnecté.Visible = true;
                    Thread.Sleep(500);
                    PBX_EtatConnecté.Visible = false;
                    Thread.Sleep(500);  
                }
            }).Start();
        }

    }
}
