namespace iTasks
{
    partial class frmKanban
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstTodo = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstDoing = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstDone = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ficheiroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarParaCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilizadoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gerirUtilizadoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gerirTiposDeTarefasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listagensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tarefasTerminadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tarefasEmCursoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btSetDoing = new System.Windows.Forms.Button();
            this.btSetDone = new System.Windows.Forms.Button();
            this.btSetTodo = new System.Windows.Forms.Button();
            this.btNova = new System.Windows.Forms.Button();
            this.labelBemVindo = new System.Windows.Forms.Label();
            this.btPrevisao = new System.Windows.Forms.Button();
            this.btLogout = new System.Windows.Forms.Button();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstTodo
            // 
            this.lstTodo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTodo.FormattingEnabled = true;
            this.lstTodo.Location = new System.Drawing.Point(3, 16);
            this.lstTodo.Name = "lstTodo";
            this.lstTodo.Size = new System.Drawing.Size(296, 419);
            this.lstTodo.TabIndex = 0;
            this.lstTodo.SelectedIndexChanged += new System.EventHandler(this.lstTodo_SelectedIndexChanged_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstTodo);
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 438);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ToDo";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstDoing);
            this.groupBox2.Location = new System.Drawing.Point(320, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 441);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Doing";
            // 
            // lstDoing
            // 
            this.lstDoing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDoing.FormattingEnabled = true;
            this.lstDoing.Location = new System.Drawing.Point(3, 16);
            this.lstDoing.Name = "lstDoing";
            this.lstDoing.Size = new System.Drawing.Size(296, 422);
            this.lstDoing.TabIndex = 0;
            this.lstDoing.SelectedIndexChanged += new System.EventHandler(this.lstDoing_SelectedIndexChanged_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstDone);
            this.groupBox3.Location = new System.Drawing.Point(628, 55);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(302, 441);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Done";
            // 
            // lstDone
            // 
            this.lstDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDone.FormattingEnabled = true;
            this.lstDone.Location = new System.Drawing.Point(3, 16);
            this.lstDone.Name = "lstDone";
            this.lstDone.Size = new System.Drawing.Size(296, 422);
            this.lstDone.TabIndex = 0;
            this.lstDone.SelectedIndexChanged += new System.EventHandler(this.lstDone_SelectedIndexChanged_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ficheiroToolStripMenuItem,
            this.utilizadoresToolStripMenuItem,
            this.listagensToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(943, 27);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ficheiroToolStripMenuItem
            // 
            this.ficheiroToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportarParaCSVToolStripMenuItem,
            this.toolStripMenuItem1,
            this.logoutToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.ficheiroToolStripMenuItem.Name = "ficheiroToolStripMenuItem";
            this.ficheiroToolStripMenuItem.Size = new System.Drawing.Size(68, 23);
            this.ficheiroToolStripMenuItem.Text = "Ficheiro";
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(304, 24);
            this.sairToolStripMenuItem.Text = "Sair";
            // 
            // exportarParaCSVToolStripMenuItem
            // 
            this.exportarParaCSVToolStripMenuItem.Name = "exportarParaCSVToolStripMenuItem";
            this.exportarParaCSVToolStripMenuItem.Size = new System.Drawing.Size(304, 24);
            this.exportarParaCSVToolStripMenuItem.Text = "Exportar Tarefas Concluídas para CSV";
            this.exportarParaCSVToolStripMenuItem.Click += new System.EventHandler(this.exportarParaCSVToolStripMenuItem_Click);
            // 
            // utilizadoresToolStripMenuItem
            // 
            this.utilizadoresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gerirUtilizadoresToolStripMenuItem,
            this.gerirTiposDeTarefasToolStripMenuItem});
            this.utilizadoresToolStripMenuItem.Name = "utilizadoresToolStripMenuItem";
            this.utilizadoresToolStripMenuItem.Size = new System.Drawing.Size(144, 23);
            this.utilizadoresToolStripMenuItem.Text = "Gestão da Aplicação";
            // 
            // gerirUtilizadoresToolStripMenuItem
            // 
            this.gerirUtilizadoresToolStripMenuItem.Name = "gerirUtilizadoresToolStripMenuItem";
            this.gerirUtilizadoresToolStripMenuItem.Size = new System.Drawing.Size(208, 24);
            this.gerirUtilizadoresToolStripMenuItem.Text = "Gerir Utilizadores";
            this.gerirUtilizadoresToolStripMenuItem.Click += new System.EventHandler(this.gerirUtilizadoresToolStripMenuItem_Click);
            // 
            // gerirTiposDeTarefasToolStripMenuItem
            // 
            this.gerirTiposDeTarefasToolStripMenuItem.Name = "gerirTiposDeTarefasToolStripMenuItem";
            this.gerirTiposDeTarefasToolStripMenuItem.Size = new System.Drawing.Size(208, 24);
            this.gerirTiposDeTarefasToolStripMenuItem.Text = "Gerir Tipos de Tarefas";
            this.gerirTiposDeTarefasToolStripMenuItem.Click += new System.EventHandler(this.gerirTiposDeTarefasToolStripMenuItem_Click);
            // 
            // listagensToolStripMenuItem
            // 
            this.listagensToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tarefasTerminadasToolStripMenuItem,
            this.tarefasEmCursoToolStripMenuItem});
            this.listagensToolStripMenuItem.Name = "listagensToolStripMenuItem";
            this.listagensToolStripMenuItem.Size = new System.Drawing.Size(78, 23);
            this.listagensToolStripMenuItem.Text = "Listagens";
            // 
            // tarefasTerminadasToolStripMenuItem
            // 
            this.tarefasTerminadasToolStripMenuItem.Name = "tarefasTerminadasToolStripMenuItem";
            this.tarefasTerminadasToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.tarefasTerminadasToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.tarefasTerminadasToolStripMenuItem.Text = "Tarefas Concluídas";
            this.tarefasTerminadasToolStripMenuItem.Click += new System.EventHandler(this.tarefasTerminadasToolStripMenuItem_Click);
            // 
            // tarefasEmCursoToolStripMenuItem
            // 
            this.tarefasEmCursoToolStripMenuItem.Name = "tarefasEmCursoToolStripMenuItem";
            this.tarefasEmCursoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.tarefasEmCursoToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.tarefasEmCursoToolStripMenuItem.Text = "Tarefas em Curso";
            this.tarefasEmCursoToolStripMenuItem.Click += new System.EventHandler(this.tarefasEmCursoToolStripMenuItem_Click);
            // 
            // btSetDoing
            // 
            this.btSetDoing.Location = new System.Drawing.Point(165, 502);
            this.btSetDoing.Name = "btSetDoing";
            this.btSetDoing.Size = new System.Drawing.Size(146, 23);
            this.btSetDoing.TabIndex = 5;
            this.btSetDoing.Text = "Executar Tarefa >>";
            this.btSetDoing.UseVisualStyleBackColor = true;
            this.btSetDoing.Click += new System.EventHandler(this.btSetDoing_Click_1);
            // 
            // btSetDone
            // 
            this.btSetDone.Location = new System.Drawing.Point(475, 502);
            this.btSetDone.Name = "btSetDone";
            this.btSetDone.Size = new System.Drawing.Size(144, 23);
            this.btSetDone.TabIndex = 6;
            this.btSetDone.Text = "Terminar Tarefa >>";
            this.btSetDone.UseVisualStyleBackColor = true;
            this.btSetDone.Click += new System.EventHandler(this.btSetDone_Click);
            // 
            // btSetTodo
            // 
            this.btSetTodo.Location = new System.Drawing.Point(323, 502);
            this.btSetTodo.Name = "btSetTodo";
            this.btSetTodo.Size = new System.Drawing.Size(144, 23);
            this.btSetTodo.TabIndex = 7;
            this.btSetTodo.Text = "<< Reiniciar Tarefa";
            this.btSetTodo.UseVisualStyleBackColor = true;
            this.btSetTodo.Click += new System.EventHandler(this.btSetTodo_Click_1);
            // 
            // btNova
            // 
            this.btNova.Location = new System.Drawing.Point(15, 502);
            this.btNova.Name = "btNova";
            this.btNova.Size = new System.Drawing.Size(104, 23);
            this.btNova.TabIndex = 8;
            this.btNova.Text = "Nova Tarefa";
            this.btNova.UseVisualStyleBackColor = true;
            this.btNova.Click += new System.EventHandler(this.btNova_Click);
            // 
            // labelBemVindo
            // 
            this.labelBemVindo.AutoSize = true;
            this.labelBemVindo.Location = new System.Drawing.Point(778, 34);
            this.labelBemVindo.Name = "labelBemVindo";
            this.labelBemVindo.Size = new System.Drawing.Size(149, 13);
            this.labelBemVindo.TabIndex = 9;
            this.labelBemVindo.Text = "Bem vindo: <Nome Utilizador>";
            // 
            // btPrevisao
            // 
            this.btPrevisao.Location = new System.Drawing.Point(12, 29);
            this.btPrevisao.Name = "btPrevisao";
            this.btPrevisao.Size = new System.Drawing.Size(167, 23);
            this.btPrevisao.TabIndex = 10;
            this.btPrevisao.Text = "Ver Previsão de Conclusão";
            this.btPrevisao.UseVisualStyleBackColor = true;
            this.btPrevisao.Click += new System.EventHandler(this.btPrevisao_Click);
            // 
            // btLogout
            // 
            this.btLogout.Location = new System.Drawing.Point(1161, 618);
            this.btLogout.Name = "btLogout";
            this.btLogout.Size = new System.Drawing.Size(75, 28);
            this.btLogout.TabIndex = 11;
            this.btLogout.Text = "Logout";
            this.btLogout.UseVisualStyleBackColor = true;
            this.btLogout.Click += new System.EventHandler(this.btLogout_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(301, 6);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(304, 24);
            this.logoutToolStripMenuItem.Text = "Logout";
            
            // 
            // frmKanban
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 661);
            this.Controls.Add(this.btLogout);
            this.ClientSize = new System.Drawing.Size(943, 537);
            this.Controls.Add(this.btPrevisao);
            this.Controls.Add(this.labelBemVindo);
            this.Controls.Add(this.btNova);
            this.Controls.Add(this.btSetTodo);
            this.Controls.Add(this.btSetDone);
            this.Controls.Add(this.btSetDoing);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmKanban";
            this.Text = "Kanban";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKanban_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstTodo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstDoing;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstDone;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ficheiroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilizadoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gerirUtilizadoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gerirTiposDeTarefasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarParaCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listagensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tarefasTerminadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tarefasEmCursoToolStripMenuItem;
        private System.Windows.Forms.Button btSetDoing;
        private System.Windows.Forms.Button btSetDone;
        private System.Windows.Forms.Button btSetTodo;
        private System.Windows.Forms.Button btNova;
        private System.Windows.Forms.Label labelBemVindo;
        private System.Windows.Forms.Button btPrevisao;
        private System.Windows.Forms.Button btLogout;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
    }
}