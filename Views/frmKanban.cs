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
        private List<Tarefa> tarefasToDo;
        private List<Tarefa> tarefasDoing;
        private List<Tarefa> tarefasDone;

        public void CarregarTarefas()
        {
            var todasTarefas = Contexto.Tarefas.ToList();

            var tarefasToDo = todasTarefas.Where(tarefa => tarefa.EstadoAtual == EstadoAtual.ToDo).ToList();
            var tarefasDoing = todasTarefas.Where(tarefa => tarefa.EstadoAtual == EstadoAtual.Doing).ToList();
            var tarefasDone = todasTarefas.Where(tarefa => tarefa.EstadoAtual == EstadoAtual.Done).ToList();

            lstTodo.Items.Add(tarefasToDo);
            lstDoing.Items.Add(tarefasDoing);
            lstDone.Items.Add(tarefasDone);
        }
        public frmKanban(Utilizador userlogado)
        {
            InitializeComponent();
            labelBemVindo.Text = $"Bem-vindo, {userlogado.Username}";
            CarregarTarefas();
        }



        private void btSetDoing_Click(object sender, EventArgs e)
        {
            CarregarTarefas();
            lstTodo.Items.Add(tarefasToDo);
        }
    }
}
