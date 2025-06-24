using iTasks.Data;
using iTasks.Models;
using iTasks.Controllers;
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
using iTasks.Views;

namespace iTasks
{
    public partial class frmKanban : Form
    {
        public Utilizador _user;
        public TarefaController controller = new TarefaController();
        public List<Tarefa> listaTarefas = new List<Tarefa>();
        public List<Tarefa> tarefasToDo;
        public List<Tarefa> tarefasDoing;
        public List<Tarefa> tarefasDone;
        public frmKanban(Utilizador userLogado)
        {
            InitializeComponent();
            _user = userLogado;
            //Grisa o menu de gestão de users se o utilizador não for gestor. (SE FOR PROGRAMADOR)
            if (_user is Gestor)
            {
                if (_user is Gestor gestor && gestor.gereUtilizadores == true)
                {
                    gerirUtilizadoresToolStripMenuItem.Enabled = true;
                }
                else
                {
                    gerirUtilizadoresToolStripMenuItem.Enabled = false;
                }
                btNova.Enabled = true;
            }
            else
            {
                utilizadoresToolStripMenuItem.Enabled = false;
                btNova.Enabled = false;
                exportarParaCSVToolStripMenuItem.Enabled = false;
                btApagarTarefa.Enabled = false;
            }
            labelBemVindo.Text = $"Bem-vindo, {userLogado.Nome}"; // alterei para mostrar nome em vez de username ( MP - 15/06/2025)
            CarregarTarefas();
        }

        private void btSetDoing_Click_1(object sender, EventArgs e) //BTN AVANCAR TAREFA
        {
            var userLogado = _user;
            Tarefa tarefaSelecionada = verOndeEstaTarefaSelecionada(); ;

            listaTarefas = controller.GetTarefas();

            if (tarefaSelecionada == null)
            {
                MessageBox.Show("AVISO: Selecione uma tarefa primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                switch (tarefaSelecionada.EstadoAtual)
                {
                    case EstadoAtual.ToDo:
                        if (userLogado.Id == tarefaSelecionada.Programador.Id)
                        {
                            List<Tarefa> tarefasDoingProg = controller.GetTarefasProgramadorDoing(userLogado.Id);
                            if (tarefasDoingProg.Count < 2)
                            {
                                Tarefa tarefaMenor = controller.GetTarefasProgramadorMenorOrdem(userLogado.Id);
                                if (tarefaMenor == tarefaSelecionada)
                                {
                                    controller.AvancarTarefa(tarefaSelecionada);
                                }
                                else
                                {
                                    MessageBox.Show("ERRO: Ordem", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //não é a tarefa menor, logo não pode ser mudada
                                    return;
                                }

                            }
                            else
                            {
                                MessageBox.Show("ERRO: 2 Tarefas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //já tem 2 tarefas no doing
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("ERRO: User", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //não é o user que está logado
                            return;
                        }
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
        }

        private void btSetTodo_Click_1(object sender, EventArgs e) //BTN REINICIAR TAREFA
        {
            Tarefa tarefaSelecionada = verOndeEstaTarefaSelecionada();
            if (tarefaSelecionada == null)
            {
                MessageBox.Show("AVISO: Selecione uma tarefa primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                switch (tarefaSelecionada.EstadoAtual)  //VALIDACAO A VER SE ALGO FOR SELECIONADO
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
        }

        private void btSetDone_Click(object sender, EventArgs e) //BTN TERMINAR TAREFA
        {
            Tarefa tarefaSelecionada = verOndeEstaTarefaSelecionada();
            var userLogado = _user;

            if (tarefaSelecionada == null)
            {
                MessageBox.Show("AVISO: Selecione uma tarefa primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                switch (tarefaSelecionada.EstadoAtual)  //VALIDACAO A VER SE ALGO FOR SELECIONADO
                {
                    case EstadoAtual.ToDo:
                        MessageBox.Show(
                                "ERRO: Está a tentar terminar uma tarefa que ainda não foi iniciada!",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        break;

                    case EstadoAtual.Doing:
                        if (tarefaSelecionada.Programador.Id == userLogado.Id)
                        {
                            //é o user logado
                            Tarefa tarefaMenor = controller.GetTarefasProgramadorMenorOrdem(userLogado.Id);
                            if (tarefaMenor == tarefaSelecionada)
                            {
                                controller.AvancarTarefa(tarefaSelecionada);
                                CarregarTarefas();
                            }
                            else
                            {
                                MessageBox.Show("ERRO: Ordem", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //não é a próxima tarefa, logo não pode ser finalizada
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("ERRO: Esta tarefa não lhe está atribuída.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                    case EstadoAtual.Done:
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
        }

        public void CarregarTarefas()
        {
            //Separação das tarefas por estados
            tarefasToDo = controller.ObterTarefasToDo();
            tarefasDoing = controller.ObterTarefasDoing();
            tarefasDone = controller.ObterTarefasDone();

            //Limpar os items que possam estar na list no início
            lstTodo.DataSource = null;
            lstDoing.DataSource = null;
            //lstDone.Items.Clear();

            //Adicionar os itens por lista, por estado.
            lstTodo.DataSource = tarefasToDo;
            lstDoing.DataSource = tarefasDoing;
            lstDone.DataSource = tarefasDone;
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

            var detalhesTarefa = new frmDetalhesTarefa(_user, null);
            detalhesTarefa.TarefaCriada += AtualizarListaTarefasToDo;
            detalhesTarefa.Show();
            CarregarTarefas();

        }


        //IMPEDIR QUE O USER SELECIONE TAREFAS SIMULTÂNEAS
        private void lstTodo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lstTodo.SelectedIndex != -1)
            {
                lstDoing.ClearSelected();
                lstDone.ClearSelected();
            }
        }

        private void lstDoing_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lstDoing.SelectedIndex != -1)
            {
                lstTodo.ClearSelected();
                lstDone.ClearSelected();
            }
        }

        private void lstDone_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lstDone.SelectedIndex != -1)
            {
                lstTodo.ClearSelected();
                lstDoing.ClearSelected();
            }
        }
        //IMPEDIR QUE O USER SELECIONE TAREFAS SIMULTÂNEAS


        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereTiposTarefas gereTiposTarefas = new frmGereTiposTarefas();
            gereTiposTarefas.ShowDialog();
        }

        private void btPrevisao_Click(object sender, EventArgs e)
        {

            if (!(_user is Gestor))
            {
                btPrevisao.Visible = false;

            }


            Tarefa tarefaSelecionada = verOndeEstaTarefaSelecionada();

            if (tarefaSelecionada == null)
            {
                MessageBox.Show(
                            "AVISO: Selecione uma tarefa para conseguir ver a sua previsão de conclusão.",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                return;
            }
            else
            {


                //POPUP
                frmPrevisaoConclusao frm = new frmPrevisaoConclusao(tarefaSelecionada);

                frm.ShowDialog();

            }
        }

        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultarTarefasConcluidas frm = new frmConsultarTarefasConcluidas();

            frm.ShowDialog();
        }

        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaTarefasEmCurso frm = new frmConsultaTarefasEmCurso();

            frm.ShowDialog();
        }

        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Ficheiros CSV (.csv)|.csv";
            saveFileDialog.Title = "Guardar tarefas concluídas como CSV";
            saveFileDialog.FileName = "tarefas_concluidas.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    controller.ExportarTarefasConcluidasParaCsv(saveFileDialog.FileName);
                    MessageBox.Show("Exportação concluída com sucesso!", "Sucesso");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao exportar: " + ex.Message, "Erro");
                }
            }
        }







        private void frmKanban_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void btLogout1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Close();
            Application.Exit();
        }

        private void AtualizarListaTarefasToDo()
        {
            lstTodo.DataSource = null;
            lstTodo.DataSource = controller.ObterTarefasToDo();
        }

        private void lstTodo_DoubleClick(object sender, EventArgs e)
        {
            Tarefa tarefaSelecionada = (Tarefa)lstTodo.SelectedItem;
            if (tarefaSelecionada != null)
            {
                var detalhesTarefa = new frmDetalhesTarefa(_user, tarefaSelecionada);
                detalhesTarefa.Show();
            }
        }

        private void btApagarTarefa_Click(object sender, EventArgs e)
        {

            if (lstTodo.SelectedIndex != -1)
            {
                // Guarda o resultado da MessageBox
                DialogResult resposta = MessageBox.Show(
                    "Pretende apagar a tarefa selecionada?",
                    "Aviso",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                // Verifica se o utilizador clicou em Sim
                if (resposta == DialogResult.Yes)
                {
                    var tarefaSelecionada = (Tarefa)lstTodo.SelectedItem;
                    controller.ApagarTarefa(tarefaSelecionada.Id);
                    AtualizarListaTarefasToDo();
                }
                // Não é necessário o else, pois se clicar em Não, não faz nada
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para apagar.", "Aviso");
            }

        }
    }
}