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
        private Utilizador _user;
        public frmKanban(Utilizador user)
        {
            InitializeComponent();
            labelBemVindo.Text = "Bem vindo: " + user.Nome;
            _user = user;

            //Grisa o menu de gestão de users se o utilizador não for gestor. (SE FOR PROGRAMADOR)
            if (user is Gestor)
            {
                utilizadoresToolStripMenuItem.Enabled = true;
            }
            else
            {
                utilizadoresToolStripMenuItem.Enabled = false;
            }

        }

        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereUtilizadores gereUtilizadores = new frmGereUtilizadores();
            gereUtilizadores.ShowDialog();
        }

        private void btNova_Click(object sender, EventArgs e)
        {
            var detalhesTarefa = new frmDetalhesTarefa(_user);
            detalhesTarefa.Show();
        }
    }
}
