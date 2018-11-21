using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        }
        #region robot attributes
        private static readonly Lazy<CRS_A255> lazy = new Lazy<CRS_A255>(() => new CRS_A255());
        public static CRS_A255 Instance { get { return lazy.Value; } }
        public Task task { get; set; }
        private int PosX { get; set; }
        private int PosY { get; set; }
        private int PosZ { get; set; }
        public bool Connected { get; private set; }
        private int nbCup { get; set; } = 6;
        Position[] bouteilles = new Position[6]
        {
            new Position(130, -200, -365),
            new Position(70, -290, -365),
            new Position(-10, -350, -365),
            new Position(-100,-400,-365),
            new Position(-205,-400,-365),
            new Position(-305,-400,-365)
        };

        private Position redCupStackStation = new Position(-180, 360, -350);
        private Position redCupDrinkStation = new Position(25, 0, -400);
        private Position donneMoiLeCup = new Position(200, 0, -100);
        private Position[] redCupFin = new Position[2]
        {
            new Position(125, -60, -385),
            new Position(125,50,-385)
        };
        private int indexFin = 0;
        private Position LazyPrendreBouteille = new Position(100, 200, -365);
        private Position CreateStation = new Position(5, 90, -220);////////////////////////////////////////////////////////
        private SerialPort serialPort;
        public bool Calibration { get; set; } = false;
        private int Base { get; set; } = 0;
        private int Poignet { get; set; } = 0;
        private int Epaule { get; set; } = 0;
        private int Main { get; set; } = 0;
        private int Coude { get; set; } = 0;
        private bool Pince { get; set; } = false;
        private string Command { get; set; } = "";
        private int Speed { get; set; } = 10;
        #endregion
        #region general robot fonctions
        public int ConnexionRobot()
        {
            task = Task.Run(async () =>
            {
                //contourne un bug dans la connection avec le robot
                SetPosToStart();
                Connexion();
                Deconnexion();
                await Task.Delay(100);
                Connexion();
                Connected = true;
            });
            return 1;

        }
        private void Connexion()
        {
            serialPort = new SerialPort("COM1", 19200);
            serialPort.Open();
            byte[] Entrer1 = { 0x52, 0x21, 0x05 };
            byte[] Entrer2 = { 0x01, 0x21, 0x21, 0x00, 0x48, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x03, 0x8B };
            byte[] Entrer3 = { 0x06 };
            byte[] Entrer4 = { 0x04 };
            FuncNSleep(() => serialPort.Write(Entrer1, 0, 3), 100);
            FuncNSleep(() => serialPort.Write(Entrer2, 0, Entrer2.Length), 100);
            FuncNSleep(() => serialPort.Write(Entrer3, 0, 1), 100);
            FuncNSleep(() => serialPort.Write(Entrer4, 0, 1), 100);
            FuncNSleep(() => serialPort.Write("NOHELP\r"), 100);
        }
        public void Deconnexion()
        {
            task.Dispose();
            serialPort.Close();
            Connected = false;
        }
        private void SetSartPos()
        {
            FuncNSleep(() => serialPort.Write("HERE START\r"), 200);
        }
        public void GoToStart()
        {
            FuncNSleep(() => serialPort.Write("MOVE START\r"), 200);
            SetPosToStart();
        }
        private void SetPosToStart()
        {
            PosX = 0;
            PosY = 0;
            PosZ = 0;
        }
        public void ManuelCommand(String command)
        {
            FuncNSleep(() => serialPort.Write(command), 200);
        }
        public bool EnMarche() => !task.IsCompleted;
        public int AjouterCup(int ajout) => nbCup = ajout; //DE MEME, JE FUS OBLIGE DE REVOIR CETTE METHODE
        public void DeplacerBase(int val)
        {
            if (!Calibration)
                return;
            Base += val;
            FuncNSleep(() => serialPort.Write("JOINT 1, " + val.ToString() + "\r"), 200);
        }
        public void DeplacerPoignet(int val)
        {
            if (!Calibration)
                return;
            Poignet += val;
            FuncNSleep(() => serialPort.Write("JOINT 4, " + val.ToString() + "\r"), 200);
        }
        public void DeplacerEpaule(int val)
        {
            if (!Calibration)
                return;
            Epaule += val;
            FuncNSleep(() => serialPort.Write("JOINT 2, " + val.ToString() + "\r"), 200);
        }
        public void DeplacerMain(int val)
        {
            if (!Calibration)
                return;
            Main += val;
            FuncNSleep(() => serialPort.Write("JOINT 5, " + val.ToString() + "\r"), 200);
        }
        private void DeplacerMainPriv(int val)
        {
            Main += val;
            FuncNSleep(() => serialPort.Write("JOINT 5, " + val.ToString() + "\r"), 200);
        }
        public void DeplacerCoude(int val)
        {
            if (!Calibration)
                return;
            Coude += val;
            FuncNSleep(() => serialPort.Write("JOINT 3, " + val.ToString() + "\r"), 200);
        }
        public void OuvrirPince(int val)
        {
            FuncNSleep(() => serialPort.Write("OPEN " + val.ToString() + "\r"), 200);
        }
        public void FermerPince(int val)
        {
            FuncNSleep(() => serialPort.Write("CLOSE " + val.ToString() + "\r"), 200);
        }
        public void Home()
        {
            if (!Calibration)
                return;
            FuncNSleep(() => serialPort.Write("HOME\r"), 200);
        }
        public void Ready()
        {
            if (!Calibration)
                return;
            FuncNSleep(() => serialPort.Write("READY\r"), 200);
        }
        public void Halt()
        {
            if (!Calibration)
                return;
            FuncNSleep(() => serialPort.Write("HALT\r"), 200);
        }
        public void SetSpeed(decimal speed)
        {
            FuncNSleep(() => serialPort.Write("SPEED " + speed + "\r"), 200);
            Speed = (int)speed;
        }
        private void JOG(int x, int y, int z)
        {
            PosX += x;
            PosY += y;
            PosZ += z;

            FuncNSleep(() => serialPort.Write("JOG " + x + "," + y + "," + z + "\r"), 200);

            FuncNSleep(() => serialPort.Write("FINISH\r"), 200);
        }
        private void CALIBRE()
        {
            FuncNSleep(() => serialPort.Write("PASSWORD 255\r"), 1000);
            FuncNSleep(() => serialPort.Write("@ZERO\r"), 1000);
            FuncNSleep(() => serialPort.Write("MOTOR 2, -18000\r"), 1000);
            FuncNSleep(() => serialPort.Write("@@CAL\r"), 1000);
        }
        #endregion
        #region Barman fonction
        public void VersPosition(ref Position pos) => JOG(pos.X - PosX, pos.Y - PosY, pos.Z - PosZ);
        private void VerserBouteille(ref (Position pos, int nbShots) pos)
        {
            SetSpeed(100);
            //------Prendre bouteille------//
            JOG(0, 0, 0); // wait
            FuncNSleep(() => OuvrirPince(100), 1000);

            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, (pos.pos.Z - PosZ) + 280);
            SetSpeed(75);
            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, pos.pos.Z - PosZ);

            JOG(0, 0, 0); // wait
            FuncNSleep(() => FermerPince(100), 2000);

            //------Apporter le bouteille a la station de travail------//
            JOG(0, 0, 300);
            JOG(CreateStation.X - PosX, CreateStation.Y - PosY, 0);
            JOG(0, 0, CreateStation.Z - PosZ);
            FuncNSleep(() => JOG(0, 0, 0), 2000);
            //------Verser------//
            for (int i = 0; i < pos.nbShots; ++i)
            {
                SetSpeed(7);
                FuncNSleep(() => DeplacerMainPriv(130), 7000);
                SetSpeed(75);
                FuncNSleep(() => DeplacerMainPriv(-130), 500);
            }
            SetSpeed(50);
            //------Rapporter la bouteille a sa place d'origine------//

            JOG(0, 0, (pos.pos.Z - PosZ) + 300);
            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, 0);
            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, pos.pos.Z - PosZ + 1);

            JOG(0, 0, 0); // wait
            FuncNSleep(() => OuvrirPince(50), 5000);
            JOG(pos.pos.X - PosX, pos.pos.Y - PosY, (pos.pos.Z - PosZ) + 300);
            JOG(0, 0, 0);
        }
        private void VerserBouteille(ref Position pos, int nbshot)
        {
            SetSpeed(100);
            //------Prendre bouteille------//
            JOG(0, 0, 0); // wait
            FuncNSleep(() => OuvrirPince(100), 1000);

            JOG(pos.X - PosX, pos.Y - PosY, (pos.Z - PosZ) + 280);
            SetSpeed(75);
            JOG(pos.X - PosX, pos.Y - PosY, pos.Z - PosZ);

            JOG(0, 0, 0); // wait
            FuncNSleep(() => FermerPince(100), 2000);

            JOG(0, 0, 320);

            //------Apporter le bouteille a la station de travail------//
            for (int i = 0; i < nbshot; ++i)
            {
                var verre = new Position(shooterCupFin[i].X - 5, shooterCupFin[i].Y + 85, shooterCupFin[i].Z + 235); ;
                JOG(verre.X - PosX, verre.Y - PosY, 0);
                JOG(0, 0, verre.Z - PosZ);
                FuncNSleep(() => JOG(0, 0, 0), 2000);
                //------Verser------//
                SetSpeed(7);
                FuncNSleep(() => DeplacerMainPriv(130), 7000);
                SetSpeed(75);
                FuncNSleep(() => DeplacerMainPriv(-130), 500);
                SetSpeed(50);
            }

            //------Rapporter la bouteille a sa place d'origine------//

            JOG(0, 0, (pos.Z - PosZ) + 280);
            JOG(pos.X - PosX, pos.Y - PosY, 0);
            JOG(pos.X - PosX, pos.Y - PosY, pos.Z - PosZ + 1);

            JOG(0, 0, 0); // wait
            FuncNSleep(() => OuvrirPince(50), 5000);
            JOG(pos.X - PosX, pos.Y - PosY, (pos.Z - PosZ) + 280);
            JOG(0, 0, 0);
        }
        private void PickUpCup(ref Position cup)
        {
            OuvrirPince(100);
            SetSpeed(100);
            Position RedCupHauteur = new Position(cup.X, cup.Y, cup.Z + 135);
            VersPosition(ref RedCupHauteur);
            SetSpeed(25);
            VersPosition(ref cup);

            JOG(0, 0, 0); // wait
            FuncNSleep(() => FermerPince(15), 8000);
            FuncNSleep(() => VersPosition(ref RedCupHauteur), 2000);

            SetSpeed(50);
        }
        private void DropCup(ref Position cup)
        {
            //GoToStart();
            Position cuptemp = new Position(cup.X, cup.Y, PosZ);
            VersPosition(ref cuptemp);
            cuptemp = new Position(PosX, PosY, cup.Z);
            VersPosition(ref cuptemp);
            VersPosition(ref cup);
            JOG(0, 0, 0); // wait
            FuncNSleep(() => OuvrirPince(100), 3000);

            Position RedCupFinHauteur = new Position(cup.X, cup.Y, cup.Z + 75);
            VersPosition(ref RedCupFinHauteur);
            GoToStart();
        }
        private void ServirCup()
        {
            PickUpCup(ref redCupDrinkStation);
            DropCup(ref redCupFin[indexFin]);
            indexFin++;
            if (indexFin == redCupFin.Length)
                indexFin = 0;
        }
        #endregion
        #region Tasks
        private void FuncNSleep(Action<int> action, int sleep)
        {
            var t = Task.Run(() => action);
            t.Wait();
            Thread.Sleep(sleep);

        }
        private void FuncNSleep(Action<string> action, int sleep)
        {
            var t = Task.Run(() => action);
            t.Wait();
            Thread.Sleep(sleep);
        }
        private void FuncNSleep(Action action, int sleep)
        {
            action.Invoke();
            Thread.Sleep(sleep);

        }
        private Task DrinkOperation(List<(Position pos, int nbShots)> positions)
        {
            return Task.Run(() =>
            {
                GoToStart(); // Se met un position de debart
                Position cuptemp = new Position(redCupStackStation.X, redCupStackStation.Y, redCupStackStation.Z + (nbCup * 5));
                PickUpCup(ref cuptemp); // Prend le cup dans la pile
                SetSpeed(75);
                DeplacerMainPriv(-180);
                //--nbCup;
                DropCup(ref redCupDrinkStation); // Depose le cup dans la station de travail
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

        #region Shooter
        private Task ShooterOperation(Position position, int nbShooter)
        {
            return Task.Run(() =>
            {
                while (nbShooter > 0)
                {
                    int nb;
                    if (nbShooter > 2)
                    {
                        nb = 2;
                        nbShooter -= 2;
                    }
                    else
                    {
                        nb = 1;
                        nbShooter--;
                    }

                    //------Prépare le bras------//
                    FuncNSleep(() => GoToStart(), 2000);
                    SetSpeed(50);
                    FuncNSleep(() => serialPort.Write("OPEN 100\r"), 200);

                    for (int i = 0; i < nb; ++i)
                    {
                        GoToStart(); // Se met un position de debart
                        Position cuptemp = new Position(redCupStackStation.X, redCupStackStation.Y, redCupStackStation.Z + (nbCup * 5));
                        PickUpCup(ref cuptemp); // Prend le cup dans la pile
                        SetSpeed(75);
                        DeplacerMainPriv(-180);

                        cuptemp = new Position(shooterCupFin[i].X, shooterCupFin[i].Y, -200);
                        FuncNSleep(() => VersPosition(ref cuptemp), 500);

                        cuptemp = new Position(shooterCupFin[i].X, shooterCupFin[i].Y, shooterCupFin[i].Z);
                        FuncNSleep(() => VersPosition(ref cuptemp), 1500);
                        FuncNSleep(() => serialPort.Write("OPEN 100\r"), 2000);
                        cuptemp = new Position(shooterCupFin[i].X, shooterCupFin[i].Y, -200);
                        FuncNSleep(() => VersPosition(ref cuptemp), 500);
                    }
                    GoToStart();
                    var temp = position;
                    VerserBouteille(ref temp, nb);
                }

            });
        }

        private Position[] shooterCupFin = new Position[2]
        {
            new Position(125, -60, -385),
            new Position(125,50,-385)
        };

        public bool MakeShooter(Position position, int nbShooter)
        {
            if (task.IsCompleted && position != null && !Calibration)
            {
                task = ShooterOperation(position, nbShooter);
                return true;
            }
            return false;
        }
        #endregion

        //retourne si il accepte de faire l'operation ou non
        public bool MakeDrink(List<(Position pos, int nbShots)> positions)
        {
            if (task.IsCompleted && positions.Capacity != 0 && !Calibration)
            {
                task = DrinkOperation(positions);
                return true;
            }
            return false;
        }
        //retourne si il accepte de faire l'operation ou non
        public bool MakeShooterTest(Position position, int nbShooter)
        {
            if (task.IsCompleted && !Calibration)
            {
                task = Task.Delay(10000);
                return true;
            }
            return true;
        }

        //retourne si il accepte de faire l'operation ou non
        public bool MakeDrinkTest(List<(Position pos, int nbShots)> positions)
        {
            if (task.IsCompleted && positions.Capacity != 0 && !Calibration)
            {
                task = Task.Delay(10000);
                return true;
            }
            return false;
        }

        public List<(Position position, int nbShots)> Exemple = new List<(Position pos, int nbShots)>
        {
            (new Position(130,-200, -365), 1),
            //(new Position(70,-290, -365), 1),
            //(new Position(-10,-350, -365), 1),
            (new Position(-100,-400, -365), 2)
            //(new Position(-205,-400, -365), 1),
            //(new Position(-305,-400, -365), 1)

        };
        public void TEST()
        {
            GoToStart();

            Task.Run(() =>
            {
                while (nbCup > 0)
                {
                    Position cuptemp = new Position(redCupStackStation.X, redCupStackStation.Y, redCupStackStation.Z + (nbCup * 5));
                    PickUpCup(ref cuptemp);

                    AjouterCup(nbCup - 1);
                    DeplacerMainPriv(-180);
                    VersPosition(ref CreateStation);
                    Thread.Sleep(10000);
                    OuvrirPince(100);
                    DeplacerMainPriv(180);
                }
            });

        }
    }
}