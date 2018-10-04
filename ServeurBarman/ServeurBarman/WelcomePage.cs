using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ServeurBarman
{
    public partial class WelcomePage : UserControl
    {
        List<Drinks> drink = new List<Drinks>();
        public List<string> Nombre { get; private set; } = new List<string>();
        public WelcomePage()
        {
            InitializeComponent();
            
        }

        public void Init_UserUI()
        {
            Nombre.Clear();
            for (int i = 0; i < drink.Count; ++i)
                Nombre.Add(drink[i].nomRecette);

            TBX_NombreClient.Text = Nombre.Count.ToString();
        }

        private void WelcomePage_Load(object sender, EventArgs e)
        {

        }
    }
}
