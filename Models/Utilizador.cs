using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public enum TipoUtilizador
    {
        Programador,
        Gestor
    }

    public enum NivelExperiencia
    {
        Junior,
        Senior
    }

    public enum Departamento
    {
        IT,
        Marketing,
        Administracao
    }
    public class Utilizador
    {
        [Required]
        public int Id { get; set; }


        [Required]
        [StringLength(450)]
        public string Username { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        public string Nome { get; set; }
        public TipoUtilizador Tipo { get; set; }
        public NivelExperiencia Experiencia { get; set; }
        public Departamento Departamento { get; set; }
        public int GestorId { get; set; }
        public Utilizador Gestor { get; set; }
        public ICollection<Tarefa> TarefasCriadas { get; set; }
        public ICollection<Tarefa> TarefasAtribuidas { get; set; }

        public Utilizador(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }



}
