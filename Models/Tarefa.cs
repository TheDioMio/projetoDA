using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace iTasks.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TipoTarefa TipoTarefa { get; set; }
        public Gestor Gestor { get; set; }
        public Programador Programador { get; set; }
        public Projeto Projeto { get; set; }
        public EstadoAtual EstadoAtual { get; set; }
        public int OrdemExecucao { get; set; }
        public int StoryPoints { get; set; }
        public DateTime DataPrevistaInicio { get; set; } = DateTime.Now;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataPrevistaFim { get; set; } = DateTime.Now;
        public DateTime DataRealInicio { get; set; } = DateTime.Now;
        public DateTime DataRealFim { get; set; } = DateTime.Now;

        public Tarefa(string descricao, EstadoAtual estadoAtual)
        {
            Descricao = descricao;
            EstadoAtual = estadoAtual;
        }

        public override string ToString()
        {
            return Descricao + EstadoAtual;
        }

        public Tarefa()
        {
        }

    }
}
