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

namespace iTasks
{
    public partial class frmDetalhesTarefa : Form
    {
        private Utilizador _gestor;
        private List<TipoTarefa> listaTipoTarefas;
        private List<Programador> listaProgramadores;
        public UtilizadorController userController = new UtilizadorController();
        public TarefaController tarefaController = new TarefaController();

        public frmDetalhesTarefa(Utilizador user)
        {
            InitializeComponent();
            listaProgramadores = userController.GetProgramadores();
            cbProgramador.DataSource = listaProgramadores;
            listaTipoTarefas = tarefaController.GetTipoTarefas();
            cbTipoTarefa.DataSource = listaTipoTarefas;
            _gestor = user;
        }

        public bool validaCamposTarefa()
        {

            if (string.IsNullOrEmpty(txtDesc.Text))
            {
                MessageBox.Show("A descrição da tarefa é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDesc.Focus();
                return false;
            }

            if (cbTipoTarefa.SelectedIndex<0)
            {
                MessageBox.Show("O tipo de tarefa não é válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbTipoTarefa.Focus();
                return false;
            }

            if (cbProgramador.SelectedIndex < 0)
            {
                MessageBox.Show("O programador associado a tarefa não é válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbProgramador.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtOrdem.Text))
            {
                MessageBox.Show("A ordem de execução da tarefa é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrdem.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtStoryPoints.Text))
            {
                MessageBox.Show("O valor de Story Points da tarefa é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStoryPoints.Focus();
                return false;
            }

            // podemos ainda validar a data para ver se é anterior ao dia atual
            // e validar ainda se a data prevista de fim não é anterior á data prevista de inicio

            return true;
        }


        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            if (validaCamposTarefa())
            {
               // temos de validar se ordem e story Points são numeros
                string desc = txtDesc.Text;
                
                int ordem;
                if (!int.TryParse(txtOrdem.Text, out ordem))
                {
                    MessageBox.Show("O valor do campo ordem de execução não é um valor válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOrdem.Text = "";
                    txtOrdem.Focus();
                    return;
                }

                int storyPoints;
                if (!int.TryParse(txtStoryPoints.Text, out storyPoints))
                {
                    MessageBox.Show("O valor do campo Story Points não é um valor válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStoryPoints.Text = "";
                    txtStoryPoints.Focus();
                    return;
                }
                                
                EstadoAtual estado = EstadoAtual.ToDo;
                DateTime dataPrevInicio = dtInicio.Value;
                DateTime dataPrevFim = dtFim.Value;
                Gestor gestor = _gestor as Gestor;
                TipoTarefa tipotarefa = listaTipoTarefas[cbTipoTarefa.SelectedIndex];
                Programador programador = listaProgramadores[cbProgramador.SelectedIndex];

                Tarefa tarefa = new Tarefa();

                tarefa.Descricao = desc;
                tarefa.OrdemExecucao = ordem;
                tarefa.DataCriacao = DateTime.Now;
                tarefa.StoryPoints = storyPoints;
                tarefa.DataPrevistaInicio = dataPrevInicio;
                tarefa.DataPrevistaFim = dataPrevFim;
                tarefa.EstadoAtual = estado;
                tarefa.Gestor = gestor;
                tarefa.Programador = programador;
                tarefa.TipoTarefa = tipotarefa;
                tarefa.DataRealInicio = DateTime.Now;
                tarefa.DataRealFim = DateTime.Now;

               

                bool success = tarefaController.Criar(tarefa);

                if (success)
                {
                    // correu bem proceder
                    //ficou aqui este código pois devemos analisar o que fazer a seguir,
                    //ou fechamos a janela, ou deixamos introduzir mais tarefas
                }
                else
                {
                    MessageBox.Show("Alguma coisa não correu bem, não foi possivel criar a Tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
