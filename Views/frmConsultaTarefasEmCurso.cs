using iTasks.Controllers;
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
    public partial class frmConsultaTarefasEmCurso : Form
    {
        private TarefaController _controller = new TarefaController();
        private Utilizador _user;
        public frmConsultaTarefasEmCurso(Utilizador user)
        {
            InitializeComponent();
            _user = user;

            var resumo = _controller.ObterResumoTarefasNaoConcluidas(_user);
            gvTarefasEmCurso.DataSource = resumo;
            gvTarefasEmCurso.ReadOnly = true;

            // Formata a coluna de dias restantes
            if (gvTarefasEmCurso.Columns.Contains("DiasRestantes"))
            {
                gvTarefasEmCurso.Columns["DiasRestantes"].DefaultCellStyle.Format = "N2";
            }
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
