using iTasks.Controllers;
using iTasks.Data;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace iTasks
{
    //usarname Unico
    //Apenas os Gestores podem efetuar o CRUD dos Utilizadores
    //Um programador devera ter um Gestor associado
    


    public partial class frmGereUtilizadores : Form
    {
        //ligação a Base de Dados
        public iTasksContexto BDContexto = new iTasksContexto();
        public UtilizadorController userController = new UtilizadorController();
        private List<Gestor> listaGestores;
        private List<Programador> listaProgramadores;
        public Utilizador _user;

        public frmGereUtilizadores(Utilizador userLogado)
        {
            InitializeComponent();
            _user = userLogado;
            cbDepartamento.DataSource = Enum.GetValues(typeof(Departamento));
            cbNivelProg.DataSource = Enum.GetValues(typeof(NivelExperiencia));
            updateListGestores();
            updateListProgramadores();

           
        }


        public void updateListGestores()
        {
            listaGestores = userController.GetGestores();
            lstListaGestores.Items.Clear();
            cbGestorProg.Items.Clear();

            if (listaGestores!= null)
            {
                foreach (var gestor in listaGestores)
                {
                    lstListaGestores.Items.Add(gestor.Nome);
                    cbGestorProg.Items.Add(gestor.Nome);
                }
                cbGestorProg.SelectedIndex = 0;
            }
            
            
        }

        public void updateListProgramadores()
        {
            //listaProgramadores = userController.GetProgramadores();
            //lstListaProgramadores.DataSource = null;
            //lstListaProgramadores.DataSource = listaProgramadores;


            listaProgramadores = userController.GetProgramadores();
            lstListaProgramadores.Items.Clear();
            foreach (var programador in listaProgramadores)
            {
                lstListaProgramadores.Items.Add(programador.Nome);
            }

        }

        public bool validarDadosGestor()
        {
            
            string name = txtNomeGestor.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("O nome do Gestor é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomeGestor.Focus();
                return false;
            }

            string userName = txtUsernameGestor.Text;
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("O campo username é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsernameGestor.Focus();
                return false;
            }

            string pass = txtPasswordGestor.Text;
            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("O campo password é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPasswordGestor.Focus();
                return false;
            }

            Departamento departamento = (Departamento)cbDepartamento.SelectedIndex;
            if (departamento < 0)
            {
                MessageBox.Show("O campo departamento não é válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public bool validarDadosProgramador()
        {

            string name = txtNomeProg.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("O nome do Programador é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomeProg.Focus();
                return false;
            }

            string userName = txtUsernameProg.Text;
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("O campo username do Programador é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsernameProg.Focus();
                return false;
            }

            string pass = txtPasswordProg.Text;
            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("O campo password do Programador é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPasswordProg.Focus();
                return false;
            }

            NivelExperiencia nivelExperiencia = (NivelExperiencia)cbNivelProg.SelectedIndex;
            if (nivelExperiencia < 0)
            {
                MessageBox.Show("O campo nivel experiência não é válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int gestorProg = cbGestorProg.SelectedIndex;
            if (gestorProg < 0)
            {
                MessageBox.Show("O campo gestor não é válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btGravarGestor_Click(object sender, EventArgs e)
        {

            if (validarDadosGestor())
            {
                string name = txtNomeGestor.Text;
                string userName = txtUsernameGestor.Text;
                string pass = txtPasswordGestor.Text;
                Departamento departamento = (Departamento)cbDepartamento.SelectedIndex;
                //verificar se o username não existe 
                Utilizador userExists = userController.ObterPorUsername(userName);
                if (userExists != null)
                {
                    MessageBox.Show("Já existe um Utilizador com o mesmo username.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsernameGestor.Focus();
                    return;
                }

                //Depois de validar vamos adicionar a base de dados

                Gestor addUser = new Gestor();
                addUser.Nome = name;
                addUser.Username = userName;
                addUser.Password = pass;
                addUser.departamento = departamento;
                addUser.gereUtilizadores = chkGereUtilizadores.Checked;

                bool success = userController.Criar(addUser);

                if (success)
                {
                    updateListGestores();
                    limparCampos();
                    MessageBox.Show("Gestor criado com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Alguma coisa não correu bem, não foi possivel criar o Gestor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //updateListGestores();

        }

        private void btAtualizarGestor_Click(object sender, EventArgs e)
        {
            if (validarDadosGestor())
            {
                int id = int.Parse(txtIdGestor.Text);
                string name = txtNomeGestor.Text;
                string userName = txtUsernameGestor.Text;
                string pass = txtPasswordGestor.Text;
                Departamento departamento = (Departamento)cbDepartamento.SelectedIndex;

                 
                Gestor user = listaGestores[lstListaGestores.SelectedIndex];
                
                if (user == null)
                {
                    MessageBox.Show("Não foi possivel encontrar o utilizador desejado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Depois de validar vamos adicionar a base de dados

                user.Nome = name;
                user.Username = userName;
                user.Password = pass;
                user.departamento = departamento;
                user.gereUtilizadores = chkGereUtilizadores.Checked;


                bool success = userController.Atualizar(user);

                if (success)
                {
                    updateListGestores();
                    //limparCampos();
                    MessageBox.Show("Gestor Atualizado com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Alguma coisa não correu bem, não foi possivel Atualizar o Gestor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void limparCamposProgramador()
        {
            txtIdProg.Text = "";
            txtNomeProg.Text = "";
            txtPasswordProg.Text = "";
            txtPasswordProg.Text = "";
            cbNivelProg.SelectedIndex = 0;
            cbGestorProg.SelectedIndex = 0;
            lstListaProgramadores.ClearSelected();

        }

        private void limparCampos() 
        {
            txtIdGestor.Text = "";
            txtNomeGestor.Text = "";
            txtPasswordGestor.Text = "";
            txtPasswordGestor.Text = "";
            txtUsernameGestor.Text = "";
            chkGereUtilizadores.Checked = false;
            cbDepartamento.SelectedIndex = 0;
            lstListaGestores.ClearSelected();

        }
        private void btApagarGestor_Click(object sender, EventArgs e)
        {
            if (lstListaGestores.SelectedIndex >= 0)
            {
                Gestor user = listaGestores[lstListaGestores.SelectedIndex];
                if (user != null)
                {
                    if(user.Id == _user.Id)
                    {
                        MessageBox.Show("Não é possível apagar a sua própria conta.");
                    } else
                    {
                        List<Programador> programadoresDeGestor = userController.ObterProgramadoresDeGestor(listaGestores[lstListaGestores.SelectedIndex]);
                        if (programadoresDeGestor.Count > 0)
                        {
                            MessageBox.Show("Não é possivel apagar o Gestor, pois tem Programadores associados!");
                            return;
                        }

                        List<Tarefa> tarefasDeGestor = userController.ObterTarefasDeGestor(listaGestores[lstListaGestores.SelectedIndex]);
                        if (tarefasDeGestor.Count > 0)
                        {
                            MessageBox.Show("Não é possivel apagar o Gestor, pois tem Tarefas associadas!");
                            return;
                        }

                        DialogResult resultado = MessageBox.Show("Deseja apagar o utilizador selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {

                            bool success = userController.Eliminar(user.Id);

                            if (success)
                            {
                                updateListGestores();
                                limparCampos();
                                MessageBox.Show("Gestor Eliminado com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MessageBox.Show("Alguma coisa não correu bem, não foi possivel eliminar o Gestor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }

        private void lstListaGestores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lstListaGestores.SelectedIndex<0)||(listaGestores == null))
            {
                return;
            }

            int index = lstListaGestores.SelectedIndex;



            Gestor user = listaGestores[index];
            if (user != null)
            {
                txtIdGestor.Text = user.Id.ToString();
                txtNomeGestor.Text = user.Nome;
                txtPasswordGestor.Text = user.Password;
                txtUsernameGestor.Text = user.Username;
                cbDepartamento.SelectedIndex = (int)user.departamento;
                chkGereUtilizadores.Checked = user.gereUtilizadores;
            }
            
        }

        private void btGravarProg_Click(object sender, EventArgs e)
        {
            string name = txtNomeProg.Text;
            string userName = txtUsernameProg.Text;
            string pass = txtPasswordProg.Text;
            NivelExperiencia nivelExperiencia = (NivelExperiencia)cbNivelProg.SelectedIndex;
            Gestor gestor = listaGestores[cbGestorProg.SelectedIndex];

            if (validarDadosProgramador())
            {

                //verificar se o username não existe 
                Utilizador userExists = userController.ObterPorUsername(userName);
                if (userExists != null)
                {
                    MessageBox.Show("Já existe um Utilizador com o mesmo username.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsernameProg.Focus();
                    return;
                }

                //Depois de validar vamos adicionar a base de dados

                Programador addUser = new Programador();
                addUser.Nome = name;
                addUser.Username = userName;
                addUser.Password = pass;
                addUser.nivelExperiencia = nivelExperiencia;
                addUser.Gestor = gestor;

                bool success = userController.Criar(addUser);

                if (success)
                {
                    updateListProgramadores();
                    MessageBox.Show("Programador criado com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Alguma coisa não correu bem, não foi possivel criar o Programador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void lstListaProgramadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lstListaProgramadores.SelectedIndex < 0) || (listaProgramadores == null))
            {
                return;
            }

            int index = lstListaProgramadores.SelectedIndex;

           

            Programador user = listaProgramadores[index];
            if (user != null)
            {
                var xpto = user.Gestor;
                txtIdProg.Text = user.Id.ToString();
                txtNomeProg.Text = user.Nome;
                txtPasswordProg.Text = user.Password;
                txtUsernameProg.Text = user.Username;
                cbNivelProg.SelectedIndex = (int)user.nivelExperiencia;
                cbGestorProg.SelectedIndex = listaGestores.FindIndex(p => p.Id == user.Gestor.Id);

            }
        }

        private void btApagarProgramador_Click(object sender, EventArgs e)
        {
            if (lstListaProgramadores.SelectedIndex >= 0)
            {
                Programador user = listaProgramadores[lstListaProgramadores.SelectedIndex];
                if (user != null)
                {

                    List<Tarefa> tarefasDeprogramador = userController.ObterTarefasDeProgramador(listaProgramadores[lstListaProgramadores.SelectedIndex]);
                    if (tarefasDeprogramador.Count > 0)
                    {
                        MessageBox.Show("Não é possivel apagar o Programador, pois tem Tarefas associadas!");
                        return;
                    }

                    DialogResult resultado = MessageBox.Show("Deseja apagar o utilizador selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        bool success = userController.Eliminar(user.Id);

                        if (success)
                        {
                            updateListProgramadores();
                            limparCamposProgramador();
                            MessageBox.Show("Programador Eliminado com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Alguma coisa não correu bem, não foi possivel eliminar o Programador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void btAtualizarProgramador_Click(object sender, EventArgs e)
        {
            if (validarDadosProgramador())
            {
                int id = int.Parse(txtIdProg.Text);
                string name = txtNomeProg.Text;
                string userName = txtUsernameProg.Text;
                string pass = txtPasswordProg.Text;

                NivelExperiencia nivelExperiencia = (NivelExperiencia)cbNivelProg.SelectedIndex;

                Gestor userGestor = listaGestores[cbGestorProg.SelectedIndex];

                //Depois de validar vamos adicionar a base de dados
                Programador user = listaProgramadores[lstListaProgramadores.SelectedIndex];
                user.Nome = name;
                user.Username = userName;
                user.Password = pass;
                user.nivelExperiencia = nivelExperiencia;
                user.Gestor = userGestor;
                bool success=userController.Atualizar(user);
                if (success)
                {
                    updateListProgramadores();
                    MessageBox.Show("Programador Atualizado com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Alguma coisa não correu bem, não foi possivel atualizar o Programador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
