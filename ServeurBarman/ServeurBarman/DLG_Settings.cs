using System;
using System.Threading.Tasks;
using Bras_Robot;

namespace ServeurBarman
{
    public partial class DLG_Settings : MetroFramework.Forms.MetroForm
    {
        CRS_A255 robot = CRS_A255.Instance;
        Task task;
        public DLG_Settings()
        {
            InitializeComponent();
            task = Task.Run(() =>
            {
                while (robot.EnMarche()) { }
                robot.Calibration = true;
            });
        }

        private void BTN_Close_Pliers_Click_1(object sender, EventArgs e)
        {
            if (task.IsCompleted)
            {
                task = Task.Run(() =>
                {
                    robot.FermerPince(75);
                });
            }
        }

        private void BTN_Open_Pliers_Click(object sender, EventArgs e)
        {
            if (task.IsCompleted)
            {
                task = Task.Run(() =>
                {
                    robot.OuvrirPince(75);
                });
            }
        }

        private void BTN_Home_Click(object sender, EventArgs e)
        {
            if (task.IsCompleted)
            {
                task = Task.Run(() =>
                {
                    robot.Home();
                    System.Threading.Thread.Sleep(8000); //TODO
                });
            }
        }

        private void Btn_Base_Right_Click(object sender, EventArgs e)
        {
            
        }

        private void Btn_Base_Left_Click(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void DLG_Settings_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            robot.Calibration = false;
        }
    }
}
