using iTasks.Controllers;
using iTasks.Data;
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
    public partial class frmGereTiposTarefas : Form
    {
        private TipoTarefaController _controller = new TipoTarefaController();
        public frmGereTiposTarefas()
        {
            InitializeComponent();
            AtualizarListaTiposTarefa();
            

        }


        private void btGravar_Click(object sender, EventArgs e)
        {


            string nome = txtDesc.Text;
            if (string.IsNullOrWhiteSpace(nome))
            {
                
                MessageBox.Show("Por favor preencha o nome da tarefa.", "Atenção!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (_controller.NomeExiste(nome))
            {
                MessageBox.Show("Já existe um tipo de tarefa com esse nome.", "Validação", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

                _controller.Adicionar(nome);
                AtualizarListaTiposTarefa();
                txtDesc.Clear();

            }
            
        }
        

        private void frmGereTiposTarefas_Load(object sender, EventArgs e)
        {
            AtualizarListaTiposTarefa();
        }

        private void AtualizarListaTiposTarefa()
        {
            var lista = _controller.ObterTodos();
            lstLista.DataSource = lista;
            lstLista.DisplayMember = "Nome";
        }

       

        private void lstLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLista.SelectedItem is iTasks.Models.TipoTarefa tipoSelecionado)
            {
                txtId.Text = tipoSelecionado.Id.ToString();
                txtDesc.Text = tipoSelecionado.Nome.ToString();
            }
            else
            {
                txtId.Text = string.Empty;
            }

        }

        private void btRemover_Click(object sender, EventArgs e)
        {
            if (lstLista.SelectedItem is iTasks.Models.TipoTarefa tipoSelecionado)
            {
                var confirm = MessageBox.Show($"Tem a certeza que pretende remover '{tipoSelecionado.Nome}'?",
                                     "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    _controller.Remover(tipoSelecionado.Id);
                    AtualizarListaTiposTarefa();
                    txtId.Clear();
                    txtDesc.Clear();
                }

            }
        }

        private void btEditar_Click(object sender, EventArgs e)
        {
            if (lstLista.SelectedItem is iTasks.Models.TipoTarefa tipoSelecionado)
            {
                string novoNome = txtDesc.Text;

                if (string.IsNullOrWhiteSpace(novoNome))
                {
                    MessageBox.Show("Por favor preencha o nome da tarefa.", "Atenção!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_controller.NomeExisteParaOutroId(novoNome, tipoSelecionado.Id))
                {
                    MessageBox.Show("Já existe um tipo de tarefa com esse nome.", "Validação",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _controller.Editar(tipoSelecionado.Id, novoNome);
                AtualizarListaTiposTarefa();
                MessageBox.Show("Tipo de tarefa editado com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Clear();
                txtDesc.Clear();
            }
            else
            {
                MessageBox.Show("Selecione um tipo de tarefa para editar.", "Atenção!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        
        }
    }

}
