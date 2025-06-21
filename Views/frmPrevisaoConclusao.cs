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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace iTasks.Views
{
    
    public partial class frmPrevisaoConclusao: Form
    {
        public TarefaController controller = new TarefaController();

        private Tarefa tarefa;

        public frmPrevisaoConclusao(Tarefa tarefaSelecionada)
        {
            InitializeComponent();
            tarefa = tarefaSelecionada;
            MostrarDadosTarefa();
            AtualizarTempoPrevisto();
        }

        private void btnFechar(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AtualizarTempoPrevisto()
        {
            //double tempoPrevisto = controller.CalcularTempoTotalPrevistoParaToDo();
            //txtPrevisao.Text = tempoPrevisto.ToString("0.##") + " horas";
        }

    

        private void MostrarDadosTarefa()
        {
            txtTarefa.Text = tarefa.Descricao;
            
        }



    }
}
