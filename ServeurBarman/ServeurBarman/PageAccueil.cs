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

namespace ServeurBarman
{
    public partial class PageAccueil : MetroFramework.Forms.MetroForm
    {
        static Boolean check;
        static SerialPort port;
        public OracleConnection connexion;
        int count = 0;
        byte[] tab = new byte[] { 0x52, 0x21, 0x05 };
        byte[] tab1 = new byte[] { 0x01, 0x21, 0x21, 0x00, 0x48, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x03, 0x8B };
        byte[] tab2 = new byte[] { 0x06 };
        byte[] tab3 = new byte[] { 0x04 };

        public PageAccueil()
        {
            InitializeComponent();
            connexion = new OracleConnection();
            PBX_EtatDeconnecté.Visible = true;
            check = true;
            port = new SerialPort();

            new Thread(() =>
            {
                while (check)
                {
                    Thread.Sleep(1000);
                    //Show_WaitingDrinksList();
                    Thread.Sleep(1000);
                }
            }).Start();
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
            count = 0;
            string cmd = "select descriptions from ingredient";
            LBX_WaitingList.Items.Clear();
            try
            {
                OracleCommand listeDiv = new OracleCommand(cmd, connexion);
                listeDiv.CommandType = CommandType.Text;
                OracleDataReader divisionReader = listeDiv.ExecuteReader();
                while (divisionReader.Read())
                {
                    LBX_WaitingList.Items.Add(divisionReader.GetString(0));
                    count++;
                }
                divisionReader.Close();
                welcomePage1.nombreClient = count.ToString();
                welcomePage1.Init_UserUI();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }
        }

        private void mBtnConnexionRobot_Click(object sender, EventArgs e)
        {
            BTN_Setting.Enabled = true;
            DLG_Settings.port = port;
            Update_UI();
            Write_To_Robot(); // Envoie des paramètres de configuration au robot
            Read_From_Robot(); // Réception de la réponse du robot suite aux paramètres envoyés
            if (port.IsOpen)
            {
                MessageBox.Show("Connexion robot réussie");
                ConnexionRobot();
            }
            else
            {
                MessageBox.Show("Connexion robot impossible");
                port.Close();
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            connexion.Close();
        }

        private void ConnexionRobot()
        {
            PBX_EtatDeconnecté.Visible = false;
            new Thread(() =>
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

        private void Write_To_Robot()
        {
            port.Write(tab, 0x00, tab.Length);
            Thread.Sleep(100);
            port.Write(tab1, 0x00, tab1.Length);
            Thread.Sleep(100);
            port.Write(tab2, 0x00, tab2.Length);
            Thread.Sleep(100);
            port.Write(tab3, 0x00, tab3.Length);
            Thread.Sleep(100);
            port.Write("NOHELP\r");
            Thread.Sleep(100);
        }

        private void Read_From_Robot()
        {
            byte[] tabre = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            port.Read(tabre, 0x00, tabre.Length);

            if (!tabre.Length.Equals(0))
            {
                Thread.Sleep(100);
            }
            else
            {
                port.Close();
            }
        }

        private void Update_UI()
        {
            port.Open();
            port.BaudRate = 19200;
        }
    }
}
