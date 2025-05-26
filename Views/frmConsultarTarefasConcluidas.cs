using iTasks.Controllers;
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
    public partial class frmConsultarTarefasConcluidas : Form
    {
        private TarefaController _controller = new TarefaController();

        public frmConsultarTarefasConcluidas()
        {
            InitializeComponent();

            var tarefasConcluidas = _controller.ObterTarefasDone();
            gvTarefasConcluidas.DataSource = tarefasConcluidas;

            gvTarefasConcluidas.ReadOnly = true;

        }

        private void gvTarefasConcluidas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmTarefasConcluidas_Load(object sender, EventArgs e)
        {

            


        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
