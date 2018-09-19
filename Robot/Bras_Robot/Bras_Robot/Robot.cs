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
        //public int NbShot { get; private set; }
        public Position()
        {
            // 100000 pour etre "out of range"
            X = 100000;
            Y = 100000;
            Z = 100000;
        }
        public Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
    sealed class CRS_A255
    {
        private CRS_A255()
        {
            task = Task.Run(() =>
            {
                //contourne un bug dans la connection avec le robot
                SetPosToStart();
                Connexion();
                Deconnexion();
                System.Threading.Thread.Sleep(100);
                Connexion();
                Connected = true;
                SetSpeed(Speed);
            });
        }

        #region robot attributes
        private static readonly Lazy<CRS_A255> lazy = new Lazy<CRS_A255>(() => new CRS_A255());
        public static CRS_A255 Instance { get { return lazy.Value; } }
        private Task task { get; set; }
        private int PosX { get; set; }
        private int PosY { get; set; }
        private int PosZ { get; set; }
        public bool Connected { get; private set; }

        Position[] Bouteilles = new Position[6]
        {
            new Position(0,-300, -365),
            new Position(-110,-300, -365),
            new Position(-220,-300, -365),
            new Position(-330,-300, -365),
            new Position(-440,-300, -375),
            new Position(-550,-300, -375),
        };
        private Position LazyPrendreBouteille = new Position(100, 200, -365);
        private Position CreateStation = new Position(-10, 80, -220);
        private SerialPort serialPort;
        private bool Record { get; set; } = false;
        public bool Calibration { get; private set; } = false;
        private int Base { get; set; } = 0;
        private int Poignet { get; set; } = 0;
        private int Epaule { get; set; } = 0;
        private int Main { get; set; } = 0;
        private int Coude { get; set; } = 0;
        private bool Pince { get; set; } = false;
        private string Command { get; set; } = "";
        private int Speed { get; set; } = 100;
        #endregion

        #region general robot fonctions
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
            if (Calibration)
                return;
            serialPort.Write("MOVE START\r");
            System.Threading.Thread.Sleep(200);
            SetPosToStart();
        }
        private void SetPosToStart()
        {
            if (Calibration)
                return;
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
            if (!Calibration)
                return;
            serialPort.Write(command);
            System.Threading.Thread.Sleep(200);
        }
        public bool EnMarche() => task.IsCompleted;
        public void DeplacerBase(int val)
        {
            if (!Calibration)
                return;
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
            if (!Calibration)
                return;
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
            if (!Calibration)
                return;
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
            if (!Calibration)
                return;
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
            if (!Calibration)
                return;
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
            if (!Calibration)
                return;
            if (Record)
            {
                Command = Command + "OPEN " + val.ToString() + "\r\n";
            }
            serialPort.Write("OPEN " + val.ToString() + "\r");
            System.Threading.Thread.Sleep(200);
        }
        public void FermerPince(int val)
        {
            if (!Calibration)
                return;
            if (Record)
            {
                Command = Command + "CLOSE " + val.ToString() + "\r\n";
            }
            serialPort.Write("CLOSE " + val.ToString() + "\r");
            System.Threading.Thread.Sleep(200);
        }
        public void Home()
        {
            if (!Calibration)
                return;
            serialPort.Write("HOME\r");
            System.Threading.Thread.Sleep(100);
            if (Record)
            {
                Command = Command + "HOME\r\n";
            }
        }
        public void Ready()
        {
            if (!Calibration)
                return;
            serialPort.Write("READY\r");
            System.Threading.Thread.Sleep(200);
            if (Record)
            {
                Command = Command + "READY\r\n";
            }
        }
        public void Halt()
        {
            if (!Calibration)
                return;
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
        #endregion

        #region Barman fonction
        private void VersPosition(ref Position pos)
        {
            JOG(pos.X - PosX, pos.Y - PosY, pos.Z - PosZ);
        }
        private void VerserBouteille(ref (Position pos, int nbShots) pos)
        {
            SetSpeed(50);
            //------Prendre bouteille------//
            JOG(0, 0, 0); // wait
            OuvrirPince(100);
            System.Threading.Thread.Sleep(1000);
            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, (pos.pos.Z - PosZ) + 280);
            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, pos.pos.Z - PosZ);

            JOG(0, 0, 0); // wait
            FermerPince(100);
            System.Threading.Thread.Sleep(5000);

            //------Apporter le bouteille a la station de travail------//
            JOG(0, 0, 280);
            JOG(CreateStation.X - PosX, CreateStation.Y - PosY, 0);
            JOG(0, 0, CreateStation.Z - PosZ);
            JOG(0, 0, 0);
            //------Verser------//
            SetSpeed(25);
            System.Threading.Thread.Sleep(3500);
            for (int i = 0; i < pos.nbShots; ++i)
            {
                DeplacerMain(130);
                System.Threading.Thread.Sleep(2000);
                DeplacerMain(-130);
                System.Threading.Thread.Sleep(2000);
            }
            SetSpeed(50);
            //------Rapporter la bouteille a sa place d'origine------//

            JOG(0, 0, (pos.pos.Z - PosZ) + 280);
            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, 0);
            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, pos.pos.Z - PosZ + 1);

            JOG(0, 0, 0); // wait
            OuvrirPince(50);
            System.Threading.Thread.Sleep(7000);
            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, (pos.pos.Z - PosZ) + 280);
            JOG(0, 0, 0);
        }
        // TODO bcp de fine tunning
        private void PlacerBouteille(ref (Position pos, int nbShots) pos)
        {
            //------Prendre bouteille------//

            JOG(0, 0, 0); // wait
            OuvrirPince(100);
            System.Threading.Thread.Sleep(1000);
            JOG(LazyPrendreBouteille.X - PosX, LazyPrendreBouteille.Y - PosY, (LazyPrendreBouteille.Z - PosZ) + 300);
            JOG(LazyPrendreBouteille.X - PosX, LazyPrendreBouteille.Y - PosY, LazyPrendreBouteille.Z - PosZ);
            System.Threading.Thread.Sleep(5000);
            JOG(0, 0, 0); // wait
            FermerPince(100);
            System.Threading.Thread.Sleep(1000);

            //------Apporter le bouteille a la station de travail------//
            JOG(0, 0, 300);
            JOG(CreateStation.X, CreateStation.Y, 300);
            VersPosition(ref CreateStation);
            JOG(0, 0, 0);
            //------Rapporter la bouteille a sa place d'origine------//

            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, (pos.pos.Z - PosZ) + 300);
            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, pos.pos.Z - PosZ + 10);

            JOG(0, 0, 0); // wait
            OuvrirPince(100);
            System.Threading.Thread.Sleep(3000);
            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, (pos.pos.Z - PosZ) + 300);
            JOG(0, 0, 0);
        }

        private Position RedCupStackStation = new Position(-200, 350, -420);
        private Position RedCupDrinkStation = new Position(0, 0, -395);
        private Position DONNEMOILECUP = new Position(200, 0, -100);
        private Position RedCupFin = new Position(100, 200, -425);

        private void PickUpCup(ref Position cup)
        {
            OuvrirPince(100);
            SetSpeed(25);
            Position RedCupHauteur = new Position(cup.X, cup.Y, cup.Z + 100);

            VersPosition(ref RedCupHauteur);
            VersPosition(ref cup);

            JOG(0, 0, 0); // wait
            FermerPince(15);
            System.Threading.Thread.Sleep(10000);

            SetSpeed(50);
        }
        private void DropCup(ref Position cup)
        {
            GoToStart();
            Position cuptemp = new Position(cup.X, cup.Y, cup.Z + 20);
            VersPosition(ref cuptemp);
            VersPosition(ref cup);
            JOG(0, 0, 0);
            OuvrirPince(100);
            System.Threading.Thread.Sleep(4000);
            Position RedCupFinHauteur = new Position(cup.X, cup.Y, cup.Z + 100);
            VersPosition(ref RedCupFinHauteur);
            GoToStart();
        }
        private void ServirCup()
        {
            PickUpCup(ref RedCupDrinkStation);
            GoToStart();
            //VersPosition(ref DONNEMOILECUP);
            //System.Threading.Thread.Sleep(6000);
            DropCup(ref RedCupFin);
        }
        #endregion

        #region Tasks
        private Task DrinkOperation(List<(Position pos, int nbShots)> positions)
        {
            return Task.Run(() =>
            {
                GoToStart(); // Se met un position de debart
                PickUpCup(ref RedCupStackStation); // Prend le cup dans la pile
                DropCup(ref RedCupDrinkStation); // Depose le cup dans la station de travail
                foreach (var position in positions) // Verse les bouteille une par une
                {
                    var p = position;
                    VerserBouteille(ref p);
                }
                GoToStart();
                JOG(0, 0, 0);
                ServirCup(); // prend le cup et le depose devant le client
            });
        }
        #endregion

        public void MakeDrink(List<(Position pos, int nbShots)> positions)
        {
            if (task.IsCompleted && positions.Capacity != 0 && !Calibration)
                task = DrinkOperation(positions);
        }

        public List<(Position position, int nbShots)> Exemple = new List<(Position pos, int nbShots)>
        {
            (new Position(0,-300, -375), 3),
            (new Position(-220,-300, -375), 2)
        };
        public void TEST()
        {
            /*
            Task.Run(() =>
            {
                foreach (var b in Bouteilles)
                {
                    var a = (b, 1);
                    PlacerBouteille(ref a);
                }
            });*/
        }
    }
}