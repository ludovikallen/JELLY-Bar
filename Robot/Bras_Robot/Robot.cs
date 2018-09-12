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
    public sealed class Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public int NbShot { get; private set; }
        public Position()
        {
            // 100000 pour etre "out of range"
            X = 100000;
            Y = 100000;
            Z = 100000;
            NbShot = 0;
        }
        public Position(int x, int y, int z, int nbShot)
        {
            X = x;
            Y = y;
            Z = z;
            NbShot = nbShot;
        }
        public Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            NbShot = 1;
        }
    }
    sealed class CRS_A255
    {
        private static readonly Lazy<CRS_A255> lazy = new Lazy<CRS_A255>(() => new CRS_A255());
        public static CRS_A255 Instance { get { return lazy.Value; } }
        private int PosX { get; set; }
        private int PosY { get; set; }
        private int PosZ { get; set; }
        public bool Connected { get; private set; }
        public bool EnOperation { get; private set; } = false;

        Position[] Bouteilles = new Position[6]
        {
            new Position(0,-300, -350),
            new Position(-100,-300, -350),
            new Position(-200,-300, -350),
            new Position(-300,-300, -350),
            new Position(-400,-300, -350),
            new Position(-500,-300, -350),
        };

        private Position CreateStation = new Position(0, 150, -150);
        private SerialPort serialPort;
        public bool Record { get; set; } = false;
        private int Base { get; set; } = 0;
        private int Poignet { get; set; } = 0;
        private int Epaule { get; set; } = 0;
        private int Main { get; set; } = 0;
        private int Coude { get; set; } = 0;
        private bool Pince { get; set; } = false;
        public string Command { get; set; } = "";
        public int Speed { get; set; } = 100;
        private CRS_A255()
        {
            SetPosToStart();
            Connexion();
            Deconnexion();
            System.Threading.Thread.Sleep(100);
            Connexion();
            Connected = true;
            SetSpeed(Speed);
        }
        private void Connexion()
        {
            serialPort = new SerialPort("COM1", 19200);
            serialPort.Open();
            byte[] Entrer1 = { 0x52, 0x21, 0x05 };
            byte[] Entrer2 = { 0x01, 0x21, 0x21, 0x00, 0x48, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x03, 0x8B };
            byte[] Entrer3 = { 0x06 };
            byte[] Entrer4 = { 0x04 };
            serialPort.Write(Entrer1, 0, 3);
            System.Threading.Thread.Sleep(100);
            serialPort.Write(Entrer2, 0, Entrer2.Length);
            System.Threading.Thread.Sleep(100);
            serialPort.Write(Entrer3, 0, 1);
            System.Threading.Thread.Sleep(100);
            serialPort.Write(Entrer4, 0, 1);
            System.Threading.Thread.Sleep(100);
            serialPort.Write("NOHELP\r");
        }
        public void Deconnexion()
        {
            serialPort.Close();
            Connected = false;
        }
        private void SetSartPos()
        {
            serialPort.Write("HERE START\r");
            System.Threading.Thread.Sleep(200);
        }
        public void GoToStart()
        {
            serialPort.Write("MOVE START\r");
            System.Threading.Thread.Sleep(200);
            SetPosToStart();
        }
        private void SetPosToStart()
        {
            PosX = 0;
            PosY = 0;
            PosZ = 0;
        }
        public void Recording()
        {
            if (Record)
                Record = false;
            else
            {
                Record = true;
                Command = "";
            }
        }
        public void ManuelCommand(String command)
        {
            serialPort.Write(command);
            System.Threading.Thread.Sleep(200);
        }
        public void DeplacerBase(int val)
        {
            Base += val;
            if (Record)
            {
                Command = Command + "JOINT 1, " + val.ToString() + "\r\n";
            }
            serialPort.Write("JOINT 1, " + val.ToString() + "\r");
            System.Threading.Thread.Sleep(200);
        }
        public void DeplacerPoignet(int val)
        {
            Poignet += val;
            if (Record)
            {
                Command = Command + "JOINT 4, " + val.ToString() + "\r\n";
            }
            serialPort.Write("JOINT 4, " + val.ToString() + "\r");
            System.Threading.Thread.Sleep(200);
        }
        public void DeplacerEpaule(int val)
        {
            Epaule += val;
            if (Record)
            {
                Command = Command + "JOINT 2, " + val.ToString() + "\r\n";
            }
            serialPort.Write("JOINT 2, " + val.ToString() + "\r");
            System.Threading.Thread.Sleep(200);
        }
        public void DeplacerMain(int val)
        {
            Main += val;
            if (Record)
            {
                Command = Command + "JOINT 5, " + val.ToString() + "\r\n";
            }
            serialPort.Write("JOINT 5, " + val.ToString() + "\r");
            System.Threading.Thread.Sleep(200);
        }
        public void DeplacerCoude(int val)
        {
            Coude += val;
            if (Record)
            {
                Command = Command + "JOINT 3, " + val.ToString() + "\r\n";
            }
            serialPort.Write("JOINT 3, " + val.ToString() + "\r");
            System.Threading.Thread.Sleep(200);
        }
        public void OuvrirPince(int val)
        {
            if (Record)
            {
                Command = Command + "OPEN " + val.ToString() + "\r\n";
            }
            serialPort.Write("OPEN " + val.ToString() + "\r");
            System.Threading.Thread.Sleep(200);
        }
        public void FermerPince(int val)
        {
            if (Record)
            {
                Command = Command + "CLOSE " + val.ToString() + "\r\n";
            }
            serialPort.Write("CLOSE " + val.ToString() + "\r");
            System.Threading.Thread.Sleep(200);
        }
        public void Home()
        {
            serialPort.Write("HOME\r");
            System.Threading.Thread.Sleep(100);
            if (Record)
            {
                Command = Command + "HOME\r\n";
            }
        }
        public void Ready()
        {
            serialPort.Write("READY\r");
            System.Threading.Thread.Sleep(200);
            if (Record)
            {
                Command = Command + "READY\r\n";
            }
        }
        public void Halt()
        {
            serialPort.Write("HALT\r");
            System.Threading.Thread.Sleep(200);
            if (Record)
            {
                Command = Command + "HALT\r\n";
            }
        }
        private void SetSpeed(decimal speed)
        {
            serialPort.Write("SPEED " + speed + "\r");
            System.Threading.Thread.Sleep(200);
            if (Record)
            {
                Command = Command + "SPEED " + speed + "\r\n";
            }
            Speed = (int)speed;
        }
        private void JOG(int x, int y, int z)
        {
            PosX += x;
            PosY += y;
            PosZ += z;

            serialPort.Write("JOG " + x + "," + y + "," + z + "\r");
            System.Threading.Thread.Sleep(200);
            serialPort.Write("FINISH\r");
            System.Threading.Thread.Sleep(200);
        }
        private void VersPosition(ref Position pos)
        {
            JOG(pos.X - PosX, pos.Y - PosY, pos.Z - PosZ);
        }
        private void VerserBouteille(ref Position pos)
        {
            //------Prendre bouteille------//

            JOG(0, 0, 0); // wait
            OuvrirPince(100);
            System.Threading.Thread.Sleep(1000);
            JOG(pos.X - PosX, pos.Y - PosY, (pos.Z - PosZ) + 200);
            JOG(pos.X - PosX, pos.Y - PosY, pos.Z - PosZ);

            JOG(0, 0, 0); // wait
            FermerPince(100);
            System.Threading.Thread.Sleep(1000);

            //------Apporter le bouteille a la station de travail------//
            JOG(0, 0, 200);
            VersPosition(ref CreateStation);
            JOG(0, 0, 0);
            //------Verser------//
            for (int i = 0; i < pos.NbShot; ++i)
            {
                DeplacerMain(130);
                System.Threading.Thread.Sleep(2000);
                DeplacerMain(-130);
                System.Threading.Thread.Sleep(1000);
            }

            //------Rapporter la bouteille a sa place d'origine------//

            JOG(pos.X - PosX, pos.Y - PosY, (pos.Z - PosZ) + 200);
            JOG(pos.X - PosX, pos.Y - PosY, pos.Z - PosZ);

            JOG(0, 0, 0); // wait
            OuvrirPince(100);
            System.Threading.Thread.Sleep(3000);
            JOG(pos.X - PosX, pos.Y - PosY, (pos.Z - PosZ) + 200);
            JOG(0, 0, 0);
        }

        private Position RedCup = new Position(0, 0, -420);
        private Position DONNEMOILECUP = new Position(200, 0, -100);
        private Position RedCupFin = new Position(100, 200, -420);
        private void PickUpCup(ref Position cup)
        {
            OuvrirPince(100);
            SetSpeed(25);
            Position RedCupHauteur = new Position(cup.X, cup.Y, cup.Z + 100);

            VersPosition(ref RedCupHauteur);
            VersPosition(ref cup);

            JOG(0, 0, 0); // wait
            FermerPince(15);
            System.Threading.Thread.Sleep(8000);

            SetSpeed(100);
        }
        private void ServirCup()
        {
            PickUpCup(ref RedCup);
            GoToStart();
            VersPosition(ref DONNEMOILECUP);
            System.Threading.Thread.Sleep(6000);

            GoToStart();
            VersPosition(ref RedCupFin);
            JOG(0, 0, 0);
            OuvrirPince(100);
            System.Threading.Thread.Sleep(4000);
            Position RedCupFinHauteur = new Position(RedCupFin.X, RedCupFin.Y, RedCupFin.Z + 100);
            VersPosition(ref RedCupFinHauteur);
            GoToStart();
        }
        public void MakeDrink(List<Position> positions)
        {
            EnOperation = true;
            foreach (var pos in positions)
            {
                Position p = pos;
                VerserBouteille(ref p);
            }
            GoToStart();
            JOG(0, 0, 0);
            EnOperation = false;
        }
        public List<Position> Exemple = new List<Position>
        {
            new Position(0,-300, -350),
            new Position(-500,-300, -350),
            new Position(-300,-300, -350),
            new Position(-100,-300, -350),
            new Position(-400,-300, -350),
            new Position(-500,-300, -350),
            new Position(0,-300, -350),
            new Position(-400,-300, -350),
            new Position(-500,-300, -350)
        };
        public List<string> TEST()
        {
            List<string> list = new List<string>();

            MakeDrink(Exemple);
            //list.Add(PosX + " " + PosY + " " + PosZ);

            return list;
        }
    }
}
