using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visual
{
    static class Program
    {
        private const string connectionString =
         "User Id=jenapp;Password=jen123;Data Source=192.168.1.25:1521/XEPDB1;";
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmPrincipalcs());
        }
    }
}
