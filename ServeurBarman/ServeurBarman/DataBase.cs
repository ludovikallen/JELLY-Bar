using Bras_Robot;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServeurBarman
{
    /// <summary>
    /// La classe principale.
    /// Elle contient toutes les mtéthodes permettant d'accéder à la base de 
    /// données de façcon sécuritaire
    /// </summary>
    public class DataBase
    {
        /// <summary>
        /// Obtient l'état de la base de données
        /// </summary>
        public OracleConnection EtatBaseDonnées { get; private set; }

        public static readonly DataBase instance_bd = new DataBase();
        

        private DataBase()
        {
            EtatBaseDonnées = new OracleConnection();
        }
        static DataBase() { }

        /// <summary>
        /// Cette méthode permet l'établissement de la connexionb à 
        /// la base de données
        /// </summary>
        /// <param name="user">Nom d'utilisateur de l'usager souhaitant se connecter à la base de données</param>
        /// <param name="pass"></param>
        /// <returns>Une variable de type OracleConnection, qui permet de vérifier l'établissement de la connexion</returns>
        /// <exception cref="Exception">Lance une exception lorqu'une erreur durant la connexion à la base de données</exception>
        public OracleConnection Connexion(string user, string pass)
        {
            try
            {
                string dsource = "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 205.237.244.251)(PORT = 1521)) (CONNECT_DATA =(SERVICE_NAME = orcl.clg.qc.ca)))";
                string bd = "Data Source=" + dsource + ";User id=" + user + ";Password=" + pass;
                EtatBaseDonnées.ConnectionString = bd;
                EtatBaseDonnées.Open();
            }
            catch (Exception) {  }

            return EtatBaseDonnées;
        }

        /// <summary>
        /// Ferme la connexion d'avec la base de données
        /// </summary>
        public void FermerConnexion()
        {
            EtatBaseDonnées.Close();
        }

        public bool Ingredient_Est_Disponible(int num)
        {
            List<object> listeIngredient = new List<object>();
            bool ingredientDisponible = true;

            string cmd = "select e.bouteillepresente from ingredient e inner join commande c on e.codebouteille=c.ingredient where c.numcommande=" + num.ToString();

            OracleCommand listeDiv = new OracleCommand(cmd, EtatBaseDonnées);
            listeDiv.CommandType = CommandType.Text;
            OracleDataReader divisionReader = listeDiv.ExecuteReader();
            try
            {
                while (divisionReader.Read())
                {
                    listeIngredient.Add(divisionReader.GetValue(0));
                }
                divisionReader.Close();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            foreach (var s in listeIngredient)
            {
                if (s.Equals("0"))
                {
                    ingredientDisponible = false;
                }
            }
            return ingredientDisponible;
        }


        /// <summary>
        /// Cette méthode permet de lister la liste des commandes 
        /// </summary>
        /// <returns>Elle retourne une liste de commandes selon de l'ordre de paasation</returns>
        /// <exception cref="Exception">Lance une exception lorsque la table commande n'existe pas</exception>
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

        /// <summary>
        /// Permet d'établir le nombre de verre de shooter dans la base de données
        /// </summary>
        /// <returns>Retourne le nombre de verre de shooter disponible dans base de données</returns>
        /// <exception cref="Exception">Lance une exception si la table Shooter n'existe pas</exception>
        /// <seealso cref="DataBase.NombreDeVerreRouge()"/>
        /// <seealso cref="DataBase.NombreIngredients()"/>
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

        public bool VerreShooterSuffisant(int numcommnde)
        {
            int val = 0;

            string cmd = "select qty from commande where numcommande=" + numcommnde.ToString();
            OracleCommand listeDiv = new OracleCommand(cmd, EtatBaseDonnées);
            listeDiv.CommandType = CommandType.Text;
            OracleDataReader divisionReader = listeDiv.ExecuteReader();
            try
            {
                while (divisionReader.Read())
                {
                    val= divisionReader.GetInt32(0);
                }
                divisionReader.Close();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            if (val > Int32.Parse(NombreDeShooter()))
                return true;

            return false;
        }

        /// <summary>
        /// Permet d'établir le nombre de verre de rouge dans la base de données
        /// </summary>
        /// <returns>Retourne le nombre de verre de rouge disponible dans base de données</returns>
        /// <exception cref="Exception">Lance une exception si la table verre rouge n'existe pas</exception>
        /// <seealso cref="DataBase.NombreDeShooter()"/>
        /// <seealso cref="DataBase.NombreIngredients()"/>
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

        /// <summary>
        /// Permet d'établir le nombre d'ingrédient dans la base de données
        /// </summary>
        /// <returns>Retourne le nombre d'ingrédient disponible dans base de données</returns>
        /// <exception cref="Exception">Lance une exception si la table Ingrédient n'existe pas</exception>
        /// <seealso cref="DataBase.NombreDeShooter()"/>
        /// <seealso cref="DataBase.NombreDeVerreRouge()"/>
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


        /// <summary>
        /// Permet d'ajouter des verres de shooter à la table shooter de la base de données
        /// </summary>
        /// <param name="nombre">nombre de verres de shooter à ajouter à la table shooter</param>
        /// <exception cref="Exception">Lance une exception si la table shooter n'existe pas</exception>
        /// <seealso cref="DataBase.AjouterShooter(int)"/> Ajouter des ingrédients
        public void AjouterShooter(ref int nombre )
        {
            try
            {
                string cmd = "update verreshooter set nbshooter= " + nombre.ToString() ;

                OracleCommand disc = new OracleCommand(cmd, EtatBaseDonnées);
                disc.ExecuteNonQuery();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            try
            {
                string cmd1 = "commit";
                OracleCommand disc1 = new OracleCommand(cmd1, EtatBaseDonnées);
                disc1.ExecuteNonQuery();
            }
            catch (Exception) { MessageBox.Show(" Échec de l'enregistrement."); }
        }

        /// <summary>
        /// Permet d'ajouter des ingrédients à la table ingrédient de la base de données
        /// </summary>
        /// <param name="ingredients">Une collection contenant toutes les informations nécessaires sur l'ingrédient à
        /// ajouter, à savoir le code de la bouteille, le nom, les positions (x,yet z) et la quantité du liquide</param>
        /// <exception cref="Exception">Lance une exception si l'un des éléments de la liste présente une erreur</exception>
        /// <seealso cref="DataBase.AjouterShooter(int)"/> Ajouter des verres de shooter
        public void AjouterIngredients(List<string> ingredients)
        {
            try
            {
                string cmd = "insert into ingredient values(" + Int32.Parse(ingredients[0]) + "," + Int32.Parse(ingredients[1]) + ","
                    + Int32.Parse(ingredients[2]) + "," + Int32.Parse(ingredients[3]) + "," + "'1'" + "," + Int32.Parse(ingredients[4]) + ",'"
                    + ingredients[5] + "','" + ingredients[6] + "')";

                OracleCommand insert = new OracleCommand(cmd, EtatBaseDonnées);
                insert.ExecuteNonQuery();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }

            try
            {
                string cmd1 = "commit";
                OracleCommand commit = new OracleCommand(cmd1, EtatBaseDonnées);
                commit.ExecuteNonQuery();
            }
            catch (Exception) { MessageBox.Show(" Échec de l'enregistrement."); }
        }

        /// <summary>
        /// Cette méthode permet de supprimer toutes les commandes de la table Commande
        /// </summary>
        /// <exception cref="Exception">Lève une exception si la table commande n'existe pas</exception>
        /// <see cref="DataBase.SupprimerCommande(string)"/> Pour supprimer la commande dont le numero est en paramètre
        public void SupprimerCommande()
        {
            try
            {
                string cmd = "delete  from commande";

                OracleCommand delete = new OracleCommand(cmd, EtatBaseDonnées);
                delete.ExecuteNonQuery();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message); }
        }

        /// <summary>
        /// Cette méthode permet de supprimer la commandes dont le numéro est passé en paramètre.
        /// </summary>
        /// <exception cref="Exception">Lève une exception si la table commande n'existe pas</exception>
        /// <see cref="DataBase.SupprimerCommande()"/> Pour supprimer toutes les commandes disponibles
        public void SupprimerCommande(int num)
        {
            try
            {
                string cmd = "delete  from commande where numcommande="+num.ToString();

                OracleCommand delete = new OracleCommand(cmd, EtatBaseDonnées);
                delete.ExecuteNonQuery();
            }
            catch (Exception sel) { MessageBox.Show(sel.Message.ToString()); }
        }
    }


   /// <summary>
   /// Classe contenant des méthodes indispensables pour passer une commande
   /// </summary>
    public class Commande
    {
        private SpecificateurCommande commande;
        //private CRS_A255 robot;
        //private DataBase base2Donnees;

        /// <summary>
        /// Constructeur par défaut,
        /// Permet de construire une commande normale
        /// </summary>
        //public Commande()
        //{
        //    commande = new Commande_Normale();
        //    robot = CRS_A255.Instance;
        //    base2Donnees = DataBase.instance_bd;
        //}

        /// <summary>
        /// Constructeur paramétrique,
        /// permet de construire une commande en fonction de son numéro identificateur
        /// </summary>
        /// <param name="num">Le numéro idificateur de commande, soit 0 pour normale et 1 pour shooter</param>
        public Commande(int num)
        {
            if (num == 0)
                commande = new Commande_Normale();
            else
                commande = new Shooter();
        }

        //public void ServirClient(int item1, int item2)
        //{

        //    if (item2 == 0)
        //    {
        //        commande = new Commande_Normale();
        //        var p = commande.TypeReel();
        //        var ing = p.Ingredients(item1);

        //        while(!robot.MakeDrink(ing.ToList()));
        //    }
        //    else
        //    {
        //        /*
        //         * IL S'AGIT D'UN SHOOTER
        //         */
        //        commande = new Shooter();
        //        var p = commande.TypeReel();
        //        var ing = p.Ingredients(item1);
        //    }

        //}

        /// <summary>
        /// Cette méthode permet de déterminer le type réel d'une commande
        /// </summary>
        /// <returns>Retourne  le type de la commande, soit normale ou shooter</returns>
        public SpecificateurCommande TypeReel()
        {
            return commande.TypeReel();
        }

        /// <summary>
        /// Cette méthode permet de lister tous les ingrédients constituant une commande
        /// </summary>
        /// <param name="numcom">Numéro de  commande</param>
        /// <returns>Collection contenant la position et la quantité de la commande</returns>
        public List<(Position, int)> ListerIngredients(int numcom)
        {
            return commande.Ingredients(numcom);
        }
    }


    // CLASS QUI GÈRE LES COMMANDES PASSÉES PAR LE CLIENT ANDROID(Interface)
    public abstract class SpecificateurCommande
    {
        protected DataBase Connexion { get; } = DataBase.instance_bd;

        // ENUMÈRE LA LISTE DES INGRÉDIENTS DE LA COMMANDE DU CLIENT ANDROID
        public abstract List<(Position, int)> Ingredients(int numcom);
        // OBTIENT LE TYPE RÉEL DE LA COMMANDE,
        // SOIT (NORMALE OU SHOOTER)
        public abstract SpecificateurCommande TypeReel();

    }



    /// <summary>
    /// Classe gérant les commandes de type Shooter
    /// </summary>
    /// <seealso cref="Commande_Normale"/> Classe Commande normale
    class Shooter : SpecificateurCommande
    {
        List<(Position, int)> ingredients = new List<(Position, int)>();
        public Shooter() : base()
        {

        }
        public override List<(Position, int)> Ingredients(int numcom)
        {
            string cmd = "select e.POSITIONX,e.POSITIONY,e.POSITIONZ,c.QTY from ingredient e inner join commande c on e.codebouteille=c.ingredient where c.numcommande=" + numcom.ToString();

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


    /// <summary>
    /// Classe gérant les commandes normales
    /// </summary>
    /// <seealso cref="Shooter"/> Classe Shooter
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
