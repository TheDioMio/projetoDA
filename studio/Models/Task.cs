using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    internal class Task
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        //Definição do tipo de tarefas
        public int TaskTypeID { get; set; }
        public Tasktype taskType { get; set; }


        //Definição de quem criou (GESTOR)
        public int GestorID { get; set; }
        public User Gestor { get; set; }

        //Atribuição ao PROGRAMADOR
        public int ProgrammerID { get; set; }
        public User Programmer { get; set; }

        //Localização do Projeto e Informações
        public int ProjectID { get; set; }
        public Project project { get; set; }

        public EstadoAtual Estado { get; set; } = EstadoAtual.ToDo;
        public int Order { get; set; }
        public int StoryPoints { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataPrevistaInicio { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public DateTime DataRealInicio { get; set; }
        public DateTime DataRealFim { get; set; }

        //Definição das enumerações possíveis
        public enum EstadoAtual
        {
            ToDo,
            Doing,
            Done
        }




    }
}
