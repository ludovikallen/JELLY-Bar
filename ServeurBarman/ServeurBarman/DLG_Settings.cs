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
    public partial class DLG_Settings : MetroFramework.Forms.MetroForm
    {
        public static SerialPort port;
        public DLG_Settings()
        {
            InitializeComponent();
        }

        

        private void BTN_Close_Pliers_Click(object sender, EventArgs e)
        {
             port.Write("CLOSE 100\r");
             Thread.Sleep(100);
        }

        private void BTN_Open_Pliers_Click(object sender, EventArgs e)
        {
            port.Write("OPEN 100\r");
            Thread.Sleep(100);
        }

        private void BTN_Home_Click(object sender, EventArgs e)
        {
            port.Write("HOME\r");
            Thread.Sleep(100);
        }

        private void Btn_Base_Right_Click(object sender, EventArgs e)
        {
            int angle = +5 * 72000 / 360;
            string pulsion = "MOTOR 1," + angle.ToString();
            port.Write(pulsion + "\r");
            Thread.Sleep(100);
        }

        private void Btn_Base_Left_Click(object sender, EventArgs e)
        {
            int angle = -5 * 72000 / 360;
            string pulsion = "MOTOR 1," + angle.ToString();
            port.Write(pulsion + "\r");
            Thread.Sleep(100);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int angle = -5 * 72000 / 360;
            string pulsion = "MOTOR 3," + angle.ToString();
            port.Write(pulsion + "\r");
            Thread.Sleep(100);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int angle = 5 * 72000 / 360;
            string pulsion = "MOTOR 3," + angle.ToString();
            port.Write(pulsion + "\r");
            Thread.Sleep(100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int angle = -5 * 72000 / 360;
            string pulsion = "MOTOR 2," + angle.ToString();
            port.Write(pulsion + "\r");
            Thread.Sleep(100);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int angle = 5 * 72000 / 360;
            string pulsion = "MOTOR 2," + angle.ToString();
            port.Write(pulsion + "\r");
            Thread.Sleep(100);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int angle = -5 * 72000 / 360;
            string pulsion = "MOTOR 5," + angle.ToString();
            port.Write(pulsion + "\r");
            Thread.Sleep(100);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int angle = 5 * 72000 / 360;
            string pulsion = "MOTOR 5," + angle.ToString();
            port.Write(pulsion + "\r");
            Thread.Sleep(100);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int angle = 5 * 72000 / 360;
            string pulsion = "MOTOR 4," + angle.ToString();
            port.Write(pulsion + "\r");
            Thread.Sleep(100);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int angle = -5 * 72000 / 360;
            string pulsion = "MOTOR 4," + angle.ToString();
            port.Write(pulsion + "\r");
            Thread.Sleep(100);
        }
    }
}
