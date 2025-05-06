using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTasks.Models
{
    public class Tarefa
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

        // mapeada para a coluna nvarchar(4000) existente no BD
        [Column("Estado")]
        [Required]
        [MaxLength(4000)]
        public string EstadoString { get; set; }

        // propriedade enum para usar no teu código C#
        [NotMapped]
        public EstadoAtual Estado
        {
            get
            {
                if (Enum.TryParse<EstadoAtual>(EstadoString, out var e))
                    return e;
                throw new InvalidOperationException($"Valor de Estado inválido no BD: {EstadoString}");
            }
            set
            {
                EstadoString = value.ToString();
            }
        }

        public int Ordem { get; set; }
        public int StoryPoints { get; set; }
        public DateTime DataPrevistaInicio { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataPrevistaFim { get; set; }
        public DateTime DataRealInicio { get; set; }
        public DateTime DataRealFim { get; set; }
    }

    public enum EstadoAtual
    {
        ToDo,
        Doing,
        Done
    }
}
