using iTasks.Controllers;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace iTasks
{
//        ->  ao criar uma tarefa tratar melhor a data real de inicio e de fim em caso da tarefa estár no estado ToDo
//      8.  Apenas os Gestores podem criar Tarefas; não esá feito, quando abre a janela kambam, se o utilizador for programador esconder o botão para criar tarefas
//      9.  Um Gestor apenas pode associar os seus programadores a uma tarefa; de momento está a carregar todos, alterar
//      10. Na criação de uma nova Tarefa, o campo do gestor deverá ser preenchido automaticamente
//          com o Id do Gestor que a está a criar;                                                      feito
//      22. O Programador poderá consultar os detalhes da Tarefa selecionada.Utilizar a mesma janela
//          de criar tarefas em modo ReadOnly para a visualização dos detalhes de uma Tarefa; 
//      23. O Gestor, poderá criar e alterar os dados de uma Tarefa;



    public partial class frmDetalhesTarefa : Form
    {
        private Utilizador _gestor;
        private Tarefa _tarefa;
        private List<TipoTarefa> listaTipoTarefas;
        private List<Programador> listaProgramadores;
        public UtilizadorController userController = new UtilizadorController();
        public TarefaController tarefaController = new TarefaController();
        private bool editavel = false;

        public delegate void TarefaCriadaHandler(); 
        
        public event Action TarefaCriada;

        public frmDetalhesTarefa(Utilizador user, Tarefa tarefa)
        {
            InitializeComponent();
            _tarefa = tarefa;
            // carrega a lista de tipos de tarefa
            listaTipoTarefas = tarefaController.GetTipoTarefas();
            cbTipoTarefa.DataSource = listaTipoTarefas;

            // se atarefa vier a null é porque é uma tarefa nova
            // se for um gestor pode criar as tarefas
            if (tarefa == null)
            {
                //if ((user is Gestor)) // se o utilizador é um gestor pode criar a tarefa
                //{

                // é uma tarefa nova
                // carrega a lista de progaramadores do gestor 
                    retirarEnable();
                    listaProgramadores = userController.ObterProgramadoresDeGestor(user as Gestor);
                    cbProgramador.DataSource = listaProgramadores;
                    _gestor = user;
                    dtInicio.Value = DateTime.Today;
                    dtFim.Value = DateTime.Today.AddDays(1);
                    
                //}  
            }
            else //se a tarefa vier preenchida é porque é uma tarefa já criada
            {
                if ((tarefa.Gestor.Id == user.Id)&&(tarefa.EstadoAtual != EstadoAtual.Done)) // a tarefa foi criada pelo mesmo gestor que a está a abrir ou seja pode editar
                {
                    listaProgramadores = userController.ObterProgramadoresDeGestor(tarefa.Gestor);
                    cbProgramador.DataSource = listaProgramadores;
                    retirarEnable();
                }
                else
                {
                    listaProgramadores = userController.GetProgramadores();
                    cbProgramador.DataSource = listaProgramadores;
                    colocaEnable();
                }
                cbProgramador.SelectedItem = tarefa.Programador;
                   
                if (tarefa.DataRealInicio == null)
                {
                    txtDataRealini.Text = "";
                }
                else
                {
                    txtDataRealini.Text = tarefa.DataRealInicio.ToString();
                }

                if (tarefa.DataRealFim == null)
                {
                    txtdataRealFim.Text = "";
                }
                else
                {
                    txtdataRealFim.Text = tarefa.DataRealFim.ToString();
                }

                txtId.Text = tarefa.Id.ToString();
                txtEstado.Text = tarefa.EstadoAtual.ToString();
                txtDataCriacao.Text = tarefa.DataCriacao.ToString();
                txtDesc.Text = tarefa.Descricao;
                txtOrdem.Text = tarefa.OrdemExecucao.ToString();
                txtStoryPoints.Text = tarefa.StoryPoints.ToString();
                dtInicio.Value = tarefa.DataPrevistaInicio;
                dtFim.Value = tarefa.DataPrevistaFim;
                cbTipoTarefa.SelectedItem = tarefa.TipoTarefa;
                _gestor = tarefa.Gestor;
            }
        }

        private void colocaEnable()
        {
            txtDesc.Enabled = false;
            txtStoryPoints.Enabled = false;
            dtInicio.Enabled = false;
            dtFim.Enabled = false;
            cbTipoTarefa.Enabled = false;
            cbProgramador.Enabled = false;
            btGravar.Enabled = false;
            editavel = false;
        }

        private void retirarEnable()
        {
            txtDesc.Enabled = true;
            txtStoryPoints.Enabled = true;
            dtInicio.Enabled = true;
            dtFim.Enabled = true;
            cbTipoTarefa.Enabled = true;
            cbProgramador.Enabled = true;
            btGravar.Enabled = true;
            editavel = true;
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

            if (dtInicio.Value > dtFim.Value)
            {
                MessageBox.Show("A data prevista de inicio de tarefa não pode ser superior á data prevista de fim.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtInicio.Focus();
                return false;
            }

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

                


                if (string.IsNullOrEmpty(txtId.Text)) // é uma nova tarefa
                {
                    // vê se a data de inicio não é inferior a data atual
                    // só se aplica a criar, pois quando é a editar pode ser anterior
                    if (dtInicio.Value.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show("A data prevista de inicio não pode ser inferior a data atual!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Tarefa tarefa = new Tarefa();

                    tarefa.Descricao = desc;
                    tarefa.OrdemExecucao = ordem;
                    tarefa.DataCriacao = DateTime.Now;
                    tarefa.StoryPoints = storyPoints;
                    tarefa.DataPrevistaInicio = dtInicio.Value;
                    tarefa.DataPrevistaFim = dtFim.Value;
                    tarefa.EstadoAtual = EstadoAtual.ToDo;
                    tarefa.Gestor = _gestor as Gestor;
                    tarefa.Programador = (Programador)cbProgramador.SelectedItem;
                    tarefa.TipoTarefa = (TipoTarefa)cbTipoTarefa.SelectedItem;
                    tarefa.DataRealInicio = null;
                    tarefa.DataRealFim = null;

                    bool success = tarefaController.Criar(tarefa);

                    if (success)
                    {
                        // correu bem proceder
                        //ficou aqui este código pois devemos analisar o que fazer a seguir,
                        //ou fechamos a janela, ou deixamos introduzir mais tarefas
                        MessageBox.Show("Tarefa criada com sucesso.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TarefaCriada?.Invoke(); // Chama o evento que atualiza listbox to do no Kanban 
                    }
                    else
                    {
                        MessageBox.Show("Alguma coisa não correu bem, não foi possivel criar a Tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else // então vamos editar a tarefa 
                {
                    _tarefa.Descricao = desc;
                    _tarefa.OrdemExecucao = ordem;
                    //_tarefa.DataCriacao = DateTime.Now;
                    _tarefa.StoryPoints = storyPoints;
                    _tarefa.DataPrevistaInicio = dtInicio.Value;
                    _tarefa.DataPrevistaFim = dtFim.Value;
                    //_tarefa.EstadoAtual = EstadoAtual.ToDo;
                    _tarefa.Gestor = _gestor as Gestor;
                    _tarefa.Programador = (Programador)cbProgramador.SelectedItem;
                    _tarefa.TipoTarefa = (TipoTarefa)cbTipoTarefa.SelectedItem;
                    //_tarefa.DataRealInicio = null;
                    _tarefa.DataRealFim = null;

                    bool success = tarefaController.Atualizar(_tarefa);

                    if (success)
                    {
                        // correu bem proceder
                        //ficou aqui este código pois devemos analisar o que fazer a seguir,
                        //ou fechamos a janela, ou deixamos introduzir mais tarefas
                        MessageBox.Show("Tarefa atualizada com sucesso.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TarefaCriada?.Invoke(); // Chama o evento que atualiza listbox to do no Kanban 
                    }
                    else
                    {
                        MessageBox.Show("Alguma coisa não correu bem, não foi possivel atualizar a Tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    

                    //    Tarefa tarefa = new Tarefa();

                    //tarefa.Descricao = desc;
                    //tarefa.OrdemExecucao = ordem;
                    //tarefa.DataCriacao = DateTime.Now;
                    //tarefa.StoryPoints = storyPoints;
                    //tarefa.DataPrevistaInicio = dtInicio.Value;
                    //tarefa.DataPrevistaFim = dtFim.Value;
                    //tarefa.EstadoAtual = EstadoAtual.ToDo;
                    //tarefa.Gestor = _gestor as Gestor;
                    //tarefa.Programador = (Programador)cbProgramador.SelectedItem;
                    //tarefa.TipoTarefa = (TipoTarefa)cbTipoTarefa.SelectedItem;


                    //tarefa.DataRealInicio = null;
                    //tarefa.DataRealFim = null;
                    ////tarefa.DataRealInicio = DateTime.MaxValue;
                    ////tarefa.DataRealFim = DateTime.MaxValue;

                    //if (string.IsNullOrEmpty(txtId.Text))
                    //{
                    //    bool success = tarefaController.Criar(tarefa);

                    //    if (success)
                    //    {
                    //        // correu bem proceder
                    //        //ficou aqui este código pois devemos analisar o que fazer a seguir,
                    //        //ou fechamos a janela, ou deixamos introduzir mais tarefas
                    //        MessageBox.Show("Tarefa criada com sucesso.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        TarefaCriada?.Invoke(); // Chama o evento que atualiza listbox to do no Kanban 
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Alguma coisa não correu bem, não foi possivel criar a Tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    }
                    //}
                    //else
                    //{
                    //    bool success = tarefaController.Atualizar(tarefa);

                    //    if (success)
                    //    {
                    //        MessageBox.Show("Tarefa atualizada com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        // correu bem proceder
                    //        //ficou aqui este código pois devemos analisar o que fazer a seguir,
                    //        //ou fechamos a janela, ou deixamos introduzir mais tarefas
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Alguma coisa não correu bem, não foi possivel atualizar a Tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    }
                    //}
                    this.Close();
                }
            }
        }

        private void cbProgramador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (editavel == true)
            {
                if (cbProgramador.SelectedIndex >= 0)
                {
                    Programador programador = (Programador)cbProgramador.SelectedItem;
                    Tarefa tarefaSelected = tarefaController.GetTarefasProgramadorMaiorOrdem(programador.Id);
                    if (tarefaSelected == null)
                    {
                        txtOrdem.Text = "1";
                    }
                    else
                    {
                        txtOrdem.Text = (tarefaSelected.OrdemExecucao + 1).ToString();
                    }
                }
            }      
        }
    }
}
