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

namespace iTasks
{
    public partial class frmLogin : Form
    {
        //Dar link do frmlogin à base de dados
        private UtilizadorController contexto = new UtilizadorController(); 
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
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

            // 2) Busca o utilizador pelo username
            var user = contexto.ObterPorUsername(username);

            // 3) Se não encontrar ou a password não bater
            if (user == null || user.Password != password)
            {
                MessageBox.Show(
                    "Credenciais inválidas",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // 4) Login bem‑sucedido
            Sessao.UtilizadorLogado = user;
            var kanban = new frmKanban();
            this.Hide();
            kanban.Show();
        }
    }
}
