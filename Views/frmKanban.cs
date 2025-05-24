using iTasks.Controllers;
using iTasks.Data;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmKanban : Form
    {
        public iTasksContexto Contexto = new iTasksContexto();
        public TarefaController controller = new TarefaController();
        private List<Tarefa> tarefasToDo;
        private List<Tarefa> tarefasDoing;
        private List<Tarefa> tarefasDone;

        public frmKanban(Utilizador userLogado)
        {
            InitializeComponent();
            labelBemVindo.Text = $"Bem-vindo, {userLogado.Username}";
            CarregarTarefas();
        }

        private void btSetDoing_Click(object sender, EventArgs e)
        {
            Tarefa tarefaSelecionada = lstTodo.SelectedItem as Tarefa;
            controller.AvancarTarefa(tarefaSelecionada);

            //Atualizar as listas depois de mudar o estado da tarefa
            CarregarTarefas();
        }

        private void btSetTodo_Click(object sender, EventArgs e)
        {
            Tarefa tarefaSelecionada = lstDoing.SelectedItem as Tarefa;
            controller.RetrocederTarefa(tarefaSelecionada);

            //Atualizar as listas depois de se mudar o estado da tarefa
            CarregarTarefas();
        }

        private void btSetDone_Click(object sender, EventArgs e)
        {
            Tarefa tarefaSelecionada = lstDoing.SelectedItem as Tarefa;
            controller.AvancarTarefa(tarefaSelecionada);

            //Atualizar as listas depois de se mudar o estado da tarefa
            CarregarTarefas();
        }




        public void CarregarTarefas()
        {
            //Separação das tarefas por estados
            var tarefasToDo = Contexto.Tarefas.Where(tarefa => tarefa.EstadoAtual == EstadoAtual.ToDo).ToList();
            var tarefasDoing = Contexto.Tarefas.Where(tarefa => tarefa.EstadoAtual == EstadoAtual.Doing).ToList();
            var tarefasDone = Contexto.Tarefas.Where(tarefa => tarefa.EstadoAtual == EstadoAtual.Done).ToList();

            //Limpar os items que possam estar na list no início
            lstTodo.Items.Clear();
            lstDoing.Items.Clear();
            lstDone.Items.Clear();

            //Adicionar os itens por lista, por estado.
            lstTodo.Items.AddRange(tarefasToDo.ToArray());
            lstDoing.Items.AddRange(tarefasDoing.ToArray());
            lstDone.Items.AddRange(tarefasDone.ToArray());
        }
    }
}
