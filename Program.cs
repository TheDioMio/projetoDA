using iTasks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin()); //Inicia o frmLogin primeiro
            using (var ctx = new iTasksContexto())
            {
                var existeAlgo = ctx.Utilizadores.FirstOrDefault(); // Força uma query
            }
        }
    }
}
