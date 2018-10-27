using Bras_Robot;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServeurBarman
{
    public class DataBase
    {
        public OracleConnection EtatBaseDonnées { get; private set; }

        public static readonly DataBase instance_bd = new DataBase();

        private DataBase()
        {

        }
        static DataBase() { }

        public OracleConnection Connexion(string user, string pass)
        {
            EtatBaseDonnées = new OracleConnection();
            try
            {
                string dsource = "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 205.237.244.251)(PORT = 1521)) (CONNECT_DATA =(SERVICE_NAME = orcl.clg.qc.ca)))";
                string bd = "Data Source=" + dsource + ";User id=" + user + ";Password=" + pass;
                EtatBaseDonnées.ConnectionString = bd;
                EtatBaseDonnées.Open();
                MessageBox.Show("Connecté avec succès!!!!");

            }
            catch (Exception) { MessageBox.Show("Erreur de connexion!!!"); }

            return EtatBaseDonnées;
        }
        public void FermerConnexion()
        {
            EtatBaseDonnées.Close();
        }

        public List<(int, int)> ListeCommande()
        {
            List<(int, int)> numcommande = new List<(int, int)>();
            string cmd = "select numcommande,shooter from commande";
            try
            {
                OracleCommand listeDiv = new OracleCommand(cmd, EtatBaseDonnées);
                listeDiv.CommandType = CommandType.Text;
                OracleDataReader divisionReader = listeDiv.ExecuteReader();
                while (divisionReader.Read())
                {
                    numcommande.Add((divisionReader.GetInt32(0), divisionReader.GetInt32(1)));
                }
                divisionReader.Close();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            //ON ORDONNE LA LISTE DES COMMANDES
            for (int i = 0; i < numcommande.Count; ++i)
            {
                for (int j = 0; j < numcommande.Count; ++j)
                    if (numcommande[i].Item1 == numcommande[j].Item1 && i != j)
                        numcommande.Remove(numcommande[j]);
            }
            return numcommande;
        }

        public string NombreDeShooter()
        {
            string nombreShooter = "";
            string cmd = "select nbshooter from verreshooter";
            OracleCommand listeDiv = new OracleCommand(cmd, EtatBaseDonnées);
            listeDiv.CommandType = CommandType.Text;
            OracleDataReader divisionReader = listeDiv.ExecuteReader();
            try
            {
                while (divisionReader.Read())
                {
                    nombreShooter = divisionReader.GetInt32(0).ToString();
                }
                divisionReader.Close();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            return nombreShooter;
        }

        public string NombreDeVerreRouge()
        {
            string nombreVerreRouge = "";
            string cmd1 = "select nbverre from verrerouge";
            OracleCommand listeDiv1 = new OracleCommand(cmd1, EtatBaseDonnées);
            listeDiv1.CommandType = CommandType.Text;
            OracleDataReader divisionReader1 = listeDiv1.ExecuteReader();
            try
            {
                while (divisionReader1.Read())
                {
                    nombreVerreRouge = divisionReader1.GetInt32(0).ToString();
                }
                divisionReader1.Close();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            return nombreVerreRouge;
        }

        public string NombreIngredients()
        {
            string nbreIngredients = "";
            string cmd = "select count(*) from ingredient";
            OracleCommand listeDiv = new OracleCommand(cmd, EtatBaseDonnées);
            listeDiv.CommandType = CommandType.Text;
            OracleDataReader divisionReader = listeDiv.ExecuteReader();
            try
            {
                while (divisionReader.Read())
                {
                    nbreIngredients = divisionReader.GetInt32(0).ToString();
                }
                divisionReader.Close();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            return nbreIngredients;
        }

        public void AjouterIngredients(List<string> ingredients)
        {
            try
            {
                string cmd = "insert into ingredient values(" + Int32.Parse(ingredients[0]) + "," + Int32.Parse(ingredients[1]) + ","
                    + Int32.Parse(ingredients[2]) + "," + Int32.Parse(ingredients[3]) + "," + "'1'" + "," + Int32.Parse(ingredients[4]) + ",'"
                    + ingredients[5] + "','" + ingredients[6] + "')";

                OracleCommand disc = new OracleCommand(cmd, EtatBaseDonnées);
                disc.ExecuteNonQuery();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message); }

            try
            {
                string cmd1 = "commit";
                OracleCommand disc1 = new OracleCommand(cmd1, EtatBaseDonnées);
                disc1.ExecuteNonQuery();
            }
            catch (Exception) { MessageBox.Show(" Échec de l'enregistrement."); }
        }
    }


    // abstraction
    public class Commande
    {
        private SpecificateurCommande commande;
        private int numcommande;

        public Commande(int num)
        {
            if (num == 0)
                commande = new Commande_Normale();
            else
                commande = new Shooter();
        }


        public SpecificateurCommande TypeReel()
        {
            return commande.TypeReel();
        }

        public List<(Position, int)> ListerIngredients(int numcom)
        {
            return commande.Ingredients(numcom);
        }
    }


    // CLASS QUI GERE LES COMMANDES PASSEES PAR LE CLIENT ANDROID(Interface)
    public abstract class SpecificateurCommande
    {
        protected DataBase Connexion { get; } = DataBase.instance_bd;

        // ENUMÈRE LA LISTE DES INGREDIENTS DE LA COMMANDE DU CLIENT ANDROID
        public abstract List<(Position, int)> Ingredients(int numcom);
        // OBTIENT LE TYPE REEL DE LA COMMANDE,
        // SOIT (NORMALE OU SHOOTER)
        public abstract SpecificateurCommande TypeReel();

    }



    // CLASSE SHOOTER
    class Shooter : SpecificateurCommande
    {
        List<(Position, int)> ingredients = new List<(Position, int)>();
        public Shooter() : base()
        {

        }
        public override List<(Position, int)> Ingredients(int numcom)
        {
            string cmd = "e.POSITIONX,e.POSITIONY,e.POSITIONZ,c.QTY" +
            "from ingredient e inner join commande c on e.codebouteille=c.ingredient where c.numcommande=" + numcom.ToString();

            OracleCommand listeDiv = new OracleCommand(cmd, Connexion.EtatBaseDonnées);
            listeDiv.CommandType = CommandType.Text;
            OracleDataReader divisionReader = listeDiv.ExecuteReader();
            try
            {
                while (divisionReader.Read())
                {
                    ingredients.Add((new Position(divisionReader.GetInt32(0), divisionReader.GetInt32(1), divisionReader.GetInt32(2)), divisionReader.GetInt32(3)));
                }
                divisionReader.Close();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            return ingredients;
        }

        public override SpecificateurCommande TypeReel()
        {
            return new Shooter();
        }
    }


    // CLASSE COMMANDE NORMALE
    public class Commande_Normale : SpecificateurCommande
    {
        List<(Position, int)> ingredients = new List<(Position, int)>();
        public Commande_Normale() : base()
        {

        }
        public override SpecificateurCommande TypeReel()
        {
            return new Commande_Normale();
        }

        public override List<(Position, int)> Ingredients(int numcom)
        {
            string cmd = "select e.POSITIONX,e.POSITIONY,e.POSITIONZ,c.QTY from ingredient e inner join commande c on e.codebouteille=c.ingredient where numcommande=" + numcom.ToString();

            OracleCommand listeDiv = new OracleCommand(cmd, Connexion.EtatBaseDonnées);
            listeDiv.CommandType = CommandType.Text;
            OracleDataReader divisionReader = listeDiv.ExecuteReader();
            try
            {
                while (divisionReader.Read())
                {
                    ingredients.Add((new Position(divisionReader.GetInt32(0), divisionReader.GetInt32(1), divisionReader.GetInt32(2)), divisionReader.GetInt32(3)));
                }
                divisionReader.Close();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            return ingredients;
        }
    }
}
