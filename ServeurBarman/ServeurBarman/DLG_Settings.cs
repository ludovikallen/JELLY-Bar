using System;
using System.Threading.Tasks;
using Bras_Robot;

namespace ServeurBarman
{
    public partial class DLG_Settings : MetroFramework.Forms.MetroForm
    {
        CRS_A255 robot = CRS_A255.Instance;
        private bool isRunning = false;
        int angle = 5;
        int wait = 500;
        public DLG_Settings()
        {
            InitializeComponent();

            robot.Calibration = true;

            robot.SetSpeed(75);

            Btn_Base_Left.MouseDown += (sender, args) =>
            {
                isRunning = true;
                Task.Run(() =>
                {
                    while (isRunning)
                    {
                        if (robot.task.IsCompleted)
                        {
                            robot.task = Task.Run(() =>
                            {
                                robot.DeplacerBase(-angle);
                                System.Threading.Thread.Sleep(wait); //TODO
                            });
                        }
                    }
                });
            };
            Btn_Base_Right.MouseDown += (sender, args) =>
            {
                isRunning = true;

                Task.Run(() =>
                {
                    while (isRunning)
                    {
                        if (robot.task.IsCompleted)
                        {
                            robot.task = Task.Run(() =>
                            {
                                robot.DeplacerBase(angle);
                                System.Threading.Thread.Sleep(wait); //TODO
                            });
                        }
                    }
                });
            };
            Btn_Epaule_Left.MouseDown += (sender, args) =>
            {
                isRunning = true;

                Task.Run(() =>
                {
                    while (isRunning)
                    {
                        if (robot.task.IsCompleted)
                        {
                            robot.task = Task.Run(() =>
                            {
                                robot.DeplacerEpaule(-angle);
                                System.Threading.Thread.Sleep(wait); //TODO
                            });
                        }
                    }
                });
            };
            Btn_Epaule_Right.MouseDown += (sender, args) =>
            {
                isRunning = true;

                Task.Run(() =>
                {
                    while (isRunning)
                    {
                        if (robot.task.IsCompleted)
                        {
                            robot.task = Task.Run(() =>
                            {
                                robot.DeplacerEpaule(angle);
                                System.Threading.Thread.Sleep(wait); //TODO
                            });
                        }
                    }
                });
            };
            Btn_Coude_Left.MouseDown += (sender, args) =>
            {
                isRunning = true;

                Task.Run(() =>
                {
                    while (isRunning)
                    {
                        if (robot.task.IsCompleted)
                        {
                            robot.task = Task.Run(() =>
                            {
                                robot.DeplacerCoude(angle);
                                System.Threading.Thread.Sleep(wait); //TODO
                            });
                        }
                    }
                });
                
            };
            Btn_Coude_Right.MouseDown += (sender, args) =>
            {
                isRunning = true;

                Task.Run(() =>
                {
                    while (isRunning)
                    {
                        if (robot.task.IsCompleted)
                        {
                            robot.task = Task.Run(() =>
                            {
                                robot.DeplacerCoude(-angle);
                                System.Threading.Thread.Sleep(wait); //TODO
                            });
                        }
                    }
                });
            };
            Btn_Main_Right.MouseDown += (sender, args) =>
            {
                isRunning = true;

                Task.Run(() =>
                {
                    while (isRunning)
                    {
                        if (robot.task.IsCompleted)
                        {
                            robot.task = Task.Run(() =>
                            {
                                robot.DeplacerMain(-angle);
                                System.Threading.Thread.Sleep(wait); //TODO
                            });
                        }
                    }
                });
                
            };
            Btn_Main_Left.MouseDown += (sender, args) =>
            {
                isRunning = true;

                Task.Run(() =>
                {
                    while (isRunning)
                    {
                        if (robot.task.IsCompleted)
                        {
                            robot.task = Task.Run(() =>
                            {
                                robot.DeplacerMain(angle);
                                System.Threading.Thread.Sleep(wait); //TODO
                            });
                        }
                    }
                });
            };
            Btn_Poignet_Up.MouseDown += (sender, args) =>
            {
                isRunning = true;

                Task.Run(() =>
                {
                    while (isRunning)
                    {
                        if (robot.task.IsCompleted)
                        {
                            robot.task = Task.Run(() =>
                            {
                                robot.DeplacerPoignet(angle);
                                System.Threading.Thread.Sleep(wait); //TODO
                            });
                        }
                    }
                });
            };
            Btn_Poignet_Down.MouseDown += (sender, args) =>
            {
                isRunning = true;

                Task.Run(() =>
                {
                    while (isRunning)
                    {
                        if (robot.task.IsCompleted)
                        {
                            robot.task = Task.Run(() =>
                            {
                                robot.DeplacerPoignet(-angle);
                                System.Threading.Thread.Sleep(wait); //TODO
                            });
                        }
                    }
                });
            };

            Btn_Base_Left.MouseUp += (sender, args) => isRunning = false;
            Btn_Base_Right.MouseUp += (sender, args) => isRunning = false;
            Btn_Epaule_Left.MouseUp += (sender, args) => isRunning = false;
            Btn_Epaule_Right.MouseUp += (sender, args) => isRunning = false;
            Btn_Coude_Left.MouseUp += (sender, args) => isRunning = false;
            Btn_Coude_Right.MouseUp += (sender, args) => isRunning = false;
            Btn_Main_Right.MouseUp += (sender, args) => isRunning = false;
            Btn_Main_Left.MouseUp += (sender, args) => isRunning = false;
            Btn_Poignet_Up.MouseUp += (sender, args) => isRunning = false;
            Btn_Poignet_Down.MouseUp += (sender, args) => isRunning = false;


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

        private void DLG_Settings_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            robot.Calibration = false;
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
        }

        private void btnSpeed_Click(object sender, EventArgs e)
        {
            angle = 1;
            wait = 0;
        }

        private void btnSpeed2x_Click(object sender, EventArgs e)
        {
            angle = 5;
            wait = 500;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            robot.TEST();
        }

        private void Btn_Main_Right_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Main_Left_Click(object sender, EventArgs e)
        {

        }
    }
}
