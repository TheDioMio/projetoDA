using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    internal enum EstadoAtual
    {
        ToDo,
        Doing,
        Done
    }
    internal class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public int TipoTarefaId { get; set; }
        public TipoTarefa TipoTarefa { get; set; }

        public int CriadorId { get; set; }
        public Utilizador Criador { get; set; }

        public int ProgramadorId { get; set; }
        public Utilizador Programador { get; set; }

        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }

        public EstadoAtual Estado { get; set; } = EstadoAtual.ToDo;
        public int Ordem { get; set; }
        public int StoryPoints { get; set; }
        public DateTime DataPrevistaInicio{ get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataPrevistaFim { get; set; }
        public DateTime DataRealInicio { get; set; }
        public DateTime DataRealFim { get; set; }

    }
}
