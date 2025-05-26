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
            //TipoTarefa tarefa2 = new TipoTarefa("TESTE2");
            //Contexto.TiposTarefa.Add(tarefa2);
            //TipoTarefa tarefa3 = new TipoTarefa("TESTE3");
            //Contexto.TiposTarefa.Add(tarefa3);
            //TipoTarefa tarefa4 = new TipoTarefa("TESTE4");
            //Contexto.TiposTarefa.Add(tarefa4);
            //TipoTarefa tarefa5 = new TipoTarefa("TESTE5");
            //Contexto.TiposTarefa.Add(tarefa5);
            //Contexto.SaveChanges();


            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            //// 1) Campos obrigatórios
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
            /*Sessao.UtilizadorLogado = user;*/
            
            //var frmUtilizadores = new frmGereUtilizadores();
            //this.Hide();
            //frmUtilizadores.Show();
            var kanban = new frmKanban(user);
            this.Hide();
            kanban.Show();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //se o user clicou na tecla ENTER:
            {
                btLogin_Click(sender, e);
            }
        }
    }
}