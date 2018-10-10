using System;
using System.Threading.Tasks;
using Bras_Robot;

namespace ServeurBarman
{
    public partial class DLG_Settings : MetroFramework.Forms.MetroForm
    {
        CRS_A255 robot = CRS_A255.Instance;
        private bool isRunning;
        public DLG_Settings()
        {
            InitializeComponent();

            Btn_Base_Left.MouseDown += (sender, args) =>
            {

            };
            
            Btn_Base_Left.MouseUp += (sender, args) => isRunning = false;
            //Btn_Base_Right;
            //Btn_Epaule_Left;
            //Btn_Epaule_Right;
            

            robot.task = Task.Run(() =>
            {
                while (robot.EnMarche()) { }
                robot.Calibration = true;
            });
        }

        private void BTN_Close_Pliers_Click_1(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.FermerPince(75);
                });
            }
        }

        private void BTN_Open_Pliers_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.OuvrirPince(75);
                });
            }
        }

        private void BTN_Home_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.Home();
                });
            }
        }

        private void Btn_Base_Right_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.DeplacerBase(5);
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void Btn_Base_Left_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.DeplacerBase(-5);
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void DLG_Settings_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            robot.Calibration = false;
        }

        private void Btn_Coude_Left_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.DeplacerCoude(5);
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void Btn_Coude_Right_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.DeplacerCoude(-5);
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void Btn_Epaule_Left_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.DeplacerEpaule(-5);
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void Btn_Epaule_Right_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.DeplacerEpaule(5);
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void Btn_Poignet_Up_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.DeplacerPoignet(5);
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void Btn_Poignet_Down_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.DeplacerPoignet(-5);
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void Btn_Main_Left_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.DeplacerMain(5);
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void Btn_Main_Right_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.DeplacerMain(-5);
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void BTN_Ready_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.Ready();
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (robot.task.IsCompleted)
            {
                robot.task = Task.Run(() =>
                {
                    robot.GoToStart();
                    System.Threading.Thread.Sleep(1000); //TODO
                });
            }
        }

        private void Calibrer_Robot_Click(object sender, EventArgs e)
        {
            robot.CALIBRE();
        }
    }
}
