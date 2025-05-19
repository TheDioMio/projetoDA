using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime DataPrevistaInicio { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataPrevistaFim { get; set; }
        public DateTime DataRealInicio { get; set; }
        public DateTime DataRealFim { get; set; }
    }

}
