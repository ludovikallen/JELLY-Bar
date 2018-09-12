using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace Bras_Robot
{
    public partial class Form1 : Form
    {
        private CRS_A255 Robot;
        public Form1()
        {
            InitializeComponent();
            Init_UI();
            Robot = CRS_A255.Instance;
        }

        public void Init_UI()
        {
            if (!Directory.Exists("COMMAND"))
                Directory.CreateDirectory("COMMAND");
            else
            {
                string[] Files = Directory.GetFiles("COMMAND", "*.txt");
                for(int i = 0; i < Files.Length; ++i)
                {
                    string line = Files[i];
                    line = line.Remove(0, 8);
                    LB_COMMAND.Items.Add(line);
                }
            }
        }

        // Contourne un bug dans le robot
        // il faut ouvrir deux fois le port pour
        // établir la connexion
        private void BTN_CONNEXION_Click(object sender, EventArgs e)
        {
            //Robot = new CRS_A255();
            BTN_CONNEXION.BackColor = Color.Green;
            Robot.SetSpeed(Robot.Speed);
            NUD_SPEED.Value = Robot.Speed;
        }

        private void BTN_BASE_GAUCHE_Click(object sender, EventArgs e)
        {
            Robot.DeplacerBase((int)TB_BASE_ANGLE.Value * -1);
        }

        private void BTN_BASE_DROITE_Click(object sender, EventArgs e)
        {
            Robot.DeplacerBase((int)TB_BASE_ANGLE.Value);
        }

        private void BTN_EPAULE_GAUCHE_Click(object sender, EventArgs e)
        {
            Robot.DeplacerEpaule((int)TB_EPAULE_ANGLE.Value * -1);
        }

        private void BTN_EPAULE_DROITE_Click(object sender, EventArgs e)
        {
            Robot.DeplacerEpaule((int)TB_EPAULE_ANGLE.Value);
        }

        private void BTN_COUDE_GAUCHE_Click(object sender, EventArgs e)
        {
            Robot.DeplacerCoude((int)TB_COUDE_ANGLE.Value * -1);
        }

        private void BTN_COUDE_DROITE_Click(object sender, EventArgs e)
        {
            Robot.DeplacerCoude((int)TB_COUDE_ANGLE.Value);
        }

        private void BTN_POIGNET_ROTATION_GAUCHE_Click(object sender, EventArgs e)
        {
            Robot.DeplacerPoignet((int)TB_POIGNET_ANGLE.Value * -1);
        }

        private void BTN_POIGNET_ROTATION_DROITE_Click(object sender, EventArgs e)
        {
            Robot.DeplacerPoignet((int)TB_POIGNET_ANGLE.Value);
        }

        private void BTN_MAIN_GAUCHE_Click(object sender, EventArgs e)
        {
            Robot.DeplacerMain((int)TB_MAIN_ANGLE.Value * -1);
        }

        private void BTN_MAIN_DROITE_Click(object sender, EventArgs e)
        {
            Robot.DeplacerMain((int)TB_MAIN_ANGLE.Value);
        }

        private void BTN_PINCE_ACTION_Click(object sender, EventArgs e)
        {
            Robot.OuvrirPince(TB_PINCE_SPEED.Value);
        }

        private void BTN_PINCE_CLOSE_Click(object sender, EventArgs e)
        {
            Robot.FermerPince(TB_PINCE_SPEED.Value);
        }

        private void BTN_HOME_Click(object sender, EventArgs e)
        {
            Robot.Home();
        }

        private void BTN_READY_Click(object sender, EventArgs e)
        {
            Robot.Ready();
        }

        private void BTN_HALT_Click(object sender, EventArgs e)
        {
            Robot.Halt();
        }

        private void BTN_RECORD_Click(object sender, EventArgs e)
        {
            if (BTN_RECORD.Text == "RECORD")
            {
                BTN_RECORD.Text = "DONE";
            }
            else if(BTN_RECORD.Text == "DONE")
            {
                BTN_RECORD.Text = "RECORD";
            }
            Robot.Recording();
        }

        private void BTN_SAVE_Click(object sender, EventArgs e)
        {
            if(Robot.Command != "" && Robot.Record == false)
            {
                using (FileStream fs = File.Create("COMMAND\\" + TB_COMMAND.Text + ".txt"))
                {
                    fs.Close();
                    using (StreamWriter Writer = new StreamWriter("COMMAND\\" + TB_COMMAND.Text + ".txt"))
                    {
                        Writer.WriteLine(Robot.Command);
                        Writer.Close();
                    }
                    LB_COMMAND.Items.Add(TB_COMMAND.Text);
                }
            }
        }

        private void BTN_PLAY_Click(object sender, EventArgs e)
        {
            string line = File.ReadAllText("COMMAND\\" + LB_COMMAND.SelectedItem.ToString());
            string FileText = string.Empty;

            while (!string.IsNullOrEmpty(line))
            {
                FileText = line.Split('\n')[0];
                if (FileText != "\r")
                    Robot.ManuelCommand(FileText);
                System.Threading.Thread.Sleep(200);
                line = line.Remove(0, FileText.Length + 1);
            }
        }
        private void BTN_Ok_Click(object sender, EventArgs e)
        {
            Robot.SetSpeed(NUD_SPEED.Value);
        }

        private void BTN_CONSOLE_Click(object sender, EventArgs e)
        {
            if(TB_CONSOLE.Text != "")
            {
                Robot.ManuelCommand(TB_CONSOLE.Text + "\r");
                LB_CONSOLE.Items.Add(TB_CONSOLE.Text);
                TB_CONSOLE.Text = "";
            }
        }

        private void LB_CONSOLE_SelectedIndexChanged(object sender, EventArgs e)
        {
            TB_CONSOLE.Text = LB_CONSOLE.Text;
        }

        private void BTN_POS_SAVE_Click(object sender, EventArgs e)
        {
            LB_CONSOLE.Items.Add(TB_POS.Text);
            TB_CONSOLE.Text = "";
        }

        private void BTN_POS_EXECUTE_Click(object sender, EventArgs e)
        {

        }

        private void BTN_DECONNEXION_Click(object sender, EventArgs e)
        {
            Robot.Deconnexion();
        }

        private void BTN_Test_Click(object sender, EventArgs e)
        {
            List<string> list =  Robot.TEST();
            foreach(var s in list)
            {
                LB_POS.Items.Add(s);
            }
        }

        private void BTN_Reset_Click(object sender, EventArgs e)
        {
            Robot.GoToStart();
        }

        private void BTN_SetStart_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Pos_Click(object sender, EventArgs e)
        {
            //var bouteille = new CRS_A255.Position(Convert.ToInt32(TB_X.Text), Convert.ToInt32(TB_Y.Text), Convert.ToInt32(TB_Z.Text));
            //Robot.VersPosition(ref bouteille);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}