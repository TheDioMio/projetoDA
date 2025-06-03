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
                utilizadoresToolStripMenuItem.Enabled = true;
            }
            labelBemVindo.Text = $"Bem-vindo, {userLogado.Username}";
            CarregarTarefas();
        }
        
        private void btSetDoing_Click_1(object sender, EventArgs e) //BTN AVANCAR TAREFA
        {
            Tarefa tarefaSelecionada = verOndeEstaTarefaSelecionada();
            if (tarefaSelecionada == null)
            {
                MessageBox.Show("Selecione uma tarefa primeiro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_user is Programador programador)
            {
                listaTarefas = controller.GetTarefas();
                if (VerificacoesMudar_ToDo_Doing(tarefaSelecionada, programador, listaTarefas, EstadoAtual.Doing))
                {
                    controller.AvancarTarefa(tarefaSelecionada);
                }
            }

            CarregarTarefas();

            //switch (tarefaSelecionada.EstadoAtual)
            //{
            //    case EstadoAtual.ToDo:
            //        if (userLogado is Programador programador) //Converte o userLogado do tipo Utilizador para o tipo Programador, caso o seja
            //        {
            //            if (VerificacoesMudar_ToDo_Doing(tarefaSelecionada, programador, listaTarefas, EstadoAtual.ToDo) == true)
            //            {
            //                controller.AvancarTarefa(tarefaSelecionada);
            //            }
            //            else
            //            {
            //                MessageBox.Show(
            //                "ERRO: VALIDAÇÃO DENTRO DA FUNÇÃO",
            //                "Aviso",
            //                MessageBoxButtons.OK,
            //                MessageBoxIcon.Error);
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show(
            //                "ERRO: VALIDAÇÃO DENTRO DO BTN",
            //                "Aviso",
            //                MessageBoxButtons.OK,
            //                MessageBoxIcon.Error);
            //        }
            //        CarregarTarefas();
            //        break;

            //    case EstadoAtual.Doing:
            //        MessageBox.Show(
            //                "ERRO: Está a tentar avançar uma tarefa que já está em Doing!",
            //                "Aviso",
            //                MessageBoxButtons.OK,
            //                MessageBoxIcon.Error);
            //        break;

            //    case EstadoAtual.Done:
            //        MessageBox.Show(
            //                "ERRO: Está a tentar avançar uma tarefa que já terminou!",
            //                "Aviso",
            //                MessageBoxButtons.OK,
            //                MessageBoxIcon.Error);
            //        break;

            //    default:
            //        MessageBox.Show("Estado desconhecido!");
            //        break;
            //}
        }

        public bool VerificacoesMudar_ToDo_Doing(Tarefa tarefaSelecionada, Programador programadorLogado, List<Tarefa> listaTarefas, EstadoAtual novoEstado)
        {
            //FLAG 1 - Ter alguma coisa NULL
            if (tarefaSelecionada == null || programadorLogado == null || listaTarefas == null)
            {
                MessageBox.Show(
                            "ERRO: Tarefa ou programador inválido",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                return false;
            }

            //FLAG 2 - Verificar se a tarefa pertence ao programador logado
            if (tarefaSelecionada.Programador.Id != programadorLogado.Id)
            {
                MessageBox.Show(
                            "ERRO: A tarefa não pertence ao programador logado",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                return false;
            }

            //FLAG 3 - Transição: ToDo -> Doing
            if (tarefaSelecionada.EstadoAtual == EstadoAtual.ToDo && novoEstado == EstadoAtual.Doing)
            {
                // 3.1. Verificar limite de 2 tarefas no estado Doing
                int doingCount = listaTarefas.Count(t => t.Programador.Id == programadorLogado.Id && t.EstadoAtual == EstadoAtual.Doing);
                if (doingCount >= 2)
                {
                    MessageBox.Show(
                            "ERRO: O programador já possui 2 tarefas em Doing",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    return false;
                }

                // 3.2. Verificar se é a tarefa com menor ordem entre as que estão em ToDo para o programador
                var menorToDo = listaTarefas
                    .Where(tarefa => tarefa.Programador.Id == programadorLogado.Id && tarefa.EstadoAtual == EstadoAtual.ToDo)
                    .OrderBy(tarefa => tarefa.OrdemExecucao)
                    .FirstOrDefault();

                if (menorToDo == null || tarefaSelecionada.Id != menorToDo.Id)
                {
                    MessageBox.Show(
                            "ERRO: A tarefa não é a próxima na ordem de execução",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }

            return false; //DEFAULT PARA RETORNAR FALSE.
        }


        public bool VerificacoesMudar_Doing_Done(Tarefa tarefaSelecionada, Programador programadorLogado, List<Tarefa> listaTarefas, EstadoAtual novoEstado)
        {
            //FLAG 1 - Ter alguma coisa NULL
            if (tarefaSelecionada == null || programadorLogado == null || listaTarefas == null)
            {
                MessageBox.Show(
                            "ERRO: Tarefa ou programador inválido",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                return false;
            }

            //FLAG 2 - Verificar se a tarefa pertence ao programador logado
            if (tarefaSelecionada.Programador.Id != programadorLogado.Id)
            {
                MessageBox.Show(
                            "ERRO: A tarefa não pertence ao programador logado",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                return false;
            }

            //FLAG 3 - Verificar se é a tarefa com menor ordem entre as que estão em Doing
            if (tarefaSelecionada.EstadoAtual == EstadoAtual.Doing && novoEstado == EstadoAtual.Done)
            {
                var menorDoing = listaTarefas
                    .Where(tarefa => tarefa.Programador.Id == programadorLogado.Id && tarefa.EstadoAtual == EstadoAtual.Doing)
                    .OrderBy(tarefa => tarefa.OrdemExecucao)
                    .FirstOrDefault();

                if (menorDoing == null || tarefaSelecionada.Id != menorDoing.Id)
                {
                    MessageBox.Show(
                            "ERRO: A tarefa não é a próxima na ordem de execução",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }

            return false; //Default retorna FALSE
        }

        /*Validações a fazer para passar de ToDo para Doing:
            1. A tarefa tem de pertencer ao Programador que está logado
                (CADA PROGRAMADOR SÓ PODE MOVIMENTAR AS SUAS TAREFAS),
            2. O programador só pode ter no máx 2 tarefas no "Doing" ao msm tempo,
            3. A tarefa tem de estar na ordem de execução correta
                (1, só depois 2, etc etc etc)*/

        /*Validações a fazer para passar de Doing para Done:
            1. A tarefa tem de pertencer ao Programador que está logado,
            2. A tarefa tem de estar no estado Doing,
            3. A tarefa tem de ser a próxima na ordem de execução*/

        /*OBS IMPORTANTES:
         Gestor não pode atribuir duas tarefas com a mesma ordem a um programador
        As datas reais de início e fim são automaticamente atualizadasw:
        DATA DE INICIO É QUANDO A TAREFA PASSA PARA DOING,
        DATA DE FIM QUANDO PASSA PARA DONE.*/













        private void btSetTodo_Click_1(object sender, EventArgs e) //BTN REINICIAR TAREFA
        {
            //utilizadoresToolStripMenuItem.Enabled = false;
            Tarefa tarefaSelecionada = verOndeEstaTarefaSelecionada();
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

        private void btSetDone_Click(object sender, EventArgs e) //BTN TERMINAR TAREFA
        {
            Tarefa tarefaSelecionada = verOndeEstaTarefaSelecionada();
            if (tarefaSelecionada == null)
            {
                MessageBox.Show("Selecione uma tarefa primeiro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_user is Programador programador)
            {
                listaTarefas = controller.GetTarefas();

                if (VerificacoesMudar_Doing_Done(tarefaSelecionada, programador, listaTarefas, EstadoAtual.Done))
                {
                    controller.AvancarTarefa(tarefaSelecionada);
                }
            }

            CarregarTarefas();
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
            //lstTodo.Items.AddRange(tarefasToDo.ToArray());
            lstDoing.DataSource = tarefasDoing;
            //lstDone.Items.AddRange(tarefasDone.ToArray());
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

            if(tarefaSelecionada == null)
            {
                MessageBox.Show(
                            "AVISO: Selecione uma tarefa para conseguir ver a sua previsão de conclusão.",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                return;
            } else {


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
            saveFileDialog.Filter = "Ficheiros CSV (*.csv)|*.csv";
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
    }
}
