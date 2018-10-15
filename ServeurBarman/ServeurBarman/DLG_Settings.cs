﻿using System;
using System.Threading.Tasks;
using Bras_Robot;

namespace ServeurBarman
{
    public partial class DLG_Settings : MetroFramework.Forms.MetroForm
    {
        CRS_A255 robot = CRS_A255.Instance;
        private bool isRunning = false;
        private int speed = 5;
        public DLG_Settings()
        {
            InitializeComponent();

            Btn_Base_Left.MouseDown += (sender, args) =>
            {
                isRunning = true;
                Task.Run(() =>
                {
                    while (isRunning)
                    {
                        if (robot.task.IsCompleted)
                        {
                            robot.task = Task.Run(async () =>
                            {
                                robot.DeplacerBase(speed);
                                await Task.Delay(1000);
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
                            robot.task = Task.Run(async () =>
                            {
                                robot.DeplacerBase(-speed);
                                await Task.Delay(1000);
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
                            robot.task = Task.Run(async () =>
                            {
                                robot.DeplacerEpaule(-speed);
                                await Task.Delay(1000);
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
                            robot.task = Task.Run(async () =>
                            {
                                robot.DeplacerEpaule(speed);
                                await Task.Delay(1000);
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
                            robot.task = Task.Run(async () =>
                            {
                                robot.DeplacerCoude(speed);
                                await Task.Delay(1000);
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
                            robot.task = Task.Run(async () =>
                            {
                                robot.DeplacerCoude(-speed);
                                await Task.Delay(1000);
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
                            robot.task = Task.Run(async () =>
                            {
                                robot.DeplacerCoude(-speed);
                                await Task.Delay(1000);
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
                            robot.task = Task.Run(async () =>
                            {
                                robot.DeplacerMain(speed);
                                await Task.Delay(1000);
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
                            robot.task = Task.Run(async () =>
                            {
                                robot.DeplacerPoignet(speed);
                                await Task.Delay(1000);
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
                            robot.task = Task.Run(async () =>
                            {
                                robot.DeplacerPoignet(-speed);
                                await Task.Delay(1000);
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
            robot.CALIBRE();
        }
    }
}
