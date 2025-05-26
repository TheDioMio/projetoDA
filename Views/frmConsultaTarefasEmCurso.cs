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
    public partial class frmConsultaTarefasEmCurso : Form
    {
        private TarefaController _controller = new TarefaController();
        public frmConsultaTarefasEmCurso()
        {
            InitializeComponent();

            var tarefasEmCurso = _controller.ObterTarefasDoing();
            gvTarefasEmCurso.DataSource = tarefasEmCurso;
            gvTarefasEmCurso.ReadOnly = true;
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
