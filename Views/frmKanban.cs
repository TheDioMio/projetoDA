using iTasks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmKanban : Form
    {
        public frmKanban()
        {
            //InitializeComponent();
            //labelBemVindo.Text = $"Bem-vindo, {Sessao.UtilizadorLogado.Nome}";

            ////Grisa o menu de gestão de users se o utilizador não for gestor. (SE FOR PROGRAMADOR)
            //if (Sessao.UtilizadorLogado.Tipo == TipoUtilizador.Programador)
            //{
            //    utilizadoresToolStripMenuItem.Enabled = false;
            //} else
            //{
            //    utilizadoresToolStripMenuItem.Enabled = true;
            //}

        }
    }
}
