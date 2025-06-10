namespace iTasks.Views
{
    partial class frmPrevisaoConclusao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrevisaoConclusao));
            this.txtTarefa = new System.Windows.Forms.TextBox();
            this.labelTarefa = new System.Windows.Forms.Label();
            this.labelPrevisao = new System.Windows.Forms.Label();
            this.txtPrevisao = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTarefa
            // 
            this.txtTarefa.Location = new System.Drawing.Point(146, 10);
            this.txtTarefa.Name = "txtTarefa";
            this.txtTarefa.ReadOnly = true;
            this.txtTarefa.Size = new System.Drawing.Size(402, 22);
            this.txtTarefa.TabIndex = 0;
            // 
            // labelTarefa
            // 
            this.labelTarefa.AutoSize = true;
            this.labelTarefa.Location = new System.Drawing.Point(13, 13);
            this.labelTarefa.Name = "labelTarefa";
            this.labelTarefa.Size = new System.Drawing.Size(50, 16);
            this.labelTarefa.TabIndex = 1;
            this.labelTarefa.Text = "Tarefa:";
            // 
            // labelPrevisao
            // 
            this.labelPrevisao.AutoSize = true;
            this.labelPrevisao.Location = new System.Drawing.Point(12, 51);
            this.labelPrevisao.Name = "labelPrevisao";
            this.labelPrevisao.Size = new System.Drawing.Size(126, 16);
            this.labelPrevisao.TabIndex = 1;
            this.labelPrevisao.Text = "Conclusão Prevista:";
            // 
            // txtPrevisao
            // 
            this.txtPrevisao.Location = new System.Drawing.Point(146, 48);
            this.txtPrevisao.Name = "txtPrevisao";
            this.txtPrevisao.ReadOnly = true;
            this.txtPrevisao.Size = new System.Drawing.Size(176, 22);
            this.txtPrevisao.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "Fechar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnFechar);
            // 
            // frmPrevisaoConclusao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 120);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelPrevisao);
            this.Controls.Add(this.txtPrevisao);
            this.Controls.Add(this.labelTarefa);
            this.Controls.Add(this.txtTarefa);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrevisaoConclusao";
            this.Text = "Previsão de Conclusão";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTarefa;
        private System.Windows.Forms.Label labelTarefa;
        private System.Windows.Forms.Label labelPrevisao;
        private System.Windows.Forms.TextBox txtPrevisao;
        private System.Windows.Forms.Button button1;
    }
}