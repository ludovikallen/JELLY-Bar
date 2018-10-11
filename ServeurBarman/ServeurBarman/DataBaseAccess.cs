using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeurBarman
{
    class DataBaseAccess
    {
        public static List<Drinks> LoadDrink()
        {
            using (IDbConnection connection=new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.Query<Drinks>("select NomRecette from Recette", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveDrinks(Drinks drink)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
               connection.Execute("insert into Drinks (NomRecette) values (@NomRecette)", drink);
                
            }
        }

        private static string LoadConnectionString(string id="Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
