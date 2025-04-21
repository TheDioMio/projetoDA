using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    internal enum TipoUtilizador
    {
        Programador,
        Gestor
    }

    internal enum NivelExperiencia
    {
        Junior,
        Senior
    }

    internal enum Departamento
    {
        IT,
        Marketing,
        Administracao
    }
    internal class Utilizador
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public TipoUtilizador Tipo { get; set; }
        public NivelExperiencia Experiencia { get; set; }
        public Departamento Departamento { get; set; }
        public int GestorId { get; set; }
        public Utilizador Gestor { get; set; }
        public ICollection<Tarefa> TarefasCriadas { get; set; }
        public ICollection<Tarefa> TarefasAtribuidas { get; set; }
    }

}
