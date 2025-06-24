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
using iTasks.Data;

namespace iTasks
{
    public partial class frmLogin : Form
    {
        public iTasksContexto Contexto = new iTasksContexto();
        //Dar link do frmlogin à base de dados
        public UtilizadorController contexto = new UtilizadorController();
        public frmLogin()
        {

            InitializeComponent();

        }

        private void btLogin_Click(object sender, EventArgs e)
        {

            //TipoTarefa tarefa1 = new TipoTarefa("TESTE1");
            //Contexto.TiposTarefa.Add(tarefa1);

            //Gestor user1 = new Gestor
            //{
            //    Nome = "Admin",
            //    Password = "Admin",
            //    Username = "Admin",
            //};
            //user1.gereUtilizadores = true;
            //Contexto.Utilizadores.Add(user1);
            //Contexto.SaveChanges();
            //txtPassword.Text = "Admin";
            //txtUsername.Text = "Admin";

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;


            // 1) Campos obrigatórios
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Preencha todos os campos.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // 2) Cria a instâmncia do utilizador logado, para a passar para as próximas páginas
            var user = contexto.ObterPorUsername(username);

            if (user== null)
            {
                MessageBox.Show(
                    "Utilizador não encontrado!",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            // 3) Se não encontrar ou a password não bater
            if (password != user.Password)
            {
                MessageBox.Show(
                    "Credenciais inválidas",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            

            var kanban = new frmKanban(user);
            this.Hide();
            kanban.ShowDialog();
            txtPassword.Clear(); // Limpa o campo da password após login
            this.Show();
        }

        private void txtPassword_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //se o user clicou na tecla ENTER:
            {
                btLogin_Click(sender, e);
            }
        }
    }
}