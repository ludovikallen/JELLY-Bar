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
        public OracleConnection Connexion { get; set; }
        public List<string> Commands { get; private set; }
    }
}
