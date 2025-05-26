using iTasks.Data;
using iTasks.Models;
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
    public partial class frmKanban : Form
    {
        private Utilizador _user;
        public iTasksContexto Contexto = new iTasksContexto();
        public TarefaController controller = new TarefaController();
        private List<Tarefa> tarefasToDo;
        private List<Tarefa> tarefasDoing;
        private List<Tarefa> tarefasDone;
        public frmKanban(Utilizador userLogado)
        {
            InitializeComponent();
            Contexto = new iTasksContexto();

            _user = userLogado;

            //Grisa o menu de gestão de users se o utilizador não for gestor. (SE FOR PROGRAMADOR)
            if (_user is Gestor)
            {
                utilizadoresToolStripMenuItem.Enabled = true;
            }
            labelBemVindo.Text = $"Bem-vindo, {userLogado.Username}";
            CarregarTarefas();
        }

        private void btSetDoing_Click(object sender, EventArgs e) //BTN AVANCAR TAREFA
        {
            Tarefa tarefaSelecionada = verOndeEstaTarefaSelecionada();
            switch (tarefaSelecionada.EstadoAtual)
            {
                case EstadoAtual.ToDo:
                    controller.AvancarTarefa(tarefaSelecionada);
                    CarregarTarefas();
                    break;

                case EstadoAtual.Doing:
                    MessageBox.Show(
                            "ERRO: Está a tentar avançar uma tarefa que já está em Doing!",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    break;

                case EstadoAtual.Done:
                    MessageBox.Show(
                            "ERRO: Está a tentar avançar uma tarefa que já terminou!",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    break;

                default:
                    MessageBox.Show("Estado desconhecido!");
                    break;
            }
        }

        private void btSetTodo_Click(object sender, EventArgs e) //BTN REINICIAR TAREFA
        {
            utilizadoresToolStripMenuItem.Enabled = false;
            Tarefa tarefaSelecionada = verOndeEstaTarefaSelecionada();
            switch (tarefaSelecionada.EstadoAtual)
            {
                
                case EstadoAtual.ToDo:
                    MessageBox.Show(
                            "ERRO: Impossível reiniciar uma tarefa em ToDo!",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    break;

                case EstadoAtual.Doing:
                    controller.RetrocederTarefa(tarefaSelecionada);
                    CarregarTarefas();
                    break;

                case EstadoAtual.Done:
                    MessageBox.Show(
                            "ERRO: Está a tentar reiniciar uma tarefa que já foi dada como terminada.",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    break;

                default:
                    MessageBox.Show("Estado desconhecido!");
                    break;
            }
        }

        private void btSetDone_Click(object sender, EventArgs e) //BTN TERMINAR TAREFA
        {
            Tarefa tarefaSelecionada = verOndeEstaTarefaSelecionada();
            switch (tarefaSelecionada.EstadoAtual)
            {
                case EstadoAtual.ToDo:
                    MessageBox.Show(
                            "ERRO: Está a tentar terminar uma tarefa que ainda não foi iniciada!",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    break;

                case EstadoAtual.Doing:
                    controller.AvancarTarefa(tarefaSelecionada);
                    CarregarTarefas();
                    break;

                case EstadoAtual.Done: //PQ É QUE NA PRIMEIRA VEZ, DÁ PARA FAZER ISTO, E DPS É QUE APARECE O AVISO?
                    MessageBox.Show(
                            "ERRO: Está a tentar terminar uma tarefa que já foi dada como terminada.",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    break;

                default:
                    MessageBox.Show("Estado desconhecido!");
                    break;
            }
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

        public Tarefa verOndeEstaTarefaSelecionada()
        {
            if (lstTodo.SelectedItem as Tarefa != null)
            {
                Tarefa tarefaSelecionada = lstTodo.SelectedItem as Tarefa;
                return tarefaSelecionada;
            }
            else if (lstDoing.SelectedItem as Tarefa != null)
            {
                Tarefa tarefaSelecionada = lstDoing.SelectedItem as Tarefa;
                return tarefaSelecionada;
            }
            else if (lstDone.SelectedItem as Tarefa != null)
            {
                Tarefa tarefaSelecionada = lstDone.SelectedItem as Tarefa;
                return tarefaSelecionada;
            }
            else
            {
                return null;
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
        //IMPEDIR QUE O USER SELECIONE TAREFAS SIMULTÂNEAS
        private void lstTodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTodo.SelectedIndex != -1)
            {
                lstDoing.ClearSelected();
                lstDone.ClearSelected();
            }
        }

        private void lstDoing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDoing.SelectedIndex != -1)
            {
                lstTodo.ClearSelected();
                lstDone.ClearSelected();
            }
        }

        private void lstDone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDone.SelectedIndex != -1)
            {
                lstTodo.ClearSelected();
                lstDoing.ClearSelected();
            }
        }

        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereTiposTarefas gereTiposTarefas = new frmGereTiposTarefas();
            gereTiposTarefas.ShowDialog();
        }
        //IMPEDIR QUE O USER SELECIONE TAREFAS SIMULTÂNEAS
    }
}
